using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using X_Core.Comp.SMLib.Flow;

namespace X_Core.Comp.SMLib.Path
{
    public class SMPathOutPlug : SMPathOut
    {
        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SMPathOutPlug()
        {
            GridDistance = float.NaN;
        }
        #endregion Constructors
        /// <summary>
        /// Initialize this PathSegment
        /// </summary>
        /// <param name="vertical"></param>
        public  override void Initialize(SMFlowBase flowBase, bool vertical) { }
    }
}
