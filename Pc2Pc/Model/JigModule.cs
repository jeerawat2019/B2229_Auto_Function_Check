using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pc2Pc.Model
{
  
    public class JigCommand
    {
        public Dictionary<string, string> CmdSend { get; set; } = new Dictionary<string, string>()
        {
            {"Header", "SET-A" },
            {"PCSation", null },
            {"JigNo",null },
            {"Jig2DCode",null },
            {"JigProcess",null }
        };
        public Dictionary<string, string> CmdRecive { get; set; } = new Dictionary<string, string>()
        {
            {"Header", null },
            {"PCSation", null },
            {"JigNo",null },
            {"JigProcess",null },
            {"StepError",null },
            {"JigResult",null }
        };

    }
}
