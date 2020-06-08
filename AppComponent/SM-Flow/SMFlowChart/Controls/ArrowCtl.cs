using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using X_Core.Comp.SMLib.Flow;
using X_Core.Comp.SMLib.Path;

namespace X_Core.Comp.SMLib.SMFlowChart.Controls
{
    public partial class ArrowCtl : UserControl
    {
        private Image _origImage = null;
        private SMContainerPanel _containerPanel = null;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="containerPanel"></param>
        /// <param name="name"></param>
        public ArrowCtl(SMContainerPanel containerPanel, string name)
        {
            _containerPanel = containerPanel;
            InitializeComponent();
            this.Name = name;
            this.Size = global::MCore.Comp.SMLib.SMFlowChart.Properties.Resources.ArrowSelected.Size;
        }
        public void SetBackgroundImage(Image image)
        {
            _origImage = image;
            this.BackgroundImage = image;
        }
        public void Selected()
        {
            this.BackgroundImage = global::MCore.Comp.SMLib.SMFlowChart.Properties.Resources.ArrowSelected;
        }
        public void Unselected()
        {
            this.BackgroundImage = _origImage;
        }
        /// <summary>
        /// Move the arrow to the right position
        /// </summary>
        /// <param name="flowItem"></param>
        /// <param name="pathOut"></param>
        public void MoveIt(SMFlowBase flowItem, SMPathOut pathOut)
        {
            try
            {

                SMCtlBase ctlBaseTgt = _containerPanel.GetFlowCtl(pathOut.TargetID);
                if (ctlBaseTgt == null)
                    return;
                Size arrowSize = global::MCore.Comp.SMLib.SMFlowChart.Properties.Resources.ArrowUp.Size;
                Point borderLocation = ctlBaseTgt.Location;
                Size borderSize = ctlBaseTgt.Size;
                borderLocation.Offset(-arrowSize.Width, -arrowSize.Height);
                borderSize.Width += arrowSize.Width;
                borderSize.Height += arrowSize.Height;
                Rectangle rcBorder = new Rectangle(borderLocation, borderSize);
                PointF endPt = flowItem.FindEndPoint(pathOut);
                Point pixXY = SMContainerPanel.GridToPixel(endPt);
                SMPathSegment pathLastSeg = pathOut.Last;
                if (pathLastSeg.Vertical)
                {
                    if (pathLastSeg.GridDistance < 0)
                    {  // Up
                        SetBackgroundImage(global::MCore.Comp.SMLib.SMFlowChart.Properties.Resources.ArrowUp);
                        Location = new Point(pixXY.X - arrowSize.Width/2, rcBorder.Bottom);
                    }
                    else
                    {  // Down
                        SetBackgroundImage(global::MCore.Comp.SMLib.SMFlowChart.Properties.Resources.ArrowDown);
                        Location = new Point(pixXY.X - arrowSize.Width/2, rcBorder.Top);
                    }
                }
                else
                {
                    if (pathLastSeg.GridDistance < 0)
                    {  // Left
                        SetBackgroundImage(global::MCore.Comp.SMLib.SMFlowChart.Properties.Resources.ArrowLeft);
                        Location = new Point(rcBorder.Right, pixXY.Y - arrowSize.Height/2);
                    }
                    else
                    {  // Right
                        SetBackgroundImage(global::MCore.Comp.SMLib.SMFlowChart.Properties.Resources.ArrowRight);
                        Location = new Point(rcBorder.Left, pixXY.Y - arrowSize.Height/2);
                    }
                }
                Show();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}
