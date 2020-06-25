using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using X_Core;

namespace B2229_AT_FuncCheck.Dev_AppStation.Controller
{
    public class PLC_Qcpu : PLC_Base
    {
        int msec = 35;
        [Browsable(true)]
        [Category("Controller")]
        [XmlIgnore]
        public bool IsMemStartPartOn
        {
            [StateMachineEnabled]
            get
            {
                X_CoreS.Delay(msec);
                return GetPropValue(() => IsMemStartPartOn, false);
            }
            [StateMachineEnabled]
            set { SetPropValue(() => IsMemStartPartOn, value); }
        }
        [Browsable(true)]
        [Category("Controller")]
        [XmlIgnore]
        public bool IsJigStationSfit1PartOn
        {
            [StateMachineEnabled]
            get
            {
                X_CoreS.Delay(msec);
                return GetPropValue(() => IsJigStationSfit1PartOn, false);
            }
            [StateMachineEnabled]
            set { SetPropValue(() => IsJigStationSfit1PartOn, value); }
        }
        [Browsable(true)]
        [Category("Controller")]
        [XmlIgnore]
        public bool IsJigStationSfit2PartOn
        {
            [StateMachineEnabled]
            get
            {
                X_CoreS.Delay(msec);
                return GetPropValue(() => IsJigStationSfit2PartOn, false);
            }
            [StateMachineEnabled]
            set { SetPropValue(() => IsJigStationSfit2PartOn, value); }
        }
        [Browsable(true)]
        [Category("Controller")]
        [XmlIgnore]
        public bool IsJigStationAngingPartOn
        {
            [StateMachineEnabled]
            get
            {
                X_CoreS.Delay(msec);
                return GetPropValue(() => IsJigStationAngingPartOn, false);
            }
            [StateMachineEnabled]
            set { SetPropValue(() => IsJigStationAngingPartOn, value); }
        }
        [Browsable(true)]
        [Category("Controller")]
        [XmlIgnore]
        public bool IsJigStationWDPartOn
        {
            [StateMachineEnabled]
            get
            {
                X_CoreS.Delay(msec);
                return GetPropValue(() => IsJigStationWDPartOn, false);
            }
            [StateMachineEnabled]
            set { SetPropValue(() => IsJigStationWDPartOn, value); }
        }
        public PLC_Qcpu() : base() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public PLC_Qcpu(string name) : base(name) { }
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
        [StateMachineEnabled]
        public void GetAllMemControlWord()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public void GetMemDataWord()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public void PartUpdate()
        {
            //Dev_AppMachine.Machine.This.PartJigColSfit1.PartJigViews[0].CDPlayer.IsProcess = AppMachine.AppResult.Part.Process.Start;
            //Dev_AppMachine.Machine.This.PartJigColSfit2.PartJigViews[0].CDPlayer.IsProcess = AppMachine.AppResult.Part.Process.Start;
            //Dev_AppMachine.Machine.This.PartJigColAngingView.PartJigViews[0].CDPlayer.IsProcess = AppMachine.AppResult.Part.Process.Start;
            //Dev_AppMachine.Machine.This.PartJigColWDView.PartJigViews[0].CDPlayer.IsProcess = AppMachine.AppResult.Part.Process.Start;
            
        }
    }
}
