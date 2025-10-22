namespace MES_Server
{
    partial class Server
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
            tableLayoutPanel_Btn = new TableLayoutPanel();
            btn_Connection = new Button();
            btn_Cancel = new Button();
            tableLayoutPanel_ListBox = new TableLayoutPanel();
            listBox_Event = new ListBox();
            tableLayoutPanel_Label1 = new TableLayoutPanel();
            lbl_inactive = new Label();
            pictureBox_Green = new PictureBox();
            pictureBox_Red = new PictureBox();
            lbl_active = new Label();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel_Btn.SuspendLayout();
            tableLayoutPanel_Label1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_Green).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_Red).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 514F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(776, 936);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel_Btn, 0, 5);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel_ListBox, 0, 3);
            tableLayoutPanel2.Controls.Add(listBox_Event, 0, 4);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel_Label1, 0, 2);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(131, 0);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 7;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 600F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(514, 936);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel_Btn
            // 
            tableLayoutPanel_Btn.ColumnCount = 2;
            tableLayoutPanel_Btn.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel_Btn.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel_Btn.Controls.Add(btn_Connection, 0, 0);
            tableLayoutPanel_Btn.Controls.Add(btn_Cancel, 1, 0);
            tableLayoutPanel_Btn.Location = new Point(0, 760);
            tableLayoutPanel_Btn.Margin = new Padding(0);
            tableLayoutPanel_Btn.Name = "tableLayoutPanel_Btn";
            tableLayoutPanel_Btn.RowCount = 1;
            tableLayoutPanel_Btn.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel_Btn.Size = new Size(514, 60);
            tableLayoutPanel_Btn.TabIndex = 0;
            // 
            // btn_Connection
            // 
            btn_Connection.Dock = DockStyle.Fill;
            btn_Connection.Location = new Point(0, 0);
            btn_Connection.Margin = new Padding(0);
            btn_Connection.Name = "btn_Connection";
            btn_Connection.Size = new Size(257, 60);
            btn_Connection.TabIndex = 0;
            btn_Connection.Text = "서버 연결";
            btn_Connection.UseVisualStyleBackColor = true;
            // 
            // btn_Cancel
            // 
            btn_Cancel.Dock = DockStyle.Fill;
            btn_Cancel.Location = new Point(257, 0);
            btn_Cancel.Margin = new Padding(0);
            btn_Cancel.Name = "btn_Cancel";
            btn_Cancel.Size = new Size(257, 60);
            btn_Cancel.TabIndex = 1;
            btn_Cancel.Text = "연결 종료";
            btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel_ListBox
            // 
            tableLayoutPanel_ListBox.ColumnCount = 1;
            tableLayoutPanel_ListBox.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel_ListBox.Dock = DockStyle.Fill;
            tableLayoutPanel_ListBox.Location = new Point(0, 120);
            tableLayoutPanel_ListBox.Margin = new Padding(0);
            tableLayoutPanel_ListBox.Name = "tableLayoutPanel_ListBox";
            tableLayoutPanel_ListBox.RowCount = 1;
            tableLayoutPanel_ListBox.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel_ListBox.Size = new Size(514, 40);
            tableLayoutPanel_ListBox.TabIndex = 1;
            // 
            // listBox_Event
            // 
            listBox_Event.Dock = DockStyle.Fill;
            listBox_Event.FormattingEnabled = true;
            listBox_Event.ItemHeight = 30;
            listBox_Event.Location = new Point(0, 160);
            listBox_Event.Margin = new Padding(0);
            listBox_Event.Name = "listBox_Event";
            listBox_Event.Size = new Size(514, 600);
            listBox_Event.TabIndex = 0;
            // 
            // tableLayoutPanel_Label1
            // 
            tableLayoutPanel_Label1.ColumnCount = 4;
            tableLayoutPanel_Label1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 51F));
            tableLayoutPanel_Label1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 137F));
            tableLayoutPanel_Label1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 51F));
            tableLayoutPanel_Label1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel_Label1.Controls.Add(lbl_inactive, 3, 0);
            tableLayoutPanel_Label1.Controls.Add(pictureBox_Green, 0, 0);
            tableLayoutPanel_Label1.Controls.Add(pictureBox_Red, 2, 0);
            tableLayoutPanel_Label1.Controls.Add(lbl_active, 1, 0);
            tableLayoutPanel_Label1.Dock = DockStyle.Fill;
            tableLayoutPanel_Label1.Location = new Point(0, 80);
            tableLayoutPanel_Label1.Margin = new Padding(0);
            tableLayoutPanel_Label1.Name = "tableLayoutPanel_Label1";
            tableLayoutPanel_Label1.RowCount = 1;
            tableLayoutPanel_Label1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel_Label1.Size = new Size(514, 40);
            tableLayoutPanel_Label1.TabIndex = 2;
            // 
            // lbl_inactive
            // 
            lbl_inactive.Location = new Point(239, 0);
            lbl_inactive.Margin = new Padding(0);
            lbl_inactive.Name = "lbl_inactive";
            lbl_inactive.Size = new Size(275, 40);
            lbl_inactive.TabIndex = 3;
            lbl_inactive.Text = "서버 비활성";
            lbl_inactive.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pictureBox_Green
            // 
            pictureBox_Green.Dock = DockStyle.Fill;
            pictureBox_Green.Image = Properties.Resources.Green;
            pictureBox_Green.Location = new Point(0, 0);
            pictureBox_Green.Margin = new Padding(0);
            pictureBox_Green.Name = "pictureBox_Green";
            pictureBox_Green.Size = new Size(51, 40);
            pictureBox_Green.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox_Green.TabIndex = 0;
            pictureBox_Green.TabStop = false;
            // 
            // pictureBox_Red
            // 
            pictureBox_Red.Dock = DockStyle.Fill;
            pictureBox_Red.Image = Properties.Resources.Red;
            pictureBox_Red.Location = new Point(188, 0);
            pictureBox_Red.Margin = new Padding(0);
            pictureBox_Red.Name = "pictureBox_Red";
            pictureBox_Red.Size = new Size(51, 40);
            pictureBox_Red.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox_Red.TabIndex = 1;
            pictureBox_Red.TabStop = false;
            // 
            // lbl_active
            // 
            lbl_active.Location = new Point(51, 0);
            lbl_active.Margin = new Padding(0);
            lbl_active.Name = "lbl_active";
            lbl_active.Size = new Size(137, 40);
            lbl_active.TabIndex = 2;
            lbl_active.Text = "서버 활성";
            lbl_active.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Server
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(776, 936);
            Controls.Add(tableLayoutPanel1);
            Name = "Server";
            Text = "MES_Server";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel_Btn.ResumeLayout(false);
            tableLayoutPanel_Label1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox_Green).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_Red).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel_Btn;
        private Button btn_Connection;
        private Button btn_Cancel;
        private TableLayoutPanel tableLayoutPanel_ListBox;
        private ListBox listBox_Event;
        private TableLayoutPanel tableLayoutPanel_Label1;
        private Label lbl_inactive;
        private PictureBox pictureBox_Green;
        private PictureBox pictureBox_Red;
        private Label lbl_active;
    }
}
