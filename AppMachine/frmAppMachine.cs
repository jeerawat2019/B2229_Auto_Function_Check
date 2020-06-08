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
using X_Core.Comp.SMLib;
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
            ///
            mCtrBrowser.Dock = DockStyle.Fill;
            ///
            tabCompnent.Controls.Add(mCtrBrowser);
            ///
            if (!this.DesignMode)
            {
                X_CoreS.RootComp.ApplicationSetup(This, @"C:\AiMachine\AppConfig");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private CtrBrowser mCtrBrowser = new CtrBrowser();
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
                #region Create object station controll
                ///
                mAllStation.Add(new Dev_AppStation.TesterStation.PC1_SFIT(Dev_AppMachine.StaticName.PC1_S_FIT));
                mAllStation.Add(new Dev_AppStation.TesterStation.PC2_SFIT(Dev_AppMachine.StaticName.PC2_S_FIT));
                mAllStation.Add(new Dev_AppStation.TesterStation.PC3_AGING(Dev_AppMachine.StaticName.PC3_AGING));
                mAllStation.Add(new Dev_AppStation.TesterStation.PC5_WD(Dev_AppMachine.StaticName.PC4_WD));
                ///
                mMachineFactory.Add(mAllStation);
                ///
                X_CoreS.RootComp.Add(mMachineFactory);
                #endregion

                #region Create object state machine
                ///
                CompFactory smDef = new CompFactory((typeof(CompBase)), Dev_AppMachine.StaticName.AllStateMachine);
                smDef.Add(new SMStateMachine(Dev_AppMachine.StaticName.SMHomeRes));
                smDef.Add(new SMStateMachine(Dev_AppMachine.StaticName.SMMain));
                smDef.Add(new SMStateMachine(Dev_AppMachine.StaticName.SMPC1_SFIT));
                smDef.Add(new SMStateMachine(Dev_AppMachine.StaticName.SMPC2_SFIT));
                smDef.Add(new SMStateMachine(Dev_AppMachine.StaticName.SMPC3_AGING));
                smDef.Add(new SMStateMachine(Dev_AppMachine.StaticName.SMPC4_WD));
                ///
                X_CoreS.RootComp.Add(smDef);
                #endregion



                /// Initialize call after all component definitions
                X_CoreS.RootComp.InitializeIDReferences();
                ///
                mCtrBrowser.Rebuild();
                ///
                GetSM_BindControl();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private SMStateMachine mSMHomeRes = null;
        ///
        private SMStateMachine mSMMain = null;
        /// <summary>
        /// 
        /// </summary>
        private List<SMStateMachine> mAllStateMachine = new List<SMStateMachine>();
        /// <summary>
        /// 
        /// </summary>
        private void GetSM_BindControl()
        {
            ///
            mSMHomeRes = X_CoreS.GetComponent(Dev_AppMachine.StaticName.SMHomeRes) as SMStateMachine;
            ///
            mSMMain = X_CoreS.GetComponent(Dev_AppMachine.StaticName.SMMain) as SMStateMachine;
            ///
            X_Core.CompElement.CompBase mAllMachineState = X_CoreS.GetComponent(Dev_AppMachine.StaticName.AllStateMachine);
            ///
            foreach (CompBase smchild in mAllMachineState.ChildArray)
            {
                SMStateMachine sm = smchild as SMStateMachine;
                ///
                if(sm != null)
                {
                    if(sm.Name == Dev_AppMachine.StaticName.SMHomeRes)
                    {
                        mAllStateMachine.Add(sm);
                        ///
                        AppMachine.AppControlBase.AppFloatableSMFlowChart appFloatableSMFlowChart = new AppMachine.AppControlBase.AppFloatableSMFlowChart();
                        ///
                        appFloatableSMFlowChart.Dock = DockStyle.Fill;
                        ///
                        appFloatableSMFlowChart.Bind = sm;
                        ///
                        TabPage tpSM = new TabPage(sm.Name);
                        ///
                        tpSM.Controls.Add(appFloatableSMFlowChart);
                        ///
                        tcSMReset.Controls.Add(tpSM);
                    }
                    else if(sm.Name == Dev_AppMachine.StaticName.SMMain)
                    {
                        mAllStateMachine.Add(sm);
                        ///
                        AppMachine.AppControlBase.AppFloatableSMFlowChart appFloatableSMFlowChart = new AppMachine.AppControlBase.AppFloatableSMFlowChart();
                        ///
                        appFloatableSMFlowChart.Dock = DockStyle.Fill;
                        ///
                        appFloatableSMFlowChart.Bind = sm;
                        ///
                        TabPage tpSM = new TabPage(sm.Name);
                        ///
                        tpSM.Controls.Add(appFloatableSMFlowChart);
                        ///
                        tcSMMain.Controls.Add(tpSM);
                    }
                    else
                   {
                        mAllStateMachine.Add(sm);
                        ///
                        AppMachine.AppControlBase.AppFloatableSMFlowChart appFloatableSMFlowChart = new AppMachine.AppControlBase.AppFloatableSMFlowChart();
                        ///
                        appFloatableSMFlowChart.Dock = DockStyle.Fill;
                        ///
                        appFloatableSMFlowChart.Bind = sm;
                        ///
                        TabPage tpSM = new TabPage(sm.Name);
                        ///
                        tpSM.Controls.Add(appFloatableSMFlowChart);
                        ///
                        tcSMStation.Controls.Add(tpSM);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAppMachine_FormClosing(object sender, FormClosingEventArgs e)
        {
           if( MessageBox.Show(this, "Are you sure to Close?", "Confirm Closing", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DestroyAll();
                ///
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private static void DestroyAll()
        {
            X_CoreS.LogInfo("We are going to save and close!");
            try
            {
                //if(_sm)
                CompRoot.AppStatus("Pre-Destroy");
                X_CoreS.RootComp.PreDestroy();
                X_CoreS.RootComp.SaveSettings();
                CompRoot.AppStatus("Destroying Root");
                X_CoreS.RootComp.Destroy();
            }
            catch (X_CoreException ex)
            {

                X_CoreS.Log(ex);
            }
            catch (Exception ex)
            {
                X_CoreS.LogPopup(ex, "Destroy Problem");
            }
            
        }
    }
}
