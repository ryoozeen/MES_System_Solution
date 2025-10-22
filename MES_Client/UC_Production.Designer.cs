namespace MES_Client
{
    partial class UC_Production
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
            label2 = new Label();
            tableLayoutPanel5 = new TableLayoutPanel();
            tableLayoutPanel6 = new TableLayoutPanel();
            label32 = new Label();
            label31 = new Label();
            label30 = new Label();
            label29 = new Label();
            label28 = new Label();
            label26 = new Label();
            label25 = new Label();
            label24 = new Label();
            label23 = new Label();
            label22 = new Label();
            label20 = new Label();
            label19 = new Label();
            label18 = new Label();
            label17 = new Label();
            label16 = new Label();
            label14 = new Label();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            clockTimer = new System.Windows.Forms.Timer(components);
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
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
            tableLayoutPanel1.Padding = new Padding(20);
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10.217391F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 89.78261F));
            tableLayoutPanel1.Size = new Size(832, 523);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = Color.White;
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 280F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Controls.Add(lblClock, 2, 0);
            tableLayoutPanel2.Controls.Add(pictureBox1, 0, 0);
            tableLayoutPanel2.Controls.Add(label1, 1, 0);
            tableLayoutPanel2.Location = new Point(20, 20);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(792, 40);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // lblClock
            // 
            lblClock.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            lblClock.Location = new Point(512, 0);
            lblClock.Margin = new Padding(0);
            lblClock.Name = "lblClock";
            lblClock.Size = new Size(280, 40);
            lblClock.TabIndex = 2;
            lblClock.Text = "현재 날짜/시간";
            lblClock.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pictureBox1
            //

            pictureBox1.Margin = new Padding(0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Padding = new Padding(5);
            pictureBox1.Size = new Size(50, 40);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label1.Location = new Point(50, 0);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Padding = new Padding(5);
            label1.Size = new Size(462, 40);
            label1.TabIndex = 0;
            label1.Text = "생산 및 공정 관리";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(tableLayoutPanel4, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(20, 69);
            tableLayoutPanel3.Margin = new Padding(0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(792, 434);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.BackColor = Color.White;
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(label2, 0, 0);
            tableLayoutPanel4.Controls.Add(tableLayoutPanel5, 0, 1);
            tableLayoutPanel4.Controls.Add(tableLayoutPanel6, 0, 2);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(0, 0);
            tableLayoutPanel4.Margin = new Padding(0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.Padding = new Padding(20);
            tableLayoutPanel4.RowCount = 3;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 39F));
            tableLayoutPanel4.Size = new Size(792, 434);
            tableLayoutPanel4.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
            label2.Location = new Point(20, 20);
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Size = new Size(752, 40);
            label2.TabIndex = 1;
            label2.Text = "작업지시서";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.BackColor = Color.Black;
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(20, 60);
            tableLayoutPanel5.Margin = new Padding(0);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Size = new Size(752, 1);
            tableLayoutPanel5.TabIndex = 2;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.BackColor = Color.FromArgb(224, 224, 224);
            tableLayoutPanel6.ColumnCount = 11;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66667F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 5F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666622F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 5F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 5F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 5F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 5F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66667F));
            tableLayoutPanel6.Controls.Add(label32, 10, 4);
            tableLayoutPanel6.Controls.Add(label31, 8, 4);
            tableLayoutPanel6.Controls.Add(label30, 6, 4);
            tableLayoutPanel6.Controls.Add(label29, 4, 4);
            tableLayoutPanel6.Controls.Add(label28, 2, 4);
            tableLayoutPanel6.Controls.Add(label26, 10, 3);
            tableLayoutPanel6.Controls.Add(label25, 8, 3);
            tableLayoutPanel6.Controls.Add(label24, 6, 3);
            tableLayoutPanel6.Controls.Add(label23, 4, 3);
            tableLayoutPanel6.Controls.Add(label22, 2, 3);
            tableLayoutPanel6.Controls.Add(label20, 10, 2);
            tableLayoutPanel6.Controls.Add(label19, 8, 2);
            tableLayoutPanel6.Controls.Add(label18, 6, 2);
            tableLayoutPanel6.Controls.Add(label17, 4, 2);
            tableLayoutPanel6.Controls.Add(label16, 2, 2);
            tableLayoutPanel6.Controls.Add(label14, 10, 1);
            tableLayoutPanel6.Controls.Add(label13, 8, 1);
            tableLayoutPanel6.Controls.Add(label12, 6, 1);
            tableLayoutPanel6.Controls.Add(label11, 4, 1);
            tableLayoutPanel6.Controls.Add(label10, 2, 1);
            tableLayoutPanel6.Controls.Add(label8, 10, 0);
            tableLayoutPanel6.Controls.Add(label7, 8, 0);
            tableLayoutPanel6.Controls.Add(label6, 6, 0);
            tableLayoutPanel6.Controls.Add(label5, 4, 0);
            tableLayoutPanel6.Controls.Add(label4, 2, 0);
            tableLayoutPanel6.Controls.Add(label3, 0, 0);
            tableLayoutPanel6.Controls.Add(button1, 0, 1);
            tableLayoutPanel6.Controls.Add(button2, 0, 2);
            tableLayoutPanel6.Controls.Add(button3, 0, 3);
            tableLayoutPanel6.Controls.Add(button4, 0, 4);
            tableLayoutPanel6.Location = new Point(20, 81);
            tableLayoutPanel6.Margin = new Padding(0, 20, 0, 0);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 5;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel6.Size = new Size(752, 333);
            tableLayoutPanel6.TabIndex = 3;
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Dock = DockStyle.Fill;
            label32.Location = new Point(630, 264);
            label32.Margin = new Padding(0);
            label32.Name = "label32";
            label32.Size = new Size(122, 69);
            label32.TabIndex = 29;
            label32.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Dock = DockStyle.Fill;
            label31.Location = new Point(504, 264);
            label31.Margin = new Padding(0);
            label31.Name = "label31";
            label31.Size = new Size(121, 69);
            label31.TabIndex = 28;
            label31.Text = "작업자명";
            label31.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Dock = DockStyle.Fill;
            label30.Location = new Point(378, 264);
            label30.Margin = new Padding(0);
            label30.Name = "label30";
            label30.Size = new Size(121, 69);
            label30.TabIndex = 27;
            label30.Text = "상태";
            label30.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Dock = DockStyle.Fill;
            label29.Location = new Point(252, 264);
            label29.Margin = new Padding(0);
            label29.Name = "label29";
            label29.Size = new Size(121, 69);
            label29.TabIndex = 26;
            label29.Text = "0";
            label29.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Dock = DockStyle.Fill;
            label28.Location = new Point(126, 264);
            label28.Margin = new Padding(0);
            label28.Name = "label28";
            label28.Size = new Size(121, 69);
            label28.TabIndex = 25;
            label28.Text = "프레스기";
            label28.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Dock = DockStyle.Fill;
            label26.Location = new Point(630, 198);
            label26.Margin = new Padding(0);
            label26.Name = "label26";
            label26.Size = new Size(122, 66);
            label26.TabIndex = 23;
            label26.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Dock = DockStyle.Fill;
            label25.Location = new Point(504, 198);
            label25.Margin = new Padding(0);
            label25.Name = "label25";
            label25.Size = new Size(121, 66);
            label25.TabIndex = 22;
            label25.Text = "작업자명";
            label25.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Dock = DockStyle.Fill;
            label24.Location = new Point(378, 198);
            label24.Margin = new Padding(0);
            label24.Name = "label24";
            label24.Size = new Size(121, 66);
            label24.TabIndex = 21;
            label24.Text = "상태";
            label24.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Dock = DockStyle.Fill;
            label23.Location = new Point(252, 198);
            label23.Margin = new Padding(0);
            label23.Name = "label23";
            label23.Size = new Size(121, 66);
            label23.TabIndex = 20;
            label23.Text = "0";
            label23.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Dock = DockStyle.Fill;
            label22.Location = new Point(126, 198);
            label22.Margin = new Padding(0);
            label22.Name = "label22";
            label22.Size = new Size(121, 66);
            label22.TabIndex = 19;
            label22.Text = "프레스기";
            label22.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Dock = DockStyle.Fill;
            label20.Location = new Point(630, 132);
            label20.Margin = new Padding(0);
            label20.Name = "label20";
            label20.Size = new Size(122, 66);
            label20.TabIndex = 17;
            label20.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Dock = DockStyle.Fill;
            label19.Location = new Point(504, 132);
            label19.Margin = new Padding(0);
            label19.Name = "label19";
            label19.Size = new Size(121, 66);
            label19.TabIndex = 16;
            label19.Text = "작업자명";
            label19.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Dock = DockStyle.Fill;
            label18.Location = new Point(378, 132);
            label18.Margin = new Padding(0);
            label18.Name = "label18";
            label18.Size = new Size(121, 66);
            label18.TabIndex = 15;
            label18.Text = "상태";
            label18.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Dock = DockStyle.Fill;
            label17.Location = new Point(252, 132);
            label17.Margin = new Padding(0);
            label17.Name = "label17";
            label17.Size = new Size(121, 66);
            label17.TabIndex = 14;
            label17.Text = "0";
            label17.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Dock = DockStyle.Fill;
            label16.Location = new Point(126, 132);
            label16.Margin = new Padding(0);
            label16.Name = "label16";
            label16.Size = new Size(121, 66);
            label16.TabIndex = 13;
            label16.Text = "조립 로봇";
            label16.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Dock = DockStyle.Fill;
            label14.Location = new Point(630, 66);
            label14.Margin = new Padding(0);
            label14.Name = "label14";
            label14.Size = new Size(122, 66);
            label14.TabIndex = 11;
            label14.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Dock = DockStyle.Fill;
            label13.Location = new Point(504, 66);
            label13.Margin = new Padding(0);
            label13.Name = "label13";
            label13.Size = new Size(121, 66);
            label13.TabIndex = 10;
            label13.Text = "작업자명";
            label13.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Dock = DockStyle.Fill;
            label12.Location = new Point(378, 66);
            label12.Margin = new Padding(0);
            label12.Name = "label12";
            label12.Size = new Size(121, 66);
            label12.TabIndex = 9;
            label12.Text = "상태";
            label12.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Dock = DockStyle.Fill;
            label11.Location = new Point(252, 66);
            label11.Margin = new Padding(0);
            label11.Name = "label11";
            label11.Size = new Size(121, 66);
            label11.TabIndex = 8;
            label11.Text = "0";
            label11.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Dock = DockStyle.Fill;
            label10.Location = new Point(126, 66);
            label10.Margin = new Padding(0);
            label10.Name = "label10";
            label10.Size = new Size(121, 66);
            label10.TabIndex = 7;
            label10.Text = "CNC 가공기";
            label10.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = SystemColors.ActiveCaption;
            label8.Dock = DockStyle.Fill;
            label8.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
            label8.Location = new Point(630, 0);
            label8.Margin = new Padding(0);
            label8.Name = "label8";
            label8.Size = new Size(122, 66);
            label8.TabIndex = 5;
            label8.Text = "기타";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = SystemColors.ActiveCaption;
            label7.Dock = DockStyle.Fill;
            label7.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
            label7.Location = new Point(504, 0);
            label7.Margin = new Padding(0);
            label7.Name = "label7";
            label7.Size = new Size(121, 66);
            label7.TabIndex = 4;
            label7.Text = "작업자";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = SystemColors.ActiveCaption;
            label6.Dock = DockStyle.Fill;
            label6.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
            label6.Location = new Point(378, 0);
            label6.Margin = new Padding(0);
            label6.Name = "label6";
            label6.Size = new Size(121, 66);
            label6.TabIndex = 3;
            label6.Text = "진행상태";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.ActiveCaption;
            label5.Dock = DockStyle.Fill;
            label5.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
            label5.Location = new Point(252, 0);
            label5.Margin = new Padding(0);
            label5.Name = "label5";
            label5.Size = new Size(121, 66);
            label5.TabIndex = 2;
            label5.Text = "수량";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.ActiveCaption;
            label4.Dock = DockStyle.Fill;
            label4.FlatStyle = FlatStyle.Flat;
            label4.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
            label4.Location = new Point(126, 0);
            label4.Margin = new Padding(0);
            label4.Name = "label4";
            label4.Size = new Size(121, 66);
            label4.TabIndex = 1;
            label4.Text = "설비명";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.ActiveCaption;
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
            label3.Location = new Point(0, 0);
            label3.Margin = new Padding(0);
            label3.Name = "label3";
            label3.Size = new Size(121, 66);
            label3.TabIndex = 0;
            label3.Text = "설비 ID";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            button1.BackColor = Color.White;
            button1.Dock = DockStyle.Fill;
            button1.Location = new Point(0, 66);
            button1.Margin = new Padding(0);
            button1.Name = "button1";
            button1.Size = new Size(121, 66);
            button1.TabIndex = 30;
            button1.Text = "설비 #1";
            button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = Color.White;
            button2.Dock = DockStyle.Fill;
            button2.Location = new Point(0, 132);
            button2.Margin = new Padding(0);
            button2.Name = "button2";
            button2.Size = new Size(121, 66);
            button2.TabIndex = 31;
            button2.Text = "설비 #2";
            button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.BackColor = Color.White;
            button3.Dock = DockStyle.Fill;
            button3.Location = new Point(0, 198);
            button3.Margin = new Padding(0);
            button3.Name = "button3";
            button3.Size = new Size(121, 66);
            button3.TabIndex = 32;
            button3.Text = "설비 #3";
            button3.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            button4.BackColor = Color.White;
            button4.Dock = DockStyle.Fill;
            button4.Location = new Point(0, 264);
            button4.Margin = new Padding(0);
            button4.Name = "button4";
            button4.Size = new Size(121, 69);
            button4.TabIndex = 33;
            button4.Text = "설비 #4";
            button4.UseVisualStyleBackColor = false;
            // 
            // clockTimer
            // 
            clockTimer.Enabled = true;
            clockTimer.Interval = 1000;
            clockTimer.Tick += clockTimer_Tick;
            // 
            // UC_Production
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "UC_Production";
            Size = new Size(832, 523);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label lblClock;
        private PictureBox pictureBox1;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Timer clockTimer;
        private TableLayoutPanel tableLayoutPanel4;
        private Label label2;
        private TableLayoutPanel tableLayoutPanel5;
        private TableLayoutPanel tableLayoutPanel6;
        private Label label32;
        private Label label31;
        private Label label30;
        private Label label29;
        private Label label28;
        private Label label26;
        private Label label25;
        private Label label24;
        private Label label23;
        private Label label22;
        private Label label20;
        private Label label19;
        private Label label18;
        private Label label17;
        private Label label16;
        private Label label14;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}
