
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
        protected bool simulater = true;
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
                return GetPropValue(() => StationID, "01"); 
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
        //[Browsable(true)]
        //[Category("Station")]
        //[XmlIgnore]
        //public string SetFormate
        //{
        //    [StateMachineEnabled]
        //    get 
        //    {
        //        X_CoreS.Delay(msec);
        //        return GetPropValue(() => SetFormate);
        //    }
        //    [StateMachineEnabled]
        //    set { SetPropValue(() => SetFormate, value); }
        //}
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
            mComPLCLink = X_CoreS.GetComponent(Dev_AppMachine.StaticName.PLC_TATTURN) as Dev_Component.ComuPLCLink;
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
        private Dev_Component.ComuPLCLink mComPLCLink = null;
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public virtual void StationInitialize()
        {
            this.JigNumber = 1;
            this.StationIndex = 0;
          
            //this.mPartList = new List<string>();
        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public virtual void StationReset()
        {
            this.Data2Dcode = null;
            //this.SetFormate = null;
            this.JigNumber = 0;
            this.StationIndex = 0;
        }
        public enum JIG
        {
            START,
            STOP,
            RE_TEST,
            JIG_NOT_USE
        };
        public string[] process = new string[]
        {
            "01",//Start
            "02",//Stop
            "03",//RE-Test
            "99",//Jig Not Use
        };
        //[StateMachineEnabled]
        public virtual double CalculateTrayCycleTime(DateTime timeCapture)
        {
            DateTime current = DateTime.Now;
            ///
            TimeSpan diff = current - timeCapture;//this.TrayCycleTime
            ///
            return (double)diff.TotalMilliseconds / 1000;
        }
        public virtual bool GetStatusTestJig()
        {
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memWord"></param>
        /// <param name="strBin"></param>
        /// <returns></returns>
        public SequenceError SetJigResultMemory(string memWord, string strDec)
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
                return SequenceError.SetConfRead2DCode;

            }

            //}
        }
        public void Bin16_DataOut(string strBin,int lenght ,out string strOut)
        {
            strOut = "";
            ///
            if (strBin.Length != lenght)
            {
                var num = strBin.Length;
                ///
                for (int i = 0; i < (lenght - num); i++)
                    ///
                    strBin = string.Format("{0}{1}", "0", strBin);
                ///
                strOut = strBin;
                    
            }
            else strOut = strBin;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public SequenceError GetStatusMemory(string strMemFinnish, out string[] result)
        {
            result = null;
            ///
            int i = 1;
            ///
            try
            {
                lock (this)
                {


                    if (string.IsNullOrEmpty(strMemFinnish))
                        throw new Exception("GetPartModels:> IsNullOrEmpty");

                    ///
                    this.mComPLCLink.ReadDeviceRandom2(new System.Windows.Forms.TextBox() { Text = strMemFinnish }, i.ToString(), out result);//20 word size
                    ///
                    return SequenceError.Normal;
                }
            }
            catch (Exception ex)
            {
                X_CoreS.LogError(ex, $"TimeOut waiting for GetPartModel of'{this.Nickname}'");
                return SequenceError.GetPartModel;
            }
        }
        public SequenceError SetResult_StrToPLC(string WordStart = "D1000", string strResult = "", int ELEMENT_SIZE_WORD = 1)
        {
            try
            {
                lock (this)
                {

                    //set
                    //{
                    //var result = (status == true) ? 1 : 0;

                    this.mComPLCLink.WriteMuiltiWordData(WordStart, strResult, ELEMENT_SIZE_WORD);
                    //}
                    return SequenceError.Normal;
                }
            }
            catch (Exception ex)
            {
                X_CoreS.LogError(ex, $"TimeOut waiting for GetPartModel of'{this.Nickname}'");
                ///
                return SequenceError.SetResultPartToPLC;
            }
        }
        public void BinToDecPartFinnish(char[] charBin, string strMemControlPart)
        {
            //Array.Reverse(charBin);
            ///
            string strDec = new string(charBin);
            ///
            int outputfinnish = Convert.ToInt32(strDec, 2);

            string strMem = string.Format("{0}{1}", "R", strMemControlPart);
            ///
            if (SetJigResultMemory(strMem, outputfinnish.ToString()) != SequenceError.Normal)
            {
                X_CoreS.LogAlarmPopup("Plc Wtite result error!", $"TimeOut waiting for SetStatusProcess of'{this.Nickname}'");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        protected virtual string CmdSetTestJig(JIG jigProcess,out string cmdSet)
        {
            var cmd = new JigCommand(); cmdSet = "";
            ///
            try
            {
                //if(string.IsNullOrEmpty(jigProcess))
                //    ///
                //    throw new X_CoreExceptionError($"Can't is data");

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
                    cmd.SendCmdSet["PCSation"] = this.StationID;
                    cmd.SendCmdSet["JigNo"] = this.JigNumber.ToString();
                    cmd.SendCmdSet["Jig2DCode"] = this.Data2Dcode.ToUpper();
                    cmd.SendCmdSet["JigProcess"] = process[(int)jigProcess];                  
                    ///
                    string SetFormate = ConvertToFormat(cmd);
                    byte[] str = Encoding.ASCII.GetBytes(SetFormate);
                    string CRC8 = crc8(str).ToString();
                    ///
                    cmdSet = string.Format("{0},{1}", SetFormate, CRC8);
                    cmd.SendCmdSet["CRC8"] = CRC8;
                    ///
                    return CRC8;
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
        [StateMachineEnabled]
        protected virtual string CmdGetStatusJig(out string cmdSet)
        {
            var cmd = new JigCommand(); cmdSet = "";
            ///
            try
            {
                //if(string.IsNullOrEmpty(jigProcess))
                //    ///
                //    throw new X_CoreExceptionError($"Can't is data");

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
                    cmd.SendCmdGet["PCSation"] = this.StationID;
                    cmd.SendCmdGet["JigNo"] = this.JigNumber.ToString();
                    cmd.SendCmdGet["Status"] = this.Data2Dcode.ToUpper();
                    
                    ///
                    string SetFormate = ConvertToFormat(cmd);
                    byte[] str = Encoding.ASCII.GetBytes(SetFormate);
                    string CRC8 = crc8(str).ToString();
                    ///
                    cmdSet = string.Format("{0},{1}", SetFormate, CRC8);
                    cmd.SendCmdSet["CRC8"] = CRC8;
                    ///
                    return CRC8;
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
        /// <param name="str"></param>
        /// <returns></returns>
        public byte crc8(byte[] str) // no need to pass length in 'safe' code
        {
            int len = str.Length;
            int i, f;
            byte data;
            byte crc;
            crc = 0;
            int j = 0;

            while (len-- != 0)
            {
                data = str[j++];
                for (i = 0; i < 8; i++)
                {
                    f = 1 & (data ^ crc);
                    crc >>= 1;
                    data >>= 1;
                    if (f != 0)
                    {
                        crc ^= 0x8c;
                    }
                }
            }

            return crc;
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
                    foreach (string key in cmd.SendCmdGet.Keys.ToList())
                        cmd.SendCmdGet[key] = Word[i++];
                    ///
                  return cmd.SendCmdGet.Where(x =>
                    {
                        if (x.Key.Contains("JigResult"))
                        {
                            if (x.Value == null)
                                return false;
                            return (x.Value == "OK") ? true : false;
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
            if (cmd.SendCmdSet == null) return null;
            //this._aoiBoatheader.ForEach(x => csv.Append(string.Format("{0},", x)));
            string str = null;
            ///
            cmd.SendCmdSet.All(x =>
            {
                str += (x.Value + ",");
                return true;
            }).ToString();
            ///
            return str.Substring(0, str.Length - 1);
        }
    }
}
