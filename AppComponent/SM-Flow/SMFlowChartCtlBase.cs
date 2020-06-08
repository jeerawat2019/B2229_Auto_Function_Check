﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using X_Core.Comp.SMLib;
using X_Core.Comp.SMLib.Flow;

namespace X_Core.Comp.SMLib
{
    public partial class SMFlowChartCtlBase : UserControl
    {
        public SMFlowChartCtlBase()
        {
            InitializeComponent();
        }

        protected SMStateMachine _stateMachine = null;
        public SMStateMachine RefStateMachine
        {
            get { return _stateMachine; }
        }

        /// <summary>
        /// Create background controls
        /// </summary>
        public virtual void Rebuild()
        {
        }
        /// <summary>
        /// Enter a flow item
        /// </summary>
        /// <param name="currentFlowItem"></param>
        /// <param name="stepping"></param>
        public virtual void RefreshFlowItem(SMFlowBase currentFlowItem,bool stepping)
        {
        }
    }
}
