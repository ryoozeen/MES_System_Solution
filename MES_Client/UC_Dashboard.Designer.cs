namespace MES_Client
{
    partial class UC_Dashboard
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
            components = new System.ComponentModel.Container();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            lblClock = new Label();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            tableLayoutPanel3 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            tableLayoutPanel5 = new TableLayoutPanel();
            label4 = new Label();
            lbl_product = new Label();
            label3 = new Label();
            tableLayoutPanel6 = new TableLayoutPanel();
            tableLayoutPanel7 = new TableLayoutPanel();
            lbl_Availability = new Label();
            label5 = new Label();
            label7 = new Label();
            tableLayoutPanel8 = new TableLayoutPanel();
            tableLayoutPanel9 = new TableLayoutPanel();
            lbl_faulty = new Label();
            label8 = new Label();
            label10 = new Label();
            tableLayoutPanel10 = new TableLayoutPanel();
            tableLayoutPanel11 = new TableLayoutPanel();
            tableLayoutPanel12 = new TableLayoutPanel();
            label14 = new Label();
            tableLayoutPanel16 = new TableLayoutPanel();
            tableLayoutPanel17 = new TableLayoutPanel();
            tableLayoutPanel21 = new TableLayoutPanel();
            label24 = new Label();
            label25 = new Label();
            label26 = new Label();
            tableLayoutPanel20 = new TableLayoutPanel();
            label20 = new Label();
            label21 = new Label();
            label22 = new Label();
            tableLayoutPanel19 = new TableLayoutPanel();
            label16 = new Label();
            label17 = new Label();
            label18 = new Label();
            tableLayoutPanel18 = new TableLayoutPanel();
            label23 = new Label();
            label19 = new Label();
            label15 = new Label();
            tableLayoutPanel13 = new TableLayoutPanel();
            clockTimer = new System.Windows.Forms.Timer(components);
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            tableLayoutPanel9.SuspendLayout();
            tableLayoutPanel11.SuspendLayout();
            tableLayoutPanel12.SuspendLayout();
            tableLayoutPanel17.SuspendLayout();
            tableLayoutPanel21.SuspendLayout();
            tableLayoutPanel20.SuspendLayout();
            tableLayoutPanel19.SuspendLayout();
            tableLayoutPanel18.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 1);
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(34, 40, 34, 40);
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10.217391F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 89.78261F));
            tableLayoutPanel1.Size = new Size(1426, 1046);
            tableLayoutPanel1.TabIndex = 0;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = Color.White;
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 86F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 480F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 34F));
            tableLayoutPanel2.Controls.Add(lblClock, 2, 0);
            tableLayoutPanel2.Controls.Add(pictureBox1, 0, 0);
            tableLayoutPanel2.Controls.Add(label1, 1, 0);
            tableLayoutPanel2.Location = new Point(34, 40);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(1347, 80);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // lblClock
            // 
            lblClock.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            lblClock.Location = new Point(867, 0);
            lblClock.Margin = new Padding(0);
            lblClock.Name = "lblClock";
            lblClock.Size = new Size(480, 80);
            lblClock.TabIndex = 2;
            lblClock.Text = "현재 날짜/시간";
            lblClock.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Dashboard;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Padding = new Padding(9, 10, 9, 10);
            pictureBox1.Size = new Size(86, 80);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label1.Location = new Point(86, 0);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Padding = new Padding(9, 10, 9, 10);
            label1.Size = new Size(781, 80);
            label1.TabIndex = 0;
            label1.Text = "대시보드";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(tableLayoutPanel4, 0, 0);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel11, 0, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(34, 138);
            tableLayoutPanel3.Margin = new Padding(0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(1358, 868);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 5;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 34F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 34F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel4.Controls.Add(tableLayoutPanel5, 0, 1);
            tableLayoutPanel4.Controls.Add(tableLayoutPanel7, 2, 1);
            tableLayoutPanel4.Controls.Add(tableLayoutPanel9, 4, 1);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(0, 0);
            tableLayoutPanel4.Margin = new Padding(0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 3;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel4.Size = new Size(1358, 434);
            tableLayoutPanel4.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.BackColor = Color.White;
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Controls.Add(label4, 0, 3);
            tableLayoutPanel5.Controls.Add(lbl_product, 0, 0);
            tableLayoutPanel5.Controls.Add(label3, 0, 2);
            tableLayoutPanel5.Controls.Add(tableLayoutPanel6, 0, 1);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(0, 40);
            tableLayoutPanel5.Margin = new Padding(0);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.Padding = new Padding(34, 40, 34, 40);
            tableLayoutPanel5.RowCount = 4;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 2F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Size = new Size(430, 354);
            tableLayoutPanel5.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label4.ForeColor = Color.Gray;
            label4.Location = new Point(34, 218);
            label4.Margin = new Padding(0);
            label4.Name = "label4";
            label4.Size = new Size(362, 96);
            label4.TabIndex = 2;
            label4.Text = "오늘 생산 수량";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lbl_product
            // 
            lbl_product.AutoSize = true;
            lbl_product.Dock = DockStyle.Fill;
            lbl_product.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
            lbl_product.Location = new Point(34, 40);
            lbl_product.Margin = new Padding(0);
            lbl_product.Name = "lbl_product";
            lbl_product.Size = new Size(362, 80);
            lbl_product.TabIndex = 0;
            lbl_product.Text = "생산량 ";
            lbl_product.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("맑은 고딕", 14F, FontStyle.Bold);
            label3.ForeColor = Color.Blue;
            label3.Location = new Point(34, 122);
            label3.Margin = new Padding(0);
            label3.Name = "label3";
            label3.Size = new Size(362, 96);
            label3.TabIndex = 1;
            label3.Text = "수량";
            label3.TextAlign = ContentAlignment.BottomLeft;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.BackColor = Color.Black;
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(34, 120);
            tableLayoutPanel6.Margin = new Padding(0);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.Size = new Size(362, 2);
            tableLayoutPanel6.TabIndex = 3;
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.BackColor = Color.White;
            tableLayoutPanel7.ColumnCount = 1;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.Controls.Add(lbl_Availability, 0, 0);
            tableLayoutPanel7.Controls.Add(label5, 0, 3);
            tableLayoutPanel7.Controls.Add(label7, 0, 2);
            tableLayoutPanel7.Controls.Add(tableLayoutPanel8, 0, 1);
            tableLayoutPanel7.Dock = DockStyle.Fill;
            tableLayoutPanel7.Location = new Point(464, 40);
            tableLayoutPanel7.Margin = new Padding(0);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.Padding = new Padding(34, 40, 34, 40);
            tableLayoutPanel7.RowCount = 4;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 2F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel7.Size = new Size(430, 354);
            tableLayoutPanel7.TabIndex = 1;
            // 
            // lbl_Availability
            // 
            lbl_Availability.AutoSize = true;
            lbl_Availability.Dock = DockStyle.Fill;
            lbl_Availability.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
            lbl_Availability.Location = new Point(34, 40);
            lbl_Availability.Margin = new Padding(0);
            lbl_Availability.Name = "lbl_Availability";
            lbl_Availability.Size = new Size(362, 80);
            lbl_Availability.TabIndex = 4;
            lbl_Availability.Text = "설비 가동률";
            lbl_Availability.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Dock = DockStyle.Fill;
            label5.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label5.ForeColor = Color.Gray;
            label5.Location = new Point(34, 218);
            label5.Margin = new Padding(0);
            label5.Name = "label5";
            label5.Size = new Size(362, 96);
            label5.TabIndex = 2;
            label5.Text = "전체 설비 총 가동률";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Dock = DockStyle.Fill;
            label7.Font = new Font("맑은 고딕", 14F, FontStyle.Bold);
            label7.ForeColor = Color.Blue;
            label7.Location = new Point(34, 122);
            label7.Margin = new Padding(0);
            label7.Name = "label7";
            label7.Size = new Size(362, 96);
            label7.TabIndex = 1;
            label7.Text = "%";
            label7.TextAlign = ContentAlignment.BottomLeft;
            // 
            // tableLayoutPanel8
            // 
            tableLayoutPanel8.BackColor = Color.Black;
            tableLayoutPanel8.ColumnCount = 1;
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.Dock = DockStyle.Fill;
            tableLayoutPanel8.Location = new Point(34, 120);
            tableLayoutPanel8.Margin = new Padding(0);
            tableLayoutPanel8.Name = "tableLayoutPanel8";
            tableLayoutPanel8.RowCount = 1;
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.Size = new Size(362, 2);
            tableLayoutPanel8.TabIndex = 3;
            // 
            // tableLayoutPanel9
            // 
            tableLayoutPanel9.BackColor = Color.White;
            tableLayoutPanel9.ColumnCount = 1;
            tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel9.Controls.Add(lbl_faulty, 0, 0);
            tableLayoutPanel9.Controls.Add(label8, 0, 3);
            tableLayoutPanel9.Controls.Add(label10, 0, 2);
            tableLayoutPanel9.Controls.Add(tableLayoutPanel10, 0, 1);
            tableLayoutPanel9.Dock = DockStyle.Fill;
            tableLayoutPanel9.Location = new Point(928, 40);
            tableLayoutPanel9.Margin = new Padding(0);
            tableLayoutPanel9.Name = "tableLayoutPanel9";
            tableLayoutPanel9.Padding = new Padding(34, 40, 34, 40);
            tableLayoutPanel9.RowCount = 4;
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Absolute, 2F));
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel9.Size = new Size(430, 354);
            tableLayoutPanel9.TabIndex = 2;
            // 
            // lbl_faulty
            // 
            lbl_faulty.AutoSize = true;
            lbl_faulty.Dock = DockStyle.Fill;
            lbl_faulty.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
            lbl_faulty.Location = new Point(34, 40);
            lbl_faulty.Margin = new Padding(0);
            lbl_faulty.Name = "lbl_faulty";
            lbl_faulty.Size = new Size(362, 80);
            lbl_faulty.TabIndex = 5;
            lbl_faulty.Text = "불량률";
            lbl_faulty.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Dock = DockStyle.Fill;
            label8.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label8.ForeColor = Color.Gray;
            label8.Location = new Point(34, 218);
            label8.Margin = new Padding(0);
            label8.Name = "label8";
            label8.Size = new Size(362, 96);
            label8.TabIndex = 2;
            label8.Text = "품질 불량 비율";
            label8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Dock = DockStyle.Fill;
            label10.Font = new Font("맑은 고딕", 14F, FontStyle.Bold);
            label10.ForeColor = Color.Blue;
            label10.Location = new Point(34, 122);
            label10.Margin = new Padding(0);
            label10.Name = "label10";
            label10.Size = new Size(362, 96);
            label10.TabIndex = 1;
            label10.Text = "%";
            label10.TextAlign = ContentAlignment.BottomLeft;
            // 
            // tableLayoutPanel10
            // 
            tableLayoutPanel10.BackColor = Color.Black;
            tableLayoutPanel10.ColumnCount = 1;
            tableLayoutPanel10.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel10.Dock = DockStyle.Fill;
            tableLayoutPanel10.Location = new Point(34, 120);
            tableLayoutPanel10.Margin = new Padding(0);
            tableLayoutPanel10.Name = "tableLayoutPanel10";
            tableLayoutPanel10.RowCount = 1;
            tableLayoutPanel10.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel10.Size = new Size(362, 2);
            tableLayoutPanel10.TabIndex = 3;
            // 
            // tableLayoutPanel11
            // 
            tableLayoutPanel11.ColumnCount = 2;
            tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65.90909F));
            tableLayoutPanel11.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34.0909081F));
            tableLayoutPanel11.Controls.Add(tableLayoutPanel12, 0, 0);
            tableLayoutPanel11.Controls.Add(tableLayoutPanel13, 1, 0);
            tableLayoutPanel11.Location = new Point(0, 434);
            tableLayoutPanel11.Margin = new Padding(0);
            tableLayoutPanel11.Name = "tableLayoutPanel11";
            tableLayoutPanel11.RowCount = 1;
            tableLayoutPanel11.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel11.Size = new Size(1358, 434);
            tableLayoutPanel11.TabIndex = 1;
            // 
            // tableLayoutPanel12
            // 
            tableLayoutPanel12.BackColor = Color.White;
            tableLayoutPanel12.ColumnCount = 1;
            tableLayoutPanel12.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel12.Controls.Add(label14, 0, 0);
            tableLayoutPanel12.Controls.Add(tableLayoutPanel16, 0, 1);
            tableLayoutPanel12.Controls.Add(tableLayoutPanel17, 0, 2);
            tableLayoutPanel12.Dock = DockStyle.Fill;
            tableLayoutPanel12.Location = new Point(0, 0);
            tableLayoutPanel12.Margin = new Padding(0);
            tableLayoutPanel12.Name = "tableLayoutPanel12";
            tableLayoutPanel12.Padding = new Padding(34, 40, 34, 40);
            tableLayoutPanel12.RowCount = 3;
            tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Absolute, 2F));
            tableLayoutPanel12.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel12.Size = new Size(895, 434);
            tableLayoutPanel12.TabIndex = 0;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Dock = DockStyle.Fill;
            label14.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
            label14.Location = new Point(34, 40);
            label14.Margin = new Padding(0);
            label14.Name = "label14";
            label14.Size = new Size(827, 80);
            label14.TabIndex = 1;
            label14.Text = "생산 라인 현황";
            label14.TextAlign = ContentAlignment.MiddleLeft;
            label14.Click += label14_Click;
            // 
            // tableLayoutPanel16
            // 
            tableLayoutPanel16.BackColor = Color.Black;
            tableLayoutPanel16.ColumnCount = 1;
            tableLayoutPanel16.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel16.Dock = DockStyle.Fill;
            tableLayoutPanel16.Location = new Point(34, 120);
            tableLayoutPanel16.Margin = new Padding(0);
            tableLayoutPanel16.Name = "tableLayoutPanel16";
            tableLayoutPanel16.RowCount = 1;
            tableLayoutPanel16.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel16.Size = new Size(827, 2);
            tableLayoutPanel16.TabIndex = 2;
            // 
            // tableLayoutPanel17
            // 
            tableLayoutPanel17.ColumnCount = 4;
            tableLayoutPanel17.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel17.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel17.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel17.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel17.Controls.Add(tableLayoutPanel21, 3, 0);
            tableLayoutPanel17.Controls.Add(tableLayoutPanel20, 2, 0);
            tableLayoutPanel17.Controls.Add(tableLayoutPanel19, 1, 0);
            tableLayoutPanel17.Controls.Add(tableLayoutPanel18, 0, 0);
            tableLayoutPanel17.Dock = DockStyle.Fill;
            tableLayoutPanel17.Location = new Point(34, 122);
            tableLayoutPanel17.Margin = new Padding(0);
            tableLayoutPanel17.Name = "tableLayoutPanel17";
            tableLayoutPanel17.RowCount = 1;
            tableLayoutPanel17.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel17.Size = new Size(827, 272);
            tableLayoutPanel17.TabIndex = 3;
            // 
            // tableLayoutPanel21
            // 
            tableLayoutPanel21.BackColor = Color.Gray;
            tableLayoutPanel21.ColumnCount = 1;
            tableLayoutPanel21.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel21.Controls.Add(label24, 0, 2);
            tableLayoutPanel21.Controls.Add(label25, 0, 1);
            tableLayoutPanel21.Controls.Add(label26, 0, 0);
            tableLayoutPanel21.Location = new Point(635, 20);
            tableLayoutPanel21.Margin = new Padding(17, 20, 17, 20);
            tableLayoutPanel21.Name = "tableLayoutPanel21";
            tableLayoutPanel21.Padding = new Padding(17, 20, 17, 20);
            tableLayoutPanel21.RowCount = 3;
            tableLayoutPanel21.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tableLayoutPanel21.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel21.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel21.Size = new Size(171, 232);
            tableLayoutPanel21.TabIndex = 3;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Dock = DockStyle.Fill;
            label24.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label24.ForeColor = Color.White;
            label24.Location = new Point(22, 156);
            label24.Margin = new Padding(5, 0, 5, 0);
            label24.Name = "label24";
            label24.Size = new Size(127, 56);
            label24.TabIndex = 2;
            label24.Text = "가동률 표시";
            label24.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Dock = DockStyle.Fill;
            label25.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label25.ForeColor = Color.White;
            label25.Location = new Point(22, 100);
            label25.Margin = new Padding(5, 0, 5, 0);
            label25.Name = "label25";
            label25.Size = new Size(127, 56);
            label25.TabIndex = 1;
            label25.Text = "상태 표시";
            label25.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.BackColor = Color.Gray;
            label26.Dock = DockStyle.Fill;
            label26.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            label26.ForeColor = Color.White;
            label26.Location = new Point(22, 20);
            label26.Margin = new Padding(5, 0, 5, 0);
            label26.Name = "label26";
            label26.Size = new Size(127, 80);
            label26.TabIndex = 0;
            label26.Text = "설비 #4";
            label26.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel20
            // 
            tableLayoutPanel20.BackColor = Color.Gray;
            tableLayoutPanel20.ColumnCount = 1;
            tableLayoutPanel20.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel20.Controls.Add(label20, 0, 2);
            tableLayoutPanel20.Controls.Add(label21, 0, 1);
            tableLayoutPanel20.Controls.Add(label22, 0, 0);
            tableLayoutPanel20.Location = new Point(429, 20);
            tableLayoutPanel20.Margin = new Padding(17, 20, 17, 20);
            tableLayoutPanel20.Name = "tableLayoutPanel20";
            tableLayoutPanel20.Padding = new Padding(17, 20, 17, 20);
            tableLayoutPanel20.RowCount = 3;
            tableLayoutPanel20.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tableLayoutPanel20.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel20.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel20.Size = new Size(171, 232);
            tableLayoutPanel20.TabIndex = 2;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Dock = DockStyle.Fill;
            label20.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label20.ForeColor = Color.White;
            label20.Location = new Point(22, 156);
            label20.Margin = new Padding(5, 0, 5, 0);
            label20.Name = "label20";
            label20.Size = new Size(127, 56);
            label20.TabIndex = 2;
            label20.Text = "가동률 표시";
            label20.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Dock = DockStyle.Fill;
            label21.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label21.ForeColor = Color.White;
            label21.Location = new Point(22, 100);
            label21.Margin = new Padding(5, 0, 5, 0);
            label21.Name = "label21";
            label21.Size = new Size(127, 56);
            label21.TabIndex = 1;
            label21.Text = "상태 표시";
            label21.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.BackColor = Color.Gray;
            label22.Dock = DockStyle.Fill;
            label22.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            label22.ForeColor = Color.White;
            label22.Location = new Point(22, 20);
            label22.Margin = new Padding(5, 0, 5, 0);
            label22.Name = "label22";
            label22.Size = new Size(127, 80);
            label22.TabIndex = 0;
            label22.Text = "설비 #3";
            label22.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel19
            // 
            tableLayoutPanel19.BackColor = Color.Gray;
            tableLayoutPanel19.ColumnCount = 1;
            tableLayoutPanel19.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel19.Controls.Add(label16, 0, 2);
            tableLayoutPanel19.Controls.Add(label17, 0, 1);
            tableLayoutPanel19.Controls.Add(label18, 0, 0);
            tableLayoutPanel19.Location = new Point(223, 20);
            tableLayoutPanel19.Margin = new Padding(17, 20, 17, 20);
            tableLayoutPanel19.Name = "tableLayoutPanel19";
            tableLayoutPanel19.Padding = new Padding(17, 20, 17, 20);
            tableLayoutPanel19.RowCount = 3;
            tableLayoutPanel19.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tableLayoutPanel19.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel19.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel19.Size = new Size(171, 232);
            tableLayoutPanel19.TabIndex = 1;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Dock = DockStyle.Fill;
            label16.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label16.ForeColor = Color.White;
            label16.Location = new Point(22, 156);
            label16.Margin = new Padding(5, 0, 5, 0);
            label16.Name = "label16";
            label16.Size = new Size(127, 56);
            label16.TabIndex = 2;
            label16.Text = "가동률 표시";
            label16.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Dock = DockStyle.Fill;
            label17.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label17.ForeColor = Color.White;
            label17.Location = new Point(22, 100);
            label17.Margin = new Padding(5, 0, 5, 0);
            label17.Name = "label17";
            label17.Size = new Size(127, 56);
            label17.TabIndex = 1;
            label17.Text = "상태 표시";
            label17.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.BackColor = Color.Gray;
            label18.Dock = DockStyle.Fill;
            label18.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            label18.ForeColor = Color.White;
            label18.Location = new Point(22, 20);
            label18.Margin = new Padding(5, 0, 5, 0);
            label18.Name = "label18";
            label18.Size = new Size(127, 80);
            label18.TabIndex = 0;
            label18.Text = "설비 #2";
            label18.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel18
            // 
            tableLayoutPanel18.BackColor = Color.Gray;
            tableLayoutPanel18.ColumnCount = 1;
            tableLayoutPanel18.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel18.Controls.Add(label23, 0, 2);
            tableLayoutPanel18.Controls.Add(label19, 0, 1);
            tableLayoutPanel18.Controls.Add(label15, 0, 0);
            tableLayoutPanel18.Location = new Point(17, 20);
            tableLayoutPanel18.Margin = new Padding(17, 20, 17, 20);
            tableLayoutPanel18.Name = "tableLayoutPanel18";
            tableLayoutPanel18.Padding = new Padding(17, 20, 17, 20);
            tableLayoutPanel18.RowCount = 3;
            tableLayoutPanel18.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tableLayoutPanel18.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel18.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel18.Size = new Size(171, 232);
            tableLayoutPanel18.TabIndex = 0;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Dock = DockStyle.Fill;
            label23.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label23.ForeColor = Color.White;
            label23.Location = new Point(22, 156);
            label23.Margin = new Padding(5, 0, 5, 0);
            label23.Name = "label23";
            label23.Size = new Size(127, 56);
            label23.TabIndex = 2;
            label23.Text = "가동률 표시";
            label23.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Dock = DockStyle.Fill;
            label19.Font = new Font("맑은 고딕", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label19.ForeColor = Color.White;
            label19.Location = new Point(22, 100);
            label19.Margin = new Padding(5, 0, 5, 0);
            label19.Name = "label19";
            label19.Size = new Size(127, 56);
            label19.TabIndex = 1;
            label19.Text = "상태 표시";
            label19.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.BackColor = Color.Gray;
            label15.Dock = DockStyle.Fill;
            label15.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            label15.ForeColor = Color.White;
            label15.Location = new Point(22, 20);
            label15.Margin = new Padding(5, 0, 5, 0);
            label15.Name = "label15";
            label15.Size = new Size(127, 80);
            label15.TabIndex = 0;
            label15.Text = "설비 #1";
            label15.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel13
            // 
            tableLayoutPanel13.ColumnCount = 2;
            tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.116105F));
            tableLayoutPanel13.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 92.8838959F));
            tableLayoutPanel13.Dock = DockStyle.Fill;
            tableLayoutPanel13.Location = new Point(895, 0);
            tableLayoutPanel13.Margin = new Padding(0);
            tableLayoutPanel13.Name = "tableLayoutPanel13";
            tableLayoutPanel13.RowCount = 1;
            tableLayoutPanel13.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel13.Size = new Size(463, 434);
            tableLayoutPanel13.TabIndex = 1;
            // 
            // clockTimer
            // 
            clockTimer.Enabled = true;
            clockTimer.Interval = 1000;
            clockTimer.Tick += clockTimer_Tick;
            // 
            // UC_Dashboard
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(5, 6, 5, 6);
            Name = "UC_Dashboard";
            Size = new Size(1426, 1046);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            tableLayoutPanel7.ResumeLayout(false);
            tableLayoutPanel7.PerformLayout();
            tableLayoutPanel9.ResumeLayout(false);
            tableLayoutPanel9.PerformLayout();
            tableLayoutPanel11.ResumeLayout(false);
            tableLayoutPanel12.ResumeLayout(false);
            tableLayoutPanel12.PerformLayout();
            tableLayoutPanel17.ResumeLayout(false);
            tableLayoutPanel21.ResumeLayout(false);
            tableLayoutPanel21.PerformLayout();
            tableLayoutPanel20.ResumeLayout(false);
            tableLayoutPanel20.PerformLayout();
            tableLayoutPanel19.ResumeLayout(false);
            tableLayoutPanel19.PerformLayout();
            tableLayoutPanel18.ResumeLayout(false);
            tableLayoutPanel18.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label1;
        private PictureBox pictureBox1;
        private Label lblClock;
        private System.Windows.Forms.Timer clockTimer;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel5;
        private Label label4;
        private Label label3;
        private Label lbl_product;
        private TableLayoutPanel tableLayoutPanel6;
        private TableLayoutPanel tableLayoutPanel7;
        private Label label5;
        private Label label7;
        private TableLayoutPanel tableLayoutPanel8;
        private TableLayoutPanel tableLayoutPanel9;
        private Label label8;
        private Label label10;
        private TableLayoutPanel tableLayoutPanel10;
        private TableLayoutPanel tableLayoutPanel11;
        private TableLayoutPanel tableLayoutPanel12;
        private Label label14;
        private Label lbl_Availability;
        private Label lbl_faulty;
        private TableLayoutPanel tableLayoutPanel16;
        private TableLayoutPanel tableLayoutPanel17;
        private TableLayoutPanel tableLayoutPanel18;
        private Label label19;
        private Label label15;
        private Label label23;
        private TableLayoutPanel tableLayoutPanel21;
        private Label label24;
        private Label label25;
        private Label label26;
        private TableLayoutPanel tableLayoutPanel20;
        private Label label20;
        private Label label21;
        private Label label22;
        private TableLayoutPanel tableLayoutPanel19;
        private Label label16;
        private Label label17;
        private Label label18;
        private TableLayoutPanel tableLayoutPanel13;
    }
}
