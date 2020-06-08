using System;
using System.Collections.Generic;
using System.Text;
using MPT.USN.CommonLib.Comp;
using System.Windows.Forms;
using System.Drawing;

#region SSafe History
/*
 * $History: FloatablePage.cs $
*******************Version 2********************
 * User:dhnhuy Date:2010-01-22 Time:02:49:10
 * Updated in:/USN/CommonLib
*******************Version 1********************
 * User:dhnhuy Date:2009-11-24 Time:08:24:39
 * Created in:/USN/CommonLib
 * 
 */
#endregion

namespace MPT.USN.CommonLib
{
    /// <summary>
    /// Base class for pages which can float itself
    /// </summary>
    public class FloatablePage : PageBase
    {
        #region Private
        private Control _dockParentControl;
        private Size _dockSize;
        /// <summary>
        /// Label for dock or floating text
        /// </summary>
        protected System.Windows.Forms.LinkLabel _lnkDockFloating;
        private Form _floatingForm;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public FloatablePage() : this(null) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="refObj"></param>
        public FloatablePage(object refObj) : base(refObj) 
        {
            InitializeComponent();
            _floatingForm = new Form();
            _floatingForm.ShowInTaskbar = false;
            _floatingForm.TopMost = true;
            // Handle the closing event to dock back
            _floatingForm.FormClosing += new FormClosingEventHandler(_floatingForm_FormClosing);
            _floatingForm.Hide();

        }

        void _floatingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DoDocking();

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Destroy
        /// </summary>
        public override void Destroy()
        {
            if (_floatingForm != null)
            {
                _floatingForm.FormClosing -= new FormClosingEventHandler(_floatingForm_FormClosing);
                _floatingForm.Dispose();
            }

            base.Destroy();
        }

        private void InitializeComponent()
        {
            this._lnkDockFloating = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // _lnkDockFloating
            // 
            this._lnkDockFloating.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._lnkDockFloating.AutoSize = true;
            this._lnkDockFloating.Location = new System.Drawing.Point(233, 3);
            this._lnkDockFloating.Name = "_lnkDockFloating";
            this._lnkDockFloating.Size = new System.Drawing.Size(44, 13);
            this._lnkDockFloating.TabIndex = 0;
            this._lnkDockFloating.TabStop = true;
            this._lnkDockFloating.Text = "Floating";
            this._lnkDockFloating.VisitedLinkColor = System.Drawing.Color.Blue;
            this._lnkDockFloating.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDockFloating_LinkClicked);
            // 
            // FloatablePage
            // 
            this.Controls.Add(this._lnkDockFloating);
            this.Name = "FloatablePage";
            this.SizeChanged += new System.EventHandler(this.FloatablePage_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void lnkDockFloating_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            if (_lnkDockFloating.Text == "Floating")
            {
                DoFloating();
            }
            else
            {
                DoDocking();
            }
        }

        private void DoDocking()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MPT.USN.CommonLib.MPTBase.DelegateParmVoid(DoDocking));
                return;
            }

            // If not currently belong to the floating form, do nothing
            if (this.Parent != _floatingForm)
            {
                return;
            }

            // Remove itself from the form
            _floatingForm.Controls.Remove(this);

            // Add back to the old parent
            this.Size = _dockSize;
            _dockParentControl.Controls.Add(this);

            // Hide the form
            _floatingForm.Hide();

            // Set the link text
            _lnkDockFloating.Text = "Floating";
        }

        /// <summary>
        /// Float itself
        /// </summary>
        private void DoFloating()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MPT.USN.CommonLib.MPTBase.DelegateParmVoid(DoFloating));
                return;
            }

            // If already float, do nothing
            if (this.Parent == _floatingForm)
            {
                return;
            }

            // Back up the parent and size
            _dockParentControl = this.Parent;
            _dockSize = this.Size;

            // Remove itself from current parent
            this.Parent.Controls.Remove(this);

            // Add to floating form and show
            _floatingForm.ClientSize = this.Size;
            _floatingForm.Controls.Add(this);
            _floatingForm.Show();

            // Set the link text
            _lnkDockFloating.Text = "Dock";

        }

        private void FloatablePage_SizeChanged(object sender, EventArgs e)
        {
            int newLabelLocationX = this.Size.Width - _lnkDockFloating.Size.Width - 3;
            int newLabelocationY = 3;

            _lnkDockFloating.Location = new Point(newLabelLocationX, newLabelocationY);
        }

    }
}
