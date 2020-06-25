using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using X_Core;

namespace B2229_AT_FuncCheck.Dev_AppStation.TesterStation
{
    public class PC5_WD : StationBase
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
                if (Dev_AppMachine.Machine.This.PartJigColWDView.PartJigViews == null) return false;
                ///
                if (this.StationIndex < 0 || this.StationIndex > Dev_AppMachine.Machine.This.PartJigColWDView.PartJigViews.Length) return false;
                ///
                bool result = (Dev_AppMachine.Machine.This.PartJigColWDView.PartJigViews[this.StationIndex].CDPlayer.IsProcess
                    == AppMachine.AppResult.Part.Process.Start) ? true : false;
                if (result)
                {
                    Dev_AppMachine.Machine.This.PartJigColWDView.PartJigViews[this.StationIndex].CDPlayer.IsProcess
                        = AppMachine.AppResult.Part.Process.Testting;
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
                if (Dev_AppMachine.Machine.This.PartJigColWDView.PartJigViews == null) return false;
                ///
                if (this.StationIndex < 0 || this.StationIndex > Dev_AppMachine.Machine.This.PartJigColWDView.PartJigViews.Length) return false;
                ///
                return (Dev_AppMachine.Machine.This.PartJigColWDView.PartJigViews[this.StationIndex].CDPlayer.IsProcess
                    == AppMachine.AppResult.Part.Process.Testting) ? true : false;
            }
        }
        [XmlIgnore]
        public override bool IsTestFinnish
        {
            [StateMachineEnabled]
            get
            {
                X_CoreS.Delay(msec);
                ///
                if (Dev_AppMachine.Machine.This.PartJigColWDView.PartJigViews == null) return false;
                ///
                if (this.StationIndex < 0 || this.StationIndex > Dev_AppMachine.Machine.This.PartJigColWDView.PartJigViews.Length) return false;
                ///
                else
                    return SetCmdStatusTestJig();
            }
        }
        public PC5_WD() { }
        public PC5_WD(string name) : base(name) { }
        public override void Initialize()
        {
            base.Initialize();
           
        }
        private Dev_Component.ComuPCLink mComPC3_Anging = null;
        public override void InitializeIDReferences()
        {
            base.InitializeIDReferences();
            ///
            mComPC3_Anging = X_CoreS.GetComponent(Dev_AppMachine.StaticName.T_ANGIN_NO01_NO13) as Dev_Component.ComuPCLink;
            ///
            this.EndStationIndex = 1;
        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public override void StationInitialize()
        {
            this.StationID = "WD";
        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public void SetCmdStartTestJig()
        {
            base.SetCmdTestJig("START");
            ///
            mComPC3_Anging.OnSendPortCommand(this.SetFormate);
            ///
            Dev_AppMachine.Machine.This.PartJigColWDView.PartJigViews[this.StationIndex].CDPlayer.IsProcess = AppMachine.AppResult.Part.Process.Testting;
        }

        public bool SetCmdStatusTestJig()
        {
            //base.SetCmdTestJig("STATUS");
            ///
            this.PartResult = base.ChangFormateDataRecive("GET-A,1,1,RUN,0,PASS");
            ///
            ///base.ChangFormateDataRecive(mComPC1_Sfit.OnSendPortCommand(this.SetFormate));
            /// 
            return this.PartResult;
            //if (result)
            //{
            //    Dev_AppMachine.Machine.This.PartJigColWDView.PartJigViews[this.StationIndex].CDPlayer.IsProcess = AppMachine.AppResult.Part.Process.Finnish;
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public void GetData2DCodeByJig()
        {
            this.Data2Dcode = Dev_AppMachine.Machine.This.PartJigColWDView.PartJigViews[this.StationIndex].CDPlayer.Data2DCode;
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
                    Dev_AppMachine.Machine.This.PartJigColWDView.PartJigViews[this.StationIndex].CDPlayer.IsProcess = AppMachine.AppResult.Part.Process.Empty;
                    break;
                case "T":
                    Dev_AppMachine.Machine.This.PartJigColWDView.PartJigViews[this.StationIndex].CDPlayer.IsProcess = AppMachine.AppResult.Part.Process.Testting;
                    break;
                case "F":
                    Dev_AppMachine.Machine.This.PartJigColWDView.PartJigViews[this.StationIndex].CDPlayer.IsProcess = AppMachine.AppResult.Part.Process.Finnish;
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
            Dev_AppMachine.Machine.This.PartJigColWDView.PartJigViews[this.StationIndex].CDPlayer.PartStatus = (this.PartResult == true) ? "OK" : "NG";
        }
    }
}

