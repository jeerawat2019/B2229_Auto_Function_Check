namespace B2229_AT_FuncCheck.AppResult.AppConsignePart
{
    partial class AppPartJigView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblPartId = new X_Core.ControlElement.LabelCtl();
            this.lblResult = new X_Core.ControlElement.LabelCtl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblEmpty = new X_Core.ControlElement.LabelCtl();
            this.lblFinnish = new X_Core.ControlElement.LabelCtl();
            this.lblRun = new X_Core.ControlElement.LabelCtl();
            this.SuspendLayout();
            // 
            // lblPartId
            // 
            this.lblPartId.AutoSize = true;
            this.lblPartId.BackColor = System.Drawing.Color.Yellow;
            this.lblPartId.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPartId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartId.Location = new System.Drawing.Point(0, 0);
            this.lblPartId.Name = "lblPartId";
            this.lblPartId.Size = new System.Drawing.Size(21, 13);
            this.lblPartId.TabIndex = 0;
            this.lblPartId.Text = "99";
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.BackColor = System.Drawing.Color.Yellow;
            this.lblResult.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblResult.Location = new System.Drawing.Point(0, 52);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(27, 13);
            this.lblResult.TabIndex = 4;
            this.lblResult.Text = "N/A";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(19, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(10, 12);
            this.panel1.TabIndex = 5;
            // 
            // lblEmpty
            // 
            this.lblEmpty.AutoSize = true;
            this.lblEmpty.BackColor = System.Drawing.Color.Yellow;
            this.lblEmpty.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEmpty.Location = new System.Drawing.Point(0, 13);
            this.lblEmpty.Name = "lblEmpty";
            this.lblEmpty.Size = new System.Drawing.Size(27, 13);
            this.lblEmpty.TabIndex = 1;
            this.lblEmpty.Text = "N/A";
            // 
            // lblFinnish
            // 
            this.lblFinnish.AutoSize = true;
            this.lblFinnish.BackColor = System.Drawing.Color.Yellow;
            this.lblFinnish.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFinnish.Location = new System.Drawing.Point(0, 39);
            this.lblFinnish.Name = "lblFinnish";
            this.lblFinnish.Size = new System.Drawing.Size(27, 13);
            this.lblFinnish.TabIndex = 3;
            this.lblFinnish.Text = "N/A";
            // 
            // lblRun
            // 
            this.lblRun.AutoSize = true;
            this.lblRun.BackColor = System.Drawing.Color.Yellow;
            this.lblRun.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRun.Location = new System.Drawing.Point(0, 26);
            this.lblRun.Name = "lblRun";
            this.lblRun.Size = new System.Drawing.Size(27, 13);
            this.lblRun.TabIndex = 2;
            this.lblRun.Text = "N/A";
            // 
            // AppPartJigView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.lblFinnish);
            this.Controls.Add(this.lblRun);
            this.Controls.Add(this.lblEmpty);
            this.Controls.Add(this.lblPartId);
            this.Name = "AppPartJigView";
            this.Size = new System.Drawing.Size(25, 66);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private X_Core.ControlElement.LabelCtl lblPartId;
        private X_Core.ControlElement.LabelCtl lblResult;
        private System.Windows.Forms.Panel panel1;
        private X_Core.ControlElement.LabelCtl lblEmpty;
        private X_Core.ControlElement.LabelCtl lblFinnish;
        private X_Core.ControlElement.LabelCtl lblRun;
    }
}
