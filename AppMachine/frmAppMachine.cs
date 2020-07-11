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
using B2229_AT_FuncCheck.Dev_AppStation.Controller;
using B2229_AT_FuncCheck.Dev_Component;
using AiComp.ConnectType.Commu;

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
                CreateAllStationMachine(mAllStation);
                ///
                CreateAllTesttingSolution(mMachineFactory);
                ///
                CreateController(mMachineFactory);
                ///
                CreateLogManager(mMachineFactory);
                ///
                #endregion
                ///
                mMachineFactory.Add(mAllStation);
                ///
                try
                {
                    X_CoreS.RootComp.Add(mMachineFactory);
                }
                catch (Exception ex)
                {

                    X_CoreS.LogPopup(ex, "Machine Initialize Unseccessful in loading of application");
                }
                ///
                #region Create object state machine
                ///
                CompFactory smDef = new CompFactory((typeof(CompBase)), Dev_AppMachine.StaticName.AllStateMachine);

                CreateAllStateMachineContriller(smDef);
                ///
                try
                {
                    X_CoreS.RootComp.Add(smDef);
                }
                catch (Exception ex)
                {

                    X_CoreS.LogPopup(ex, "Machine Initialize Unseccessful in loading of application");
                }
                #endregion

                /// Initialize call after all component definitions
                X_CoreS.RootComp.InitializeIDReferences();
                ///
                mCtrBrowser.Rebuild();
                ///
                GetSM_BindControl();
                ///
                GetAllDisplay();
                ///
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void CreateController(CompFactory mMachineFactory)
        {
            
            ///
            CompFactory mAllTesterSys = mMachineFactory.Add(new CompBase(Dev_AppMachine.StaticName.AllController));
            ///
            Dev_Component.ComuPLCLink mPLC_Qcpu = new ComuPLCLink(Dev_AppMachine.StaticName.PLC_TATTURN);
            ///
            mAllTesterSys.Add(mPLC_Qcpu);
            ///
          
        }
        private void CreateLogManager(CompFactory mMachineFactory)
        {

            ///
            CompFactory mAllTesterSys = mMachineFactory.Add(new CompBase(Dev_AppMachine.StaticName.AllLogging));
            ///
            Dev_DataLogs.LogsManager mLogsManager = new Dev_DataLogs.LogsManager(Dev_AppMachine.StaticName.DATA_LOGSMANAGER);
            ///
            mAllTesterSys.Add(mLogsManager);
            ///

        }

        private void CreateAllTesttingSolution(CompFactory mMachineFactory)
        {
            CompFactory mAllTesterSys = mMachineFactory.Add(new CompBase(Dev_AppMachine.StaticName.AllTesttingSolution));
            ///
            Dev_Component.ComuPCLink mPC1Test_Sfit = new Dev_Component.ComuPCLink(Dev_AppMachine.StaticName.T_SFIT_NO01_NO04);
            ///
            Dev_Component.ComuPCLink mPC2Test_Sfit = new Dev_Component.ComuPCLink(Dev_AppMachine.StaticName.T_SFIT_NO05_NO08);
            ///
            Dev_Component.ComuPCLink mPC3Test_Angin = new Dev_Component.ComuPCLink(Dev_AppMachine.StaticName.T_ANGIN_NO01_NO13);
            ///
            Dev_Component.ComuPCLink mPC4Test_WD = new Dev_Component.ComuPCLink(Dev_AppMachine.StaticName.T_WD_NO01);
            ///

            mAllTesterSys.Add(mPC1Test_Sfit);
            ///
            mAllTesterSys.Add(mPC2Test_Sfit);
            ///
            mAllTesterSys.Add(mPC3Test_Angin);
            ///
            mAllTesterSys.Add(mPC4Test_WD);
            ///
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="smDef"></param>
        private static void CreateAllStateMachineContriller(CompFactory smDef)
        {
            smDef.Add(new SMStateMachine(Dev_AppMachine.StaticName.SMHomeRes));
            ///
            smDef.Add(new SMStateMachine(Dev_AppMachine.StaticName.SMMain));
            ///
            smDef.Add(new SMStateMachine(Dev_AppMachine.StaticName.SMController));
            ///
            smDef.Add(new SMStateMachine(Dev_AppMachine.StaticName.SMPC1_SFIT));
            ///
            smDef.Add(new SMStateMachine(Dev_AppMachine.StaticName.SMPC2_SFIT));
            ///
            smDef.Add(new SMStateMachine(Dev_AppMachine.StaticName.SMPC3_AGING));
            ///
            smDef.Add(new SMStateMachine(Dev_AppMachine.StaticName.SMPC4_WD));
            ///
            smDef.Add(new SMStateMachine(Dev_AppMachine.StaticName.SMDATALOGS));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mAllStation"></param>
        private static void CreateAllStationMachine(CompBase mAllStation)
        {
            ///
            mAllStation.Add(new Dev_AppStation.Controller.PLC_System(Dev_AppMachine.StaticName.PLC_Q02CPU));
            ///
            mAllStation.Add(new Dev_AppStation.TesterStation.PC1_SFIT(Dev_AppMachine.StaticName.ST_PC1_SFIT));
            ///
            mAllStation.Add(new Dev_AppStation.TesterStation.PC2_SFIT(Dev_AppMachine.StaticName.ST_PC2_SFIT));
            ///
            mAllStation.Add(new Dev_AppStation.TesterStation.PC3_AGING(Dev_AppMachine.StaticName.ST_PC3_AGING));
            ///
            mAllStation.Add(new Dev_AppStation.TesterStation.PC5_WD(Dev_AppMachine.StaticName.ST_PC4_WD));
            ///
            mAllStation.Add(new Dev_AppStation.Data.LoggingResult(Dev_AppMachine.StaticName.DATA_LOGRESULT));
        }
        /// <summary>
        /// 
        /// </summary>
        private Display.Production.UserProduction mUserProductionDisplay = null;
        /// <summary>
        /// 
        /// </summary>
        private void GetAllDisplay()
        {
            mUserProductionDisplay = new Display.Production.UserProduction();
            ///
            tabProduction.Controls.Add(mUserProductionDisplay);
            ///
            tabAllSetup.Controls.Add(new Display.Setup.UserSetup());

        }
        /// <summary>
        /// 
        /// </summary>
        private SMStateMachine mSMHomeRes = null;
        /// <summary>
        /// 
        /// </summary>
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
            #region Create control state machine module
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
                if (sm != null)
                {
                    if (sm.Name == Dev_AppMachine.StaticName.SMHomeRes)
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
                    else if (sm.Name == Dev_AppMachine.StaticName.SMMain)
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
            #endregion
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

        private void btnApply_Click(object sender, EventArgs e)
        {
            X_CoreS.RootComp.SaveSettings();
        }
        private SMStateMachine mActiveSM = null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRun_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                //this.BeginInvoke(new _delBtnEventClick(btnStartRun_Click), sender, e);
                this.BeginInvoke(new EventHandler(btnRun_Click), new object[] { sender, e });
                return;
            }
            //mcbStopWhenFinished.Enabled = true;
            mActiveSM = mSMMain;
            RunAndWaitForSM();
           
        }

        private void RunAndWaitForSM()
        {
            mActiveSM.Go();
            UpdateControlButtons();
            do
            {
                X_CoreS.SleepWithEvents(100);
            } while (SMRunning());
            mActiveSM = null;
            UpdateControlButtons();
        }


        private bool SMRunning()
        {
            foreach (SMStateMachine sm in mAllStateMachine)
            {
                if (sm.IsRunning)
                {
                    Dev_AppMachine.Machine.This.AnySMRunning = true;
                    return true;
                }
            }
            Dev_AppMachine.Machine.This.AnySMRunning = false;
            return false;
        }

        private void UpdateControlButtons()
        {
            if (mActiveSM == null && !Dev_AppMachine.Machine.This.HasReset)
            {
                btnRun.Text = "Run";
                btnRun.Enabled = false;
                btnPause.Enabled = false;

                btnHomeAll.Enabled = true;

                btnApply.Enabled = true;
                Dev_AppMachine.Machine.This.RunStatus = Dev_AppMachine.Machine.eRunStatus.Stopped;
            }
            else if (mActiveSM == null && Dev_AppMachine.Machine.This.HasReset)
            {
                Dev_AppMachine.Machine.This.StopWhenFinished = false;
  
                btnRun.Text = "Run";
                btnRun.Enabled = true;
                btnPause.Enabled = false;

                btnHomeAll.Enabled = true;
                btnApply.Enabled = true;
                Dev_AppMachine.Machine.This.RunStatus = Dev_AppMachine.Machine.eRunStatus.Stopped;
            }
            else if (mActiveSM != null && mActiveSM == mSMMain)//&& AppMachine.Comp.AppMachine.This.IsDisChart == 
            {
                btnRun.Text = "Running...";
                btnRun.Enabled = false;
                btnPause.Enabled = true;

                btnHomeAll.Enabled = false;
                btnApply.Enabled = false;
                Dev_AppMachine.Machine.This.RunStatus = Dev_AppMachine.Machine.eRunStatus.Running;
            }

            else if (mActiveSM != null && mActiveSM == mSMHomeRes)
            {
                btnRun.Text = "Homing...";
                btnRun.Enabled = false;
                btnPause.Enabled = false;

                btnHomeAll.Enabled = false;
                btnApply.Enabled = false;
                Dev_AppMachine.Machine.This.RunStatus = Dev_AppMachine.Machine.eRunStatus.Running;
            }

            btnPause.Text = "Pause";
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                //this.BeginInvoke(new _delBtnEventClick(btnPause_Click), sender, e);
                this.BeginInvoke(new EventHandler(btnPause_Click), new object[] { sender, e });
                return;
            }

            if (mActiveSM != null)
            {
                if (btnPause.Text == "Pause")
                {

                    foreach (SMStateMachine sm in mAllStateMachine)
                    {

                        if (sm.IsRunning)
                        {
                            sm.Pause();
                        }

                    }

                    btnPause.Text = "Continue";
                    //mcbStopWhenFinished.Enabled = false;
                    Dev_AppMachine.Machine.This.RunStatus = Dev_AppMachine.Machine.eRunStatus.Pause;
                }
                else if (btnPause.Text == "Continue")
                {
                    //autoLoadReady.SetTrue();
                    foreach (SMStateMachine sm in mAllStateMachine)
                    {
                        if (sm.IsRunning)
                        {
                            sm.Go();
                        }
                    }

                    btnPause.Text = "Pause";
                    //mcbStopWhenFinished.Enabled = true;
                    Dev_AppMachine.Machine.This.RunStatus = Dev_AppMachine.Machine.eRunStatus.Running;
                }
            }
        }
    }
}
