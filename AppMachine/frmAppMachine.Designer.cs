namespace B2229_AT_FuncCheck
{
    partial class frmAppMachine
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabProduction = new System.Windows.Forms.TabPage();
            this.tabAllSetup = new System.Windows.Forms.TabPage();
            this.tabCompnent = new System.Windows.Forms.TabPage();
            this.tabStateMachine = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabHomeRes = new System.Windows.Forms.TabPage();
            this.tcSMReset = new System.Windows.Forms.TabControl();
            this.tabMain = new System.Windows.Forms.TabPage();
            this.tcSMMain = new System.Windows.Forms.TabControl();
            this.tabStation = new System.Windows.Forms.TabPage();
            this.tcSMStation = new System.Windows.Forms.TabControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnHomeAll = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabStateMachine.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabHomeRes.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabStation.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 89.71519F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.28481F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tabControl3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.32558F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.674418F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1264, 749);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabProduction);
            this.tabControl1.Controls.Add(this.tabAllSetup);
            this.tabControl1.Controls.Add(this.tabCompnent);
            this.tabControl1.Controls.Add(this.tabStateMachine);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1127, 685);
            this.tabControl1.TabIndex = 1;
            // 
            // tabProduction
            // 
            this.tabProduction.Location = new System.Drawing.Point(4, 22);
            this.tabProduction.Name = "tabProduction";
            this.tabProduction.Padding = new System.Windows.Forms.Padding(3);
            this.tabProduction.Size = new System.Drawing.Size(1119, 659);
            this.tabProduction.TabIndex = 0;
            this.tabProduction.Text = "Production";
            this.tabProduction.UseVisualStyleBackColor = true;
            // 
            // tabAllSetup
            // 
            this.tabAllSetup.Location = new System.Drawing.Point(4, 22);
            this.tabAllSetup.Name = "tabAllSetup";
            this.tabAllSetup.Padding = new System.Windows.Forms.Padding(3);
            this.tabAllSetup.Size = new System.Drawing.Size(1119, 659);
            this.tabAllSetup.TabIndex = 1;
            this.tabAllSetup.Text = "All Setup";
            this.tabAllSetup.UseVisualStyleBackColor = true;
            // 
            // tabCompnent
            // 
            this.tabCompnent.Location = new System.Drawing.Point(4, 22);
            this.tabCompnent.Name = "tabCompnent";
            this.tabCompnent.Padding = new System.Windows.Forms.Padding(3);
            this.tabCompnent.Size = new System.Drawing.Size(1119, 659);
            this.tabCompnent.TabIndex = 2;
            this.tabCompnent.Text = "Component";
            this.tabCompnent.UseVisualStyleBackColor = true;
            // 
            // tabStateMachine
            // 
            this.tabStateMachine.Controls.Add(this.tabControl2);
            this.tabStateMachine.Location = new System.Drawing.Point(4, 22);
            this.tabStateMachine.Name = "tabStateMachine";
            this.tabStateMachine.Padding = new System.Windows.Forms.Padding(3);
            this.tabStateMachine.Size = new System.Drawing.Size(1119, 659);
            this.tabStateMachine.TabIndex = 3;
            this.tabStateMachine.Text = "State Machine";
            this.tabStateMachine.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl2.Controls.Add(this.tabHomeRes);
            this.tabControl2.Controls.Add(this.tabMain);
            this.tabControl2.Controls.Add(this.tabStation);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(3, 3);
            this.tabControl2.Multiline = true;
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1113, 653);
            this.tabControl2.TabIndex = 0;
            // 
            // tabHomeRes
            // 
            this.tabHomeRes.Controls.Add(this.tcSMReset);
            this.tabHomeRes.Location = new System.Drawing.Point(23, 4);
            this.tabHomeRes.Name = "tabHomeRes";
            this.tabHomeRes.Padding = new System.Windows.Forms.Padding(3);
            this.tabHomeRes.Size = new System.Drawing.Size(1086, 645);
            this.tabHomeRes.TabIndex = 0;
            this.tabHomeRes.Text = "Home & Reset";
            this.tabHomeRes.UseVisualStyleBackColor = true;
            // 
            // tcSMReset
            // 
            this.tcSMReset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSMReset.Location = new System.Drawing.Point(3, 3);
            this.tcSMReset.Name = "tcSMReset";
            this.tcSMReset.SelectedIndex = 0;
            this.tcSMReset.Size = new System.Drawing.Size(1080, 639);
            this.tcSMReset.TabIndex = 0;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tcSMMain);
            this.tabMain.Location = new System.Drawing.Point(23, 4);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabMain.Size = new System.Drawing.Size(1086, 645);
            this.tabMain.TabIndex = 1;
            this.tabMain.Text = "Main";
            this.tabMain.UseVisualStyleBackColor = true;
            // 
            // tcSMMain
            // 
            this.tcSMMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSMMain.Location = new System.Drawing.Point(3, 3);
            this.tcSMMain.Name = "tcSMMain";
            this.tcSMMain.SelectedIndex = 0;
            this.tcSMMain.Size = new System.Drawing.Size(1080, 639);
            this.tcSMMain.TabIndex = 0;
            // 
            // tabStation
            // 
            this.tabStation.Controls.Add(this.tcSMStation);
            this.tabStation.Location = new System.Drawing.Point(23, 4);
            this.tabStation.Name = "tabStation";
            this.tabStation.Padding = new System.Windows.Forms.Padding(3);
            this.tabStation.Size = new System.Drawing.Size(1086, 645);
            this.tabStation.TabIndex = 2;
            this.tabStation.Text = "Station";
            this.tabStation.UseVisualStyleBackColor = true;
            // 
            // tcSMStation
            // 
            this.tcSMStation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSMStation.Location = new System.Drawing.Point(3, 3);
            this.tcSMStation.Name = "tcSMStation";
            this.tcSMStation.SelectedIndex = 0;
            this.tcSMStation.Size = new System.Drawing.Size(1080, 639);
            this.tcSMStation.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnHomeAll);
            this.panel1.Controls.Add(this.btnPause);
            this.panel1.Controls.Add(this.btnRun);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 694);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1127, 52);
            this.panel1.TabIndex = 3;
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage1);
            this.tabControl3.Controls.Add(this.tabPage2);
            this.tabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl3.Location = new System.Drawing.Point(1136, 3);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(125, 685);
            this.tabControl3.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(117, 659);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Controller";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(117, 659);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Monitor";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnApply);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(1136, 694);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(125, 52);
            this.panel2.TabIndex = 5;
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.Location = new System.Drawing.Point(-4, 7);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(132, 39);
            this.btnApply.TabIndex = 15;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnHomeAll
            // 
            this.btnHomeAll.BackColor = System.Drawing.Color.Khaki;
            this.btnHomeAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHomeAll.Location = new System.Drawing.Point(773, 6);
            this.btnHomeAll.Name = "btnHomeAll";
            this.btnHomeAll.Size = new System.Drawing.Size(132, 39);
            this.btnHomeAll.TabIndex = 16;
            this.btnHomeAll.Text = "Reset All";
            this.btnHomeAll.UseVisualStyleBackColor = false;
            // 
            // btnPause
            // 
            this.btnPause.BackColor = System.Drawing.Color.RosyBrown;
            this.btnPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPause.Location = new System.Drawing.Point(359, 6);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(132, 40);
            this.btnPause.TabIndex = 15;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnRun
            // 
            this.btnRun.BackColor = System.Drawing.Color.PaleGreen;
            this.btnRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRun.Location = new System.Drawing.Point(221, 6);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(132, 40);
            this.btnRun.TabIndex = 14;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = false;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // frmAppMachine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 749);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmAppMachine";
            this.Text = "appMachine";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAppMachine_FormClosing);
            this.Load += new System.EventHandler(this.frmAppMachine_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabStateMachine.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabHomeRes.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabStation.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabProduction;
        private System.Windows.Forms.TabPage tabAllSetup;
        private System.Windows.Forms.TabPage tabCompnent;
        private System.Windows.Forms.TabPage tabStateMachine;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabHomeRes;
        private System.Windows.Forms.TabPage tabMain;
        private System.Windows.Forms.TabPage tabStation;
        private System.Windows.Forms.TabControl tcSMReset;
        private System.Windows.Forms.TabControl tcSMMain;
        private System.Windows.Forms.TabControl tcSMStation;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnHomeAll;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnRun;
    }
}

