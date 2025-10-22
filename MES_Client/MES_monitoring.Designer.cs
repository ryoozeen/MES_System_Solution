namespace MES_Client
{
    public partial class MES_Monitoring : Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel6 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            btn_production = new Button();
            btn_dashboard = new Button();
            penal_Profile = new Panel();
            uc_Profile = new UC_Profile();
            tableLayoutPanel4 = new TableLayoutPanel();
            panel1 = new Panel();
            uc_Dashboard = new UC_Dashboard();
            uc_Production = new UC_Production();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            penal_Profile.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17.72784F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 82.27216F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 1, 0);
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1733, 1046);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = SystemColors.ActiveCaption;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel6, 0, 0);
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 65.91928F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 34.08072F));
            tableLayoutPanel2.Size = new Size(307, 1046);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Controls.Add(tableLayoutPanel3, 0, 2);
            tableLayoutPanel6.Controls.Add(penal_Profile, 0, 0);
            tableLayoutPanel6.Location = new Point(0, 0);
            tableLayoutPanel6.Margin = new Padding(0);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.Padding = new Padding(17, 20, 17, 20);
            tableLayoutPanel6.RowCount = 3;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Absolute, 240F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Size = new Size(307, 688);
            tableLayoutPanel6.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(btn_production, 0, 1);
            tableLayoutPanel3.Controls.Add(btn_dashboard, 0, 0);
            tableLayoutPanel3.Location = new Point(17, 280);
            tableLayoutPanel3.Margin = new Padding(0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 3;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel3.Size = new Size(273, 388);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // btn_production
            // 
            btn_production.BackColor = Color.White;
            btn_production.Dock = DockStyle.Fill;
            btn_production.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_production.Location = new Point(0, 129);
            btn_production.Margin = new Padding(0);
            btn_production.Name = "btn_production";
            btn_production.Padding = new Padding(3, 4, 3, 4);
            btn_production.Size = new Size(273, 129);
            btn_production.TabIndex = 1;
            btn_production.Text = "생산 및 공정 관리";
            btn_production.UseVisualStyleBackColor = false;
            // 
            // btn_dashboard
            // 
            btn_dashboard.BackColor = Color.White;
            btn_dashboard.Dock = DockStyle.Fill;
            btn_dashboard.Font = new Font("맑은 고딕", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_dashboard.Location = new Point(0, 0);
            btn_dashboard.Margin = new Padding(0);
            btn_dashboard.Name = "btn_dashboard";
            btn_dashboard.Padding = new Padding(3, 4, 3, 4);
            btn_dashboard.Size = new Size(273, 129);
            btn_dashboard.TabIndex = 0;
            btn_dashboard.Text = "대시보드";
            btn_dashboard.UseVisualStyleBackColor = false;
            // 
            // penal_Profile
            // 
            penal_Profile.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            penal_Profile.BackColor = Color.White;
            penal_Profile.Controls.Add(uc_Profile);
            penal_Profile.Location = new Point(17, 20);
            penal_Profile.Margin = new Padding(0);
            penal_Profile.Name = "penal_Profile";
            penal_Profile.Size = new Size(273, 240);
            penal_Profile.TabIndex = 1;
            // 
            // uc_Profile
            // 
            uc_Profile.Dock = DockStyle.Fill;
            uc_Profile.Location = new Point(0, 0);
            uc_Profile.Margin = new Padding(5, 6, 5, 6);
            uc_Profile.Name = "uc_Profile";
            uc_Profile.Size = new Size(273, 240);
            uc_Profile.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Controls.Add(panel1, 0, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(307, 0);
            tableLayoutPanel4.Margin = new Padding(0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 9.865471F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 90.13453F));
            tableLayoutPanel4.Size = new Size(1426, 1046);
            tableLayoutPanel4.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.Controls.Add(uc_Dashboard);
            panel1.Controls.Add(uc_Production);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1426, 1046);
            panel1.TabIndex = 0;
            // 
            // uc_Dashboard
            // 
            uc_Dashboard.Dock = DockStyle.Fill;
            uc_Dashboard.Location = new Point(0, 0);
            uc_Dashboard.Margin = new Padding(5, 6, 5, 6);
            uc_Dashboard.Name = "uc_Dashboard";
            uc_Dashboard.Size = new Size(1426, 1046);
            uc_Dashboard.TabIndex = 0;
            // 
            // uc_Production
            // 
            uc_Production.Dock = DockStyle.Fill;
            uc_Production.Location = new Point(0, 0);
            uc_Production.Margin = new Padding(5, 6, 5, 6);
            uc_Production.Name = "uc_Production";
            uc_Production.Size = new Size(1426, 1046);
            uc_Production.TabIndex = 1;
            // 
            // MES_Monitoring
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1728, 1042);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(5, 6, 5, 6);
            MinimumSize = new Size(1738, 1056);
            Name = "MES_Monitoring";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MES System";
            Load += MES_Monitoring_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            penal_Profile.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private Button btn_dashboard;
        private Button btn_production;
        private TableLayoutPanel tableLayoutPanel6;
        private TableLayoutPanel tableLayoutPanel4;
        private Panel panel1;
        private UC_Dashboard dashboard1;
        private UC_Production production1;
        private Panel penal_Profile;
        private UC_Profile UC_Profile1;
        private UC_Profile UC_Profile;
        private UC_Dashboard uc_dashboard;
        private UC_Profile uc_Profile;
        private UC_Dashboard uc_Dashboard;
        private UC_Production uc_Production;
    }
}
