using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using X_Core.Comp.SMLib.Path;
using X_Core.Comp.SMLib.Flow;

namespace X_Core.Comp.SMLib.SMFlowChart.Controls
{
    public class YesNoLabel : Label, ISelectable
    {
        public SMPathOutBool PathSegmentBool { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pathSegmentBool"></param>
        public YesNoLabel(SMPathOutBool pathSegmentBool)
        {
            PathSegmentBool = pathSegmentBool;
        }

        bool ISelectable.SMSelected 
        { 
            get
            {
                return false;
            }
            set
            {
                if (value)
                {
                    BackColor = System.Drawing.Color.LightBlue;
                }
                else
                {
                    BackColor = System.Drawing.Color.Transparent;
                }
            }
        }

    }
}
