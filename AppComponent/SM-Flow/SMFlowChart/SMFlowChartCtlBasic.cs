using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using X_Core.Comp.SMLib.Flow;
using X_Core.ControlElement;

namespace X_Core.Comp.SMLib.SMFlowChart
{
    public partial class SMFlowChartCtlBasic : SMFlowChartCtlBase, IComponentBinding<SMStateMachine>
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public SMFlowChartCtlBasic()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Bind to the State Machine
        /// </summary>
        /// <param name="axis"></param>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SMStateMachine Bind
        {
            get { return base._stateMachine; }
            set
            {
                base._stateMachine = value;
                _stateMachine.RegisterFlowPanel(this);
                UpdateAutoScope();
                X_CoreS.RegisterOnChanged(() => _stateMachine.IsRunning, OnChangedIsRunning);
            }
        }

        private void OnChangedIsRunning(bool isRunning)
        {
            btnRun.BackColor = isRunning ? Color.LightGreen : SystemColors.Control;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _stateMachine != null)
            {
                //if (_currentContainerPanel != null)
                //{
                //    _currentContainerPanel.EditMode = false;
                //}
                _stateMachine.UnregisterFlowPanel(this);
            }
            base.Dispose(disposing);
        }


        private SMFlowContainer _currentFlowContainer = null;
        private SMContainerPanel _currentContainerPanel = null;
       /// <summary>
        /// Remove this subroutine panel
        /// </summary>
        /// <param name="subroutine"></param>
        public void RemoveFlowContainer(SMFlowContainer flowContainer)
        {
            if (flowContainer != null && Controls.ContainsKey(flowContainer.Nickname))
            {
                SMContainerPanel containerPanel = Controls[flowContainer.Nickname] as SMContainerPanel;
                Controls.Remove(containerPanel);
                containerPanel.Dispose();
            }
        }
        /// <summary>
        /// Show this panel and create or show the subroutine panel
        /// </summary>
        /// <param name="subroutine"></param>
        public void ShowFlowContainer(SMFlowContainer flowContainer)
        {
            if (_currentContainerPanel != null)
            {
                if (_currentContainerPanel.Name == flowContainer.Nickname)
                    return;
                _currentContainerPanel.Hide();
            }
            if (!Controls.ContainsKey(flowContainer.Nickname))
            {
                // Constructor will rebuild
                SMContainerPanel subPanel = new SMContainerPanel(this, flowContainer, lblHeader.Size.Height);
                this.Controls.Add(subPanel);
            }
            _currentContainerPanel = this.Controls[flowContainer.Nickname] as SMContainerPanel;
            _currentContainerPanel.Show();
            _currentFlowContainer = flowContainer;
            lblHeader.Text = _currentFlowContainer.PathText;
            tbSubroutineName.Text = _currentFlowContainer.Text;
            UpdateAutoScope();

            tbSubroutineName.Hide();
            if (_currentFlowContainer is SMStateMachine)
            {
                if (_currentFlowContainer.IsEditing(_currentContainerPanel.Name))
                {
                    btnClose.Text = "X";
                    btnClose.Enabled = true;
                }
                else
                {
                    btnClose.Text = string.Empty;
                    btnClose.Enabled = false;
                }
            }
            else
            {
                btnClose.Text = "X";
                btnClose.Enabled = true;
            }

            lblHeader.BringToFront();
            btnClose.BringToFront();
        }

        /// <summary>
        /// Set if we show the title editor (TextBox)
        /// </summary>
        public bool OnEditMode
        {
            set
            {
                if (value)
                {
                    if (_currentFlowContainer is SMStateMachine)
                    {
                        btnClose.Text = "X";
                        btnClose.Enabled = true;
                    }
                    else
                    {
                        lblHeader.Text = string.Empty;
                        tbSubroutineName.Show();
                        tbSubroutineName.BringToFront();
                    }
                }
            }
        }

        /// <summary>
        /// Rebuild all controls
        /// </summary>
        public override void Rebuild()
        {
            int count = this.Controls.Count;
            if (count > 0)
            {
                Control[] ctls = new Control[count];
                this.Controls.CopyTo(ctls, 0);
                foreach (Control ctl in ctls)
                {
                    // Only dispose of SMContainerPanels
                    if (ctl is SMContainerPanel)
                    {
                        this.Controls.Remove(ctl);
                        ctl.Dispose();
                    }
                }
            }
            _currentContainerPanel = null;
            ShowFlowContainer(_stateMachine);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _currentContainerPanel.EditMode = false;
            if (_currentFlowContainer is SMStateMachine)
            {
                btnClose.Text = string.Empty;
                btnClose.Enabled = false;
            }
            else
            {
                SMFlowContainer currentFlowContainer = _currentFlowContainer as SMFlowContainer;
                _currentFlowContainer.Text = tbSubroutineName.Text;
                ShowFlowContainer(_currentFlowContainer.Parent as SMFlowContainer);
                //_currentContainerPanel.Redraw(currentFlowContainer);
            }
        }
        private delegate void delFlowBase(SMFlowBase currentFlowItem, bool stepping);
        public override void RefreshFlowItem(SMFlowBase currentFlowItem, bool stepping)
        {
            if (InvokeRequired)
            {
                try
                {
                    BeginInvoke(new delFlowBase(RefreshFlowItem), currentFlowItem, stepping);
                }
                catch (ObjectDisposedException) { } 
                return;
            }
            else if (!IsDisposed && _currentContainerPanel != null)
            {
                if (Visible && stepping)
                {
                    ShowFlowContainer(currentFlowItem.ParentContainer);
                }
                _currentContainerPanel.RefreshFlowItem(currentFlowItem);
            }
        }
        /// <summary>
        /// Give a name for this Property Page
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Basic Flow Chart";
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            SMStateMachine sm = _currentFlowContainer is SMStateMachine ? _currentFlowContainer as SMStateMachine : _currentFlowContainer.GetParent<SMStateMachine>();
            sm.Go();
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
            SMStateMachine sm = _currentFlowContainer is SMStateMachine ? _currentFlowContainer as SMStateMachine : _currentFlowContainer.GetParent<SMStateMachine>();
            sm.Step();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            SMStateMachine sm = _currentFlowContainer is SMStateMachine ? _currentFlowContainer as SMStateMachine : _currentFlowContainer.GetParent<SMStateMachine>();
            sm.Stop();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            SMStateMachine sm = _currentFlowContainer is SMStateMachine ? _currentFlowContainer as SMStateMachine : _currentFlowContainer.GetParent<SMStateMachine>();
            sm.Pause();
        }

        private void UpdateAutoScope()
        {
            if (string.IsNullOrEmpty(_currentFlowContainer.ScopeID))
            {
                cbAutoScope.Text = "Auto Scope : None";
            }
            else
            {
                cbAutoScope.Text = "Auto Scope : " + _currentFlowContainer.ScopeID;
            }
            cbAutoScope.Checked = !string.IsNullOrEmpty(_currentFlowContainer.ScopeID);
        }
                
        private void OnChangedAutoScope(object sender, EventArgs e)
        {
            _currentFlowContainer.AutoScope(cbAutoScope.Checked);
            UpdateAutoScope();
        }

        private void OnChangedAutoScopeSize(object sender, EventArgs e)
        {
            cbAutoScope.Left = Width - btnClose.Width - cbAutoScope.Width;
            lblHeader.Width = cbAutoScope.Left - lblHeader.Left;
        }
    }
}
