using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AppMachine.AppResult
{
    public class PartResultBase : Part
    {
        
        public PartResultBase()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="partId"></param>
        public PartResultBase(string name, int partId)
            : base(name, partId)
        {
            this.PartId = partId;
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public virtual bool HasPart
        {
            get { return GetPropValue(() => HasPart, false); }
            set { SetPropValue(() => HasPart, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public virtual string Data2DCode
        {
            get { return GetPropValue(() => Data2DCode, ""); }
            set { SetPropValue(() => Data2DCode, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public virtual int PartDataMemory
        {
            get { return GetPropValue(() => PartDataMemory, 0); }
            set { SetPropValue(() => PartDataMemory, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public virtual int MemRang
        {
            get { return GetPropValue(() => MemRang, 20); }
            set { SetPropValue(() => MemRang, value); }
        }
    }
}
