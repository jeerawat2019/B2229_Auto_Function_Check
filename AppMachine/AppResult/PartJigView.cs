using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AppMachine.AppResult
{
    public class PartJigView : PartResultBase
    {
        public enum Process
        {
            Wait,
            Testing,
            Finnish
        }
        [XmlIgnore]
        public Process JigProcess
        {
            get { return GetPropValue(() => JigProcess, Process.Wait); }
            set { SetPropValue(() => JigProcess, value); }
        }
        [XmlIgnore]
        public PartCDPlayerView LastPart
        {
            get { return GetPropValue(() => LastPart, null); }
            set { SetPropValue(() => LastPart, value); }
        }
        [XmlIgnore]
        public override bool HasPart
        {
            get { return GetPropValue(() => HasPart, false); }
            set { SetPropValue(() => HasPart, value); }
        }

        public PartJigView() { }
        public PartJigView(string name, int partId)
            : base(name, partId)
        {
            this.PartId = partId;
        }
        /// <summary>
        /// สวมใส่ Part
        /// </summary>
        /// <param name="cdPrayer"></param>
        public void PartWear(PartCDPlayerView cdPrayer)
        {
            this.LastPart = cdPrayer;
        }
      
    }
}
