using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;

namespace Equipment_Client
{
    public partial class EquipmentLogin : Form
    {
        public string? LoggedInEmployeeId { get; private set; }
        public string? AssignedEquipmentId { get; private set; }

        private TextBox txtEmployeeId;
        private Button btnLogin, btnCancel;
        private Label lblTitle, lblEmployeeId;

        public EquipmentLogin()
        {
            SetupUI();
            WireEvents();
        }

        private void SetupUI()
        {
            this.Text = "설비 클라이언트 로그인";
            this.Size = new System.Drawing.Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            lblTitle = new Label
            {
                Text = "설비 작업자 로그인",
                Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Bold),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Location = new System.Drawing.Point(50, 60),
                Size = new System.Drawing.Size(500, 70)
            };

            lblEmployeeId = new Label
            {
                Text = "사번:",
                Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(80, 180),
                Size = new System.Drawing.Size(120, 45),
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };

            txtEmployeeId = new TextBox
            {
                Location = new System.Drawing.Point(220, 180),
                Size = new System.Drawing.Size(300, 45),
                Font = new System.Drawing.Font("맑은 고딕", 16F),
                PlaceholderText = "예: 00002"
            };

            btnLogin = new Button
            {
                Text = "로그인",
                Location = new System.Drawing.Point(150, 270),
                Size = new System.Drawing.Size(140, 70),
                Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold),
                BackColor = System.Drawing.Color.LightGreen
            };

            btnCancel = new Button
            {
                Text = "취소",
                Location = new System.Drawing.Point(310, 270),
                Size = new System.Drawing.Size(140, 70),
                Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold),
                BackColor = System.Drawing.Color.LightCoral,
                DialogResult = DialogResult.Cancel
            };

            this.Controls.AddRange(new Control[] { lblTitle, lblEmployeeId, txtEmployeeId, btnLogin, btnCancel });
            this.AcceptButton = btnLogin;
            this.CancelButton = btnCancel;
        }

        private void WireEvents()
        {
            btnLogin.Click += async (_, __) => await DoLoginAsync();
        }

        private async Task DoLoginAsync()
        {
            var employeeId = txtEmployeeId.Text?.Trim();
            if (string.IsNullOrEmpty(employeeId))
            {
                MessageBox.Show("사번을 입력하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmployeeId.Focus();
                return;
            }

            // ⭐ 일반 사원 전용: "admin" 거부
            if (employeeId == "admin")
            {
                MessageBox.Show("설비 클라이언트는 일반 사원 전용입니다.\n관리자는 MES_Client를 사용하세요.", "접근 거부",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmployeeId.SelectAll();
                txtEmployeeId.Focus();
                return;
            }

            // 서버 연결
            if (!NetClient.ClientApp.Net.IsConnected)
            {
                var connected = await NetClient.ClientApp.Net.ConnectAsync(
                    NetClient.ClientApp.Host, 
                    NetClient.ClientApp.Port, 
                    2000);

                if (!connected)
                {
                    MessageBox.Show("서버 연결 실패\n\n서버가 실행 중인지 확인하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // 로그인 요청
            var tcs = new TaskCompletionSource<(bool ok, string? equipmentId, int role)>();
            
            void Handler(string line)
            {
                try
                {
                    using var doc = JsonDocument.Parse(line);
                    var type = doc.RootElement.GetProperty("type").GetString();
                    
                    if (type == "QueryProfileAck")
                    {
                        var body = doc.RootElement.GetProperty("body");
                        var ok = body.GetProperty("ok").GetBoolean();
                        
                        if (ok)
                        {
                            // equipment_id 가져오기 (서버에서 equipment_id로 보냄)
                            string? eqId = null;
                            if (body.TryGetProperty("equipment_id", out var eqProp))
                            {
                                eqId = eqProp.GetString();
                            }
                            
                            // role 가져오기
                            int role = body.TryGetProperty("role", out var roleProp) ? roleProp.GetInt32() : 0;
                            
                            tcs.TrySetResult((true, eqId, role));
                        }
                        else
                        {
                            tcs.TrySetResult((false, null, 0));
                        }
                    }
                    else if (type == "LoginAck")
                    {
                        var body = doc.RootElement.GetProperty("body");
                        var ok = body.GetProperty("ok").GetBoolean();
                        
                        if (!ok)
                        {
                            // ⭐ 중복 로그인 체크
                            string? reason = null;
                            if (body.TryGetProperty("reason", out var reasonProp))
                            {
                                reason = reasonProp.GetString();
                            }
                            
                            // reason을 TaskCompletionSource에 전달하기 위해 특수한 role 값 사용
                            if (reason == "already_logged_in")
                            {
                                tcs.TrySetResult((false, null, -1)); // role = -1: 중복 로그인
                            }
                            else
                            {
                                tcs.TrySetResult((false, null, 0));
                            }
                        }
                        // LoginAck이 ok이면 이어서 QueryProfileReq를 보냄
                    }
                }
                catch { }
            }

            NetClient.ClientApp.Net.OnLine += Handler;

            try
            {
                // 1. 로그인 요청
                await NetClient.ClientApp.Net.SendAsync(new 
                { 
                    type = "LoginReq", 
                    body = new { id = employeeId } 
                });

                // 약간의 대기 후 프로필 조회
                await Task.Delay(200);

                // 2. 프로필 조회 (equipment_id 가져오기)
                await NetClient.ClientApp.Net.SendAsync(new 
                { 
                    type = "QueryProfileReq", 
                    body = new { employee_id = employeeId } 
                });

                using var cts = new System.Threading.CancellationTokenSource(3000);
                await using var _ = cts.Token.Register(() => tcs.TrySetCanceled());

                var (ok, equipmentId, role) = await tcs.Task;

                if (ok && role == 0)
                {
                    // ✅ 일반 사원 로그인 성공
                    LoggedInEmployeeId = employeeId;
                    
                    // 서버에서 받은 equipment_id 사용 (없으면 기본값)
                    AssignedEquipmentId = equipmentId ?? GetEquipmentIdByEmployeeId(employeeId);
                    
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else if (ok && role == 1)
                {
                    // ❌ 관리자는 거부
                    MessageBox.Show("관리자는 설비 클라이언트에 로그인할 수 없습니다.\nMES_Client를 사용하세요.", "접근 거부",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmployeeId.SelectAll();
                    txtEmployeeId.Focus();
                }
                else if (role == -1)
                {
                    // ⭐ 중복 로그인 거부
                    MessageBox.Show($"사번 '{employeeId}'은(는) 이미 다른 설비에서 로그인되어 있습니다.\n\n먼저 기존 로그인을 종료해주세요.", "중복 로그인",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmployeeId.SelectAll();
                    txtEmployeeId.Focus();
                }
                else
                {
                    MessageBox.Show("로그인 실패. 사번을 확인하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmployeeId.SelectAll();
                    txtEmployeeId.Focus();
                }
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("로그인 응답 시간 초과", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                NetClient.ClientApp.Net.OnLine -= Handler;
            }
        }

        // 임시: 사번으로 설비 ID 매핑
        private string GetEquipmentIdByEmployeeId(string employeeId)
        {
            return employeeId switch
            {
                "00002" => "설비1",
                "00003" => "설비2",
                "00004" => "설비3",
                "00005" => "설비4",
                _ => "설비1" // 기본값
            };
        }
    }
}



