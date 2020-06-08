using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using X_Core.Comp.SMLib.Path;

namespace X_Core.Comp.SMLib.Flow
{
    public class SMSubroutine : SMFlowContainer
    {    
        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SMSubroutine()
        {
        }
        /// <summary>
        /// Manual Creation Constructor
        /// </summary>
        /// <param name="text"></name>
        public SMSubroutine(string name)
            : base(name)
        {
            // Must have a Path in and a path out
            // Must have a Path in and a path out
            this[eDir.Down] = new SMPathOut(0.5F);
            this[eDir.Up] = new SMPathOutPlug();
            this[eDir.Left] = new SMPathOutStop(-0.5F);
            this[eDir.Right] = new SMPathOutPlug();
            Add(new SMStart(string.Empty) { Text = "Enter", GridLoc = new PointF(2.5f, 0.5f) } );
            Add(new SMExit(string.Empty) { Text = "Return", GridLoc = new PointF(2.5f, 6.5f) } );
            Add(new SMReturnStop(string.Empty) { Text = "Stop", GridLoc = new PointF(0.5f, 3.5f) } );
        }
        /// <summary>
        /// Text to be displayed in cursor
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Subroutine";
        }
        #endregion Constructors

        /// <summary>
        /// Call this subroutine
        /// </summary>
        /// <remarks>Single entry limited</remarks>
        [StateMachineEnabled]
        public void Call()
        {
            lock (this)
            {
                SMPathOut smOut = Run();
                if (smOut is SMPathOutStop || smOut == null)
                {
                    StateMachine.ReceivedStop = true;
                }
                else if (smOut is SMPathOutError)
                {
                    // Already logged
                    throw new Exception();
                }
            }
        }

    }
}
