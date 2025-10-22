using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MES.Common;

public class TCPIP
{
    private TcpListener? _listener;
    private readonly ConcurrentDictionary<TcpClient, bool> _clients = new();
    private readonly ConcurrentDictionary<string, TcpClient> _loggedInEmployees = new(); // 사번 -> 클라이언트
    private readonly ConcurrentDictionary<TcpClient, string> _clientToEmployee = new(); // 클라이언트 -> 사번
    private CancellationTokenSource? _internalCts;
    public event Action<string>? OnLog;

    public async Task StartAsync(int port, CancellationToken externalToken)
    {
        if (_listener != null) return;
        _listener = new TcpListener(IPAddress.Any, port);
        _listener.Start();
        Log($"리스너 시작: 0.0.0.0:{port}");
        _internalCts = CancellationTokenSource.CreateLinkedTokenSource(externalToken);
        _ = AcceptLoop(_internalCts.Token);
        await Task.CompletedTask;
    }

    public async Task StopAsync()
    {
        try
        {
            _internalCts?.Cancel();
            _listener?.Stop();
            _listener = null;

            foreach (var c in _clients.Keys)
                try { c.Close(); } catch { }
            _clients.Clear();

            Log("리스너/클라이언트 정리 완료");
            await Task.CompletedTask;
        }
        catch (Exception ex) { Log($"정지 중 예외: {ex.Message}"); }
    }

    private async Task AcceptLoop(CancellationToken token)
    {
        try
        {
            while (!token.IsCancellationRequested)
            {
                var client = await _listener!.AcceptTcpClientAsync(token);
                _clients[client] = true;
                var ep = client.Client?.RemoteEndPoint?.ToString() ?? "unknown";
                Log($"클라이언트 접속: {ep}");
                _ = HandleClientAsync(client, token);
            }
        }
        catch (OperationCanceledException) { }
        catch (ObjectDisposedException) { } // 종료 중 정상
        catch (Exception ex) { Log($"AcceptLoop 예외: {ex.Message}"); }
    }

    private async Task HandleClientAsync(TcpClient client, CancellationToken token)
    {
        // Dispose 이후에도 안전하게 로그를 남기기 위해 문자열로 보관
        string endpoint = client.Client?.RemoteEndPoint?.ToString() ?? "unknown";

        using (client)
        using (var ns = client.GetStream())
        using (var rd = new StreamReader(ns, Encoding.UTF8))
        using (var wr = new StreamWriter(ns, new UTF8Encoding(false)) { AutoFlush = true })
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    var line = await rd.ReadLineAsync();
                    if (line == null) break;

                    try
                    {
                        using var doc = JsonDocument.Parse(line);
                        if (!doc.RootElement.TryGetProperty("type", out var tp)) continue;
                        var type = tp.GetString();

                        // Ping/Pong
                        if (type == MsgTypes.Ping)
                        {
                            try
                            {
                                var body = JsonMsg.ReadBody<PingPong>(line);
                                long ts = body?.ts ?? DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                                // 수동 JSON 구성
                                string pong = $"{{\"type\":\"Pong\",\"body\":{{\"ts\":{ts}}}}}";
                                await wr.WriteLineAsync(pong);
                                Log($"PONG -> {endpoint}");
                            }
                            catch (Exception ex)
                            {
                                Log($"PONG 실패: {ex.Message}");
                            }
                            continue;
                        }

                        // 1) 로그인
                        if (type == "LoginReq")
                        {
                            try
                        {
                            var body = doc.RootElement.GetProperty("body");
                            var empId = body.GetProperty("id").GetString() ?? "";
                            
                            // ⭐ 중복 로그인 체크
                            if (_loggedInEmployees.ContainsKey(empId))
                            {
                                Log($"LOGIN 거부: {empId}는 이미 로그인된 사번입니다.");
                                string escapedIdDup = empId.Replace("\"", "\\\"").Replace("\\", "\\\\");
                                string jsonResponseDup = $"{{\"type\":\"LoginAck\",\"body\":{{\"ok\":false,\"id\":\"{escapedIdDup}\",\"reason\":\"already_logged_in\"}}}}";
                                await wr.WriteLineAsync(jsonResponseDup);
                                continue;
                            }
                            
                            var ok = await Db.ExistsEmployeeAsync(empId);

                            // 로그인 성공 시 추적 목록에 추가
                            if (ok)
                            {
                                _loggedInEmployees.TryAdd(empId, client);
                                _clientToEmployee.TryAdd(client, empId);
                                Log($"LOGIN 성공: {empId} 로그인됨");
                            }

                            // 수동 JSON 구성
                            string escapedId = empId.Replace("\"", "\\\"").Replace("\\", "\\\\");
                            string jsonResponse = $"{{\"type\":\"LoginAck\",\"body\":{{\"ok\":{ok.ToString().ToLower()},\"id\":\"{escapedId}\",\"reason\":\"\"}}}}";
                            
                            await wr.WriteLineAsync(jsonResponse);
                            Log($"LOGIN: empId={empId}, ok={ok}");
                            }
                            catch (Exception ex)
                            {
                                Log($"LOGIN 처리 실패: {ex.Message}");
                                await wr.WriteLineAsync("{\"type\":\"LoginAck\",\"body\":{\"ok\":false,\"id\":\"\",\"reason\":\"error\"}}");
                            }
                            continue;
                        }

                        // 2) 프로필 단건 조회
                        if (type == "QueryProfileReq")
                        {
                            try
                        {
                            var body = doc.RootElement.GetProperty("body");
                            var empId = body.GetProperty("employee_id").GetString() ?? "";
                            
                            var row = await Db.GetProfileAsync(empId);

                            if (row is null)
                            {
                                    // 수동 JSON 구성
                                    await wr.WriteLineAsync("{\"type\":\"QueryProfileAck\",\"body\":{\"ok\":false}}");
                                Log($"PROFILE: id={empId}, ok=False");
                            }
                            else
                            {
                                    // 수동 JSON 구성 - 특수 문자 이스케이프
                                    string escapedName = row.name.Replace("\"", "\\\"").Replace("\\", "\\\\");
                                    string escapedDept = row.department.Replace("\"", "\\\"").Replace("\\", "\\\\");
                                    string escapedPos = row.position.Replace("\"", "\\\"").Replace("\\", "\\\\");
                                    string escapedId = row.employee_id.Replace("\"", "\\\"").Replace("\\", "\\\\");
                                    string escapedEqId = (row.equipment_id ?? "").Replace("\"", "\\\"").Replace("\\", "\\\\");
                                    
                                    // snake_case 필드명으로 전송
                                    string jsonResponse = $"{{\"type\":\"QueryProfileAck\",\"body\":{{" +
                                        $"\"ok\":true," +
                                        $"\"employee_id\":\"{escapedId}\"," +
                                        $"\"name\":\"{escapedName}\"," +
                                        $"\"department\":\"{escapedDept}\"," +
                                        $"\"position\":\"{escapedPos}\"," +
                                        $"\"equipment_id\":{(string.IsNullOrEmpty(row.equipment_id) ? "null" : $"\"{escapedEqId}\"")}," +
                                        $"\"role\":{row.role}" +
                                        $"}}}}";
                                    
                                    await wr.WriteLineAsync(jsonResponse);
                                    Log($"PROFILE: id={empId}, ok=True, role={row.role}");
                                }
                            }
                            catch (Exception ex)
                            {
                                Log($"PROFILE 처리 실패: {ex.Message}");
                                await wr.WriteLineAsync("{\"type\":\"QueryProfileAck\",\"body\":{\"ok\":false}}");
                            }
                            continue;
                        }

                        // 3) 장비 등록
                        if (type == MsgTypes.EquipmentRegister)
                        {
                            try
                            {
                                var reg = JsonMsg.ReadBody<EquipmentRegister>(line);
                                bool ok = !string.IsNullOrWhiteSpace(reg?.equipment_id) && await Db.ExistsEquipmentAsync(reg!.equipment_id);
                                // 수동 JSON 구성
                                string ack = $"{{\"type\":\"EquipmentRegisterAck\",\"body\":{{\"ok\":{ok.ToString().ToLower()}}}}}";
                                await wr.WriteLineAsync(ack);
                                Log($"EQUIP.REG: id={reg?.equipment_id}, ok={ok}");
                            }
                            catch (Exception ex)
                            {
                                Log($"EQUIP.REG 실패: {ex.Message}");
                                await wr.WriteLineAsync("{\"type\":\"EquipmentRegisterAck\",\"body\":{\"ok\":false}}");
                            }
                            continue;
                        }

                        // 4) 상태 리포트
                        if (type == MsgTypes.StateReport)
                        {
                            var st = JsonMsg.ReadBody<StateReport>(line);
                            Log($"STATE: eq={st?.equipment_id}, state={st?.state}, reason={st?.reason}");
                            if (!string.IsNullOrWhiteSpace(st?.equipment_id) && !string.IsNullOrWhiteSpace(st?.state))
                            {
                                _ = Db.InsertStateReportAsync(st!.equipment_id, st.state, st.reason);
                            }
                            continue;
                        }

                        // 5) 사원 등록
                        if (type == "AddProfileReq")
                        {
                            try
                            {
                                var req = JsonMsg.ReadBody<AddProfileReq>(line);
                                var (ok, msg) = await Db.InsertProfileAsync(
                                    req!.employee_id,
                                    req.name,
                                    req.department,
                                    req.position,
                                    req.equipment_id);
                                // 수동 JSON 구성 (⭐ null 안전 처리)
                                string escapedMsg = (msg ?? "").Replace("\"", "\\\"").Replace("\\", "\\\\");
                                string ack = $"{{\"type\":\"AddProfileAck\",\"body\":{{\"ok\":{ok.ToString().ToLower()},\"message\":\"{escapedMsg}\"}}}}";
                                await wr.WriteLineAsync(ack);
                                Log($"ADD PROFILE: id={req.employee_id}, name={req.name}, ok={ok}, msg={msg ?? "(null)"}");
                            }
                            catch (Exception ex)
                            {
                                Log($"ADD PROFILE 실패: {ex.Message}\n스택: {ex.StackTrace}");
                                await wr.WriteLineAsync("{\"type\":\"AddProfileAck\",\"body\":{\"ok\":false,\"message\":\"오류 발생\"}}");
                            }
                            continue;
                        }

                        // 6) 사원 수정
                        if (type == "UpdateProfileReq")
                        {
                            try
                            {
                                var req = JsonMsg.ReadBody<UpdateProfileReq>(line);
                                var (ok, msg) = await Db.UpdateProfileAsync(
                                    req!.employee_id,
                                    req.name,
                                    req.department,
                                    req.position,
                                    req.equipment_id);
                                // 수동 JSON 구성 (⭐ null 안전 처리)
                                string escapedMsg = (msg ?? "").Replace("\"", "\\\"").Replace("\\", "\\\\");
                                string ack = $"{{\"type\":\"UpdateProfileAck\",\"body\":{{\"ok\":{ok.ToString().ToLower()},\"message\":\"{escapedMsg}\"}}}}";
                                await wr.WriteLineAsync(ack);
                                Log($"UPDATE PROFILE: id={req.employee_id}, ok={ok}, msg={msg ?? "(null)"}");
                            }
                            catch (Exception ex)
                            {
                                Log($"UPDATE PROFILE 실패: {ex.Message}\n스택: {ex.StackTrace}");
                                await wr.WriteLineAsync("{\"type\":\"UpdateProfileAck\",\"body\":{\"ok\":false,\"message\":\"오류 발생\"}}");
                            }
                            continue;
                        }

                        // 7) 사원 삭제
                        if (type == "DeleteProfileReq")
                        {
                            try
                            {
                                var req = JsonMsg.ReadBody<DeleteProfileReq>(line);
                                var (ok, msg) = await Db.DeleteProfileAsync(req!.employee_id);
                                // 수동 JSON 구성 (⭐ null 안전 처리)
                                string escapedMsg = (msg ?? "").Replace("\"", "\\\"").Replace("\\", "\\\\");
                                string ack = $"{{\"type\":\"DeleteProfileAck\",\"body\":{{\"ok\":{ok.ToString().ToLower()},\"message\":\"{escapedMsg}\"}}}}";
                                await wr.WriteLineAsync(ack);
                                Log($"DELETE PROFILE: id={req.employee_id}, ok={ok}, msg={msg ?? "(null)"}");
                            }
                            catch (Exception ex)
                            {
                                Log($"DELETE PROFILE 실패: {ex.Message}");
                                await wr.WriteLineAsync("{\"type\":\"DeleteProfileAck\",\"body\":{\"ok\":false,\"message\":\"오류 발생\"}}");
                            }
                            continue;
                        }

                        // 8) 사원 목록 조회
                        if (type == "ListProfilesReq")
                        {
                            try
                            {
                                var profiles = await Db.ListProfilesAsync();
                                // 수동 JSON 구성
                                var sb = new System.Text.StringBuilder();
                                sb.Append("{\"type\":\"ListProfilesAck\",\"body\":{\"ok\":true,\"profiles\":[");
                                
                                for (int i = 0; i < profiles.Length; i++)
                                {
                                    var p = profiles[i];
                                    if (i > 0) sb.Append(",");
                                    
                                    string escapedId = p.employee_id.Replace("\"", "\\\"").Replace("\\", "\\\\");
                                    string escapedName = p.name.Replace("\"", "\\\"").Replace("\\", "\\\\");
                                    string escapedDept = p.department.Replace("\"", "\\\"").Replace("\\", "\\\\");
                                    string escapedPos = p.position.Replace("\"", "\\\"").Replace("\\", "\\\\");
                                    string escapedEqId = (p.equipment_id ?? "").Replace("\"", "\\\"").Replace("\\", "\\\\");
                                    
                                    // snake_case 필드명으로 전송
                                    string escapedStatus = p.status.Replace("\"", "\\\"").Replace("\\", "\\\\");
                                    
                                    sb.Append($"{{\"employee_id\":\"{escapedId}\",");
                                    sb.Append($"\"name\":\"{escapedName}\",");
                                    sb.Append($"\"department\":\"{escapedDept}\",");
                                    sb.Append($"\"position\":\"{escapedPos}\",");
                                    sb.Append($"\"equipment_id\":{(string.IsNullOrEmpty(p.equipment_id) ? "null" : $"\"{escapedEqId}\"")},");
                                    sb.Append($"\"role\":{p.role},");
                                    sb.Append($"\"status\":\"{escapedStatus}\"}}");
                                }
                                
                                sb.Append("]}}");
                                
                                await wr.WriteLineAsync(sb.ToString());
                                Log($"LIST PROFILES: count={profiles.Length}");
                            }
                            catch (Exception ex)
                            {
                                Log($"LIST PROFILES 실패: {ex.Message}");
                                await wr.WriteLineAsync("{\"type\":\"ListProfilesAck\",\"body\":{\"ok\":false,\"profiles\":[]}}");
                            }
                            continue;
                        }

                        // 9) 다음 사원번호 자동 생성
                        if (type == "GetNextEmployeeIdReq")
                        {
                            try
                            {
                                var nextId = await Db.GetNextEmployeeIdAsync();
                                var ok = nextId != null;
                                var escapedId = (nextId ?? "").Replace("\"", "\\\"").Replace("\\", "\\\\");
                                string ack = $"{{\"type\":\"GetNextEmployeeIdAck\",\"body\":{{\"ok\":{ok.ToString().ToLower()},\"next_employee_id\":\"{escapedId}\"}}}}";
                                await wr.WriteLineAsync(ack);
                                Log($"GET NEXT EMPLOYEE ID: {nextId}");
                            }
                            catch (Exception ex)
                            {
                                Log($"GET NEXT EMPLOYEE ID 실패: {ex.Message}");
                                await wr.WriteLineAsync("{\"type\":\"GetNextEmployeeIdAck\",\"body\":{\"ok\":false,\"next_employee_id\":null}}");
                            }
                            continue;
                        }

                        // 10) 사원 퇴사 처리
                        if (type == "RetireEmployeeReq")
                        {
                            try
                            {
                                var req = JsonMsg.ReadBody<RetireEmployeeReq>(line);
                                var ok = await Db.RetireEmployeeAsync(req!.employee_id);
                                var message = ok ? "퇴사 처리 완료" : "퇴사 처리 실패";
                                var escapedMsg = message.Replace("\"", "\\\"").Replace("\\", "\\\\");
                                string ack = $"{{\"type\":\"RetireEmployeeAck\",\"body\":{{\"ok\":{ok.ToString().ToLower()},\"message\":\"{escapedMsg}\"}}}}";
                                await wr.WriteLineAsync(ack);
                                Log($"RETIRE EMPLOYEE: id={req.employee_id}, ok={ok}");
                            }
                            catch (Exception ex)
                            {
                                Log($"RETIRE EMPLOYEE 실패: {ex.Message}");
                                await wr.WriteLineAsync("{\"type\":\"RetireEmployeeAck\",\"body\":{\"ok\":false,\"message\":\"오류 발생\"}}");
                            }
                            continue;
                        }

                        // 11) 설비 목록 조회
                        if (type == "GetEquipmentListReq")
                        {
                            try
                            {
                                var equipmentIds = await Db.GetAllEquipmentIdsAsync();
                                var sb = new System.Text.StringBuilder();
                                sb.Append("{\"type\":\"GetEquipmentListAck\",\"body\":{\"ok\":true,\"equipment_ids\":[");
                                
                                for (int i = 0; i < equipmentIds.Length; i++)
                                {
                                    if (i > 0) sb.Append(",");
                                    string escapedId = equipmentIds[i].Replace("\"", "\\\"").Replace("\\", "\\\\");
                                    sb.Append($"\"{escapedId}\"");
                                }
                                
                                sb.Append("]}}");
                                await wr.WriteLineAsync(sb.ToString());
                                Log($"GET EQUIPMENT LIST: count={equipmentIds.Length}");
                            }
                            catch (Exception ex)
                            {
                                Log($"GET EQUIPMENT LIST 실패: {ex.Message}");
                                await wr.WriteLineAsync("{\"type\":\"GetEquipmentListAck\",\"body\":{\"ok\":false,\"equipment_ids\":[]}}");
                            }
                            continue;
                        }

                        // 11) 생산 데이터 업데이트 (Equipment_Client → Server)
                        if (type == "UpdateProductionData")
                        {
                            var body = JsonMsg.ReadBody<UpdateProductionDataReq>(line);
                            if (body != null)
                            {
                                var ok = await Db.UpsertProductionDataAsync(
                                    body.equipment_id,
                                    body.production_count,
                                    body.faulty_count,
                                    body.faulty_rate,
                                    body.operating_time,
                                    body.downtime,
                                    body.total_time,
                                    body.operating_rate,
                                    body.current_status);
                                Log($"UPDATE PROD DATA: {body.equipment_id}, prod={body.production_count}, ok={ok}");
                            }
                            continue;
                        }

                        // 10) 생산 데이터 조회 (MES_Client → Server)
                        if (type == "GetProductionDataReq")
                        {
                            try
                            {
                                Log($"GET PROD DATA REQ 수신");
                                var data = await Db.GetAllProductionDataAsync();
                                Log($"DB에서 데이터 조회: {data?.Length ?? 0}개");
                                
                                if (data == null || data.Length == 0)
                                {
                                    // 데이터 없음 - 빈 배열
                                    await wr.WriteLineAsync("{\"type\":\"GetProductionDataAck\",\"body\":{\"ok\":true,\"data\":[]}}");
                                    Log($"GET PROD DATA: 데이터 없음");
                                }
                                else
                                {
                                    // 안전하게 JSON 수동 구성
                                    var sb = new System.Text.StringBuilder();
                                    sb.Append("{\"type\":\"GetProductionDataAck\",\"body\":{\"ok\":true,\"data\":[");
                                    
                                    for (int i = 0; i < data.Length; i++)
                                    {
                                        var d = data[i];
                                        if (i > 0) sb.Append(",");
                                        
                                        // NaN, Infinity 체크 및 안전한 변환
                                        double safeOperatingRate = double.IsNaN(d.operating_rate) || double.IsInfinity(d.operating_rate) ? 0.0 : d.operating_rate;
                                        double safeFaultyRate = double.IsNaN(d.faulty_rate) || double.IsInfinity(d.faulty_rate) ? 0.0 : d.faulty_rate;
                                        
                                        // Escape equipment_name for JSON
                                        string escapedName = d.equipment_name.Replace("\"", "\\\"").Replace("\\", "\\\\");
                                        
                                        // snake_case 필드명으로 전송 (클라이언트와 일치)
                                        sb.Append($"{{\"equipment_id\":\"{d.equipment_id}\",");
                                        sb.Append($"\"equipment_name\":\"{escapedName}\","); // ⭐ 설비명 추가
                                        sb.Append($"\"production_count\":{d.production_count},");
                                        sb.Append($"\"faulty_count\":{d.faulty_count},");
                                        sb.Append($"\"faulty_rate\":{safeFaultyRate:F2},");
                                        sb.Append($"\"operating_time\":{d.operating_time},");
                                        sb.Append($"\"downtime\":{d.downtime},");
                                        sb.Append($"\"total_time\":{d.total_time},");
                                        sb.Append($"\"operating_rate\":{safeOperatingRate:F2},");
                                        sb.Append($"\"current_status\":\"{d.current_status}\"}}");
                                    }
                                    
                                    sb.Append("]}}");
                                    
                                    await wr.WriteLineAsync(sb.ToString());
                                    Log($"GET PROD DATA 성공: {data.Length}개 전송");
                                }
                            }
                            catch (Exception ex)
                            {
                                Log($"GET PROD DATA 실패: {ex.Message}");
                                Log($"Stack: {ex.StackTrace}");
                                
                                // 실패 시 빈 배열 응답
                                await wr.WriteLineAsync("{\"type\":\"GetProductionDataAck\",\"body\":{\"ok\":false,\"data\":[]}}");
                            }
                            continue;
                        }

                        // 11) 특정 설비의 오늘 데이터 조회 (Equipment_Client → Server)
                        if (type == "GetEquipmentDataReq")
                        {
                            try
                            {
                                var body = doc.RootElement.GetProperty("body");
                                var equipmentId = body.GetProperty("equipment_id").GetString() ?? "";
                                
                                Log($"GET EQUIPMENT DATA REQ 수신: {equipmentId}");
                                
                                var data = await Db.GetEquipmentTodayDataAsync(equipmentId);
                                
                                if (data != null)
                                {
                                    // 데이터 있음 - 복원
                                    string jsonResponse = $"{{\"type\":\"GetEquipmentDataAck\",\"body\":{{" +
                                        $"\"ok\":true," +
                                        $"\"equipment_id\":\"{data.equipment_id}\"," +
                                        $"\"production_count\":{data.production_count}," +
                                        $"\"faulty_count\":{data.faulty_count}," +
                                        $"\"operating_time\":{data.operating_time}," +
                                        $"\"downtime\":{data.downtime}," +
                                        $"\"total_time\":{data.total_time}," +
                                        $"\"current_status\":\"{data.current_status}\"" +
                                        $"}}}}";
                                    
                                    await wr.WriteLineAsync(jsonResponse);
                                    Log($"GET EQUIPMENT DATA 성공: {equipmentId}, prod={data.production_count}, time={data.operating_time}/{data.total_time}");
                                }
                                else
                                {
                                    // 데이터 없음 - 새 날짜
                                    await wr.WriteLineAsync("{\"type\":\"GetEquipmentDataAck\",\"body\":{\"ok\":false}}");
                                    Log($"GET EQUIPMENT DATA: 오늘 데이터 없음 (새 날짜)");
                                }
                            }
                            catch (Exception ex)
                            {
                                Log($"GET EQUIPMENT DATA 실패: {ex.Message}");
                                await wr.WriteLineAsync("{\"type\":\"GetEquipmentDataAck\",\"body\":{\"ok\":false}}");
                            }
                            continue;
                        }

                        // 12) 멈춤 알림 기록 (Equipment_Client → Server)
                        if (type == MsgTypes.RecordStopAlertReq)
                        {
                            try
                            {
                                var req = JsonMsg.ReadBody<RecordStopAlertReq>(line);
                                if (req != null)
                                {
                                    var ok = await Db.InsertStopAlertAsync(req.equipment_id, req.employee_id, req.stop_reason);
                                    
                                    // 수동 JSON 구성
                                    string ack = $"{{\"type\":\"RecordStopAlertAck\",\"body\":{{\"ok\":{ok.ToString().ToLower()}}}}}";
                                    await wr.WriteLineAsync(ack);
                                    Log($"RECORD STOP ALERT: eq={req.equipment_id}, emp={req.employee_id}, reason={req.stop_reason}, ok={ok}");
                                }
                            }
                            catch (Exception ex)
                            {
                                Log($"RECORD STOP ALERT 실패: {ex.Message}");
                                await wr.WriteLineAsync("{\"type\":\"RecordStopAlertAck\",\"body\":{\"ok\":false}}");
                            }
                            continue;
                        }

                        // 기타
                        Log($"RX: {line}");
                    }
                    catch (Exception ex) 
                    { 
                        Log($"❌ 파싱/핸들러 예외!");
                        Log($"받은 메시지: {line}");
                        Log($"오류: {ex.Message}");
                        Log($"스택: {ex.StackTrace}");
                    }
                }
            }
            catch (IOException) { }
            catch (Exception ex) { Log($"클라이언트 처리 예외: {ex.Message}"); }
            finally
            {
                _clients.TryRemove(client, out _);
                
                // ⭐ 로그아웃 처리: 로그인 목록에서 제거
                if (_clientToEmployee.TryRemove(client, out var empId))
                {
                    _loggedInEmployees.TryRemove(empId, out _);
                    Log($"로그아웃: {empId} 연결 종료");
                }
                
                Log($"클라이언트 종료: {endpoint}");
            }
        }
    }

    private void Log(string msg) => OnLog?.Invoke(msg);
}
