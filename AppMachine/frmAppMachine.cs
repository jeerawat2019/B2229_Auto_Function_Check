using AiComp.Logs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using X_Core;
using X_Core.CompElement;
using X_Core.ControlElement;

namespace B2229_AT_FuncCheck
{
    public partial class frmAppMachine : Form
    {
        public static frmAppMachine This = null;
        public frmAppMachine()
        {
            This = this;

            InitializeComponent();

            if(!this.DesignMode)
            {
                X_CoreS.RootComp.ApplicationSetup(This, @"C:\AiMachine\AppConfig");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAppMachine_Load(object sender, EventArgs e)
        {
            X_CoreS.GetDummyControl();
            ///
            CompFactory mMachineFactory = new CompFactory(typeof(Dev_AppMachine.Machine), Dev_AppMachine.StaticName.MainApp);
            ///
            X_Core.CompElement.CompBase mAllStation = new CompBase(Dev_AppMachine.StaticName.AllStation);
            ///
            try
            {
                X_CoreS.RootComp.Add(new DefaultLogger(Dev_AppMachine.StaticName.AppLogingMachine));
                ///
                #region Create station controll
                ///
                mAllStation.Add(new Dev_AppStation.TesterStation.PC1_SFIT(Dev_AppMachine.StaticName.PC1_S_FIT));
                mAllStation.Add(new Dev_AppStation.TesterStation.PC1_SFIT(Dev_AppMachine.StaticName.PC2_S_FIT));
                mAllStation.Add(new Dev_AppStation.TesterStation.PC1_SFIT(Dev_AppMachine.StaticName.PC3_AGING));
                mAllStation.Add(new Dev_AppStation.TesterStation.PC1_SFIT(Dev_AppMachine.StaticName.PC4_WD));
                ///
                #endregion
                mMachineFactory.Add(mAllStation);
                ///
                X_CoreS.RootComp.Add(mMachineFactory);
                /// Initialize call after all component definitions
                X_CoreS.RootComp.InitializeIDReferences();
                ///
                ///CtrBrowser ctrBrowser = new CtrBrowser();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
