using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MES.Common;

namespace MES_Client
{
    public partial class Loding : Form
    {
        private bool _disconnectHooked = false;

        public Loding()
        {
            InitializeComponent();
            this.Shown += Loding_Shown; // 폼이 보일 때 시작
        }

        private async void Loding_Shown(object? sender, EventArgs e)
        {
            // 1) 게이지바 3초 (약 60ms × 50회 ≈ 3초)
            for (int v = 0; v <= 100; v += 2)
            {
                percentBar.Value = v;
                await Task.Delay(60);
            }

            // 2) 서버 연결 시도 (타임아웃 2초)
            bool ok = await ClientApp.Net.ConnectAsync(ClientApp.Host, ClientApp.Port, timeoutMs: 2000);

            if (ok)
            {
                // ★ 끊김 이벤트를 "한 번만" 구독
                if (!_disconnectHooked)
                {
                    _disconnectHooked = true;
                    ClientApp.Net.OnDisconnected += () =>
                    {
                        // UI 스레드에서 팝업 → 종료
                        ClientApp.UI?.Post(_ =>
                        {
                            try
                            {
                                MessageBox.Show("서버 연결 끊김", "알림",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            catch { /* ignore */ }
                            Application.Exit();
                        }, null);
                    };
                }

                // ★ 주기 Ping(백그라운드)
                _ = Task.Run(async () =>
                {
                    while (ClientApp.Net.IsConnected)
                    {
                        try
                        {
                            var ts = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                            await ClientApp.Net.SendAsync(new { type = MsgTypes.Ping, body = new PingPong(ts) });
                        }
                        catch { /* ignore */ }
                        await Task.Delay(5000);
                    }
                });

                // 3) 성공 → 로딩 폼 완전히 숨기고 닫기
                this.Hide();
                this.Close();
            }
            else
            {
                // 4) 실패 → 팝업 후 전체 종료
                MessageBox.Show("서버 연결 실패", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}
