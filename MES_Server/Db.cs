using MySql.Data.MySqlClient;
using System.Data;
using System.Threading.Tasks;
using MES.Common;

public static class Db
{
    // 환경에 맞게 조정하세요.
    // ⚠️ MySQL root 비밀번호를 확인하고 수정하세요!
    private static readonly string ConnStr =
        "Server=localhost;Port=3306;Database=mes_solution;Uid=root;Pwd=1234;SslMode=None;";

    public static async Task<bool> HealthAsync()
    {
        try
        {
            await using var con = new MySqlConnection(ConnStr);
            await con.OpenAsync();
            await using var cmd = new MySqlCommand("SELECT 1", con);
            var obj = await cmd.ExecuteScalarAsync();
            return obj is not null;
        }
        catch { return false; }
    }

    /// <summary>
    /// 서버 시작 시 모든 설비를 "미가동" 상태로 초기화
    /// </summary>
    public static async Task<bool> ResetAllEquipmentStatusAsync()
    {
        try
        {
            await using var con = new MySqlConnection(ConnStr);
            await con.OpenAsync();
            const string sql = @"UPDATE equipment_production 
                                 SET current_status = '미가동' 
                                 WHERE date = CURDATE()";
            await using var cmd = new MySqlCommand(sql, con);
            var rowsAffected = await cmd.ExecuteNonQueryAsync();
            System.Diagnostics.Debug.WriteLine($"[DB] ResetAllEquipmentStatus: {rowsAffected}개 설비를 '미가동'으로 초기화");
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[DB] ResetAllEquipmentStatus 오류: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// "admin" 입력 시 role=1인 관리자의 실제 employee_id를 반환
    /// </summary>
    public static async Task<string?> GetAdminEmployeeIdAsync()
    {
        try
        {
            await using var con = new MySqlConnection(ConnStr);
            await con.OpenAsync();
            const string sql = @"SELECT employee_id FROM profile WHERE role=1 LIMIT 1";
            await using var cmd = new MySqlCommand(sql, con);
            var obj = await cmd.ExecuteScalarAsync();
            var adminId = obj?.ToString();
            System.Diagnostics.Debug.WriteLine($"[DB] GetAdminEmployeeIdAsync: {adminId}");
            return adminId;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[DB] GetAdminEmployeeIdAsync 오류: {ex.Message}");
            return null;
        }
    }

    public static async Task<bool> ExistsEmployeeAsync(string employeeId)
    {
        try
        {
            await using var con = new MySqlConnection(ConnStr);
            await con.OpenAsync();
            const string sql = @"SELECT 1 FROM profile WHERE employee_id=@id LIMIT 1";
            await using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", employeeId);
            var obj = await cmd.ExecuteScalarAsync();
            var exists = obj is not null;
            System.Diagnostics.Debug.WriteLine($"[DB] ExistsEmployeeAsync: {employeeId} → {exists}");
            return exists;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[DB] ExistsEmployeeAsync 오류: {employeeId} → {ex.Message}");
            return false;
        }
    }

    public static async Task<ProfileRow?> GetProfileAsync(string employeeId)
    {
        try
        {
            await using var con = new MySqlConnection(ConnStr);
            await con.OpenAsync();
            const string sql = @"SELECT employee_id, name, department, position, equipment_id, role, status
                                 FROM profile WHERE employee_id=@id LIMIT 1";
            await using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", employeeId);
            await using var rd = await cmd.ExecuteReaderAsync();
            if (!await rd.ReadAsync()) return null;
            
            var row = new ProfileRow
            {
                employee_id = rd.GetString("employee_id"),
                name = rd.GetString("name"),
                department = rd.GetString("department"),
                position = rd.GetString("position"),
                equipment_id = rd.IsDBNull(rd.GetOrdinal("equipment_id")) ? null : rd.GetString("equipment_id"),
                role = rd.IsDBNull(rd.GetOrdinal("role")) ? 0 : rd.GetInt32("role"),
                status = rd.IsDBNull(rd.GetOrdinal("status")) ? "재직중" : rd.GetString("status")
            };
            
            // 디버깅 로그
            System.Diagnostics.Debug.WriteLine($"[DB] GetProfileAsync: {employeeId}, role={row.role}");
            
            return row;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[DB] GetProfileAsync 오류: {ex.Message}");
            return null;
        }
    }

    public static async Task<bool> ExistsEquipmentAsync(string equipmentId)
    {
        try
        {
            await using var con = new MySqlConnection(ConnStr);
            await con.OpenAsync();
            const string sql = @"SELECT 1 FROM equipment WHERE equipment_id=@id LIMIT 1";
            await using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", equipmentId);
            var obj = await cmd.ExecuteScalarAsync();
            return obj is not null;
        }
        catch { return false; }
    }

    public static async Task InsertStateReportAsync(string equipmentId, string state, string? reason)
    {
        try
        {
            await using var con = new MySqlConnection(ConnStr);
            await con.OpenAsync();
            const string sql = @"INSERT INTO equipment_alert(equipment_id, equipment_name, model, status, reason)
                                 SELECT e.equipment_id, e.equipment_name, e.model, @state, @reason
                                 FROM equipment e WHERE e.equipment_id=@id";
            await using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", equipmentId);
            cmd.Parameters.AddWithValue("@state", state);
            cmd.Parameters.AddWithValue("@reason", (object?)reason ?? DBNull.Value);
            await cmd.ExecuteNonQueryAsync();
        }
        catch { }
    }

    public static async Task<(bool ok, string? message)> InsertProfileAsync(string employeeId, string name, string department, string position, string? equipmentId)
    {
        try
        {
            await using var con = new MySqlConnection(ConnStr);
            await con.OpenAsync();

            // 중복 체크
            const string chk = @"SELECT 1 FROM profile WHERE employee_id=@id LIMIT 1";
            await using (var cmd = new MySqlCommand(chk, con))
            {
                cmd.Parameters.AddWithValue("@id", employeeId);
                var obj = await cmd.ExecuteScalarAsync();
                if (obj is not null) return (false, "이미 존재하는 사번입니다.");
            }

            // ⭐ 빈 문자열을 NULL로 처리
            string? finalEquipmentId = string.IsNullOrWhiteSpace(equipmentId) ? null : equipmentId;

            // ⭐ status 컬럼을 명시적으로 포함 (DEFAULT 값 사용)
            const string ins = @"INSERT INTO profile(employee_id, name, department, position, equipment_id, status)
                                 VALUES(@id, @name, @dept, @pos, @eq, '재직중')";
            await using (var cmd = new MySqlCommand(ins, con))
            {
                cmd.Parameters.AddWithValue("@id", employeeId);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@dept", department);
                cmd.Parameters.AddWithValue("@pos", position);
                cmd.Parameters.AddWithValue("@eq", (object?)finalEquipmentId ?? DBNull.Value);
                
                System.Diagnostics.Debug.WriteLine($"[DB] InsertProfile: id={employeeId}, name={name}, eq={finalEquipmentId ?? "NULL"}");
                await cmd.ExecuteNonQueryAsync();
            }
            return (true, null);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public static async Task<(bool ok, string? message)> UpdateProfileAsync(string employeeId, string name, string department, string position, string? equipmentId)
    {
        try
        {
            await using var con = new MySqlConnection(ConnStr);
            await con.OpenAsync();

            // ⭐ 빈 문자열을 NULL로 처리
            string? finalEquipmentId = string.IsNullOrWhiteSpace(equipmentId) ? null : equipmentId;

            const string upd = @"UPDATE profile SET name=@name, department=@dept, position=@pos, equipment_id=@eq 
                                 WHERE employee_id=@id";
            await using var cmd = new MySqlCommand(upd, con);
            cmd.Parameters.AddWithValue("@id", employeeId);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@dept", department);
            cmd.Parameters.AddWithValue("@pos", position);
            cmd.Parameters.AddWithValue("@eq", (object?)finalEquipmentId ?? DBNull.Value);
            
            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0 ? (true, null) : (false, "해당 사번을 찾을 수 없습니다.");
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public static async Task<(bool ok, string? message)> DeleteProfileAsync(string employeeId)
    {
        try
        {
            await using var con = new MySqlConnection(ConnStr);
            await con.OpenAsync();

            const string del = @"DELETE FROM profile WHERE employee_id=@id";
            await using var cmd = new MySqlCommand(del, con);
            cmd.Parameters.AddWithValue("@id", employeeId);
            
            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0 ? (true, null) : (false, "해당 사번을 찾을 수 없습니다.");
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public static async Task<ProfileRow[]> ListProfilesAsync()
    {
        try
        {
            await using var con = new MySqlConnection(ConnStr);
            await con.OpenAsync();
            const string sql = @"SELECT employee_id, name, department, position, equipment_id, role, status FROM profile ORDER BY employee_id";
            await using var cmd = new MySqlCommand(sql, con);
            await using var rd = await cmd.ExecuteReaderAsync();
            
            var list = new List<ProfileRow>();
            while (await rd.ReadAsync())
            {
                list.Add(new ProfileRow
            {
                employee_id = rd.GetString("employee_id"),
                name = rd.GetString("name"),
                department = rd.GetString("department"),
                position = rd.GetString("position"),
                    equipment_id = rd.IsDBNull(rd.GetOrdinal("equipment_id")) ? null : rd.GetString("equipment_id"),
                    role = rd.IsDBNull(rd.GetOrdinal("role")) ? 0 : rd.GetInt32("role"),
                    status = rd.IsDBNull(rd.GetOrdinal("status")) ? "재직중" : rd.GetString("status")
                });
            }
            return list.ToArray();
        }
        catch
        {
            return Array.Empty<ProfileRow>();
        }
    }

    // ========================================
    // 생산 데이터 관련 메서드
    // ========================================

    public static async Task<bool> UpsertProductionDataAsync(
        string equipmentId,
        int productionCount,
        int faultyCount,
        double faultyRate,
        int operatingTime,
        int downtime,
        int totalTime,
        double operatingRate,
        string currentStatus)
    {
        try
        {
            await using var con = new MySqlConnection(ConnStr);
            await con.OpenAsync();
            
            // 오늘 날짜 기준으로 UPSERT
            const string sql = @"
                INSERT INTO equipment_production (
                    equipment_id, date, production_count, faulty_count, faulty_rate,
                    operating_time, downtime, total_time, operating_rate, current_status
                ) VALUES (
                    @eqId, CURDATE(), @prodCount, @faultyCount, @faultyRate,
                    @opTime, @downtime, @totalTime, @opRate, @status
                )
                ON DUPLICATE KEY UPDATE
                    production_count = @prodCount,
                    faulty_count = @faultyCount,
                    faulty_rate = @faultyRate,
                    operating_time = @opTime,
                    downtime = @downtime,
                    total_time = @totalTime,
                    operating_rate = @opRate,
                    current_status = @status";
            
            await using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@eqId", equipmentId);
            cmd.Parameters.AddWithValue("@prodCount", productionCount);
            cmd.Parameters.AddWithValue("@faultyCount", faultyCount);
            cmd.Parameters.AddWithValue("@faultyRate", faultyRate);
            cmd.Parameters.AddWithValue("@opTime", operatingTime);
            cmd.Parameters.AddWithValue("@downtime", downtime);
            cmd.Parameters.AddWithValue("@totalTime", totalTime);
            cmd.Parameters.AddWithValue("@opRate", operatingRate);
            cmd.Parameters.AddWithValue("@status", currentStatus);
            
            await cmd.ExecuteNonQueryAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static async Task<ProductionData[]> GetAllProductionDataAsync()
    {
        try
        {
            await using var con = new MySqlConnection(ConnStr);
            await con.OpenAsync();
            
            // ⭐ equipment 테이블과 LEFT JOIN하여 설비명 조회
            const string sql = @"
                SELECT 
                    ep.equipment_id, 
                    COALESCE(e.equipment_name, ep.equipment_id) AS equipment_name,
                    ep.production_count, 
                    ep.faulty_count, 
                    ep.faulty_rate,
                    ep.operating_time, 
                    ep.downtime, 
                    ep.total_time, 
                    ep.operating_rate, 
                    ep.current_status
                FROM equipment_production ep
                LEFT JOIN equipment e ON ep.equipment_id = e.equipment_id
                WHERE ep.date = CURDATE()
                ORDER BY ep.equipment_id";
            
            await using var cmd = new MySqlCommand(sql, con);
            await using var rd = await cmd.ExecuteReaderAsync();
            
            var list = new List<ProductionData>();
            while (await rd.ReadAsync())
            {
                list.Add(new ProductionData
                {
                    equipment_id = rd.GetString("equipment_id"),
                    equipment_name = rd.GetString("equipment_name"), // ⭐ 설비명 추가
                    production_count = rd.GetInt32("production_count"),
                    faulty_count = rd.GetInt32("faulty_count"),
                    faulty_rate = (double)rd.GetDecimal("faulty_rate"),
                    operating_time = rd.GetInt32("operating_time"),
                    downtime = rd.GetInt32("downtime"),
                    total_time = rd.GetInt32("total_time"),
                    operating_rate = (double)rd.GetDecimal("operating_rate"),
                    current_status = rd.GetString("current_status")
                });
            }
            System.Diagnostics.Debug.WriteLine($"[DB] GetAllProductionData: {list.Count}개 조회됨");
            return list.ToArray();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[DB] GetAllProductionData 오류: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"[DB] 스택: {ex.StackTrace}");
            return Array.Empty<ProductionData>();
        }
    }

    // 특정 설비의 오늘 데이터 조회 (Equipment_Client 복원용)
    public static async Task<ProductionData?> GetEquipmentTodayDataAsync(string equipmentId)
    {
        try
        {
            await using var con = new MySqlConnection(ConnStr);
            await con.OpenAsync();
            
            // ⭐ equipment 테이블과 LEFT JOIN하여 설비명 조회
            const string sql = @"
                SELECT 
                    ep.equipment_id, 
                    COALESCE(e.equipment_name, ep.equipment_id) AS equipment_name,
                    ep.production_count, 
                    ep.faulty_count, 
                    ep.faulty_rate,
                    ep.operating_time, 
                    ep.downtime, 
                    ep.total_time, 
                    ep.operating_rate, 
                    ep.current_status
                FROM equipment_production ep
                LEFT JOIN equipment e ON ep.equipment_id = e.equipment_id
                WHERE ep.equipment_id = @eqId AND ep.date = CURDATE()
                LIMIT 1";
            
            await using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@eqId", equipmentId);
            await using var rd = await cmd.ExecuteReaderAsync();
            
            if (!await rd.ReadAsync()) return null;
            
            return new ProductionData
            {
                equipment_id = rd.GetString("equipment_id"),
                equipment_name = rd.GetString("equipment_name"), // ⭐ 설비명 추가
                production_count = rd.GetInt32("production_count"),
                faulty_count = rd.GetInt32("faulty_count"),
                faulty_rate = (double)rd.GetDecimal("faulty_rate"),
                operating_time = rd.GetInt32("operating_time"),
                downtime = rd.GetInt32("downtime"),
                total_time = rd.GetInt32("total_time"),
                operating_rate = (double)rd.GetDecimal("operating_rate"),
                current_status = rd.GetString("current_status")
            };
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[DB] GetEquipmentTodayData 오류: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// 다음 사원번호를 자동 생성 (현재 최대 사번 +1)
    /// </summary>
    public static async Task<string?> GetNextEmployeeIdAsync()
    {
        try
        {
            await using var con = new MySqlConnection(ConnStr);
            await con.OpenAsync();
            
            // 현재 최대 사번 조회
            const string sql = @"SELECT MAX(employee_id) FROM profile";
            await using var cmd = new MySqlCommand(sql, con);
            var obj = await cmd.ExecuteScalarAsync();
            
            if (obj == null || obj == DBNull.Value)
            {
                // 사원이 없으면 00001부터 시작
                System.Diagnostics.Debug.WriteLine("[DB] GetNextEmployeeId: 첫 사원 → 00001");
                return "00001";
            }
            
            string maxId = obj.ToString() ?? "00000";
            
            // 숫자로 변환 후 +1
            if (int.TryParse(maxId, out int currentMax))
            {
                int nextId = currentMax + 1;
                string nextEmployeeId = nextId.ToString("D5"); // 5자리, 앞에 0 패딩
                System.Diagnostics.Debug.WriteLine($"[DB] GetNextEmployeeId: {maxId} → {nextEmployeeId}");
                return nextEmployeeId;
            }
            
            System.Diagnostics.Debug.WriteLine($"[DB] GetNextEmployeeId 실패: {maxId}는 숫자가 아님");
            return null;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[DB] GetNextEmployeeId 오류: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// 사원 퇴사 처리 (status를 '퇴사'로 변경)
    /// </summary>
    public static async Task<bool> RetireEmployeeAsync(string employeeId)
    {
        try
        {
            await using var con = new MySqlConnection(ConnStr);
            await con.OpenAsync();
            
            const string sql = @"UPDATE profile SET status = '퇴사' WHERE employee_id = @id";
            await using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", employeeId);
            
            var rowsAffected = await cmd.ExecuteNonQueryAsync();
            var success = rowsAffected > 0;
            
            System.Diagnostics.Debug.WriteLine($"[DB] RetireEmployee: {employeeId} → {(success ? "성공" : "실패")}");
            return success;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[DB] RetireEmployee 오류: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// 모든 설비 ID 목록 조회
    /// </summary>
    public static async Task<string[]> GetAllEquipmentIdsAsync()
    {
        try
        {
            await using var con = new MySqlConnection(ConnStr);
            await con.OpenAsync();
            
            const string sql = @"SELECT equipment_id FROM equipment ORDER BY equipment_id";
            await using var cmd = new MySqlCommand(sql, con);
            await using var rd = await cmd.ExecuteReaderAsync();
            
            var list = new List<string>();
            while (await rd.ReadAsync())
            {
                list.Add(rd.GetString("equipment_id"));
            }
            
            System.Diagnostics.Debug.WriteLine($"[DB] GetAllEquipmentIds: {list.Count}개 조회됨");
            return list.ToArray();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[DB] GetAllEquipmentIds 오류: {ex.Message}");
            return Array.Empty<string>();
        }
    }

    /// <summary>
    /// 멈춤 알림 기록 추가
    /// </summary>
    public static async Task<bool> InsertStopAlertAsync(string equipmentId, string employeeId, string stopReason)
    {
        try
        {
            await using var con = new MySqlConnection(ConnStr);
            await con.OpenAsync();
            
            const string sql = @"
                INSERT INTO stop_alerts (equipment_id, employee_id, stop_reason, occurred_at)
                VALUES (@equipmentId, @employeeId, @stopReason, NOW())";
            
            await using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@equipmentId", equipmentId);
            cmd.Parameters.AddWithValue("@employeeId", employeeId);
            cmd.Parameters.AddWithValue("@stopReason", stopReason);
            
            var rowsAffected = await cmd.ExecuteNonQueryAsync();
            var success = rowsAffected > 0;
            
            System.Diagnostics.Debug.WriteLine($"[DB] InsertStopAlert: eq={equipmentId}, emp={employeeId}, reason={stopReason} → {(success ? "성공" : "실패")}");
            return success;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[DB] InsertStopAlert 오류: {ex.Message}");
            return false;
        }
    }
}
