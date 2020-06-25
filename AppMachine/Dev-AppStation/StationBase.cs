
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
        int msec = 35;
        public enum SetCmdSend
        {
            NONE,
            START,
            STATUS,
            ERROR
        }

        [XmlIgnore]
        public SetCmdSend CmdSetProcess
        {
            [StateMachineEnabled]
            get 
            {
                X_CoreS.Delay(msec);
                return GetPropValue(() => CmdSetProcess, SetCmdSend.NONE); 
            }
            [StateMachineEnabled]
            set { SetPropValue(() => CmdSetProcess, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [Browsable(true)]
        [Category("Station")]
        [XmlIgnore]
        public string Data2Dcode
        {
            [StateMachineEnabled]
            get 
            {
                X_CoreS.Delay(msec);
                return GetPropValue(() => Data2Dcode, "JEERAWATPRECHANURAK");
            }
            [StateMachineEnabled]
            set { SetPropValue(() => Data2Dcode, value); }
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
        [Browsable(true)]
        [Category("Station")]
        [XmlIgnore]
        public virtual bool IsStartProcess
        {
            [StateMachineEnabled]
            get 
            {
                X_CoreS.Delay(msec);
                return GetPropValue(() => IsStartProcess, false); 
            }
            
            [StateMachineEnabled]
            set { SetPropValue(() => IsStartProcess, value); }
        }
        [Browsable(true)]
        [Category("Station")]
        [XmlIgnore]
        public virtual bool IsTestting
        {
            [StateMachineEnabled]
            get 
            {
                X_CoreS.Delay(msec);
                return GetPropValue(() => IsStartProcess, false); 
            }

            [StateMachineEnabled]
            set { SetPropValue(() => IsStartProcess, value); }
        }
        [Browsable(true)]
        [Category("Station")]
        [XmlIgnore]
        public virtual bool IsTestFinnish
        {
            [StateMachineEnabled]
            get {
                X_CoreS.Delay(msec);
                return GetPropValue(() => IsStartProcess, false); 
            }
        }
        //[Browsable(true)]
        //[Category("Station")]
        [XmlIgnore]
        public string StationID
        {
            [StateMachineEnabled]
            get 
            {
                X_CoreS.Delay(msec);
                return GetPropValue(() => StationID, "1"); 
            }
            [StateMachineEnabled]
            set { SetPropValue(() => StationID, value); }
        }
        [Browsable(true)]
        [Category("Station")]
        [XmlIgnore]
        public int EndStationIndex
        {
            [StateMachineEnabled]
            get 
            {
                X_CoreS.Delay(msec);
                return GetPropValue(() => EndStationIndex,0); 
            }
            [StateMachineEnabled]
            set { SetPropValue(() => EndStationIndex, value); }
        }
        [Browsable(true)]
        [Category("Station")]
        [XmlIgnore]
        public int StationIndex
        {
            [StateMachineEnabled]
            get 
            {
                X_CoreS.Delay(msec);
                return GetPropValue(() => StationIndex, 0); 
            }
            [StateMachineEnabled]
            set { SetPropValue(() => StationIndex, value); }
        }

        [Browsable(true)]
        [Category("Station")]
        [XmlIgnore]
        public bool IsEndStationIndex
        {
            [StateMachineEnabled]
            get 
            {
                X_CoreS.Delay(msec);
                return GetPropValue(() => IsEndStationIndex, false); 
            }
            [StateMachineEnabled]
            set { SetPropValue(() => IsEndStationIndex, value); }
        }
        [Browsable(true)]
        [Category("Station")]
        [XmlIgnore]
        public int JigNumber
        {
            [StateMachineEnabled]
            get 
            {
                X_CoreS.Delay(msec);
                return GetPropValue(() => JigNumber, 1); 
            }
            [StateMachineEnabled]
            set { SetPropValue(() => JigNumber, value); }
        }
        [Browsable(true)]
        [Category("Station")]
        [XmlIgnore]
        public string SetFormate
        {
            [StateMachineEnabled]
            get 
            {
                X_CoreS.Delay(msec);
                return GetPropValue(() => SetFormate);
            }
            [StateMachineEnabled]
            set { SetPropValue(() => SetFormate, value); }
        }
        [Browsable(true)]
        [Category("Station")]
        [XmlIgnore]
        public bool PartResult
        {
            [StateMachineEnabled]
            get 
            {
                X_CoreS.Delay(msec);
                return GetPropValue(() => PartResult); 
            }
            [StateMachineEnabled]
            set { SetPropValue(() => PartResult, value); }
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
            this.StationIndex += 1;
            ///
            this.JigNumber = this.StationIndex;
            ///
            this.IsEndStationIndex = (this.StationIndex >= this.EndStationIndex) ? true : false;

        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public virtual void StationInitialize()
        {
            this.JigNumber = 1;
            this.StationIndex = 0;
        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public virtual void StationReset()
        {
            this.Data2Dcode = null;
            this.SetFormate = null;
            this.JigNumber = 0;
            this.StationIndex = 0;
        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        protected virtual void SetCmdTestJig(string setProcess)
        {
            var cmd = new JigCommand();
            ///
            try
            {
                if(string.IsNullOrEmpty(setProcess))
                    ///
                    throw new X_CoreExceptionError($"Can't is data");

                if (StationID == null || !X_Core.X_CoreS.IsNumber(this.JigNumber.ToString()))
                    ///
                    throw new X_CoreExceptionError($"Can't is data numeric");
                ///
                //else if (string.IsNullOrEmpty(this.JigData2Dcode) || (this.JigData2Dcode.Length < 20))
                //    ///
                //    throw new X_CoreExceptionError($"Input parameter jig2DCode is null");
                ///
                else
                {
                    cmd.CmdSend["PCSation"] = this.StationID;
                    cmd.CmdSend["JigNo"] = this.JigNumber.ToString();
                    cmd.CmdSend["Jig2DCode"] = this.Data2Dcode.ToUpper();
                    cmd.CmdSend["JigProcess"] = setProcess;
                    ///
                    SetFormate = ConvertToFormat(cmd);
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
        /// <param name="cmdformate"></param>
        protected virtual bool ChangFormateDataRecive(string cmdformate)
        {
            var cmd = new JigCommand();
            ///
            try
            {
                if (string.IsNullOrEmpty(cmdformate)) throw new X_CoreExceptionError($"Can't is data");
                ///
                string[] Word = cmdformate.Split(',');
                ///
                if (StationID == null || !X_Core.X_CoreS.IsNumber(this.JigNumber.ToString()))
                    ///
                    throw new X_CoreExceptionError($"Can't is data numeric");
                ///
                else if (Word.Length != 6)
                    ///
                    throw new X_CoreExceptionError($"Can't is data");
                ///
                else
                {
                    int i = 0;
                    foreach (string key in cmd.CmdRecive.Keys.ToList())
                        cmd.CmdRecive[key] = Word[i++];
                    ///
                  return cmd.CmdRecive.Where(x =>
                    {
                        if (x.Key.Contains("JigResult"))
                        {
                            if (x.Value == null)
                                return false;
                            return (x.Value == "PASS") ? true : false;
                        }
                        else
                            return false;
                    }).Any();
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
        protected string ConvertToFormat(JigCommand cmd)
        {
            if (cmd.CmdSend == null) return null;
            //this._aoiBoatheader.ForEach(x => csv.Append(string.Format("{0},", x)));
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
