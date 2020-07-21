using AppMachine.AppControlBase;
using B2229_AT_FuncCheck.AppResult.AppConsignePart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using X_Core;
using X_Core.CompElement;
using X_Core.ControlElement;

namespace B2229_AT_FuncCheck.Dev_AppMachine
{
    public class Machine : CompBase
    {
        public enum eRunStatus
        {
            None,
            Running,
            Pause,
            Stopping,
            Stopped,
        }

        public enum eUserMode
        {
            Production,
            Tooling,
            PD,
            IPQE,
        }
        ///// <summary>
        ///// Run Status
        ///// </summary>
        //[Category("General"), Browsable(true), Description("RunStatus")]
        //[XmlIgnore]
        //public List<string> LogsHeader
        //{
        //    [StateMachineEnabled]
        //    get { return GetPropValue(() => LogsHeader); }
        //    [StateMachineEnabled]
        //    set { SetPropValue(() => LogsHeader, value); }
        //}
        /// <summary>
        /// Stop when the current operation is finished
        /// </summary>
        [Category("General"), Browsable(true), Description("Stop when the curremt operation is finished")]
        [XmlIgnore]
        public bool StopWhenFinished
        {
            [StateMachineEnabled]
            get { return GetPropValue(() => StopWhenFinished, false); }
            [StateMachineEnabled]
            set { SetPropValue(() => StopWhenFinished, value); }
        }
        /// <summary>
        /// Run Status
        /// </summary>
        [Category("General"), Browsable(true), Description("RunStatus")]
        [XmlIgnore]
        public eRunStatus RunStatus
        {
            [StateMachineEnabled]
            get { return GetPropValue(() => RunStatus, eRunStatus.None); }
            [StateMachineEnabled]
            set { SetPropValue(() => RunStatus, value); }
        }
        /// <summary>
        /// Has Reset
        /// </summary>
        [Category("General"), Browsable(true), Description("Has reset")]
        [XmlIgnore]
        public bool HasReset
        {
            [StateMachineEnabled]
            get { return GetPropValue(() => HasReset, false); }
            [StateMachineEnabled]
            set { SetPropValue(() => HasReset, value); }
        }
        /// <summary>
        /// Current Recipe
        /// </summary>
        [Category("General"), Browsable(true), Description("Any SM Running ")]
        [XmlIgnore]
        public bool AnySMRunning
        {
            [StateMachineEnabled]
            get { return GetPropValue(() => AnySMRunning, false); }
            [StateMachineEnabled]
            set { SetPropValue(() => AnySMRunning, value); }
        }
        [XmlIgnore]
        [Category("Part View"), Browsable(false), Description("Case Push Switch Reject Part")]
        public AppPartJigColSFitViewss PartJigColSfit1View
        {
            //[StateMachineEnabled]
            get { return GetPropValue(() => PartJigColSfit1View); }
            //[StateMachineEnabled]
            set { SetPropValue(() => PartJigColSfit1View, value); }
        }
        [XmlIgnore]
        [Category("Part View"), Browsable(false), Description("Case Push Switch Reject Part")]
        public AppPartJigColSFitViewss PartJigColSfit2View
        {
            //[StateMachineEnabled]
            get { return GetPropValue(() => PartJigColSfit2View); }
            //[StateMachineEnabled]
            set { SetPropValue(() => PartJigColSfit2View, value); }
        }
        [XmlIgnore]
        [Category("Part View"), Browsable(false), Description("Case Push Switch Reject Part")]
        public AppPartJigColAngingView PartJigColAngingView
        {
            //[StateMachineEnabled]
            get { return GetPropValue(() => PartJigColAngingView); }
            //[StateMachineEnabled]
            set { SetPropValue(() => PartJigColAngingView, value); }
        }
        [XmlIgnore]
        [Category("Part View"), Browsable(false), Description("Case Push Switch Reject Part")]
        public AppPartJigColWDView PartJigColWDView
        {
            //[StateMachineEnabled]
            get { return GetPropValue(() => PartJigColWDView); }
            //[StateMachineEnabled]
            set { SetPropValue(() => PartJigColWDView, value); }
        }
        [XmlIgnore]
        [Category("Part View"), Browsable(false), Description("Case Push Switch Reject Part")]
        public Dictionary<string, AppUserControlBase> UserControlPart
        {
            //[StateMachineEnabled]
            get { return GetPropValue(() => UserControlPart); }
            //[StateMachineEnabled]
            set { SetPropValue(() => UserControlPart, value); }
        }
        [XmlIgnore]
        public static Machine This = null;

        public Machine() { This = this; }
        public Machine(string name) : base(name) { This = this; }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void InitializeIDReferences()
        {
            base.InitializeIDReferences();
        }
    }
}
