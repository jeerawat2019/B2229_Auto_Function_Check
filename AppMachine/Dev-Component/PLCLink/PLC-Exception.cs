using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2229_AT_FuncCheck.Dev_Component//AiComp.Misubishis.Divice.PLC
{
    public class PLC_Exception : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public PLC_Exception() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public PLC_Exception(string message) : base(message) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public PLC_Exception(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected PLC_Exception(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
