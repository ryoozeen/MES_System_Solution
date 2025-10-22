namespace Equipment_Client
{
    public partial class Equipment_Client : Form
    {
        private string _equipmentId;
        private string _employeeId;

        // 생산 관리
        private System.Windows.Forms.Timer? _productionTimer;
        private System.Windows.Forms.Timer? _dataUpdateTimer; // DB 업데이트용
        private Random _random = new Random();
        private int _productionCount = 0;
        private int _faultyCount = 0;
        private bool _isRunning = false;
        private bool _isStopped = false; // 멈춤 상태 (보수 필요)
        private bool _isMaintenance = false; // ⭐ 보수 상태
        private bool _isInspection = false;  // ⭐ 점검 상태
        private DateTime _startTime;
        private int _operatingSeconds = 0; // 가동 시간 (초)
        private int _totalSeconds = 0; // 전체 시간 (초)

        // 멈춤 원인 목록
        private readonly string[] _stopReasons = new string[]
        {
            "센서 오류",
            "모터 과열",
            "자재 부족"
        };

        public Equipment_Client()
        {
            InitializeComponent();
            
            // 로그인 정보 가져오기
            _employeeId = Program.LoggedInEmployeeId ?? "UNKNOWN";
            _equipmentId = Program.AssignedEquipmentId ?? "설비1";
            
            this.Load += Form_Load;
            this.FormClosing += Form_Closing; // ⭐ 종료 시 이벤트 추가
            
            // 버튼 이벤트 연결
            btn_start.Click += Btn_Start_Click;
            btn_stop.Click += Btn_Stop_Click;
            btn_repair.Click += Btn_Repair_Click;
            btn_inspection.Click += Btn_Inspection_Click;

            // 생산 타이머 초기화 (1초마다)
            _productionTimer = new System.Windows.Forms.Timer();
            _productionTimer.Interval = 1000;
            _productionTimer.Tick += ProductionTimer_Tick;

            // DB 업데이트 타이머 초기화 (2초마다 - 실시간)
            _dataUpdateTimer = new System.Windows.Forms.Timer();
            _dataUpdateTimer.Interval = 2000;
            _dataUpdateTimer.Tick += DataUpdateTimer_Tick;
            _dataUpdateTimer.Start(); // 항상 실행

            // 초기 값 설정
            lbl_product_output.Text = "0";
            lbl_faulty_output.Text = "0";
            
            _startTime = DateTime.Now;
        }

        private async void Form_Load(object? sender, EventArgs e)
        {
            // 설비 번호 표시
            lbl_Equipment_number.Text = $"{_equipmentId} (작업자: {_employeeId})";
            
            // 이미 로그인 시 서버 연결되어 있음
            if (!NetClient.ClientApp.Net.IsConnected)
            {
                MessageBox.Show("서버 연결이 끊어졌습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            // 장비 등록
            await NetClient.ClientApp.Net.SendAsync(new { 
                type = MES.Common.MsgTypes.EquipmentRegister, 
                body = new MES.Common.EquipmentRegister(_equipmentId, null, null) 
            });

            // 오늘 데이터 복원 시도
            await LoadTodayDataAsync();
        }

        // 오늘 날짜의 데이터 불러오기 (복원 또는 초기화)
        private async Task LoadTodayDataAsync()
        {
            try
            {
                var tcs = new TaskCompletionSource<(bool ok, int prodCount, int faultyCount, int opTime, int totalTime)>();
                
                void Handler(string line)
                {
                    try
                    {
                        using var doc = System.Text.Json.JsonDocument.Parse(line);
                        var type = doc.RootElement.GetProperty("type").GetString();
                        
                        if (type == "GetEquipmentDataAck")
                        {
                            var body = doc.RootElement.GetProperty("body");
                            var ok = body.GetProperty("ok").GetBoolean();
                            
                            if (ok)
                            {
                                // 데이터 복원
                                int prodCount = body.GetProperty("production_count").GetInt32();
                                int faultyCount = body.GetProperty("faulty_count").GetInt32();
                                int opTime = body.GetProperty("operating_time").GetInt32();
                                int totalTime = body.GetProperty("total_time").GetInt32();
                                
                                tcs.TrySetResult((true, prodCount, faultyCount, opTime, totalTime));
                            }
                            else
                            {
                                // 새 날짜 - 0부터 시작
                                tcs.TrySetResult((false, 0, 0, 0, 0));
                            }
                        }
                    }
                    catch { tcs.TrySetResult((false, 0, 0, 0, 0)); }
                }

                NetClient.ClientApp.Net.OnLine += Handler;

                try
                {
                    // 서버에 오늘 데이터 요청
                    await NetClient.ClientApp.Net.SendAsync(new
                    {
                        type = "GetEquipmentDataReq",
                        body = new { equipment_id = _equipmentId }
                    });

                    using var cts = new System.Threading.CancellationTokenSource(3000);
                    await using var _ = cts.Token.Register(() => tcs.TrySetCanceled());

                    var (ok, prodCount, faultyCount, opTime, totalTime) = await tcs.Task;

                    if (ok)
                    {
                        // 데이터 복원
                        _productionCount = prodCount;
                        _faultyCount = faultyCount;
                        _operatingSeconds = opTime;
                        _totalSeconds = totalTime;

                        // UI 업데이트
                        lbl_product_output.Text = _productionCount.ToString();
                        lbl_faulty_output.Text = _faultyCount.ToString();

                        System.Diagnostics.Debug.WriteLine($"[Equipment_Client] ✅ 오늘 데이터 복원: 생산={prodCount}, 불량={faultyCount}, 시간={opTime}/{totalTime}");
                        MessageBox.Show($"오늘 데이터를 복원했습니다.\n생산: {prodCount}, 불량: {faultyCount}", "데이터 복원", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // 새 날짜 - 초기화
                        System.Diagnostics.Debug.WriteLine("[Equipment_Client] 🆕 새 날짜 - 데이터 초기화");
                        MessageBox.Show("오늘은 새로운 날입니다.\n데이터를 0부터 시작합니다.", "새 날짜", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                finally
                {
                    NetClient.ClientApp.Net.OnLine -= Handler;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Equipment_Client] ❌ 데이터 로드 실패: {ex.Message}");
            }
        }

        // ⭐ 폼 종료 시 실행
        private async void Form_Closing(object? sender, FormClosingEventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"[Equipment_Client] 🔴 프로그램 종료 - 설비 상태를 '미가동'으로 업데이트");

                // 타이머 정지
                _productionTimer?.Stop();
                _dataUpdateTimer?.Stop();

                // 서버에 연결되어 있으면 "미가동" 상태로 업데이트
                if (NetClient.ClientApp.Net.IsConnected)
                {
                    await NetClient.ClientApp.Net.SendAsync(new
                    {
                        type = "UpdateProductionData",
                        body = new
                        {
                            equipment_id = _equipmentId,
                            production_count = _productionCount,
                            faulty_count = _faultyCount,
                            faulty_rate = _productionCount > 0 ? (double)_faultyCount / _productionCount * 100 : 0,
                            operating_time = _operatingSeconds,
                            downtime = _totalSeconds - _operatingSeconds,
                            total_time = _totalSeconds,
                            operating_rate = _totalSeconds > 0 ? (double)_operatingSeconds / _totalSeconds * 100 : 0,
                            current_status = "미가동" // ⭐ 종료 시 "미가동"으로 설정
                        }
                    });

                    // 서버가 처리할 시간을 주기 위해 잠시 대기
                    await Task.Delay(500);

                    System.Diagnostics.Debug.WriteLine($"[Equipment_Client] ✅ '미가동' 상태 업데이트 완료");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Equipment_Client] ❌ 종료 처리 실패: {ex.Message}");
            }
        }

        // "시작" 버튼 클릭
        private void Btn_Start_Click(object? sender, EventArgs e)
        {
            if (_isStopped)
            {
                MessageBox.Show("설비가 멈춤 상태입니다. '보수' 버튼을 먼저 클릭하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_isRunning)
            {
                _isRunning = true;
                _isMaintenance = false; // ⭐ 보수 상태 해제
                _isInspection = false;  // ⭐ 점검 상태 해제
                _productionTimer?.Start();
                SendState("정상", null);
                MessageBox.Show("설비 가동을 시작합니다.", "시작", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // "정지" 버튼 클릭
        private void Btn_Stop_Click(object? sender, EventArgs e)
        {
            if (_isRunning)
            {
                _isRunning = false;
                // _productionTimer는 계속 실행 (총 시간 계산 위해)
                SendState("멈춤", "수동정지");
                
                // ⭐ stop_alerts 테이블에 기록 (긴급 정지)
                RecordStopAlert("긴급 정지");
                
                MessageBox.Show("설비를 정지합니다.", "정지", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // "보수" 버튼 클릭
        private void Btn_Repair_Click(object? sender, EventArgs e)
        {
            // ⭐ 보수 상태 활성화
            _isMaintenance = true;
            _isInspection = false; // 점검 해제
            _isRunning = false;
            
            if (_isStopped)
            {
                _isStopped = false;
                // ★ 멈춤 원인 기록은 유지 (Clear 제거)
                SendState("보수", null); // ⭐ "보수중" → "보수"
                
                // ⭐ stop_alerts 테이블에 기록 (보수중)
                RecordStopAlert("보수중");
                
                MessageBox.Show("보수 완료. 이제 '시작' 버튼을 눌러 설비를 재가동할 수 있습니다.", "보수 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                SendState("보수", null); // ⭐ "보수중" → "보수"
                
                // ⭐ stop_alerts 테이블에 기록 (보수중)
                RecordStopAlert("보수중");
                
                MessageBox.Show("보수 작업을 수행합니다.", "보수", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // "점검" 버튼 클릭
        private void Btn_Inspection_Click(object? sender, EventArgs e)
        {
            // ⭐ 점검 상태 활성화
            _isInspection = true;
            _isMaintenance = false; // 보수 해제
            _isRunning = false;
            // _productionTimer는 계속 실행 (총 시간 계산 위해)
            
            SendState("점검", null); // ⭐ "점검중" → "점검"
            
            // ⭐ stop_alerts 테이블에 기록 (점검중)
            RecordStopAlert("점검중");
            
            MessageBox.Show("설비를 점검합니다.", "점검", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // 1초마다 실행되는 생산 로직
        private void ProductionTimer_Tick(object? sender, EventArgs e)
        {
            // 전체 시간 증가 (항상)
            _totalSeconds++;
            
            if (!_isRunning || _isStopped) return;

            // 가동 시간 증가
            _operatingSeconds++;

            // % 확률로 설비 멈춤 발생
            if (_random.Next(100) < 2)
            {
                _isRunning = false;
                _isStopped = true;
                // _productionTimer는 계속 실행 (총 시간 계산 위해)

                // 멈춤 원인 선택 (3개 중 1개)
                string reason = _stopReasons[_random.Next(_stopReasons.Length)];
                
                // ★ 멈춤 원인 추가 (누적)
                listBox_stoppage.Items.Add($"[{DateTime.Now:HH:mm:ss}] {reason}");
                
                SendState("설비 고장", reason);
                
                // ⭐ stop_alerts 테이블에 기록 (확률 멈춤)
                RecordStopAlert(reason);
                
                MessageBox.Show($"설비 멈춤 발생!\n원인: {reason}\n\n'보수' 버튼을 클릭하고 '시작' 버튼을 눌러 재가동하세요.", "설비 멈춤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 생산량 1 증가
            _productionCount++;
            lbl_product_output.Text = _productionCount.ToString();

            // % 확률로 불량 발생
            if (_random.Next(100) < 5)
            {
                _faultyCount++;
                lbl_faulty_output.Text = _faultyCount.ToString();
            }
        }

        // 10초마다 DB에 생산 데이터 업데이트
        private async void DataUpdateTimer_Tick(object? sender, EventArgs e)
        {
            if (!NetClient.ClientApp.Net.IsConnected) return;

            try
            {
                // 불량률 계산
                double faultyRate = _productionCount > 0 ? 
                    (double)_faultyCount / _productionCount * 100 : 0;

                // 가동률 계산
                double operatingRate = _totalSeconds > 0 ? 
                    (double)_operatingSeconds / _totalSeconds * 100 : 0;

                // ⭐ 현재 상태 결정 (우선순위: 보수 > 점검 > 멈춤 > 가동 > 정지)
                string currentStatus = _isMaintenance ? "보수" :
                                       _isInspection ? "점검" :
                                       _isStopped ? "멈춤" : 
                                       _isRunning ? "가동" : "정지";

                // 서버로 생산 데이터 전송
                await NetClient.ClientApp.Net.SendAsync(new
                {
                    type = "UpdateProductionData",
                    body = new
                    {
                        equipment_id = _equipmentId,
                        production_count = _productionCount,
                        faulty_count = _faultyCount,
                        faulty_rate = Math.Round(faultyRate, 2),
                        operating_time = _operatingSeconds,
                        downtime = _totalSeconds - _operatingSeconds,
                        total_time = _totalSeconds,
                        operating_rate = Math.Round(operatingRate, 2),
                        current_status = currentStatus
                    }
                });
            }
            catch (Exception ex)
            {
                // 로그만 남기고 무시 (UI 방해 X)
                System.Diagnostics.Debug.WriteLine($"DB 업데이트 실패: {ex.Message}");
            }
        }

        private async void SendState(string state, string? reason)
        {
            if (!NetClient.ClientApp.Net.IsConnected) return;
            await NetClient.ClientApp.Net.SendAsync(new { type = MES.Common.MsgTypes.StateReport, body = new MES.Common.StateReport(_equipmentId, state, reason) });
        }

        /// <summary>
        /// ⭐ stop_alerts 테이블에 멈춤 알림 기록
        /// </summary>
        private async void RecordStopAlert(string stopReason)
        {
            if (!NetClient.ClientApp.Net.IsConnected) return;
            
            try
            {
                await NetClient.ClientApp.Net.SendAsync(new
                {
                    type = MES.Common.MsgTypes.RecordStopAlertReq,
                    body = new MES.Common.RecordStopAlertReq(_equipmentId, _employeeId, stopReason)
                });
                
                System.Diagnostics.Debug.WriteLine($"[Equipment_Client] ✅ stop_alerts 기록 전송: eq={_equipmentId}, emp={_employeeId}, reason={stopReason}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Equipment_Client] ❌ stop_alerts 기록 실패: {ex.Message}");
            }
        }
    }
}
