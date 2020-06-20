using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.ComponentModel;
using System.Threading;


using X_Unit;
using X_Core.CompElement;
using X_Core.ControlElement;
using X_Core;
using System.Xml.Serialization;

using Pc2Pc.Model;
using AiComp.ConnectType.Commu;

namespace AiComp.ConnectType.Pc2Pc
{
    public class Pc232Link : CommuBase//, Inf.IPc2Pc
    {
        private BackgroundWorker mBackgroundUpdateLoop = null;
        /// <summary>
        /// 
        /// </summary>
        private volatile bool mDestroy = false;
        /// <summary>
        /// 
        /// </summary>
        private SerialPort mSerial = null;
        /// <summary>
        /// 
        /// </summary>
        private ManualResetEvent mWaitPortRead = new ManualResetEvent(true);
        /// <summary>
        /// 
        /// </summary>     
        [Category("Model controller PC To PC")]
        public Pc2PcModel Pc2PcModels
        {
            //
            get { return GetPropValue(() => Pc2PcModels); }
            //
            set { SetPropValue(() => Pc2PcModels, value); }
        }
        /// <summary>
        /// 
        /// </summary>     
        [Category("Communication Port")]
        public PC232SetUp PCSetComport
        {
            //[StateMachineEnabled]
            get { return GetPropValue(() => PCSetComport); }
            //[StateMachineEnabled]
            set { SetPropValue(() => PCSetComport, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public Pc232Link()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public Pc232Link(string name) : base(name) { }
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
            InitializeComport();
        }
        /// <summary>
        /// 
        /// </summary>
        public void InitializeComport()
        {
            mSerial = new SerialPort();
            //Pc2PcModels.PCSetComport = new PC232SetUp(); 
            try
            {

                mSerial.PortName = PCSetComport.ToString();
                mSerial.BaudRate = (int)PCSetComport.BaudRate;
                mSerial.StopBits = PCSetComport.StopBits;
                mSerial.Parity = PCSetComport.Parity;
                mSerial.ReadTimeout = (int)PCSetComport.ReadWriteTimeOut;
                mSerial.WriteTimeout = (int)PCSetComport.ReadWriteTimeOut;
                ///
                //mSerial.DataReceived += MSerial_DataReceived;
                ///
                if (mSerial.IsOpen)
                    ///
                    mSerial.Close();
                ///
                mSerial.Open();
                ///
                if (!mSerial.IsOpen)
                    ///
                    throw new X_CoreExceptionError($"Port { PCSetComport.CommPort.ToString() } is not opened");
                ///
                this.Simulate = eSimulate.None;
            }
            catch (Exception ex)
            {
                ///
                mSerial.Close();
                ///
                X_CoreS.LogAlarmPopup(ex,this.Nickname);
                ///
                this.Simulate = eSimulate.SimulateDontAsk;
            }
            mBackgroundUpdateLoop = new BackgroundWorker();
            ///
            mBackgroundUpdateLoop.DoWork += MBackgroundUpdateLoop_DoWork;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void MSerial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    try
        //    {
        //        lock (mWaitPortRead)
        //        {
        //            mWaitPortRead.Set();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new X_CoreExceptionPopup(ex, $"'{this.Nickname}' received unexpected");
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MBackgroundUpdateLoop_DoWork(object sender, DoWorkEventArgs e)
        {
            do
            {
                try
                {
                    Thread.Sleep(100);
                }
                catch (Exception ex)
                {
                    return;

                }
            } while (!mDestroy);
        }
        /// <summary>
        /// 
        /// </summary>
        public  string OnSendPortCommand(string cmdSend,Miliseconds timout,bool isNeedReply = true)
        {           
            while(mSerial.BytesToRead > 0)
            {
                ///
                string existingMsg = mSerial.ReadExisting();
                ///
                System.Threading.Thread.Sleep(30);
            }
            ///
            string CmdSend = cmdSend;
            ///
            mSerial.DiscardInBuffer();
            ///
            mSerial.WriteLine(CmdSend);
            ///
            System.Threading.Thread.Sleep(10);
            //}
            if (isNeedReply)
            {
                try
                {
                    //X_CoreS.BlockOrDoEvents(mWaitPortRead, timout.ToInt);
                    string cmdRecive = "";
                    ///
                    //X_CoreS.SleepWithEvents(10);
                    do
                    {
                        cmdRecive = mSerial.ReadLine();
                        ///
                        System.Threading.Thread.Sleep(50);
                        ///
                    } while (true != cmdRecive.Contains("GET-A"));
                    ///
                    return cmdRecive;

                }
                catch (Exception ex)
                {
                    ///
                    X_CoreS.LogError(ex, $"TimeOut waiting for read port of'{this.Nickname}'");
                    ///
                    throw new X_CoreExceptionPopup(ex, $"TimOut waiting for read port of'{this.Nickname}'of command'{cmdSend}'");
                }
            } 
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        public override void Destroy()
        {
            if (mSerial == null)
                ///
                return;
            if ( mSerial.IsOpen)
                ///
                mSerial.Close();
            ///
            base.Destroy();
        }
        /// <summary>
        /// 
        /// </summary>
        public override void PreDestroy()
        {
            ///
            mDestroy = true;
            ///
            base.PreDestroy();
        }
        public override void Write(string cmd, params object[] args)
        {
            base.Write(cmd, args);
        }
        public JigCommand Pc2PcSetCommand(JigCommand jigStation)
        {
            
            StringBuilder strBuilder = new StringBuilder();
            try
            {
                //mPc2PcModel.PCSetComport.NewLine = PortSetting.eNewLine.CRLF;
                //PC Header
                //PC Station
                //PC JigNo.
                //PC 2DCode
                //PC Process
                if (jigStation == null || jigStation.CmdSend == null)
                    ///
                    throw new X_CoreExceptionPopup($"Error Object null'{this.Nickname}'");
                ///
                foreach (var item in jigStation.CmdSend)
                ///
                    strBuilder.Append($"{item.Key} +,");
                ///
                strBuilder.ToString().Substring(0, strBuilder.Length - 1);
                ///
                //strBuilder.Append(mPc2PcModel.PCSetComport.RawNewLine);              
                ///
                var cmdRecive = OnSendPortCommand(strBuilder.ToString(), 1000,true);
                ///
                if (!cmdRecive.Contains("GET-A"))
                    ///
                    throw new X_CoreExceptionPopup($"Return set command fail '{this.Nickname}'");
                else
                {
                    ///
                    string[] arrCmd = cmdRecive.Split(new string[] { "\r\n", "," }, StringSplitOptions.RemoveEmptyEntries);
                    ///
                    var result = new JigCommand();
                    ///
                    int i = 0;
                    ///
                    if(arrCmd.Length == result.CmdRecive.Count)

                        foreach (string key in jigStation.CmdRecive.Keys)
                        {
                            jigStation.CmdRecive[key] = arrCmd[i];
                            ///
                            i++;
                        }
                    return result;
                }
            }
            catch (Exception ex)
            {

                X_CoreS.LogError(ex, $"TimeOut waiting for read port of'{this.Nickname}'");
                ///
                throw new X_CoreExceptionPopup(ex, $"TimOut waiting for read port of'{this.Nickname}'of command'{strBuilder.ToString()}'");
            }

        }

    }
}
