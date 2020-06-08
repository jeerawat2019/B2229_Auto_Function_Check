using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace X_Core.Comp.SMLib.SMFlowChart.Controls
{
    public interface ISelectable
    {
        /// <summary>
        /// Get or set the selectection state
        /// </summary>
        bool SMSelected { get; set; }
        ///// <summary>
        ///// Notification that selection has changed
        ///// </summary>
        ///// <param name="selected"></param>
        //void SMSelectionChanged(bool selected);
    }
}
