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

namespace X_Core.Comp.SMLib.SMFlowChart.Controls
{
    public partial class ActionCtl : SMCtlBase
    {
        private SMActionFlow RefAction
        {
            get { return _flowItem as SMActionFlow; }
        }
        public ActionCtl() 
        {
            InitializeComponent();
        }
        public ActionCtl(SMContainerPanel containerPanel, SMFlowBase flowItem)
            : base(containerPanel, flowItem, global::MCore.Comp.SMLib.SMFlowChart.Properties.Resources.ActionFlow.Size)
        {
            InitializeComponent();
            OnChanged();
        }
        public override void OnChanged()
        {
            if (RefAction.HasProblem)
            {
                _state = eflowItemState.Problem;
            } 
            else if (RefAction.HasChildren)
            {
                _state = eflowItemState.Ok;
            }
            else
            {
                _state = eflowItemState.Empty;
            }
            base.OnChanged();
        }
        protected override void DoEditor()
        {
            new ActionEditorForm(_containerPanel, _flowItem as SMActionFlow).ShowDialog();
        }
    }
}
