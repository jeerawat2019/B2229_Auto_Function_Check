using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_Core.CompElement;
using AiComp.ConnectType.Commu;
using AiComp.Misubishis.Divice.PLC;
using System.Threading;
using X_Core;

namespace B2229_AT_FuncCheck.Dev_Component
{
    public class ComuPLCLink : PLC_Builder
    {

        public ComuPLCLink() { }
        public ComuPLCLink(string name) : base(name) { }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void InitializeIDReferences()
        {
            base.InitializeIDReferences();
        }

        ///public PLC_Commu PLC_Commu { get; set; } = new PLC_Commu();
        /// <summary>
        /// 
        /// </summary>
        //public static Dictionary<string, string> Q02UCDP_MemConfig_Tray { get; set; }
        /// <summary>
        /// <summary>
        /// 
        /// </summary>
        public bool IsPlcConnect
        {

            get;
            set;
        }
        ///// <summary>
        ///// Read Data 2DCode From PLC Start Address D7000..D7150
        ///// </summary>
        ///// 

        public virtual SequenceError GetData2DCode(string GetData2DCode, out List<string> Result)
        {
            Result = null;

            try
            {
                lock (objLock)
                {
                    ///

                    string sValue;
                    string Datatrim;
                    ///
                    //if (!IsPlcConnect)
                    //    return iError.IsPlcConnect;
                    ///
                    List<string> listValue = new List<string>();
                    ///
                    this.ReadMuiltiWordData(10, GetData2DCode, out sValue);//450 Word [1Data = 2D-Code =>11 WORD]
                                                                            //string[] ListData;
                    Datatrim = sValue.Trim(new char[] { '\0', ',' });

                    //SimulationData2DCode  BEFORE \r  Test \0
                    Result = Datatrim.Split(new char[] { '\r' }).ToList().Select(x =>
                    {
                        if (string.IsNullOrEmpty(x) || x.Contains("NGERROR") || x.Contains("[null]") || x.Contains("ERROR"))
                            return "NG-ERROR";
                        return x.Substring(0, x.IndexOf(':'));
                    }).ToList();


                    ///
                    return SequenceError.Normal;
                }
            }
            catch (Exception ex)
            {
                
                X_CoreS.LogError(ex, $"TimeOut waiting for Get2DCode of'{this.Nickname}'");
                return SequenceError.SetConfRead2DCode;
            }

        }
        //Input Zone 1 Form PLC Start Address D8600..D8609
        public SequenceError IsData2DReady(string IsData2DReady, out bool result)
        {
            result = false;
            ///
            Thread.Sleep(50);
            try
            {
                lock (objLock)
                {
                    ///
                    int i = 1;
                    if (!IsPlcConnect)
                        return SequenceError.IsPlcConnect;
                    string[] strOut;
                    ///
                    this.ReadDeviceRandom2(new System.Windows.Forms.TextBox() { Text = IsData2DReady }, i.ToString(), out strOut);
                    ///
                    result = (int.Parse(strOut[i - 1]) == 1) ? true : false;
                    ///
                    return SequenceError.Normal;
                }
            }
            catch (Exception ex)
            {
                
                X_CoreS.LogError(ex, $"TimeOut waiting for IsData2DReady of'{this.Nickname}'");
                return SequenceError.SetConfRead2DCode;
            }
        }

        public SequenceError IsConfSetResultPartToPLC(string IsConfSetResultPartToPLC, out bool result)
        {
            result = false;
            ///
            Thread.Sleep(50);
            try
            {
                lock (objLock)
                {
                    ///
                    int i = 1;
                    if (!IsPlcConnect)
                        return SequenceError.IsPlcConnect;
                    ///
                    string[] strOut;
                    ///
                    this.ReadDeviceRandom2(new System.Windows.Forms.TextBox() { Text = IsConfSetResultPartToPLC }, i.ToString(), out strOut);
                    ///
                    result = (int.Parse(strOut[i - 1]) == 1) ? true : false;
                    ///
                    return SequenceError.Normal;
                }
            }
            catch (Exception ex)
            {
                X_CoreS.LogError(ex, $"TimeOut waiting for IsConfSetResultPartToPLC of'{this.Nickname}'");
                return SequenceError.IsConfSetResultPartToPLC;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IsTrayInPosition"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public SequenceError IsTrayInPosition(string IsTrayInPosition, out bool result)
        {
            result = false;
            ///
            Thread.Sleep(50);
            try
            {
                lock (objLock)
                {
                    ///
                    int i = 1;
                    if (!IsPlcConnect)
                        return SequenceError.IsPlcConnect;
                    ///
                    string[] strOut;
                    ///
                    this.ReadDeviceRandom2(new System.Windows.Forms.TextBox() { Text = IsTrayInPosition }, i.ToString(), out strOut);
                    ///
                    result = (int.Parse(strOut[i - 1]) == 1) ? true : false;
                    ///
                    return SequenceError.Normal;
                }
            }
            catch (Exception ex)
            {
                X_CoreS.LogError(ex, $"TimeOut waiting for IsTrayInPosition of'{this.Nickname}'");
                return SequenceError.IsTrayInPosition;
            }

        }
        public SequenceError IsTrayOutPosition(string IsTrayOutPosition, out bool result)
        {
            result = false;
            ///
            Thread.Sleep(50);
            ///
            try
            {
                lock (objLock)
                {

                    int i = 1;
                    if (!IsPlcConnect)
                        return SequenceError.IsPlcConnect;
                    ///
                    string[] strOut;
                    ///
                    this.ReadDeviceRandom2(new System.Windows.Forms.TextBox() { Text = IsTrayOutPosition }, i.ToString(), out strOut);
                    ///
                    result = (int.Parse(strOut[i - 1]) == 1) ? true : false;
                    ///
                    return SequenceError.Normal;
                }
            }
            catch (Exception ex)
            {
                X_CoreS.LogError(ex, $"TimeOut waiting for IsTrayOutPosition of'{this.Nickname}'");
                return SequenceError.IsTrayOutPosition;
            }

        }
        /// Write DataResult Start Address :D + 49 = D
        /// <summary>
        /// 
        /// </summary>
        public virtual SequenceError GetDataResultFormPLC(string GetDataResultFormPLC, out List<string> result)
        {
            result = null;
            try
            {
                lock (objLock)
                {
                    ///
                    int i = 0;
                    //if (!IsPlcConnect)
                    //    return iError.IsPlcConnect;
                    ///
                    string[] strOut; string sAddress = null;
                    ///
                    int j = GetDataResultFormPLC.IndexOf('D');
                    ///
                    var address = GetDataResultFormPLC.Substring(j + 1, GetDataResultFormPLC.Length - (j + 1));
                    ///
                    ListPCSetResultToPLC.ForEach((x) =>
                    {
                        if (i != ListPCSetResultToPLC.Count() - 1)
                        {
                            sAddress += string.Format("{0}" + "{1}" + "\n", "D", (int.Parse(address) + i).ToString());
                        }
                        else
                        {
                            sAddress += string.Format("{0}" + "{1}", "D", (int.Parse(address) + i).ToString());
                        }
                        i++;
                    });
                    ///
                    //PLC_Builder.ReadDeviceRandom2(new System.Windows.Forms.TextBox() { Text = sAddress }, "", out strOut);FUJ_DataTranfer.Properties.Settings.Default.IndexData
                    this.ReadDeviceRandom2(new System.Windows.Forms.TextBox() { Text = sAddress }, "", out strOut);

                    result = strOut.Select(x =>
                    {
                        if (int.Parse(x) == 1)
                        {
                            return "OK";
                        }
                        else if (int.Parse(x) == 2) { return "NG"; }
                        else if (int.Parse(x) == 3) { return "ERR 2DCODE"; }
                        else if (int.Parse(x) == 4) { return "ERR POSC."; }
                        else if (int.Parse(x) == 5) { return "ERR PICKUP"; }
                        else
                            return "NONE";
                        ///
                    }).ToList();

                    return SequenceError.Normal;
                }
            }
            catch (Exception ex)
            {
                X_CoreS.LogError(ex, $"TimeOut waiting for GetDataResultFormPLC of'{this.Nickname}'");
                return SequenceError.GetDataResultFormPLC;
            }
        }

        internal virtual SequenceError SetStatusProcess(string SetConfStartProcess, bool status)
        {
            try
            {
                lock (objLock)
                {
                    ///         
                    if (!IsPlcConnect)
                        return SequenceError.IsPlcConnect;
                    ///
                    var result = (status == true) ? 1 : 0;
                    ///
                    this.WriteDeviceRandom2(SetConfStartProcess, "1", new System.Windows.Forms.TextBox() { Text = result.ToString() });
                    ///
                    return SequenceError.Normal;
                }
            }
            catch (Exception ex)
            {
                X_CoreS.LogError(ex, $"TimeOut waiting for SetStatusProcess of'{this.Nickname}'");
                return SequenceError.SetConfRead2DCode;
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        internal virtual SequenceError GetPartTotal(string sPartTotal, out string result)
        {
            result = null;
            ///
            string[] strOut; int i = 1;

            try
            {
                lock (objLock)
                {

                    if (string.IsNullOrEmpty(sPartTotal))
                        throw new Exception("PartTotal:> IsNullOrEmpty");
                    ///
                    this.ReadDeviceRandom2(new System.Windows.Forms.TextBox() { Text = sPartTotal }, i.ToString(), out strOut);
                    ///
                    result = strOut[i - 1];
                    ///
                    return SequenceError.Normal;
                }
            }
            catch (Exception ex)
            {
                X_CoreS.LogError(ex, $"TimeOut waiting for SetStatusProcess of'{this.Nickname}'");
                return SequenceError.GetPartTotal;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        internal virtual SequenceError GetPartModel(string sGetPartModels, out string[] result)
        {
            result = null;
            ///
            int i = 1;
            ///
            try
            {
                lock (objLock)
                {

                    if (string.IsNullOrEmpty(sGetPartModels))
                        throw new Exception("GetPartModels:> IsNullOrEmpty");
                    ///         
                    if (!IsPlcConnect)
                        return SequenceError.IsPlcConnect;
                    ///
                    this.ReadDeviceRandom2(new System.Windows.Forms.TextBox() { Text = sGetPartModels }, i.ToString(), out result);//20 word size
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="result"></param>
        internal virtual SequenceError GetPLCStatusAutoRun(string sGetPLCStatusAutoRun, out string result)
        {
            result = null;
            ///
            string[] strOut; int i = 1;

            try
            {
                lock (objLock)
                {

                    if (string.IsNullOrEmpty(sGetPLCStatusAutoRun))
                        throw new Exception("PartTotal:> IsNullOrEmpty");
                    ///
                    this.ReadDeviceRandom2(new System.Windows.Forms.TextBox() { Text = sGetPLCStatusAutoRun }, i.ToString(), out strOut);
                    ///
                    result = strOut[i - 1];
                    ///
                    return SequenceError.Normal;
                }
            }
            catch (Exception ex)
            {
                X_CoreS.LogError(ex, $"TimeOut waiting for GetPLCStatusAutoRun of'{this.Nickname}'");
                return SequenceError.GetPartTotal;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="result"></param>
        internal virtual SequenceError GetConfStatusMode(string sGetPLCStatusAutoRun, out string result)
        {
            result = null;
            ///
            string[] strOut; int i = 1;

            try
            {
                lock (objLock)
                {

                    if (string.IsNullOrEmpty(sGetPLCStatusAutoRun))
                        throw new Exception("PartTotal:> IsNullOrEmpty");
                    ///
                    this.ReadDeviceRandom2(new System.Windows.Forms.TextBox() { Text = sGetPLCStatusAutoRun }, i.ToString(), out strOut);
                    ///
                    result = strOut[i - 1];
                    ///
                    return SequenceError.Normal;
                }
            }
            catch (Exception ex)
            {
                X_CoreS.LogError(ex, $"TimeOut waiting for GetConfStatusMode of'{this.Nickname}'");
                return SequenceError.GetPartTotal;
            }
        }
        public SequenceError SetConfStatusAuto(string SetConfRead2DCode, bool status)
        {

            try
            {
                lock (objLock)
                {
                    ///         
                    if (!IsPlcConnect)
                        return SequenceError.IsPlcConnect;
                    ///
                    var result = (status == true) ? 9 : 0;
                    ///
                    this.WriteDeviceRandom2(SetConfRead2DCode, "1", new System.Windows.Forms.TextBox() { Text = result.ToString() });
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
        //Output Zone 1 Form PC Start Address D8610..D8619
        public SequenceError SetConfRead2DCode(string SetConfRead2DCode, bool status)
        {

            try
            {
                lock (objLock)
                {
                    ///         
                    if (!IsPlcConnect)
                        return SequenceError.IsPlcConnect;
                    ///
                    var result = (status == true) ? 1 : 0;
                    ///
                    this.WriteDeviceRandom2(SetConfRead2DCode, "1", new System.Windows.Forms.TextBox() { Text = result.ToString() });
                    ///
                    return SequenceError.Normal;
                }
            }
            catch (Exception ex)
            {
                X_CoreS.LogError(ex, $"TimeOut waiting for SetConfRead2DCode of'{this.Nickname}'");
                return SequenceError.SetConfRead2DCode;

            }

            //}
        }
        public SequenceError SetResultPartToPLC(string SetResultPartToPLC, bool status)
        {
            try
            {
                lock (objLock)
                {
                    ///         
                    if (!IsPlcConnect)
                        return SequenceError.IsPlcConnect;
                    //set
                    //{
                    var result = (status == true) ? 1 : 0;

                    this.WriteDeviceRandom2(SetResultPartToPLC, "1", new System.Windows.Forms.TextBox() { Text = result.ToString() });
                    //}
                    return SequenceError.Normal;
                }
            }
            catch (Exception ex)
            {
                X_CoreS.LogError(ex, $"TimeOut waiting for SetResultPartToPLC of'{this.Nickname}'");
                return SequenceError.SetResultPartToPLC;
            }
        }

        public SequenceError SetPCErrorToPLC(string SetPCErrorToPLC, SequenceError iError)
        {

            try
            {
                lock (objLock)
                {
                    ///
                    var idec = (int)iError;
                    ///         
                    if (!IsPlcConnect)
                        return SequenceError.IsPlcConnect;
                    ///
                    //var result = (status == true) ? 1 : 0;
                    ///
                    this.WriteDeviceRandom2(SetPCErrorToPLC, "1", new System.Windows.Forms.TextBox() { Text = idec.ToString() });
                    ///
                    return SequenceError.Normal;
                }
            }
            catch (Exception ex)
            {
                X_CoreS.LogError(ex, $"TimeOut waiting for SetPCErrorToPLC of'{this.Nickname}'");
                return SequenceError.IsPlcConnect;

            }

            //}
        }
        public SequenceError SetCsvFileNameDownloadToPLC(string WordStart = "D1000", string strWriteWord = "", int ELEMENT_SIZE_WORD = 40)
        {
            try
            {
                lock (objLock)
                {
                    ///         
                    if (!IsPlcConnect)
                        return SequenceError.IsPlcConnect;
                    //set
                    //{
                    //var result = (status == true) ? 1 : 0;

                    this.WriteMuiltiWordData(WordStart, strWriteWord, ELEMENT_SIZE_WORD);
                    //}
                    return SequenceError.Normal;
                }
            }
            catch (Exception ex)
            {
                X_CoreS.LogError(ex, $"TimeOut waiting for GetPartModel of'{this.Nickname}'");
                return SequenceError.SetResultPartToPLC;
            }
        }
        public SequenceError SetCsvFileNameUploadToPLC(string WordStart = "D1000", string strWriteWord = "", int ELEMENT_SIZE_WORD = 40)
        {
            try
            {
                lock (objLock)
                {
                    ///         
                    if (!IsPlcConnect)
                        return SequenceError.IsPlcConnect;
                    //set
                    //{
                    //var result = (status == true) ? 1 : 0;

                    this.WriteMuiltiWordData(WordStart, strWriteWord, ELEMENT_SIZE_WORD);
                    //}
                    return SequenceError.Normal;
                }
            }
            catch (Exception ex)
            {
                X_CoreS.LogError(ex, $"TimeOut waiting for SetCsvFileNameUploadToPLC of'{this.Nickname}'");
                return SequenceError.SetResultPartToPLC;
            }
        }
        public List<string> ListPCSetResultToPLC
        {
            get;
            internal set;
        }




        ///// <summary>
        ///// 
        ///// </summary>
        //public override void StationInitialized()
        //{
        //    base.StationInitialized();
        //    ///
        //    mPLC_Commu = new PLC_Commu("Q03CPU", "PLC Data Builder");
        //    mPLC_Commu.Id = "1";
        //    mPLC_Commu.IPAddress = "192.168.1.20";
        //    mPLC_Commu.mActSupportMsgClass = new ActSupportMsgLib.ActSupportMsgClass();
        //    mPLC_Commu.mActUtlTypeClass = new ActUtlTypeLib.ActUtlTypeClass();
        //    //mPLC_Commu.mActMLProgTypeClass = new ActProgTypeLib.ActMLProgTypeClass();
        //    mPLC_Commu.iStstionNumber = 5;
        //    mPLC_Commu.mActUtlTypeClass.ActLogicalStationNumber = 0;

        //}

        //public void PLC_Connect(PLC_Commu PLC_Commu)
        //{
        //    IsPlcConnect = (this.AxActOpen(PLC_Commu) == 0) ? true : false;
        //}
        private static object objLock = new object();
        /// <summary>
        /// 
        /// </summary>
        public SequenceError PC_LinkCommu(string PC_LinkCommu)
        {
            int i = 1;
            ///
            try
            {
                lock (objLock)
                {

                    string[] strOut;
                    ///
                    if (!IsPlcConnect)
                        return SequenceError.IsPlcConnect;
                    ///
                    this.ReadDeviceRandom2(new System.Windows.Forms.TextBox() { Text = PC_LinkCommu }, i.ToString(), out strOut);
                    ///
                    var result = (int.Parse(strOut[i - 1]) > 0) ? 0 : 1;
                    ///
                    this.WriteDeviceRandom2(PC_LinkCommu, i.ToString(), new System.Windows.Forms.TextBox() { Text = result.ToString() });
                    ///
                    Thread.Sleep(0);
                    ///
                    return SequenceError.Normal;
                }
            }
            catch (Exception ex)
            {
                X_CoreS.LogError(ex, $"TimeOut waiting for GetPartModel of'{this.Nickname}'");
                return SequenceError.PC_LinkCommu;

            }
        }
        /// Write DataResult Start Address :D8620 + 49 = D8669
        /// <summary>
        /// 
        /// </summary>
        public SequenceError PCWriteDataResultToPLC(string PCWriteDataResultToPLC)
        {
            string sAddress = null, sValue = null;

            try
            {
                lock (objLock)
                {
                    int i = 0;
                    int j = PCWriteDataResultToPLC.IndexOf('D');
                    var address = PCWriteDataResultToPLC.Substring(j + 1, PCWriteDataResultToPLC.Length - (j + 1));
                    ///

                    ///
                    if (!IsPlcConnect)
                        return SequenceError.IsPlcConnect;
                    ///
                    ListPCSetResultToPLC.ForEach((x) =>
                    {
                        if (i != ListPCSetResultToPLC.Count() - 1)
                        {
                            sAddress += string.Format("{0}" + "{1}" + "\n", "D", (int.Parse(address) + i).ToString());
                            sValue += string.Format("{0}" + "\n", x);
                        }
                        else
                        {
                            sAddress += string.Format("{0}" + "{1}", "D", (int.Parse(address) + i).ToString());
                            sValue += string.Format("{0}", x);
                        }
                        i++;
                    });
                    ///
                    this.WriteDeviceRandom2(sAddress, ListPCSetResultToPLC.Count().ToString(), new System.Windows.Forms.TextBox() { Text = sValue });
                    ///
                    return SequenceError.Normal;
                }
            }
            catch (Exception ex)
            {
                X_CoreS.LogError(ex, $"TimeOut waiting for PCWriteDataResultToPLC of'{this.Nickname}'");
                return SequenceError.PCWriteDataResultToPLC;
            }
        }

    }
}
