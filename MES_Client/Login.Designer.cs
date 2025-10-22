namespace MES_Client
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            lbl_logintitle = new Label();
            tableLayoutPanel4 = new TableLayoutPanel();
            tableLayoutPanel7 = new TableLayoutPanel();
            picBox_logo = new PictureBox();
            tableLayoutPanel5 = new TableLayoutPanel();
            txt_id = new TextBox();
            btn_login = new Button();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picBox_logo).BeginInit();
            tableLayoutPanel5.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(171, 160, 171, 160);
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(776, 936);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 0);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel4, 0, 1);
            tableLayoutPanel2.Location = new Point(176, 166);
            tableLayoutPanel2.Margin = new Padding(5, 6, 5, 6);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(424, 604);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(lbl_logintitle, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(0, 0);
            tableLayoutPanel3.Margin = new Padding(0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(424, 80);
            tableLayoutPanel3.TabIndex = 2;
            // 
            // lbl_logintitle
            // 
            lbl_logintitle.AutoSize = true;
            lbl_logintitle.Dock = DockStyle.Fill;
            lbl_logintitle.Font = new Font("맑은 고딕", 15F, FontStyle.Bold);
            lbl_logintitle.Location = new Point(0, 0);
            lbl_logintitle.Margin = new Padding(0);
            lbl_logintitle.Name = "lbl_logintitle";
            lbl_logintitle.Size = new Size(424, 80);
            lbl_logintitle.TabIndex = 0;
            lbl_logintitle.Text = "Login";
            lbl_logintitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(tableLayoutPanel7, 0, 0);
            tableLayoutPanel4.Controls.Add(tableLayoutPanel5, 0, 1);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(0, 80);
            tableLayoutPanel4.Margin = new Padding(0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Size = new Size(424, 524);
            tableLayoutPanel4.TabIndex = 5;
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.ColumnCount = 1;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel7.Controls.Add(picBox_logo, 0, 0);
            tableLayoutPanel7.Dock = DockStyle.Fill;
            tableLayoutPanel7.Location = new Point(0, 0);
            tableLayoutPanel7.Margin = new Padding(0);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.Padding = new Padding(51, 0, 51, 0);
            tableLayoutPanel7.RowCount = 1;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel7.Size = new Size(424, 262);
            tableLayoutPanel7.TabIndex = 3;
            // 
            // picBox_logo
            // 
            picBox_logo.Dock = DockStyle.Fill;
            picBox_logo.Image = Properties.Resources.MESlogo;
            picBox_logo.Location = new Point(51, 0);
            picBox_logo.Margin = new Padding(0);
            picBox_logo.Name = "picBox_logo";
            picBox_logo.Size = new Size(322, 262);
            picBox_logo.SizeMode = PictureBoxSizeMode.Zoom;
            picBox_logo.TabIndex = 0;
            picBox_logo.TabStop = false;
            picBox_logo.UseWaitCursor = true;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Controls.Add(txt_id, 0, 0);
            tableLayoutPanel5.Controls.Add(btn_login, 0, 2);
            tableLayoutPanel5.Location = new Point(0, 262);
            tableLayoutPanel5.Margin = new Padding(0);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 3;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Size = new Size(424, 121);
            tableLayoutPanel5.TabIndex = 1;
            // 
            // txt_id
            // 
            txt_id.Dock = DockStyle.Fill;
            txt_id.Font = new Font("맑은 고딕", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 129);
            txt_id.Location = new Point(0, 0);
            txt_id.Margin = new Padding(0);
            txt_id.Multiline = true;
            txt_id.Name = "txt_id";
            txt_id.PlaceholderText = "사번을 입력해주세요.";
            txt_id.Size = new Size(424, 50);
            txt_id.TabIndex = 0;
            txt_id.TextAlign = HorizontalAlignment.Center;
            // 
            // btn_login
            // 
            btn_login.BackColor = Color.LightSteelBlue;
            btn_login.Dock = DockStyle.Fill;
            btn_login.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
            btn_login.ForeColor = Color.Black;
            btn_login.Location = new Point(0, 70);
            btn_login.Margin = new Padding(0);
            btn_login.Name = "btn_login";
            btn_login.Size = new Size(424, 51);
            btn_login.TabIndex = 0;
            btn_login.Text = "로그인";
            btn_login.UseVisualStyleBackColor = false;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(776, 936);
            Controls.Add(tableLayoutPanel1);
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MES_Client";
            AcceptButton = btn_login;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picBox_logo).EndInit();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private Label lbl_logintitle;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel7;
        private PictureBox picBox_logo;
        private TableLayoutPanel tableLayoutPanel5;
        private TextBox txt_id;
        private Button btn_login;
    }
}