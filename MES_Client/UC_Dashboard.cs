using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MES.Common;

namespace MES_Client
{
    public partial class UC_Dashboard : UserControl
    {
        private System.Windows.Forms.Timer? _dataUpdateTimer;

        public UC_Dashboard()
        {
            InitializeComponent();
            
            // 데이터 업데이트 타이머 초기화 (1초마다 - 실시간)
            _dataUpdateTimer = new System.Windows.Forms.Timer();
            _dataUpdateTimer.Interval = 1000;
            _dataUpdateTimer.Tick += DataUpdateTimer_Tick;
            
            // Load 이벤트 연결
            this.Load += UC_Dashboard_Load;
        }

        private void UpdateClock()
        {
            lblClock.Text = DateTime.Now.ToString("yyyy-MM-dd (ddd) HH:mm:ss");
        }

        private void UC_Dashboard_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("=== 대시보드 로드 시작 ===");
            
            // 시계 타이머 시작
            UpdateClock();
            if (clockTimer != null)
            {
                if (clockTimer.Interval <= 0) clockTimer.Interval = 1000;
                clockTimer.Tick -= clockTimer_Tick;
                clockTimer.Tick += clockTimer_Tick;
                clockTimer.Start();
                System.Diagnostics.Debug.WriteLine("시계 타이머 시작됨");
            }

            // 데이터 업데이트 타이머 시작
            if (_dataUpdateTimer != null)
            {
                _dataUpdateTimer.Start();
                System.Diagnostics.Debug.WriteLine($"데이터 업데이트 타이머 시작됨 (Interval: {_dataUpdateTimer.Interval}ms)");
            }
            
            // 서버 연결 상태 확인
            bool isConnected = ClientApp.Net?.IsConnected ?? false;
            System.Diagnostics.Debug.WriteLine($"서버 연결 상태: {isConnected}");
            
            // 즉시 한 번 로드
            System.Diagnostics.Debug.WriteLine("첫 번째 데이터 로드 시작...");
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
            System.Diagnostics.Debug.WriteLine("[LoadProductionData] 시작");
            
            if (!ClientApp.Net.IsConnected)
            {
                System.Diagnostics.Debug.WriteLine("[LoadProductionData] ❌ 서버 미연결 - 중단");
                return;
            }

            System.Diagnostics.Debug.WriteLine("[LoadProductionData] ✅ 서버 연결 확인됨");

            try
            {
                var tcs = new TaskCompletionSource<ProductionData[]>();

                void Handler(string line)
                {
                    System.Diagnostics.Debug.WriteLine($"[LoadProductionData] 📥 서버 응답 수신: {line.Substring(0, Math.Min(100, line.Length))}...");
                    
                    try
                    {
                        using var doc = System.Text.Json.JsonDocument.Parse(line);
                        var type = doc.RootElement.GetProperty("type").GetString();
                        
                        System.Diagnostics.Debug.WriteLine($"[LoadProductionData] 메시지 타입: {type}");
                        
                        if (type == "GetProductionDataAck")
                        {
                            var body = doc.RootElement.GetProperty("body");
                            var ok = body.GetProperty("ok").GetBoolean();
                            
                            System.Diagnostics.Debug.WriteLine($"[LoadProductionData] GetProductionDataAck 수신, ok={ok}");
                            
                            if (ok && body.TryGetProperty("data", out var dataElement))
                            {
                                var dataJson = dataElement.GetRawText();
                                System.Diagnostics.Debug.WriteLine($"[LoadProductionData] 데이터 JSON (처음 200자): {dataJson.Substring(0, Math.Min(200, dataJson.Length))}");
                                
                                ProductionData[]? data = null;
                                try
                                {
                                    data = System.Text.Json.JsonSerializer.Deserialize<ProductionData[]>(
                                        dataJson,
                                        new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase }
                                    );
                                }
                                catch (Exception ex)
                                {
                                    System.Diagnostics.Debug.WriteLine($"[LoadProductionData] ❌ JSON 파싱 오류: {ex.Message}");
                                }
                                
                                System.Diagnostics.Debug.WriteLine($"[LoadProductionData] ✅ 데이터 파싱 성공: {data?.Length ?? 0}개");
                                
                                if (data != null && data.Length > 0)
                                {
                                    foreach (var item in data)
                                    {
                                        System.Diagnostics.Debug.WriteLine($"  - {item.equipment_id}: 생산={item.production_count}, 불량={item.faulty_count}, 상태={item.current_status}");
                                    }
                                }
                                
                                tcs.TrySetResult(data ?? Array.Empty<ProductionData>());
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("[LoadProductionData] ⚠️ ok=false 또는 data 없음");
                                tcs.TrySetResult(Array.Empty<ProductionData>());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"[LoadProductionData] ❌ 응답 파싱 오류: {ex.Message}");
                        tcs.TrySetResult(Array.Empty<ProductionData>());
                    }
                }

                ClientApp.Net.OnLine += Handler;

                try
                {
                    // 서버에 생산 데이터 요청 (수동 JSON 구성)
                    string request = "{\"type\":\"GetProductionDataReq\",\"body\":{}}";
                    System.Diagnostics.Debug.WriteLine($"[LoadProductionData] 📤 서버에 요청 전송: {request}");
                    await ClientApp.Net.SendAsync(request);

                    using var cts = new CancellationTokenSource(3000);
                    await using var _ = cts.Token.Register(() => tcs.TrySetCanceled());

                    var productionData = await tcs.Task;
                    
                    System.Diagnostics.Debug.WriteLine($"[LoadProductionData] 응답 대기 완료, 데이터: {productionData.Length}개");

                    // UI 업데이트 (UI 스레드에서)
                    if (this.InvokeRequired)
                    {
                        System.Diagnostics.Debug.WriteLine("[LoadProductionData] UI 스레드로 전환 (Invoke)");
                        this.Invoke(() => UpdateDashboard(productionData));
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("[LoadProductionData] UI 스레드에서 직접 실행");
                        UpdateDashboard(productionData);
                    }
                }
                catch (TaskCanceledException)
                {
                    System.Diagnostics.Debug.WriteLine("[LoadProductionData] ⏱️ 타임아웃 (3초)");
                }
                finally
                {
                    ClientApp.Net.OnLine -= Handler;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[LoadProductionData] ❌ 전체 오류: {ex.Message}\n{ex.StackTrace}");
            }
        }

        private void UpdateDashboard(ProductionData[] data)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"[Dashboard] UpdateDashboard 호출! 데이터: {data?.Length ?? 0}개");
                
                if (data == null || data.Length == 0)
                {
                    // 데이터 없음 - 기본값
                    if (label3 != null) label3.Text = "0 수량";
                    if (label7 != null) label7.Text = "0.0 %";
                    if (label10 != null) label10.Text = "0.0 %";
                    System.Diagnostics.Debug.WriteLine("[Dashboard] ⚠️ 데이터 없음");
                    return;
                }

                // 1. 총 생산량
                int totalProduction = data.Sum(d => d.production_count);
                if (label3 != null)
                {
                    label3.Text = $"{totalProduction} 수량";
                    System.Diagnostics.Debug.WriteLine($"[Dashboard] ✅ 생산량: {totalProduction}");
                }

                // 2. 총 가동률 (전체 가동 시간 / 전체 총 시간)
                int totalOperatingTime = data.Sum(d => d.operating_time);
                int totalTotalTime = data.Sum(d => d.total_time);
                double totalOperatingRate = totalTotalTime > 0 ? 
                    (double)totalOperatingTime / totalTotalTime * 100 : 0;
                if (label7 != null)
                {
                    label7.Text = $"{totalOperatingRate:F1} %";
                    System.Diagnostics.Debug.WriteLine($"[Dashboard] ✅ 총 가동률: {totalOperatingRate:F1}% (가동시간:{totalOperatingTime}/{totalTotalTime})");
                }

                // 3. 총 불량률
                int totalFaulty = data.Sum(d => d.faulty_count);
                double totalFaultyRate = totalProduction > 0 ? 
                    (double)totalFaulty / totalProduction * 100 : 0;
                if (label10 != null)
                {
                    label10.Text = $"{totalFaultyRate:F1} %";
                    System.Diagnostics.Debug.WriteLine($"[Dashboard] ✅ 불량률: {totalFaultyRate:F1}%");
                }

                // 4. 설비별 상태 및 색상 변경
                UpdateEquipmentStatus(data);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Dashboard] ❌ UpdateDashboard 오류: {ex.Message}");
            }
        }

        private void UpdateEquipmentStatus(ProductionData[] data)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"[Dashboard] UpdateEquipmentStatus 시작, 데이터: {data?.Length ?? 0}개");
                
                // 설비별 데이터 찾기 및 UI 업데이트
                for (int i = 1; i <= 4; i++)
                {
                    string equipmentId = $"설비{i}";
                    var equipData = data?.FirstOrDefault(d => d.equipment_id == equipmentId);

                    // 해당 설비의 패널과 라벨 찾기
                    TableLayoutPanel? panel = i switch
                    {
                        1 => tableLayoutPanel18, // 설비 #1
                        2 => tableLayoutPanel19, // 설비 #2
                        3 => tableLayoutPanel20, // 설비 #3
                        4 => tableLayoutPanel21, // 설비 #4
                        _ => null
                    };

                    Label? statusLabel = i switch
                    {
                        1 => label19, // 설비 #1 상태
                        2 => label17, // 설비 #2 상태
                        3 => label21, // 설비 #3 상태
                        4 => label25, // 설비 #4 상태
                        _ => null
                    };

                    Label? rateLabel = i switch
                    {
                        1 => label23, // 설비 #1 가동률
                        2 => label16, // 설비 #2 가동률
                        3 => label24, // 설비 #3 가동률
                        4 => label24, // 설비 #4 가동률
                        _ => null
                    };

                    if (panel == null || statusLabel == null || rateLabel == null)
                    {
                        System.Diagnostics.Debug.WriteLine($"[Dashboard] ⚠️ 설비{i} 컨트롤 없음");
                        continue;
                    }

                    if (equipData != null)
                    {
                        // ⭐ 상태에 따라 배경색 변경
                        Color bgColor = equipData.current_status switch
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

                        panel.BackColor = bgColor;
                        statusLabel.Text = equipData.current_status;
                        statusLabel.ForeColor = Color.White;
                        rateLabel.Text = $"{equipData.operating_rate:F1}%";
                        rateLabel.ForeColor = Color.White;
                        
                        System.Diagnostics.Debug.WriteLine($"[Dashboard] ✅ {equipmentId}: {equipData.current_status}, {equipData.operating_rate:F1}%");
                    }
                    else
                    {
                        // 데이터 없음 - 회색
                        panel.BackColor = Color.FromArgb(158, 158, 158);
                        statusLabel.Text = "미가동";
                        statusLabel.ForeColor = Color.White;
                        rateLabel.Text = "0.0%";
                        rateLabel.ForeColor = Color.White;
                        
                        System.Diagnostics.Debug.WriteLine($"[Dashboard] ⚪ {equipmentId}: 데이터 없음");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Dashboard] ❌ UpdateEquipmentStatus 오류: {ex.Message}");
            }
        }

        // 디자이너가 만들어둔 기본 핸들러들 유지
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) { }
        private void label12_Click(object sender, EventArgs e) { }
        private void label14_Click(object sender, EventArgs e) { }
    }
}
