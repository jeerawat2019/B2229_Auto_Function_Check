using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using X_Core.CompElement;

namespace Pc2Pc.Model
{
    public class Pc2PcModel
    {
        public int Pc2PcId { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string Pc2PcName { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public PC232SetUp PCSetComport { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public JigCommand JigStrFormat { set; get; }
        
    }
}
 