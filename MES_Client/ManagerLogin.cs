using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MES.Common;

namespace MES_Client
{
    public partial class ManagerLogin : Form
    {
        private TextBox txtEmployeeId;
        private Button btnLogin, btnCancel;
        private Label lblTitle, lblEmployeeId;

        public ManagerLogin()
        {
            SetupUI();
            WireEvents();
        }

        private void SetupUI()
        {
            this.Text = "관리자 인증";
            this.Size = new System.Drawing.Size(800, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            lblTitle = new Label
            {
                Text = "관리자 인증",
                Font = new System.Drawing.Font("맑은 고딕", 28F, System.Drawing.FontStyle.Bold),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Location = new System.Drawing.Point(50, 70),
                Size = new System.Drawing.Size(700, 70)
            };

            lblEmployeeId = new Label
            {
                Text = "관리자 사번:",
                Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(80, 220),
                Size = new System.Drawing.Size(250, 50),
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };

            txtEmployeeId = new TextBox
            {
                Location = new System.Drawing.Point(350, 220),
                Size = new System.Drawing.Size(350, 50),
                Font = new System.Drawing.Font("맑은 고딕", 18F),
                PlaceholderText = "관리자 사번 (예: 00001)"
            };

            btnLogin = new Button
            {
                Text = "로그인",
                Location = new System.Drawing.Point(200, 350),
                Size = new System.Drawing.Size(170, 90),
                Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold),
                BackColor = System.Drawing.Color.LightGreen
            };

            btnCancel = new Button
            {
                Text = "취소",
                Location = new System.Drawing.Point(400, 350),
                Size = new System.Drawing.Size(170, 90),
                Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold),
                BackColor = System.Drawing.Color.LightCoral,
                DialogResult = DialogResult.Cancel
            };

            this.Controls.AddRange(new Control[] { lblTitle, lblEmployeeId, txtEmployeeId, btnLogin, btnCancel });
            this.AcceptButton = btnLogin;
            this.CancelButton = btnCancel;
        }

        private void WireEvents()
        {
            btnLogin.Click += async (_, __) => await DoManagerLoginAsync();
        }

        private void InitializeComponent()
        {

        }

        private async Task DoManagerLoginAsync()
        {
            var employeeId = txtEmployeeId.Text?.Trim();
            if (string.IsNullOrEmpty(employeeId))
            {
                MessageBox.Show("사번을 입력하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmployeeId.Focus();
                return;
            }

            if (!ClientApp.Net.IsConnected)
            {
                MessageBox.Show("서버 연결이 끊어졌습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var tcs = new TaskCompletionSource<QueryProfileAck>();

                void Handler(string line)
                {
                    try
                    {
                        using var doc = System.Text.Json.JsonDocument.Parse(line);
                        var type = doc.RootElement.GetProperty("type").GetString();

                        if (type == "QueryProfileAck")
                        {
                            var body = doc.RootElement.GetProperty("body");
                            var ok = body.GetProperty("ok").GetBoolean();
                            var role = body.TryGetProperty("role", out var roleElement) ? roleElement.GetInt32() : 0;
                            var name = body.TryGetProperty("name", out var nameElement) ? nameElement.GetString() : null;
                            var empId = body.TryGetProperty("employee_id", out var empElement) ? empElement.GetString() : null;

                            tcs.TrySetResult(new QueryProfileAck(ok, empId, name, null, null, role));
                        }
                    }
                    catch { tcs.TrySetResult(new QueryProfileAck(false, null, null, null, null, 0)); }
                }

                ClientApp.Net.OnLine += Handler;

                try
                {
                    // 서버에 프로필 요청
                    await ClientApp.Net.SendAsync(JsonMsg.Wrap("QueryProfileReq", new QueryProfileReq(employeeId)));

                    using var cts = new CancellationTokenSource(3000);
                    await using var _ = cts.Token.Register(() => tcs.TrySetCanceled());

                    var profileAck = await tcs.Task;

                    // 🔍 디버깅: 받은 정보 출력
                    System.Diagnostics.Debug.WriteLine($"[ManagerLogin] 사번: {employeeId}");
                    System.Diagnostics.Debug.WriteLine($"[ManagerLogin] ok: {profileAck.ok}");
                    System.Diagnostics.Debug.WriteLine($"[ManagerLogin] role: {profileAck.role}");
                    System.Diagnostics.Debug.WriteLine($"[ManagerLogin] name: {profileAck.name}");

                    if (profileAck.ok && profileAck.role == 1)
                    {
                        MessageBox.Show($"관리자 인증 성공!\n사번: {employeeId}\n이름: {profileAck.name}", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else if (profileAck.ok && profileAck.role == 0)
                    {
                        MessageBox.Show($"관리자 권한이 없습니다.\n(받은 role 값: {profileAck.role})", "접근 거부", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtEmployeeId.SelectAll();
                        txtEmployeeId.Focus();
                    }
                    else
                    {
                        MessageBox.Show($"존재하지 않는 사번입니다.\n(ok: {profileAck.ok}, role: {profileAck.role})", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtEmployeeId.SelectAll();
                        txtEmployeeId.Focus();
                    }
                }
                catch (TaskCanceledException)
                {
                    MessageBox.Show("서버 응답 시간 초과", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    ClientApp.Net.OnLine -= Handler;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"인증 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
