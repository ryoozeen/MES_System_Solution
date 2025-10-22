namespace MES_Client

{
    partial class UC_Profile
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            UC_People = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            PB_People = new PictureBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            lbl_name = new Label();
            lbl_department = new Label();
            lbl_position = new Label();
            lbl_employee_id = new Label();
            UC_People.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PB_People).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // UC_People
            // 
            UC_People.ColumnCount = 1;
            UC_People.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.333333F));
            UC_People.Controls.Add(tableLayoutPanel2, 0, 0);
            UC_People.Controls.Add(tableLayoutPanel1, 0, 1);
            UC_People.Dock = DockStyle.Fill;
            UC_People.Location = new Point(0, 0);
            UC_People.Margin = new Padding(0);
            UC_People.Name = "UC_People";
            UC_People.Padding = new Padding(10);
            UC_People.RowCount = 2;
            UC_People.RowStyles.Add(new RowStyle(SizeType.Percent, 37F));
            UC_People.RowStyles.Add(new RowStyle(SizeType.Percent, 63F));
            UC_People.Size = new Size(514, 600);
            UC_People.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 171F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(PB_People, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(10, 10);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(494, 214);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // PB_People
            // 
            PB_People.Dock = DockStyle.Fill;
            PB_People.Image = Properties.Resources.People;
            PB_People.Location = new Point(161, 0);
            PB_People.Margin = new Padding(0);
            PB_People.Name = "PB_People";
            PB_People.Size = new Size(171, 214);
            PB_People.SizeMode = PictureBoxSizeMode.Zoom;
            PB_People.TabIndex = 0;
            PB_People.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 257F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(lbl_department, 1, 0);
            tableLayoutPanel1.Controls.Add(lbl_position, 1, 2);
            tableLayoutPanel1.Controls.Add(lbl_employee_id, 1, 1);
            tableLayoutPanel1.Controls.Add(lbl_name, 1, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(10, 224);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(494, 366);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // lbl_name
            // 
            lbl_name.Location = new Point(123, 273);
            lbl_name.Margin = new Padding(5, 0, 5, 0);
            lbl_name.Name = "lbl_name";
            lbl_name.Size = new Size(247, 81);
            lbl_name.TabIndex = 2;
            lbl_name.Text = "이름";
            lbl_name.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbl_department
            // 
            lbl_department.Dock = DockStyle.Fill;
            lbl_department.Location = new Point(118, 0);
            lbl_department.Margin = new Padding(0);
            lbl_department.Name = "lbl_department";
            lbl_department.Size = new Size(257, 91);
            lbl_department.TabIndex = 0;
            lbl_department.Text = "소속";
            lbl_department.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbl_position
            // 
            lbl_position.Dock = DockStyle.Fill;
            lbl_position.Location = new Point(123, 182);
            lbl_position.Margin = new Padding(5, 0, 5, 0);
            lbl_position.Name = "lbl_position";
            lbl_position.Size = new Size(247, 91);
            lbl_position.TabIndex = 3;
            lbl_position.Text = "직급";
            lbl_position.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbl_employee_id
            // 
            lbl_employee_id.AutoSize = true;
            lbl_employee_id.Dock = DockStyle.Fill;
            lbl_employee_id.Location = new Point(118, 91);
            lbl_employee_id.Margin = new Padding(0);
            lbl_employee_id.Name = "lbl_employee_id";
            lbl_employee_id.Size = new Size(257, 91);
            lbl_employee_id.TabIndex = 4;
            lbl_employee_id.Text = "사번";
            lbl_employee_id.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // UC_Profile
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(UC_People);
            Margin = new Padding(5, 6, 5, 6);
            Name = "UC_Profile";
            Size = new Size(514, 600);
            UC_People.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PB_People).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel UC_People;
        private TableLayoutPanel tableLayoutPanel2;
        private PictureBox PB_People;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lbl_department;
        private Label lbl_name;
        private Label lbl_position;
        private Label lbl_employee_id;
    }
}
