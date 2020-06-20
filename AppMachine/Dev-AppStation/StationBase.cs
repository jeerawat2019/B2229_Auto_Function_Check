
using B2229_AT_FuncCheck.Dev_Component;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using X_Core;
using X_Core.CompElement;
using X_Core.ControlElement;
using X_Unit;

namespace B2229_AT_FuncCheck.Dev_AppStation
{
    public class StationBase : CompBase,IStation
    {
        public enum JigStatus
        {
            Empty,      //Wait load part jig
            Process,    //Runing Process
            Finnish     //Process Finnish wait work load out
        }
        public enum JigProcess
        {
            None,
            Start,
            Stop
        }
        [XmlIgnore]
        public JigProcess JigControlProcess
        {
            [StateMachineEnabled]
            get { return GetPropValue(() => JigControlProcess, JigProcess.None); }
            [StateMachineEnabled]
            set { SetPropValue(() => JigControlProcess, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        //[Browsable(true)]
        //[Category("Station")]
        [XmlIgnore]
        public string JigData2Dcode 
        {
            [StateMachineEnabled]
            get { return GetPropValue(() => JigData2Dcode, "JEERAWATPRECHANURAK"); }
            [StateMachineEnabled]
            set { SetPropValue(() => JigData2Dcode, value); }
        }

        //[Browsable(true)]
        //[Category("Station")]
        //[XmlIgnore]
        //public string IsStartProcessTurnOn
        //{
        //    [StateMachineEnabled]
        //    get { return GetPropValue(() => IsStartProcessTurnOn, null); }
        //    [StateMachineEnabled]
        //    set { SetPropValue(() => IsStartProcessTurnOn, value); }
        //}
        //[Browsable(true)]
        //[Category("Station")]
        [XmlIgnore]
        public Dictionary<string,string> IsStartProcess
        {
            [StateMachineEnabled]
            get { return GetPropValue(() => IsStartProcess, new Dictionary<string, string>()
            {
                {"1","STOP" },
                {"2","STOP" },
                {"3","STOP" },
                {"4","STOP" },
            });}

            [StateMachineEnabled]
            set { SetPropValue(() => IsStartProcess, value); }
        }
        //[Browsable(true)]
        //[Category("Station")]
        [XmlIgnore]
        public string StationID
        {
            [StateMachineEnabled]
            get { return GetPropValue(() => StationID, "1"); }
            [StateMachineEnabled]
            set { SetPropValue(() => StationID, value); }
        }
        [XmlIgnore]
        public int EndCountJig
        {
            [StateMachineEnabled]
            get { return GetPropValue(() => EndCountJig, 0); }
            [StateMachineEnabled]
            set { SetPropValue(() => EndCountJig, value); }
        }
        [XmlIgnore]
        public int JigCounter
        {
            [StateMachineEnabled]
            get { return GetPropValue(() => JigCounter, 0); }
            [StateMachineEnabled]
            set { SetPropValue(() => JigCounter, value); }
        }
        [XmlIgnore]
        public bool IsEndJigCount
        {
            [StateMachineEnabled]
            get { return GetPropValue(() => IsEndJigCount, false); }
            [StateMachineEnabled]
            set { SetPropValue(() => IsEndJigCount, value); }
        }
        [Browsable(true)]
        [Category("Station")]
        [XmlIgnore]
        public int JigNumber
        {
            [StateMachineEnabled]
            get { return GetPropValue(() => JigNumber, 1); }
            [StateMachineEnabled]
            set { SetPropValue(() => JigNumber, value); }
        }
        [Browsable(true)]
        [Category("Station")]
        [XmlIgnore]
        public string JigFormateCmd
        {
            [StateMachineEnabled]
            get { return GetPropValue(() => JigFormateCmd); }
            [StateMachineEnabled]
            set { SetPropValue(() => JigFormateCmd, value); }
        }
       
        /// <summary>
        /// 
        /// </summary>
        public StationBase() : base() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public StationBase(string name) : base(name) { }
        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }
        /// <summary>
        /// 
        /// </summary>
        public override void InitializeIDReferences()
        {
            base.InitializeIDReferences();
            ///
        }
        [StateMachineEnabled]
        public virtual void InCrementJig()
        {
            ///
            this.JigCounter += 1;
            ///
            this.IsEndJigCount = (this.JigCounter >= this.EndCountJig) ? true : false;

        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public virtual void StationInitialize()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public virtual void ClearStation()
        {
            this.JigData2Dcode = null;
            this.JigFormateCmd = null;
            this.JigNumber = 0;
        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public virtual void SetStartTestJig()
        {
            var cmd = new JigCommand();
            ///
            try
            {
                if (StationID == null || !X_Core.X_CoreS.IsNumber(this.JigNumber.ToString()))
                    ///
                    throw new X_CoreExceptionError($"Can't is data numeric");
                ///
                else if (string.IsNullOrEmpty(this.JigData2Dcode) || (this.JigData2Dcode.Length < 20))
                    ///
                    throw new X_CoreExceptionError($"Input parameter jig2DCode is null");
                ///
                else
                {
                    cmd.CmdSend["PCSation"] = this.StationID;
                    cmd.CmdSend["JigNo"] = this.JigNumber.ToString();
                    cmd.CmdSend["Jig2DCode"] = this.JigData2Dcode.ToUpper();
                    cmd.CmdSend["JigProcess"] = "START";
                    ///
                    JigFormateCmd = ConvertToFormat(cmd);
                    ///
                }

            }
            catch (Exception ex)
            {
                throw new X_CoreExceptionPopup(ex, $"String Format fail'{this.Nickname}'");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private string ConvertToFormat(JigCommand cmd)
        {
            string str = null;
            ///
            cmd.CmdSend.All(x =>
            {
                str += (x.Value + ",");
                return true;
            }).ToString();
            ///
            return str.Substring(0, str.Length - 1);
        }
    }
}
