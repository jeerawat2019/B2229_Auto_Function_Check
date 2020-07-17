using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using X_Core;

namespace B2229_AT_FuncCheck.Dev_AppStation.TesterStation
{
    public class PC2_SFIT : StationBase
    {
        int msec = 35;
        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public bool IsMemJigStart
        {
            [StateMachineEnabled]
            get
            {
                X_CoreS.Delay(msec);
                return false;
            }
        }
        [XmlIgnore]
        public override bool IsStartProcess
        {
            [StateMachineEnabled]
            get
            {
                X_CoreS.Delay(msec);
                ///
                if (Dev_AppMachine.Machine.This.PartJigColSfit2.PartJigViews == null) return false;
                ///
                if (this.StationIndex < 0 || this.StationIndex > Dev_AppMachine.Machine.This.PartJigColSfit2.PartJigViews.Length) return false;
                ///
                var part = Dev_AppMachine.Machine.This.PartJigColSfit2.PartJigViews[this.StationIndex];
                ///
                bool result = (part.CDPlayer.IsProcess == AppMachine.AppResult.Part.Process.Start) ? true : false;
                ///
                bool data2dcode = (!string.IsNullOrEmpty(part.CDPlayer.Data2DCode));
                ///
                if (result && data2dcode)
                {
                    part.CDPlayer.IsProcess = AppMachine.AppResult.Part.Process.Testting;
                    ///
                    part.CDPlayer.StartTimeCapture = DateTime.Now;

                }
                return result;
            }
        }
        [XmlIgnore]
        public override bool IsTestting
        {
            [StateMachineEnabled]
            get
            {
                X_CoreS.Delay(msec);
                ///
                if (Dev_AppMachine.Machine.This.PartJigColSfit2.PartJigViews == null) return false;
                ///
                if (this.StationIndex < 0 || this.StationIndex > Dev_AppMachine.Machine.This.PartJigColSfit2.PartJigViews.Length) return false;
                ///
                return (Dev_AppMachine.Machine.This.PartJigColSfit2.PartJigViews[this.StationIndex].CDPlayer.IsProcess
                    ///
                    == AppMachine.AppResult.Part.Process.Testting) ? true : false;
            }
        }
        private double timesim = 5000; //5 sec
        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public override bool IsTestFinnish
        {
            [StateMachineEnabled]
            get
            {
                X_CoreS.Delay(msec);
                ///
                if (Dev_AppMachine.Machine.This.PartJigColSfit2.PartJigViews == null) return false;
                ///
                if (this.StationIndex < 0 || this.StationIndex > Dev_AppMachine.Machine.This.PartJigColSfit2.PartJigViews.Length) return false;
                ///
                else
                {
                    if (this.simulater)
                    {
                        var part = Dev_AppMachine.Machine.This.PartJigColSfit2.PartJigViews[this.StationIndex];
                        ///
                        return (CalculateTrayCycleTime(part.CDPlayer.StartTimeCapture) >= timesim) ? true : false;

                    }
                    else
                    {
                        return GetStatusTestJig();
                    }
                }
            }
        }
        [XmlIgnore]
        public  bool IsPartFail
        {
            [StateMachineEnabled]
            get
            {
                X_CoreS.Delay(msec);
                ///
                return !(Dev_AppMachine.Machine.This.PartJigColSfit2.PartJigViews[this.StationIndex].CDPlayer.IsPass);
                   
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public String CerrentResult
        {
            [StateMachineEnabled]
            get { return GetPropValue(() => CerrentResult); }
            [StateMachineEnabled]
            set { SetPropValue(() => CerrentResult, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public PC2_SFIT() { }
        public PC2_SFIT(string name) : base(name) { }
        public override void Initialize()
        {
            base.Initialize();
            
        }
        private Dev_Component.ComuPCLink mComPC2_Sfit = null;
        public override void InitializeIDReferences()
        {
            base.InitializeIDReferences();
            ///
            mComPC2_Sfit = X_CoreS.GetComponent(Dev_AppMachine.StaticName.T_SFIT_NO05_NO08) as Dev_Component.ComuPCLink;
            ///
            this.EndStationIndex = 4;
        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public override void StationInitialize()
        {
            this.StationID = "02";
        }
        /// <summary>
        /// 
        /// </summary>
        //[StateMachineEnabled]
        //public void SetCmdStartTestJig()
        //{
        //    base.CmdSetTestJig(JIG.START);
        //    ///
        //    mComPC2_Sfit.OnSendPortCommand(this.SetFormate,"@SET-01");
        //    ///
        //    Dev_AppMachine.Machine.This.PartJigColSfit2.PartJigViews[this.StationIndex].CDPlayer.IsProcess = AppMachine.AppResult.Part.Process.Testting;
        //}
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public void SetStartTestJig()
        {
            string cmdSet = null;
            ///
            string crc8 = base.CmdSetTestJig(JIG.START, out cmdSet);
            ///
            if (mComPC2_Sfit.OnSendPortCommand(cmdSet, "@SET-01").Contains(crc8))
            {
                ///
                //Dev_AppMachine.Machine.This.PartJigColSfit2.PartJigViews[this.StationIndex].CDPlayer.IsProcess = AppMachine.AppResult.Part.Process.Testting;
            }
        }

        public bool GetStatusTestJig()
        {
            string cmdGet = null; this.PartResult = false;
            ///
            string crc8 = base.CmdGetStatusJig(out cmdGet);
            ///
            if (mComPC2_Sfit.OnSendPortCommand(cmdGet, "@GET-01").Contains(crc8))
            {
                ///
                this.PartResult = base.ChangFormateDataRecive(cmdGet);//"GET-A,1,1,RUN,0,OK"
                ///
            }
            ///base.ChangFormateDataRecive(mComPC2_Sfit.OnSendPortCommand(this.SetFormate));
            /// 
            return this.PartResult;
            ///
            //if (this.PartResult)
            //{
            //    Dev_AppMachine.Machine.This.PartJigColSfit2.PartJigViews[this.StationIndex].CDPlayer.IsProcess = AppMachine.AppResult.Part.Process.Finnish;
            //}
        }
        [StateMachineEnabled]
        public void BuildCerrentResultPart()
        {
            var part = Dev_AppMachine.Machine.This.PartJigColSfit2.PartJigViews[this.StationIndex];
            //if (mPartList.Count != 0)
            //{
            //Dev_AppMachine.Machine.This.PartJigColSfit1.PartJigViews.Select(x =>
            //{
            if (part.CDPlayer.IsProcess == AppMachine.AppResult.Part.Process.Finnish)
            {
                ///
                CerrentResult = (string.Format(@"{0},{1},{2},{3},{4},{5}",
                        ///
                        DateTime.Now.ToString("ddMMyyyy"),
                        ///
                        part.CDPlayer.Data2DCode,
                        ///
                        this.StationID,
                        ///
                        part.CDPlayer.PartId.ToString(),
                        ///
                        (part.CDPlayer.IsPass) ? "OK" : "NG",
                        ///
                        CalculateTrayCycleTime(part.CDPlayer.StartTimeCapture).ToString()
                        ///
                        ));
            }
            //return true;
            //});
            //}
        }
        [StateMachineEnabled]
        public void ClearPartResult()
        {
            Dev_AppMachine.Machine.This.PartJigColSfit2.PartJigViews[this.StationIndex].CDPlayer.Data2DCode = "";
            ///
            Dev_AppMachine.Machine.This.PartJigColSfit2.PartJigViews[this.StationIndex].CDPlayer.IsProcess = AppMachine.AppResult.Part.Process.Null;
            ///
            Dev_AppMachine.Machine.This.PartJigColSfit2.PartJigViews[this.StationIndex].CDPlayer.PartStatus = "N/A";
        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public void GetData2DCodeByJig()
        {
            X_CoreS.Delay(msec);
            this.Data2Dcode = Dev_AppMachine.Machine.This.PartJigColSfit2.PartJigViews[this.StationIndex].CDPlayer.Data2DCode;
        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public void SetDisplayStatus(string process)
        {
            X_CoreS.Delay(msec);
            switch (process)
            {
                case "E":
                    Dev_AppMachine.Machine.This.PartJigColSfit2.PartJigViews[this.StationIndex].CDPlayer.IsProcess = AppMachine.AppResult.Part.Process.Empty;
                    break;
                case "T":
                    Dev_AppMachine.Machine.This.PartJigColSfit2.PartJigViews[this.StationIndex].CDPlayer.IsProcess = AppMachine.AppResult.Part.Process.Testting;
                    break;
                case "F":
                    Dev_AppMachine.Machine.This.PartJigColSfit2.PartJigViews[this.StationIndex].CDPlayer.IsProcess = AppMachine.AppResult.Part.Process.Finnish;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public void UpdateResultPart()
        {
            Dev_AppMachine.Machine.This.PartJigColSfit2.PartJigViews[this.StationIndex].CDPlayer.PartStatus = (this.PartResult == true) ? "OK" : "NG";
        }
    }
}

