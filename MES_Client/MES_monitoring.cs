using System;
using System.Linq;
using System.Windows.Forms;

namespace MES_Client
{
    public partial class MES_Monitoring : Form
    {
        // Loding/Login���� ����
        public string PendingEmployeeId { get; set; } = "";

        // ���� �гο��� ����� ���� ��(�����̳� �ν��Ͻ� ������ �ڵ忡�� ����)
        private UC_Dashboard _dashboardView;
        private UC_Production _productionView;

        public MES_Monitoring()
        {
            InitializeComponent();

            // ��ư �̺�Ʈ
            btn_dashboard.Click += (_, __) => ShowDashboard();
            btn_production.Click += (_, __) => ShowProduction();

            // �� Load
            this.Load += MES_Monitoring_Load;
        }

        private void MES_Monitoring_Load(object? sender, EventArgs e)
        {
            // 1) panel1 ���ο� �̹� �ö�� �ν��Ͻ��� ������ ����, ������ ���� ����
            _dashboardView = panel1.Controls.OfType<UC_Dashboard>().FirstOrDefault() ?? new UC_Dashboard();
            _productionView = panel1.Controls.OfType<UC_Production>().FirstOrDefault() ?? new UC_Production();

            // 2) �θ�/��ŷ ����
            EnsureInPanel(_dashboardView);
            EnsureInPanel(_productionView);

            // 3) �⺻ ȭ��: ��ú���
            _dashboardView.BringToFront();

            // 4) ���� ������ ��ȸ(�����̳ʿ� �ִ� uc_Profile ���)
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

        // panel1�� ������ �߰��ϰ� Dock=Fill
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
