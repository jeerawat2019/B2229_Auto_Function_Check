using System;
using X_Unit;
using X_Core.CompElement;
using X_Core.ControlElement;
using X_Core;
using System.Xml.Serialization;
using Pc2Pc.Model;
using System.ComponentModel;

namespace Pc2Pc.Model
{
    public class Pc2PcModel : CompBase
    {
        [XmlIgnore]
        public Pc2PcModel Pc2PcId
        {
            [StateMachineEnabled]
            get { return GetPropValue(() => Pc2PcId); }
            [StateMachineEnabled]
            set { SetPropValue(() => Pc2PcId, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public string Pc2PcName
        {
            [StateMachineEnabled]
            get { return GetPropValue(() => Pc2PcName); }
            [StateMachineEnabled]
            set { SetPropValue(() => Pc2PcName, value); }
        }
        ///// <summary>
        ///// 
        ///// </summary>
        //[XmlIgnore]
        //[Category("Communication Port")]
        //public PC232SetUp PCSetComport 
        //{
        //    [StateMachineEnabled]
        //    get { return GetPropValue(() => PCSetComport); }
        //    [StateMachineEnabled]
        //    set { SetPropValue(() => PCSetComport, value); }
        //}
        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public JigCommand JigStrFormat 
        {
            [StateMachineEnabled]
            get { return GetPropValue(() => JigStrFormat); }
            [StateMachineEnabled]
            set { SetPropValue(() => JigStrFormat, value); }
        }
        
    }
}
 