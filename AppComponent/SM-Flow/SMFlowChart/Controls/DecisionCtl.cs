using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using X_Core.Comp.SMLib.SMFlowChart.EditForms;
using X_Core.Comp.SMLib.Flow;
using X_Core.Comp.SMLib.Path;

namespace X_Core.Comp.SMLib.SMFlowChart.Controls
{
    public partial class DecisionCtl : SMCtlBase
    {
        private SMDecision RefDecision
        {
            get { return _flowItem as SMDecision; }
        }
        public DecisionCtl() 
        {
            InitializeComponent();
        }
        public DecisionCtl(SMContainerPanel containerPanel, SMFlowBase flowItem)
            : base(containerPanel, flowItem, global::MCore.Comp.SMLib.SMFlowChart.Properties.Resources.Decision.Size)
        {
            InitializeComponent();
            OnChanged();
        }
        protected override void DoEditor()
        {
            new DecisionEditorForm(_containerPanel, _flowItem as SMDecision).ShowDialog();
        }
        private YesNoLabel GetLabel(bool bTrue)
        {
            string labelName = BuildLabelName(bTrue);
            if (_containerPanel.Controls.ContainsKey(labelName))
            {
                return _containerPanel.Controls[labelName] as YesNoLabel;
            }
            return null;
        }
        private void DisposeLabel(bool bTrue)
        {
            YesNoLabel labelYN = GetLabel(bTrue);
            if (labelYN != null)
            {
                _containerPanel.Controls.Remove(labelYN);
                labelYN.Dispose();
            }
        }
        protected override void Build(SMPathOut pathOut)
        {
            base.Build(pathOut);
            if (pathOut is SMPathOutBool)
            {
                SMPathOutBool pathOutBool = pathOut as SMPathOutBool;
                YesNoLabel tbYesNo = new YesNoLabel(pathOutBool);
                tbYesNo.BackColor = System.Drawing.Color.Transparent;
                tbYesNo.Name = BuildLabelName((pathOutBool).True);
                tbYesNo.Size = new System.Drawing.Size(16, 16);
                tbYesNo.TabIndex = 0;
                tbYesNo.Text = pathOutBool.Text;
                tbYesNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                tbYesNo.Click += new EventHandler(tbYesNo_Click);
                tbYesNo.MouseMove += new MouseEventHandler(OnMouseMove);
                _containerPanel.Controls.Add(tbYesNo);
            }
        }

        void OnMouseMove(object sender, MouseEventArgs e)
        {
            _containerPanel.Cursor = Cursors.Default;
        }
        public override void OnChanged()
        {
            _state = eflowItemState.Empty;
            if (RefDecision.HasDecision)
            {
                _state = eflowItemState.Problem;
                if (RefDecision.HasCallback)
                {
                    _state = eflowItemState.Ok;
                }
            }
            base.OnChanged();
            if (_flowItem.HasChildren)
            {
                this.BackgroundImage = global::MCore.Comp.SMLib.SMFlowChart.Properties.Resources.DecisionNested;
            }
            else
            {
                this.BackgroundImage = global::MCore.Comp.SMLib.SMFlowChart.Properties.Resources.Decision;
            }
        }

        void tbYesNo_Click(object sender, EventArgs e)
        {
            if (object.ReferenceEquals(_containerPanel.CurrentSel, sender))
            {
                (_flowItem as SMDecision).SwitchLogic();
                _containerPanel.Redraw(_flowItem);

            }
            else
            {
                _containerPanel.CurrentSel = sender as ISelectable;
            }
        }
    }
}
