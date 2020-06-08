using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using X_Core.Comp.SMLib.Flow;
using X_Core.Comp.SMLib.SMFlowChart.EditForms;

namespace X_Core.Comp.SMLib.SMFlowChart.Controls
{
    public partial class SubroutineCtl : SMCtlBase
    {
        public SubroutineCtl(SMContainerPanel containerPanel, SMSubroutine flowItem)
            : base(containerPanel, flowItem, global:: MCore.Comp.SMLib.SMFlowChart.Properties.Resources.Subroutine.Size)
        {
            InitializeComponent();
        }
        protected override void DoEditor()
        {
            // Show new Panel
            new SubroutineEditorForm(_containerPanel, _flowItem as SMSubroutine).ShowDialog();
        }
    }
}
