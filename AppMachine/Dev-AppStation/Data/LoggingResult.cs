using B2229_AT_FuncCheck.AppResult.AppConsignePart;
using B2229_AT_FuncCheck.Dev_AppMachine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;
using X_Core;
using X_Core.CompElement;

namespace B2229_AT_FuncCheck.Dev_AppStation.Data
{
    public class LoggingResult : CompBase
    {
        private List<string> mPartheader = new List<string>(){
                "No","Date","Time","2DCode","StationId","JigNo","SFit-Result","TimeFinnish","StationId","JigNo","Anging-Result","TimeFinnish","StationId","JigNo","WD-Result","TimeFinnish","FinalResult",
        };
        int msec = 35;
        [XmlIgnore]
        public  bool IsSFit1Finnish
        {
            [StateMachineEnabled]
            get
            {
                X_CoreS.Delay(msec);
                ///
                return true;
            }
        }
        [XmlIgnore]
        public bool IsSFit2Finnish
        {
            [StateMachineEnabled]
            get
            {
                X_CoreS.Delay(msec);
                ///
                return true;
            }
        }
        private List<string> mPartList = null;
        [XmlIgnore]
        public List<string> PartList
        {
            [StateMachineEnabled]
            get
            {
                return mPartList;
            }
        }
        public LoggingResult() { }
        public LoggingResult(string name) : base(name) { }
        public override void Initialize()
        {
            base.Initialize();
        }
        Dev_DataLogs.LogsManager mLogsManager = null;
        public override void InitializeIDReferences()
        {
            base.InitializeIDReferences();

            mLogsManager = X_CoreS.GetComponent(Dev_AppMachine.StaticName.DATA_LOGSMANAGER) as Dev_DataLogs.LogsManager;
            ///
            mLogsManager.ColunmsHeader = this.mPartheader;
        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public void StationInitialize()
        {

        }
        [StateMachineEnabled]
        public void AttachResultPart(string result)
        {
            lock (this)
            {
                if (mPartList == null) return;
                ///
                mPartList.Add(result);
            }
        }
        [StateMachineEnabled]
        public void DisAttachResultPart(string item)
        {
            if (mPartList.Count == 0 || mPartList == null) return;
            ///
            mPartList.Remove(item);
        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public void GetPartBy2DCodeSFit1()
        {

            PropertyInfo propertie = Machine.This.UserControlPart["PartJigColSfit1"].GetType().GetProperty("PartJigViews");//.GetValue(x.Value,null);// as AppPartJigView[];
            ///AppPartJigView[] a = ((Array)propertie.GetValue(x.Value)).Cast<AppPartJigView>().ToArray();
            AppPartJigView[] PartJig = (AppPartJigView[])propertie.GetValue(Machine.This.UserControlPart["PartJigColSfit1"]);
            ///
            PartJig.Select(x =>
            {
                if (x.CDPlayer.IsProcess == AppMachine.AppResult.Part.Process.Finnish)
                {
                    ///
                    var currentPath = string.Format(@"{0},{1}", 
                        DateTime.Now.ToString("ddMMyyyy"),
                        x.CDPlayer.Data2DCode,
                        "S-FIT",
                        x.CDPlayer.PartId.ToString(),
                        (x.CDPlayer.IsPass)?"PASS":"FAIL",
                        DateTime.Now.ToString("ddMMyyyy")
                        );
                }
                return true;
            });
        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public void GetPartBy2DCodeSFit2()
        {

            PropertyInfo propertie = Machine.This.UserControlPart["PartJigColSfit2"].GetType().GetProperty("PartJigViews");//.GetValue(x.Value,null);// as AppPartJigView[];
            ///AppPartJigView[] a = ((Array)propertie.GetValue(x.Value)).Cast<AppPartJigView>().ToArray();
            AppPartJigView[] PartJig = (AppPartJigView[])propertie.GetValue(Machine.This.UserControlPart["PartJigColSfit2"]);
            ///
            PartJig.Select(x =>
            {
                if (x.CDPlayer.IsProcess == AppMachine.AppResult.Part.Process.Finnish)
                {
                    ///
                    var currentPath = string.Format(@"{0},{1}",
                        DateTime.Now.ToString("ddMMyyyy"),
                        x.CDPlayer.Data2DCode,
                        "S-FIT",
                        x.CDPlayer.PartId.ToString(),
                        (x.CDPlayer.IsPass) ? "PASS" : "FAIL",
                        DateTime.Now.ToString("ddMMyyyy")
                        );
                }
                return true;
            });
        }
    }
}
