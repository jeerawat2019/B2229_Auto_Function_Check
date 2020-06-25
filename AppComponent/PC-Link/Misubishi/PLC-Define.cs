using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiComp.Misubishis.Divice.PLC
{
    public class PLC_Define
    {

        public enum WORDMEM
        {

            [Browsable(true)]
            [Category("PLC STATUS STATUS COMMUNICATION")]
            [Description("R1")]
            PLC_STATUS = 1,

            [Browsable(true)]
            [Category("PLC WORD START READ/WRITE")]
            [Description("R1000")]
            STARTWORD = 1000,

            [Browsable(true)]
            [Category("PC STATUS COMMUNICATION")]
            [Description("R450")]
            PC_STATUS = 450,


        }
    }
}
