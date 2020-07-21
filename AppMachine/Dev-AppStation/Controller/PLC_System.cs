using AppMachine.AppControlBase;
using AppMachine.AppResult;
using B2229_AT_FuncCheck.AppResult.AppConsignePart;
using B2229_AT_FuncCheck.Dev_AppMachine;
using B2229_AT_FuncCheck.Dev_Component;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using X_Core;
using X_Core.CompElement;

namespace B2229_AT_FuncCheck.Dev_AppStation.Controller
{
    public class PLC_System : CompBase
    {
        int msec = 35;

        
        [XmlIgnore]
        public bool IsMemStartPartOn
        {
            [StateMachineEnabled]
            get
            {
                X_CoreS.Delay(msec);
                return GetPropValue(() => IsMemStartPartOn, false);
            }
            [StateMachineEnabled]
            set { SetPropValue(() => IsMemStartPartOn, value); }
        }
       
        [XmlIgnore]
        public bool IsJigStationSfit1PartOn
        {
            [StateMachineEnabled]
            get
            {
                X_CoreS.Delay(msec);
                return GetPropValue(() => IsJigStationSfit1PartOn, false);
            }
            [StateMachineEnabled]
            set { SetPropValue(() => IsJigStationSfit1PartOn, value); }
        }
       
        [XmlIgnore]
        public bool IsJigStationSfit2PartOn
        {
            [StateMachineEnabled]
            get
            {
                X_CoreS.Delay(msec);
                return GetPropValue(() => IsJigStationSfit2PartOn, false);
            }
            [StateMachineEnabled]
            set { SetPropValue(() => IsJigStationSfit2PartOn, value); }
        }
       
        [XmlIgnore]
        public bool IsJigStationAngingPartOn
        {
            [StateMachineEnabled]
            get
            {
                X_CoreS.Delay(msec);
                return GetPropValue(() => IsJigStationAngingPartOn, false);
            }
            [StateMachineEnabled]
            set { SetPropValue(() => IsJigStationAngingPartOn, value); }
        }
       
        [XmlIgnore]
        public bool IsJigStationWDPartOn
        {
            [StateMachineEnabled]
            get
            {
                X_CoreS.Delay(msec);
                return GetPropValue(() => IsJigStationWDPartOn, false);
            }
            [StateMachineEnabled]
            set { SetPropValue(() => IsJigStationWDPartOn, value); }
        }
        public PLC_System() : base() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public PLC_System(string name) : base(name) { }
        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }
        private Dev_Component.ComuPLCLink mComPLCLink = null;

        /// <summary>
        /// 
        /// </summary>
        public override void InitializeIDReferences()
        {
            base.InitializeIDReferences();
            ///
            mComPLCLink = X_CoreS.GetComponent(Dev_AppMachine.StaticName.PLC_TATTURN) as Dev_Component.ComuPLCLink;
            ///  
        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public void GetMemControlToPart()
        {
            //string[] io = new string[] { "10", "11", "65535", "1" };
            //int i = 0;
            Machine.This.UserControlPart.All((x) =>
            {
                PropertyInfo strBin = x.Value.GetType().GetProperty("MemBinProcess");
                //
                //PropertyInfo strMem = x.Value.GetType().GetProperty("MemControlPart");
                ///
                string memResult = null;
                ///
                var strMem = string.Format("{0}{1}", "R", x.Value.GetType().GetProperty("MemControlPart").GetValue(x.Value).ToString());
                ///
                GetMemControlWord(strMem, out memResult);
                ///
                strBin.SetValue(x.Value, PLCConvert.DecimalToBinary(memResult));// PLCConvert.DecimalToBinary(io[i]); i++
                ///
                return true;
            });
        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public void GetMemStatusJigEmpty()
        {
            ///
            Machine.This.UserControlPart.All((x) =>
            {
                ///
                string strBin = null;
                ///
                var strMem = string.Format("{0}{1}", "R", x.Value.GetType().GetProperty("MemStatusJigEmpty").GetValue(x.Value).ToString());
                ///        
                GetMemControlWord(strMem, out strBin);
                ///
                PropertyInfo propertie = x.Value.GetType().GetProperty("PartJigViews");//.GetValue(x.Value,null);// as AppPartJigView[];
                ///
                AppPartJigView[] PartJig = (AppPartJigView[])propertie.GetValue(x.Value);
                ///
                PartUpdateStatusJigEmpty(PartJig, strBin, x.Value);
                ///
                return true;
            });
        }
        /// <summary>
        /// 
        /// </summary>
        private void PartUpdateStatusJigEmpty(AppPartJigView[] partJig, string strBin, AppUserControlBase app)
        {
            ///
            DetermineData(partJig, ref strBin);
            ///
            partJig.All((x) =>
            {
                if (x is AppPartJigView)
                {
                    var part = (AppPartJigView)x;
                    ///
                    if (part.CDPlayer.IsProcess == Part.Process.Empty || part.CDPlayer.IsProcess == Part.Process.Null)
                    {
                        part.CDPlayer.IsProcess = (io[int.Parse(part.CDPlayer.PartId.ToString())] == '1') ? Part.Process.Start : Part.Process.Empty;
                        ///
                        if (part.CDPlayer.IsProcess == Part.Process.Start)
                        {
                            Update2DPart(part.CDPlayer);
                            ///
                            io[int.Parse(part.CDPlayer.PartId.ToString())] = '0';
                        }
                    }
                }
                ///
                return true;
            });
        }

        private static void DetermineData(AppPartJigView[] partJig, ref string strBin)
        {
            if (partJig == null) return;

            if (strBin.Length != partJig.Count())
            {
                var num = strBin.Length;
                ///
                for (int i = 0; i < partJig.Count() - num; i++)
                    ///
                    strBin = string.Format("{0}{1}", "0", strBin);
            }
            ///
            char[] io = strBin.ToCharArray();
            ///
            Array.Reverse(io);
            ///
            strBin = new string(io);

        }

        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public void GetMemNotUseJig()
        {
            ///
            Machine.This.UserControlPart.All((x) =>
            {
                ///
                string memResult = null;
                ///
                var strMem = string.Format("{0}{1}", "R", x.Value.GetType().GetProperty("MemNotUseJig").GetValue(x.Value).ToString());
                ///
                GetMemControlWord(strMem, out memResult);
                ///
                PropertyInfo propertie = x.Value.GetType().GetProperty("PartJigViews");//.GetValue(x.Value,null);// as AppPartJigView[];
                ///
                AppPartJigView[] PartJig = (AppPartJigView[])propertie.GetValue(x.Value);
                ///
                PartNotUseJig(PartJig, memResult, x.Value);
                ///
                return true;
            });
        }

        private void PartNotUseJig(AppPartJigView[] partJig, string strBin, AppUserControlBase value)
        {
            ///
            DetermineData(partJig, ref strBin);
            ///
            partJig.All((x) =>
            {
                if (x is AppPartJigView)
                {
                    var part = (AppPartJigView)x;
                    ///
                    if (part.CDPlayer.IsProcess == Part.Process.Empty || part.CDPlayer.IsProcess == Part.Process.Null)
                    {
                        part.CDPlayer.IsProcess = (io[int.Parse(part.CDPlayer.PartId.ToString())] == '1') ? Part.Process.Start : Part.Process.Empty;
                        ///
                        if (part.CDPlayer.IsProcess == Part.Process.Start)
                        {
                            Update2DPart(part.CDPlayer);
                            ///
                            io[int.Parse(part.CDPlayer.PartId.ToString())] = '0';
                        }
                    }
                }
                ///
                return true;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public void UpdateAllProcessPart()
        {

            Machine.This.UserControlPart.All((x) =>
            {
       
                PropertyInfo propertie  = x.Value.GetType().GetProperty("PartJigViews");//.GetValue(x.Value,null);// as AppPartJigView[];
                //AppPartJigView[] a = ((Array)propertie.GetValue(x.Value)).Cast<AppPartJigView>().ToArray();
                AppPartJigView[] PartJig = (AppPartJigView[])propertie.GetValue(x.Value);
                ///
                var binstr = x.Value.GetType().GetProperty("MemBinProcess").GetValue(x.Value).ToString();
                ///
                UpdateProcessPart(PartJig, binstr, x.Value);
                ///
                return true;
            });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="appPartJigViews"></param>
        /// <param name="strBin"></param>
        internal void UpdateProcessPart(AppPartJigView[] appPartJigViews, string strBin, AppUserControlBase app)
        {
            if (appPartJigViews == null) return;

            if (strBin.Length != appPartJigViews.Count())
            {
                var num = strBin.Length;
                ///
                for (int i = 0; i < appPartJigViews.Count() - num; i++)
                    ///
                    strBin = string.Format("{0}{1}", "0", strBin);
            }
            ///
            char[] io = strBin.ToCharArray();
            ///
            Array.Reverse(io);
            ///
            appPartJigViews.All((x) =>
            {
                if (x is AppPartJigView)
                {
                    var part = (AppPartJigView)x;
                    ///
                    if (part.CDPlayer.IsProcess == Part.Process.Empty || part.CDPlayer.IsProcess == Part.Process.Null)
                    {
                        part.CDPlayer.IsProcess = (io[int.Parse(part.CDPlayer.PartId.ToString())] == '1') ? Part.Process.Start : Part.Process.Empty;
                        ///
                        if (part.CDPlayer.IsProcess == Part.Process.Start)
                        {
                            Update2DPart(part.CDPlayer);
                            ///
                            io[int.Parse(part.CDPlayer.PartId.ToString())] = '0';
                        }
                    }
                }
                ///
                return true;
            });
            //Array.Reverse(io);
            /////
            //string strout = new string(io);
            /////
            //int output = Convert.ToInt32(strout, 2);
            /////
            //string strMem = string.Format("{0}{1}", "R", app.GetType().GetProperty("MemControlPart").GetValue(app).ToString());
            /////
            //if (SetOffStartJigs(strMem, output.ToString()) != SequenceError.Normal)
            //{
            //    X_CoreS.LogAlarmPopup("Plc Wtite result error!", $"TimeOut waiting for SetStatusProcess of'{this.Nickname}'");
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cDPlayer"></param>
        private void Update2DPart(PartCDPlayerView cDPlayer)
        {
            string mem2D = string.Format("{0}{1}","R",(cDPlayer.Mem2DCode).ToString());
            ///
            string data2DCode ;
            ///
            //data2DCode[0] = string.Format("Data2DCode id = {0}", cDPlayer.PartId);
            ///
            if (this.mComPLCLink.GetData2DCode(mem2D, out data2DCode) != SequenceError.Normal)
            {
                X_CoreS.LogAlarmPopup("Get Error 2D Data", $"TimeOut waiting for SetStatusProcess of'{this.Nickname}'");
            }
            else
            {
                cDPlayer.Data2DCode = data2DCode;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memWord"></param>
        /// <param name="strBin"></param>
        /// <returns></returns>
        public SequenceError SetOffStartJigs(string memWord, string strDec)
        {
            try
            {
                lock (this)
                {                   
                    ///
                    this.mComPLCLink.WriteDeviceRandom2(memWord, "1", new System.Windows.Forms.TextBox() { Text = strDec });
                    ///
                    return SequenceError.Normal;
                }
            }
            catch (Exception ex)
            {
                X_CoreS.LogError(ex, $"TimeOut waiting for SetConfStatusAuto of'{this.Nickname}'");
                ///
                return SequenceError.SetConfRead2DCode;

            }

            //}
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        internal void  GetMemControlWord(string sPartTotal, out string result)
        {
            result = null;
            ///
            string[] strOut; int i = 1;

            try
            {
                lock (this)
                {

                    if (string.IsNullOrEmpty(sPartTotal))
                        throw new Exception("PartTotal:> IsNullOrEmpty");
                    ///
                    this.mComPLCLink.ReadDeviceRandom2(new System.Windows.Forms.TextBox() { Text = sPartTotal }, i.ToString(), out strOut);
                    ///
                    result = strOut[i - 1];
                    ///
                    
                }
            }
            catch (Exception ex)
            {
                X_CoreS.LogError(ex, $"TimeOut waiting for SetStatusProcess of'{this.Nickname}'");
              
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public void GetMemDataWord()
        {

        }
        [StateMachineEnabled]
        public void StationInitialze()
        {
            mComPLCLink.AxActOpen();
            ///       
        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public void PartUpdate()
        {
            //Dev_AppMachine.Machine.This.PartJigColSfit1.PartJigViews[0].CDPlayer.IsProcess = AppMachine.AppResult.Part.Process.Start;
            //Dev_AppMachine.Machine.This.PartJigColSfit2.PartJigViews[0].CDPlayer.IsProcess = AppMachine.AppResult.Part.Process.Start;
            //Dev_AppMachine.Machine.This.PartJigColAngingView.PartJigViews[0].CDPlayer.IsProcess = AppMachine.AppResult.Part.Process.Start;
            //Dev_AppMachine.Machine.This.PartJigColWDView.PartJigViews[0].CDPlayer.IsProcess = AppMachine.AppResult.Part.Process.Start;
            
        }
    }
}
