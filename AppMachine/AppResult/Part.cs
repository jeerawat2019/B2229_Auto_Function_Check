using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using X_Core.CompElement;

namespace AppMachine.AppResult
{
    public class Part : CompBase
    {
        
        public enum Process
        {
            Null,
            Empty,
            Start,
            Testting,
            Finnish
        }
        /// <summary>
        /// Part Id
        /// </summary>
        [XmlIgnore]
        //[Category("General"), Browsable(true), Description("Part Id")]
        public int PartId
        {
            get { return GetPropValue(() => PartId); }
            set { SetPropValue(() => PartId, value); }
        }


        /// <summary>
        /// Part Index
        /// </summary>
        [XmlIgnore]
        //[Category("General"), Browsable(true), Description("Part Index")]
        public int PartIndex
        {
            get { return GetPropValue(() => PartIndex); }
            set { SetPropValue(() => PartIndex, value); }
        }
        /// <summary>
        /// Part Status
        /// </summary>
        [XmlIgnore]
        //[Category("Status"), Browsable(true), Description("Part Status")]
        public string PartStatus
        {
            get { return GetPropValue(() => PartStatus, "N/A"); }
            set { SetPropValue(() => PartStatus, value); }
        }
        // <summary>
        /// Part Status
        /// </summary>
        [XmlIgnore]
        //[Category("Status"), Browsable(true), Description("Part Status")]
        public Process IsProcess
        {
            get { return GetPropValue(() => IsProcess, Part.Process.Null); }
            set { SetPropValue(() => IsProcess, value); }
        }
        /// <summary>
        /// Part Status
        /// </summary>
        [XmlIgnore]
        //[Category("Status"), Browsable(true), Description("Part Status")]
        public bool IsPass
        {
            get
            {
                var result = this.PartStatus == "OK" ? true : false;
                return result;
            }

            set
            {
                var parser = value == true ? "OK" : "NG";
                SetPropValue(() => PartStatus, parser);
            }
        }
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Part() { }

        /// <summary>
        /// Manual Creation Constructor
        /// </summary>
        /// <param name="name"></param>
        public Part(string name)
            : base(name) { }

        /// <summary>
        /// Manual Creation Constructor
        /// </summary>
        /// <param name="name"></param>
        public Part(string name, int partId)
            : base(name)
        {
            PartId = partId;
        }

    }
}
