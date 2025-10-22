using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MES.Common;

namespace MES_Client
{
    public partial class UC_Production : UserControl
    {
        private System.Windows.Forms.Timer? _dataUpdateTimer;

        public UC_Production()
        {
            InitializeComponent();

            // 데이터 업데이트 타이머 초기화 (1초마다 - 실시간)
            _dataUpdateTimer = new System.Windows.Forms.Timer();
            _dataUpdateTimer.Interval = 1000;
            _dataUpdateTimer.Tick += DataUpdateTimer_Tick;
            
            // Load 이벤트 연결
            this.Load += UC_Production_Load;
        }

        private void UpdateClock()
        {
            lblClock.Text = DateTime.Now.ToString("yyyy-MM-dd (ddd) HH:mm:ss");
        }

        private void UC_Production_Load(object sender, EventArgs e)
        {
            // 시계 타이머 시작
            UpdateClock();
            if (clockTimer != null)
            {
                if (clockTimer.Interval <= 0) clockTimer.Interval = 1000;
                clockTimer.Tick -= clockTimer_Tick;
                clockTimer.Tick += clockTimer_Tick;
                clockTimer.Start();
            }

            // 데이터 업데이트 타이머 시작
            _dataUpdateTimer?.Start();

            // 즉시 한 번 로드
            _ = LoadProductionDataAsync();
        }

        private void clockTimer_Tick(object sender, EventArgs e)
        {
            UpdateClock();
        }

        private async void DataUpdateTimer_Tick(object? sender, EventArgs e)
        {
            await LoadProductionDataAsync();
        }

        private async Task LoadProductionDataAsync()
        {
            if (!ClientApp.Net.IsConnected) return;

            try
            {
                var tcs = new TaskCompletionSource<ProductionData[]>();

                void Handler(string line)
                {
                    try
                    {
                        using var doc = System.Text.Json.JsonDocument.Parse(line);
                        var type = doc.RootElement.GetProperty("type").GetString();

                        if (type == "GetProductionDataAck")
                        {
                            var body = doc.RootElement.GetProperty("body");
                            var ok = body.GetProperty("ok").GetBoolean();

                            if (ok && body.TryGetProperty("data", out var dataElement))
                            {
                                var data = System.Text.Json.JsonSerializer.Deserialize<ProductionData[]>(
                                    dataElement.GetRawText(),
                                    new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase }
                                );
                                tcs.TrySetResult(data ?? Array.Empty<ProductionData>());
                            }
                            else
                            {
                                tcs.TrySetResult(Array.Empty<ProductionData>());
                            }
                        }
                    }
                    catch { tcs.TrySetResult(Array.Empty<ProductionData>()); }
                }

                ClientApp.Net.OnLine += Handler;

                try
                {
                    // 서버에 생산 데이터 요청 (수동 JSON 구성)
                    string request = "{\"type\":\"GetProductionDataReq\",\"body\":{}}";
                    await ClientApp.Net.SendAsync(request);

                    using var cts = new CancellationTokenSource(3000);
                    await using var _ = cts.Token.Register(() => tcs.TrySetCanceled());

                    var productionData = await tcs.Task;

                    // UI 업데이트 (UI 스레드에서)
                    if (this.InvokeRequired)
                    {
                        this.Invoke(() => UpdateProductionTable(productionData));
                    }
                    else
                    {
                        UpdateProductionTable(productionData);
                    }
                }
                catch (TaskCanceledException)
                {
                    // 타임아웃 무시
                }
                finally
                {
                    ClientApp.Net.OnLine -= Handler;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"생산 데이터 로드 실패: {ex.Message}");
            }
        }

        private async Task<Dictionary<string, string>> GetEmployeeNamesAsync(ProductionData[] data)
        {
            var result = new Dictionary<string, string>();

            if (!ClientApp.Net.IsConnected) return result;

            try
            {
                var tcs = new TaskCompletionSource<ProfileRow[]>();

                void Handler(string line)
                {
                    try
                    {
                        using var doc = System.Text.Json.JsonDocument.Parse(line);
                        var type = doc.RootElement.GetProperty("type").GetString();

                        if (type == "ListProfilesAck")
                        {
                            var body = doc.RootElement.GetProperty("body");
                            var ok = body.GetProperty("ok").GetBoolean();

                            if (ok && body.TryGetProperty("profiles", out var profilesElement))
                            {
                                var profiles = System.Text.Json.JsonSerializer.Deserialize<ProfileRow[]>(
                                    profilesElement.GetRawText(),
                                    new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase }
                                );
                                tcs.TrySetResult(profiles ?? Array.Empty<ProfileRow>());
                            }
                            else
                            {
                                tcs.TrySetResult(Array.Empty<ProfileRow>());
                            }
                        }
                    }
                    catch { tcs.TrySetResult(Array.Empty<ProfileRow>()); }
                }

                ClientApp.Net.OnLine += Handler;

                try
                {
                    await ClientApp.Net.SendAsync(JsonMsg.Wrap("ListProfilesReq", new { }));

                    using var cts = new CancellationTokenSource(3000);
                    await using var _ = cts.Token.Register(() => tcs.TrySetCanceled());

                    var profiles = await tcs.Task;

                    // equipment_id로 이름 매핑
                    foreach (var profile in profiles)
                    {
                        if (!string.IsNullOrEmpty(profile.equipment_id))
                        {
                            result[profile.equipment_id] = profile.name;
                        }
                    }
                }
                catch (TaskCanceledException) { }
                finally
                {
                    ClientApp.Net.OnLine -= Handler;
                }
            }
            catch { }

            return result;
        }

        private async void UpdateProductionTable(ProductionData[] data)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"[Production] UpdateProductionTable 시작, 데이터: {data?.Length ?? 0}개");
                
                // 작업자 이름 가져오기
                var employeeNames = await GetEmployeeNamesAsync(data);

                for (int i = 1; i <= 4; i++)
                {
                    string equipmentId = $"설비{i}";
                    var equipData = data?.FirstOrDefault(d => d.equipment_id == equipmentId);

                    // ⭐ 설비 ID 버튼 찾기
                    Button? idButton = i switch
                    {
                        1 => button1,  // 설비 #1 설비 ID
                        2 => button2,  // 설비 #2 설비 ID
                        3 => button3,  // 설비 #3 설비 ID
                        4 => button4,  // 설비 #4 설비 ID
                        _ => null
                    };

                    // 각 설비별 라벨 찾기
                    Label? nameLabel = i switch
                    {
                        1 => label10,  // 설비 #1 설비명
                        2 => label16,  // 설비 #2 설비명
                        3 => label22,  // 설비 #3 설비명
                        4 => label28,  // 설비 #4 설비명
                        _ => null
                    };

                    Label? quantityLabel = i switch
                    {
                        1 => label11,  // 설비 #1 수량
                        2 => label17,  // 설비 #2 수량
                        3 => label23,  // 설비 #3 수량
                        4 => label29,  // 설비 #4 수량
                        _ => null
                    };

                    Label? statusLabel = i switch
                    {
                        1 => label12,  // 설비 #1 진행상태
                        2 => label18,  // 설비 #2 진행상태
                        3 => label24,  // 설비 #3 진행상태
                        4 => label30,  // 설비 #4 진행상태
                        _ => null
                    };

                    Label? workerLabel = i switch
                    {
                        1 => label13,  // 설비 #1 작업자
                        2 => label19,  // 설비 #2 작업자
                        3 => label25,  // 설비 #3 작업자
                        4 => label31,  // 설비 #4 작업자
                        _ => null
                    };

                    Label? etcLabel = i switch
                    {
                        1 => label14,  // 설비 #1 기타
                        2 => label20,  // 설비 #2 기타
                        3 => label26,  // 설비 #3 기타
                        4 => label32,  // 설비 #4 기타
                        _ => null
                    };

                    if (nameLabel == null || quantityLabel == null || statusLabel == null || 
                        workerLabel == null || etcLabel == null)
                    {
                        System.Diagnostics.Debug.WriteLine($"[Production] ⚠️ 설비{i} 컨트롤 없음");
                        continue;
                    }

                    if (equipData != null)
                    {
                        // ⭐ 설비 ID (DB의 equipment_id 표시)
                        if (idButton != null)
                        {
                            idButton.Text = equipData.equipment_id;
                        }

                        // ⭐ 설비명 (DB의 equipment_name 표시)
                        nameLabel.Text = equipData.equipment_name;

                        // 생산 수량
                        quantityLabel.Text = equipData.production_count.ToString();

                        // ⭐ 진행 상태 + 이모지
                        string statusEmoji = equipData.current_status switch
                        {
                            "가동" => "🟢",       // 초록색 동그라미
                            "정지" => "🔴",       // 빨간색 동그라미
                            "멈춤" => "🟣",       // 보라색 동그라미 ⭐ 변경
                            "보수" => "🟡",       // 노란색 동그라미 ⭐ 신규
                            "점검" => "🟠",       // 주황색 동그라미 ⭐ 신규
                            "설비 고장" => "🔴",  // 빨간색 동그라미
                            "보수중" => "🟡",     // 노란색 (하위 호환)
                            "점검중" => "🟠",     // 주황색 (하위 호환)
                            _ => "⚪"            // 흰색 동그라미
                        };
                        
                        statusLabel.Text = $"{equipData.current_status} {statusEmoji}";
                        statusLabel.ForeColor = equipData.current_status switch
                        {
                            "가동" => Color.FromArgb(76, 175, 80),      // 🟢 초록색 (Green)
                            "정지" => Color.FromArgb(244, 67, 54),      // 🔴 빨간색 (Red)
                            "멈춤" => Color.FromArgb(156, 39, 176),     // 🟣 보라색 (Purple) ⭐ 변경
                            "보수" => Color.FromArgb(255, 235, 59),     // 🟡 노란색 (Yellow) ⭐ 신규
                            "점검" => Color.FromArgb(255, 152, 0),      // 🟠 주황색 (Orange) ⭐ 신규
                            "설비 고장" => Color.FromArgb(244, 67, 54), // 🔴 빨간색
                            "보수중" => Color.FromArgb(255, 235, 59),   // 🟡 노란색 (하위 호환)
                            "점검중" => Color.FromArgb(255, 152, 0),    // 🟠 주황색 (하위 호환)
                            _ => Color.FromArgb(158, 158, 158)          // ⚪ 회색 (Gray)
                        };

                        // 작업자명
                        if (employeeNames.TryGetValue(equipmentId, out var workerName))
                        {
                            workerLabel.Text = workerName;
                        }
                        else
                        {
                            workerLabel.Text = "미배정";
                        }

                        // 기타 (가동률 표시)
                        etcLabel.Text = $"{equipData.operating_rate:F1}%";
                        
                        System.Diagnostics.Debug.WriteLine($"[Production] ✅ {equipmentId}: {equipData.current_status}, 수량={equipData.production_count}, 가동률={equipData.operating_rate:F1}%");
                    }
                    else
                    {
                        // 데이터 없음
                        nameLabel.Text = $"설비{i}";
                        quantityLabel.Text = "0";
                        statusLabel.Text = "미가동 ⚪";
                        statusLabel.ForeColor = Color.FromArgb(158, 158, 158);
                        workerLabel.Text = "미배정";
                        etcLabel.Text = "0.0%";
                        
                        System.Diagnostics.Debug.WriteLine($"[Production] ⚪ {equipmentId}: 데이터 없음");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Production] ❌ UpdateProductionTable 오류: {ex.Message}");
            }
        }
    }
}
