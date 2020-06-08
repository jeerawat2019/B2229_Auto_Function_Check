using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using X_Core.CompElement;

namespace AppMachine.AppControlBase
{
    public partial class AppUserControlBase : UserControl
    {
        private bool _firstContruction = true;
        private CompBase _comp = null;
        public bool PreventDispose = false;

        private delegate void _delParamVoid();
       

        public AppUserControlBase()
        {
            this.Handle.ToString();
            InitializeComponent();

          
        }

        public AppUserControlBase(CompBase comp)
        {
            _comp = comp;
            this.Handle.ToString();
            InitializeComponent();
        }


        /// <summary>
        /// Initializing 
        /// </summary>
        protected virtual void Initializing()
        {
            //Override method in sub class
            
        }

       

        /// <summary>
        /// Release resource
        /// </summary>
        public virtual void RecursiveDispose(System.Windows.Forms.Control parentControl)
        {
            if (parentControl.Controls.Count > 0)
            {
                foreach (System.Windows.Forms.Control childControl in parentControl.Controls)
                {
                    RecursiveDispose(childControl);
                }
                if (parentControl is AppUserControlBase)
                {
                    (parentControl as AppUserControlBase).Dispose();
                }
                parentControl.Dispose();
            }
            else
            {
                if (parentControl is AppUserControlBase)
                {
                    (parentControl as AppUserControlBase).Dispose();
                }
                parentControl.Dispose();
            }
        }

        protected virtual void UserControlBase_ParentChanged(object sender, EventArgs e)
        {
            if (this.Parent == null && !PreventDispose)
            {
                RecursiveDispose(sender as System.Windows.Forms.Control);
            }
            else if (_firstContruction)
            {
                Initializing();
            }
        }

       
    }
}
