using AiComp.ConnectType.Commu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_Core;
using X_Core.CompElement;

namespace B2229_AT_FuncCheck.Dev_Component
{
    public class JigCommand
    {
        public Dictionary<string, string> CmdSend { get; set; } = new Dictionary<string, string>()
        {
            {"Header", "SET-A" },
            {"PCSation", null },
            {"JigNo",null },
            {"Jig2DCode",null },
            {"JigProcess",null }
        };
        public Dictionary<string, string> CmdRecive { get; set; } = new Dictionary<string, string>()
        {
            {"Header", null },
            {"PCSation", null },
            {"JigNo",null },
            {"JigProcess",null },
            {"StepError",null },
            {"JigResult",null }
        };

    }
    public class ComuPCLink : RS232
    {
        /// <summary>
        /// 
        /// </summary>
        public ComuPCLink() { }
        /// <summary>
        /// 
        /// </summary>
        public ComuPCLink(string name) : base(name) { }
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmdSend"></param>
        /// <param name="isNeedReply"></param>
        /// <returns></returns>
        public string OnSendPortCommand(string cmdSend,bool isNeedReply = true)
        {
            while (base.Port.BytesToRead > 0)
            {
                ///
                string existingMsg = base.Port.ReadExisting();
                ///
                System.Threading.Thread.Sleep(30);
            }
            ///
            string CmdSend = cmdSend;
            ///
            base.Port.DiscardInBuffer();
            ///
            base.Port.WriteLine(CmdSend);
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
                        cmdRecive = base.Port.ReadLine();
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
        
    }
  
}
