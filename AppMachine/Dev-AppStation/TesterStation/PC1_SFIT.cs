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
        int msec = 30;
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
        //[XmlIgnore]
        //public bool IsStartProcess
        //{
        //    [StateMachineEnabled]
        //    get
        //    {
        //        X_CoreS.Delay(msec);
        //        ///
        //        if (string.IsNullOrEmpty(this.JigData2Dcode) || this.JigData2Dcode.Length < 20)
        //            return false;

        //        return true;
        //    }
        //}
        [XmlIgnore]
        public bool IsJigTestFinnish
        {
            get
            {
                X_CoreS.Delay(msec);
                return false;
            }
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
            //this.IsStartProcess = new Dictionary<string, string>()
            //{
            //    {"1","STOP" },
            //    {"2","STOP" },
            //    {"3","STOP" },
            //    {"4","STOP" },
            //};
        }
        [StateMachineEnabled]
        public override void StationInitialize()
        {
            this.StationID = "1";
            var dd = this.IsStartProcess;
        }
        /// <summary>
        /// 
        /// </summary>
        public override void SetStartTestJig()
        {
            base.SetStartTestJig();
            ///
            mComPC1_Sfit.OnSendPortCommand(this.JigFormateCmd);
        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public void GetData2DCodeByJig()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public void SetDisplayStatus()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public void WriteDataResultToMem()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public void SetMemJigTestFinnish()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public void GetAllDataResultJig()
        {

        }
    }
}
