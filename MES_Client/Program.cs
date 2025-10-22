using System.Threading;
using System.Windows.Forms;

namespace MES_Client
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // UI 스레드 컨텍스트 저장(팝업용 UI 스레드에서 사용)
            ClientApp.UI = SynchronizationContext.Current;

            // 앱 종료 시 네트워크 정리
            Application.ApplicationExit += (_, __) => ClientApp.Net.Close();

            // 1단계: 로딩 화면 → 서버 연결
            var loading = new Loding();
            loading.FormClosed += (_, __) =>
            {
                // 서버 연결 성공 시 메인 메뉴 표시
                if (ClientApp.Net.IsConnected)
                {
                    ShowMainMenu();
                }
                else
                {
                    // 연결 실패 시 종료
                    Application.Exit();
                }
            };
            
            Application.Run(loading);
        }

        private static void ShowMainMenu()
        {
            using var menu = new Form
            {
                Text = "MES 시스템",
                Size = new System.Drawing.Size(800, 1000),
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false
            };

            var lblTitle = new Label
            {
                Text = "MES 시스템에 오신 것을 환영합니다",
                Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Bold),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Location = new System.Drawing.Point(50, 100),
                Size = new System.Drawing.Size(700, 80)
            };

            var btnUserLogin = new Button
            {
                Text = "관리자 로그인",  // ★ 변경: 사용자 → 관리자
                Location = new System.Drawing.Point(150, 300),
                Size = new System.Drawing.Size(500, 150),
                Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold),
                BackColor = System.Drawing.Color.LightBlue
            };

            var btnManagerLogin = new Button
            {
                Text = "관리",  // ★ 변경: 관리자 로그인 → 관리
                Location = new System.Drawing.Point(150, 500),
                Size = new System.Drawing.Size(500, 150),
                Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold),
                BackColor = System.Drawing.Color.LightGreen
            };

            var btnExit = new Button
            {
                Text = "종료",
                Location = new System.Drawing.Point(300, 700),
                Size = new System.Drawing.Size(200, 100),
                Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold),
                BackColor = System.Drawing.Color.LightCoral
            };

            btnUserLogin.Click += (_, __) =>
            {
                using var login = new Login();
                if (login.ShowDialog(menu) == DialogResult.OK)
                {
                    // 로그인 성공 시 모니터링 화면 표시
                    var monitoring = new MES_Monitoring();
                    monitoring.PendingEmployeeId = login.LoggedInEmployeeId;
                    monitoring.FormClosed += (s, e) =>
                    {
                        menu.Close();
                        Application.Exit();
                    };
                    menu.Hide();  // 메뉴 숨기고
                    monitoring.ShowDialog();  // 모니터링 표시
                    menu.Close();  // 모니터링 종료 후 메뉴 닫기
                }
            };

            btnManagerLogin.Click += (_, __) =>
            {
                using var managerLogin = new ManagerLogin();
                if (managerLogin.ShowDialog(menu) == DialogResult.OK)
                {
                    using var employeeManager = new EmployeeManager();
                    employeeManager.ShowDialog();
                }
            };

            btnExit.Click += (_, __) => 
            {
                menu.Close();
                Application.Exit();
            };

            menu.Controls.AddRange(new Control[] { lblTitle, btnUserLogin, btnManagerLogin, btnExit });
            menu.ShowDialog();
        }
    }
}