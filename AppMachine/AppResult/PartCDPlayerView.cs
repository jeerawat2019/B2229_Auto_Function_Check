using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AppMachine.AppResult
{
    public class PartCDPlayerView : PartResultBase
    {
        /// Part Index
        /// </summary>
        [XmlIgnore]
        //[Category("General"), Browsable(true), Description("Part Index")]
        public bool PartEmpty
        {
            get { return GetPropValue(() => PartEmpty); }
            set { SetPropValue(() => PartEmpty, value); }
        }
        [XmlIgnore]
        //[Category("General"), Browsable(true), Description("Part Index")]
        public DateTime StartTimeCapture
        {
            get { return GetPropValue(() => StartTimeCapture); }
            set { SetPropValue(() => StartTimeCapture, value); }
        }
        [XmlIgnore]
        //[Category("General"), Browsable(true), Description("Part Index")]
        public bool CycleTime
        {
            get { return GetPropValue(() => CycleTime); }
            set { SetPropValue(() => CycleTime, value); }
        }       

        /// <summary>
        /// 
        /// </summary>
        public PartCDPlayerView() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="partId"></param>
        public PartCDPlayerView(string name, int partId)
            : base(name, partId)
        {
            this.PartId = partId;
        }
    }
}
