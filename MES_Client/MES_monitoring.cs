using System;
using System.Linq;
using System.Windows.Forms;

namespace MES_Client
{
    public partial class MES_Monitoring : Form
    {
        // Loding/Login에서 전달
        public string PendingEmployeeId { get; set; } = "";

        // 우측 패널에서 사용할 실제 뷰(디자이너 인스턴스 없으면 코드에서 생성)
        private UC_Dashboard _dashboardView;
        private UC_Production _productionView;

        public MES_Monitoring()
        {
            InitializeComponent();

            // 버튼 이벤트
            btn_dashboard.Click += (_, __) => ShowDashboard();
            btn_production.Click += (_, __) => ShowProduction();

            // 폼 Load
            this.Load += MES_Monitoring_Load;
        }

        private void MES_Monitoring_Load(object? sender, EventArgs e)
        {
            // 1) panel1 내부에 이미 올라온 인스턴스가 있으면 재사용, 없으면 새로 생성
            _dashboardView = panel1.Controls.OfType<UC_Dashboard>().FirstOrDefault() ?? new UC_Dashboard();
            _productionView = panel1.Controls.OfType<UC_Production>().FirstOrDefault() ?? new UC_Production();

            // 2) 부모/도킹 보장
            EnsureInPanel(_dashboardView);
            EnsureInPanel(_productionView);

            // 3) 기본 화면: 대시보드
            _dashboardView.BringToFront();

            // 4) 좌측 프로필 조회(디자이너에 있는 uc_Profile 사용)
            if (!string.IsNullOrWhiteSpace(PendingEmployeeId))
                uc_Profile.LoadProfileAsync(PendingEmployeeId);
        }

        private void ShowDashboard()
        {
            EnsureInPanel(_dashboardView);
            _dashboardView.BringToFront();
        }

        private void ShowProduction()
        {
            EnsureInPanel(_productionView);
            _productionView.BringToFront();
        }

        // panel1에 없으면 추가하고 Dock=Fill
        private void EnsureInPanel(Control view)
        {
            if (view == null) return;

            if (view.Parent != panel1)
            {
                view.Dock = DockStyle.Fill;
                panel1.Controls.Add(view);
            }
        }
    }
}
