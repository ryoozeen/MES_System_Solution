using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MES_Client
{
    public partial class UC_Profile : UserControl
    {
        public UC_Profile() => InitializeComponent();

        public async void LoadProfileAsync(string employeeId)
        {
            var tcs = new TaskCompletionSource<(bool ok, string id, string name, string dept, string pos)>();

            void Handler(string line)
            {
                try
                {
                    using var doc = JsonDocument.Parse(line);
                    if (doc.RootElement.GetProperty("type").GetString() != "QueryProfileAck") return;
                    var body = doc.RootElement.GetProperty("body");
                    var ok = body.TryGetProperty("ok", out var okEl) && okEl.GetBoolean();
                    if (!ok) { tcs.TrySetResult((false, "", "", "", "")); return; }

                    var id = body.GetProperty("employee_id").GetString() ?? "";
                    var name = body.GetProperty("name").GetString() ?? "";
                    var dept = body.GetProperty("department").GetString() ?? "";
                    var pos = body.GetProperty("position").GetString() ?? "";
                    tcs.TrySetResult((true, id, name, dept, pos));
                }
                catch { }
            }

            ClientApp.Net.OnLine += Handler;
            try
            {
                await ClientApp.Net.SendAsync(new
                {
                    type = "QueryProfileReq",
                    body = new { employee_id = employeeId }
                });

                using var cts = new CancellationTokenSource(3000);
                await using var _ = cts.Token.Register(() => tcs.TrySetCanceled());

                var res = await tcs.Task;
                if (res.ok) SetLabels(res.id, res.name, res.dept, res.pos);
                else SetLabels(employeeId, "-", "-", "-");
            }
            catch
            {
                SetLabels(employeeId, "-", "-", "-");
            }
            finally
            {
                ClientApp.Net.OnLine -= Handler;
            }
        }

        private void SetLabels(string id, string name, string dept, string pos)
        {
            if (InvokeRequired) { Invoke(new Action(() => SetLabels(id, name, dept, pos))); return; }
            lbl_employee_id.Text = id;
            lbl_name.Text = name;
            lbl_department.Text = dept;
            lbl_position.Text = pos;
        }

        // 디자이너가 생성한 다른 이벤트 핸들러는 그대로 두셔도 됩니다.
    }
}
