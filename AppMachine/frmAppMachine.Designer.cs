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
            this.tabMain = new System.Windows.Forms.TabPage();
            this.tabStation = new System.Windows.Forms.TabPage();
            this.tcSMReset = new System.Windows.Forms.TabControl();
            this.tcSMMain = new System.Windows.Forms.TabControl();
            this.tcSMStation = new System.Windows.Forms.TabControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabStateMachine.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabHomeRes.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabStation.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 88.72928F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.27072F));
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.32558F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.674418F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(905, 860);
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
            this.tabControl1.Size = new System.Drawing.Size(796, 788);
            this.tabControl1.TabIndex = 1;
            // 
            // tabProduction
            // 
            this.tabProduction.Location = new System.Drawing.Point(4, 22);
            this.tabProduction.Name = "tabProduction";
            this.tabProduction.Padding = new System.Windows.Forms.Padding(3);
            this.tabProduction.Size = new System.Drawing.Size(788, 762);
            this.tabProduction.TabIndex = 0;
            this.tabProduction.Text = "Production";
            this.tabProduction.UseVisualStyleBackColor = true;
            // 
            // tabAllSetup
            // 
            this.tabAllSetup.Location = new System.Drawing.Point(4, 22);
            this.tabAllSetup.Name = "tabAllSetup";
            this.tabAllSetup.Padding = new System.Windows.Forms.Padding(3);
            this.tabAllSetup.Size = new System.Drawing.Size(788, 762);
            this.tabAllSetup.TabIndex = 1;
            this.tabAllSetup.Text = "All Setup";
            this.tabAllSetup.UseVisualStyleBackColor = true;
            // 
            // tabCompnent
            // 
            this.tabCompnent.Location = new System.Drawing.Point(4, 22);
            this.tabCompnent.Name = "tabCompnent";
            this.tabCompnent.Padding = new System.Windows.Forms.Padding(3);
            this.tabCompnent.Size = new System.Drawing.Size(788, 762);
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
            this.tabStateMachine.Size = new System.Drawing.Size(788, 762);
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
            this.tabControl2.Size = new System.Drawing.Size(782, 756);
            this.tabControl2.TabIndex = 0;
            // 
            // tabHomeRes
            // 
            this.tabHomeRes.Controls.Add(this.tcSMReset);
            this.tabHomeRes.Location = new System.Drawing.Point(23, 4);
            this.tabHomeRes.Name = "tabHomeRes";
            this.tabHomeRes.Padding = new System.Windows.Forms.Padding(3);
            this.tabHomeRes.Size = new System.Drawing.Size(755, 748);
            this.tabHomeRes.TabIndex = 0;
            this.tabHomeRes.Text = "Home & Reset";
            this.tabHomeRes.UseVisualStyleBackColor = true;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tcSMMain);
            this.tabMain.Location = new System.Drawing.Point(23, 4);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabMain.Size = new System.Drawing.Size(755, 748);
            this.tabMain.TabIndex = 1;
            this.tabMain.Text = "Main";
            this.tabMain.UseVisualStyleBackColor = true;
            // 
            // tabStation
            // 
            this.tabStation.Controls.Add(this.tcSMStation);
            this.tabStation.Location = new System.Drawing.Point(23, 4);
            this.tabStation.Name = "tabStation";
            this.tabStation.Padding = new System.Windows.Forms.Padding(3);
            this.tabStation.Size = new System.Drawing.Size(755, 748);
            this.tabStation.TabIndex = 2;
            this.tabStation.Text = "Station";
            this.tabStation.UseVisualStyleBackColor = true;
            // 
            // tcSMReset
            // 
            this.tcSMReset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSMReset.Location = new System.Drawing.Point(3, 3);
            this.tcSMReset.Name = "tcSMReset";
            this.tcSMReset.SelectedIndex = 0;
            this.tcSMReset.Size = new System.Drawing.Size(749, 742);
            this.tcSMReset.TabIndex = 0;
            // 
            // tcSMMain
            // 
            this.tcSMMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSMMain.Location = new System.Drawing.Point(3, 3);
            this.tcSMMain.Name = "tcSMMain";
            this.tcSMMain.SelectedIndex = 0;
            this.tcSMMain.Size = new System.Drawing.Size(749, 742);
            this.tcSMMain.TabIndex = 0;
            // 
            // tcSMStation
            // 
            this.tcSMStation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSMStation.Location = new System.Drawing.Point(3, 3);
            this.tcSMStation.Name = "tcSMStation";
            this.tcSMStation.SelectedIndex = 0;
            this.tcSMStation.Size = new System.Drawing.Size(749, 742);
            this.tcSMStation.TabIndex = 0;
            // 
            // frmAppMachine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 860);
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
    }
}

