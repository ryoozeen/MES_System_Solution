using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MES_Client
{
    public partial class Login : Form
    {
        public string? LoggedInEmployeeId { get; private set; }

        public Login()
        {
            InitializeComponent();
            txt_id.PlaceholderText = "관리자 사번 (예: 00001)";
            btn_login.Click += async (_, __) => await DoLoginAsync();
        }

        private async Task DoLoginAsync()
        {
            var id = txt_id.Text?.Trim();
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("사번을 입력해주세요.", "알림",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_id.Focus();
                return;
            }

            if (!ClientApp.Net.IsConnected)
            {
                MessageBox.Show("서버 연결이 없습니다.", "오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var tcs = new TaskCompletionSource<(bool ok, int role)>();
            void Handler(string line)
            {
                try
                {
                    using var doc = JsonDocument.Parse(line);
                    var type = doc.RootElement.GetProperty("type").GetString();
                    
                    if (type == "LoginAck")
                    {
                        var ok = doc.RootElement.GetProperty("body").GetProperty("ok").GetBoolean();
                        if (!ok)
                        {
                            tcs.TrySetResult((false, 0));
                        }
                        // LoginAck이 ok이면 QueryProfileAck을 기다림
                    }
                    else if (type == "QueryProfileAck")
                    {
                        var body = doc.RootElement.GetProperty("body");
                        var ok = body.GetProperty("ok").GetBoolean();
                        var role = body.TryGetProperty("role", out var r) ? r.GetInt32() : 0;
                        tcs.TrySetResult((ok, role));
                    }
                }
                catch { }
            }

            ClientApp.Net.OnLine += Handler;

            try
            {
                // 1단계: 로그인
                await ClientApp.Net.SendAsync(new { type = "LoginReq", body = new { id } });
                await Task.Delay(200);

                // 2단계: 프로필 조회 (role 확인)
                await ClientApp.Net.SendAsync(new { type = "QueryProfileReq", body = new { employee_id = id } });

                using var cts = new CancellationTokenSource(3000);
                await using var _ = cts.Token.Register(() => tcs.TrySetCanceled());
                var (ok, role) = await tcs.Task;

                if (ok && role == 1)
                {
                    // ✅ 관리자 권한 확인됨
                    LoggedInEmployeeId = id;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else if (ok && role == 0)
                {
                    // ❌ 일반 사원은 거부
                    MessageBox.Show("관리자 권한이 필요합니다.\n'admin' 계정으로 로그인하세요.", "접근 거부",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_id.SelectAll();
                    txt_id.Focus();
                }
                else
                {
                    MessageBox.Show("존재하지 않는 사번입니다.", "로그인 실패",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_id.SelectAll();
                    txt_id.Focus();
                }
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("로그인 응답 시간 초과", "오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ClientApp.Net.OnLine -= Handler;
            }
        }
    }

    // 사원 등록 다이얼로그(간단 입력 → 서버로 전송)
    public partial class Login
    {
        private async Task ShowRegisterAsync()
        {
            using var dlg = new Form()
            {
                Text = "사원 등록",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false,
                Width = 320,
                Height = 280
            };

            var txtId = new TextBox { Left = 110, Top = 20, Width = 170 };
            var txtName = new TextBox { Left = 110, Top = 55, Width = 170 };
            var txtDept = new TextBox { Left = 110, Top = 90, Width = 170 };
            var txtPos = new TextBox { Left = 110, Top = 125, Width = 170 };
            var txtEq = new TextBox { Left = 110, Top = 160, Width = 170, Text = "설비1" };

            dlg.Controls.Add(new Label { Left = 20, Top = 24, Width = 80, Text = "사번" });
            dlg.Controls.Add(txtId);
            dlg.Controls.Add(new Label { Left = 20, Top = 59, Width = 80, Text = "이름" });
            dlg.Controls.Add(txtName);
            dlg.Controls.Add(new Label { Left = 20, Top = 94, Width = 80, Text = "부서" });
            dlg.Controls.Add(txtDept);
            dlg.Controls.Add(new Label { Left = 20, Top = 129, Width = 80, Text = "직급" });
            dlg.Controls.Add(txtPos);
            dlg.Controls.Add(new Label { Left = 20, Top = 164, Width = 80, Text = "설비ID" });
            dlg.Controls.Add(txtEq);

            var okBtn = new Button { Text = "등록", Left = 110, Top = 200, Width = 80, DialogResult = DialogResult.OK };
            var cancelBtn = new Button { Text = "취소", Left = 200, Top = 200, Width = 80, DialogResult = DialogResult.Cancel };
            dlg.Controls.Add(okBtn);
            dlg.Controls.Add(cancelBtn);
            dlg.AcceptButton = okBtn;
            dlg.CancelButton = cancelBtn;

            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            var id = txtId.Text.Trim();
            var name = txtName.Text.Trim();
            var dept = txtDept.Text.Trim();
            var pos = txtPos.Text.Trim();
            var eq = txtEq.Text.Trim();

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(dept) || string.IsNullOrEmpty(pos))
            {
                MessageBox.Show("모든 항목을 입력하세요.");
                return;
            }

            var tcs = new TaskCompletionSource<(bool ok, string? msg)>();
            void Handler(string line)
            {
                try
                {
                    using var doc = JsonDocument.Parse(line);
                    if (doc.RootElement.GetProperty("type").GetString() != "AddProfileAck") return;
                    var body = doc.RootElement.GetProperty("body");
                    var ok = body.GetProperty("ok").GetBoolean();
                    var msg = body.TryGetProperty("message", out var m) ? m.GetString() : null;
                    tcs.TrySetResult((ok, msg));
                }
                catch { }
            }

            ClientApp.Net.OnLine += Handler;
            try
            {
                await ClientApp.Net.SendAsync(new
                {
                    type = "AddProfileReq",
                    body = new MES.Common.AddProfileReq(id, name, dept, pos, eq)
                });

                using var cts = new CancellationTokenSource(3000);
                await using var _ = cts.Token.Register(() => tcs.TrySetCanceled());
                var res = await tcs.Task;
                if (res.ok) MessageBox.Show("등록 성공");
                else MessageBox.Show(res.msg ?? "등록 실패", "오류");
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("응답 시간 초과", "오류");
            }
            finally
            {
                ClientApp.Net.OnLine -= Handler;
            }
        }
    }
}
