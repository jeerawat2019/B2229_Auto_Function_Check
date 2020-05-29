using Pc2Pc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [XmlIgnore]
        public string JigData2Dcode 
        {
            [StateMachineEnabled]
            get { return GetPropValue(() => JigData2Dcode, null); }
            [StateMachineEnabled]
            set { SetPropValue(() => JigData2Dcode, value); }
        }
        [XmlIgnore]
        public string IsStartProcessTurnOn
        {
            [StateMachineEnabled]
            get { return GetPropValue(() => IsStartProcessTurnOn, null); }
            [StateMachineEnabled]
            set { SetPropValue(() => IsStartProcessTurnOn, value); }
        }
        [XmlIgnore]
        public string mStationID
        {
            [StateMachineEnabled]
            get { return GetPropValue(() => mStationID, null); }
            [StateMachineEnabled]
            set { SetPropValue(() => mStationID, value); }
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
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pcStstionNo"></param>
        /// <param name="jigNo"></param>
        /// <returns></returns>
        public JigCommand JigTest(string jigNo ="1",string process =null)
        {
            var cmd = new JigCommand();
            ///
            try
            {
                if (mStationID == null || !X_Core.X_CoreS.IsNumber(jigNo))
                    ///
                    throw new X_CoreExceptionError($"Can't is data numeric");
                ///
                else if (string.IsNullOrEmpty(JigData2Dcode) || (JigData2Dcode.Length < 20))
                    ///
                    throw new X_CoreExceptionError($"Input parameter jig2DCode is null");
                ///
                else
                {
                    cmd.CmdSend["PCSation"] = mStationID;
                    cmd.CmdSend["JigNo"] = jigNo;
                    cmd.CmdSend["Jig2DCode"] = JigData2Dcode.ToUpper();
                    cmd.CmdSend["JigProcess"] = process.ToUpper();
                    return cmd;
                }
            }
            catch (Exception ex)
            {
                throw new X_CoreExceptionPopup(ex, $"String Format fail'{this.Nickname}'");
            }

        }
    }
}
