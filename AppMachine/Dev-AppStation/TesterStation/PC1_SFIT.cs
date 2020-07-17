using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AiComp.ConnectType.Commu;
using B2229_AT_FuncCheck.Dev_Component;
using X_Core;
using X_Core.CompElement;

namespace B2229_AT_FuncCheck.Dev_AppStation.TesterStation
{
    public class PC1_SFIT : StationBase
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
                if (Dev_AppMachine.Machine.This.PartJigColSfit1.PartJigViews == null) return false;
                ///
                if (this.StationIndex < 0 || this.StationIndex > Dev_AppMachine.Machine.This.PartJigColSfit1.PartJigViews.Length ) return false;
                ///
                var part = Dev_AppMachine.Machine.This.PartJigColSfit1.PartJigViews[this.StationIndex];
                ///
                bool result = (part.CDPlayer.IsProcess == AppMachine.AppResult.Part.Process.Start) ? true : false;
                ///
                bool data2dcode = (!string.IsNullOrEmpty(part.CDPlayer.Data2DCode) && part.CDPlayer.Data2DCode.Length >= 12);
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
        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public override bool IsTestting
        {
            [StateMachineEnabled]
            get
            {
                X_CoreS.Delay(msec);
                ///
                if (Dev_AppMachine.Machine.This.PartJigColSfit1.PartJigViews == null) return false;
                ///
                if (this.StationIndex < 0 || this.StationIndex > Dev_AppMachine.Machine.This.PartJigColSfit1.PartJigViews.Length) return false;
                ///
                return (Dev_AppMachine.Machine.This.PartJigColSfit1.PartJigViews[this.StationIndex].CDPlayer.IsProcess
                    ///
                    == AppMachine.AppResult.Part.Process.Testting) ? true : false;
            }
        }
        private double timesim = 30; //5 sec
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
                if (Dev_AppMachine.Machine.This.PartJigColSfit1.PartJigViews == null) return false;
                ///
                if (this.StationIndex < 0 || this.StationIndex > Dev_AppMachine.Machine.This.PartJigColSfit1.PartJigViews.Length) return false;
                ///
                else
                {

                    if (this.simulater)
                    {
                        var part = Dev_AppMachine.Machine.This.PartJigColSfit1.PartJigViews[this.StationIndex];
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
        public bool IsPartFail
        {
            [StateMachineEnabled]
            get
            {
                X_CoreS.Delay(msec);
                ///
                return !(Dev_AppMachine.Machine.This.PartJigColSfit1.PartJigViews[this.StationIndex].CDPlayer.IsPass);

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
        public PC1_SFIT() { }
        public PC1_SFIT(string name) :base(name) { }
        public override void Initialize()
        {
            base.Initialize();
           
        }
        private Dev_Component.ComuPCLink mComPC1_Sfit = null;
        /// <summary>
        /// 
        /// </summary>
        public override void InitializeIDReferences()
        {
            base.InitializeIDReferences();
            ///
            mComPC1_Sfit = X_CoreS.GetComponent(Dev_AppMachine.StaticName.T_SFIT_NO01_NO04) as Dev_Component.ComuPCLink;
            ///
            this.EndStationIndex = 4;
            ///
            //this.mPartList = new List<string>();
        }
        [StateMachineEnabled]
        public override void StationInitialize()
        {
            this.StationID = "01";
            //Dev_AppMachine.Machine.This.PartJigCol1Sfit;
           
        }
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
            if (mComPC1_Sfit.OnSendPortCommand(cmdSet, "@SET-01").Contains(crc8))
            {
                ///
                //Dev_AppMachine.Machine.This.PartJigColSfit1.PartJigViews[this.StationIndex].CDPlayer.IsProcess = AppMachine.AppResult.Part.Process.Testting;
            }
        }
       
        public override bool GetStatusTestJig()
        {
            string cmdGet = null; this.PartResult = false;
            ///
            string crc8 = base.CmdGetStatusJig(out cmdGet);
            ///
            if (mComPC1_Sfit.OnSendPortCommand(cmdGet, "@GET-01").Contains(crc8))
            {
                ///
                this.PartResult = base.ChangFormateDataRecive(cmdGet);//"GET-A,1,1,RUN,0,OK"
                ///
            }
            ///base.ChangFormateDataRecive(mComPC1_Sfit.OnSendPortCommand(this.SetFormate));
            /// 
            return this.PartResult;
            ///
            //if (this.PartResult)
            //{
            //    Dev_AppMachine.Machine.This.PartJigColSfit1.PartJigViews[this.StationIndex].CDPlayer.IsProcess = AppMachine.AppResult.Part.Process.Finnish;
            //}
        }

        [StateMachineEnabled]
        public void BuildCerrentResultPart()
        {
            var part = Dev_AppMachine.Machine.This.PartJigColSfit1.PartJigViews[this.StationIndex];
            //if (mPartList.Count != 0)
            //{
            //Dev_AppMachine.Machine.This.PartJigColSfit1.PartJigViews.Select(x =>
            //{
            if (part.CDPlayer.IsProcess == AppMachine.AppResult.Part.Process.Finnish)
            {
                //CalculateTrayCycleTime(part.CDPlayer.StartTimeCapture);
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
            Dev_AppMachine.Machine.This.PartJigColSfit1.PartJigViews[this.StationIndex].CDPlayer.Data2DCode = "";
            ///
            Dev_AppMachine.Machine.This.PartJigColSfit1.PartJigViews[this.StationIndex].CDPlayer.IsProcess = AppMachine.AppResult.Part.Process.Null;
            ///
            Dev_AppMachine.Machine.This.PartJigColSfit1.PartJigViews[this.StationIndex].CDPlayer.PartStatus = "N/A";
        }
        //public void DisattachResultt()
        //{

            //}
            /// <summary>
            /// 
            /// </summary>
        [StateMachineEnabled]
        public void GetData2DCodeByJig()
        {
            this.Data2Dcode = Dev_AppMachine.Machine.This.PartJigColSfit1.PartJigViews[this.StationIndex].CDPlayer.Data2DCode;
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
                    Dev_AppMachine.Machine.This.PartJigColSfit1.PartJigViews[this.StationIndex].CDPlayer.IsProcess = AppMachine.AppResult.Part.Process.Empty;
                    break;
                case "T":
                    Dev_AppMachine.Machine.This.PartJigColSfit1.PartJigViews[this.StationIndex].CDPlayer.IsProcess = AppMachine.AppResult.Part.Process.Testting;
                    break;
                case "F":
                    Dev_AppMachine.Machine.This.PartJigColSfit1.PartJigViews[this.StationIndex].CDPlayer.IsProcess = AppMachine.AppResult.Part.Process.Finnish;
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
            Dev_AppMachine.Machine.This.PartJigColSfit1.PartJigViews[this.StationIndex].CDPlayer.PartStatus = (this.PartResult == true) ? "OK" : "NG";
        }

    }
}
