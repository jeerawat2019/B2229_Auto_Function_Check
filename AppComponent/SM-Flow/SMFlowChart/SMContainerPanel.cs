using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


using X_Core.Comp.SMLib.Flow;
using X_Core.Comp.SMLib.SMFlowChart.Controls;
using X_Core.Comp.SMLib.SMFlowChart.EditForms;

namespace X_Core.Comp.SMLib.SMFlowChart
{
    public class SMContainerPanel : Panel
    {
        #region Privates
        private SMFlowChartCtlBasic _flowChartCtlBasic = null;
        private SMFlowContainer _flowContainer = null;
        private Cursor _flowItemCursor = Cursors.Default;
        private ISelectable _currentSel = null;
        #endregion Privates


        #region Static helpers
        public static Size GridSize = Size.Empty;

        public static Point GridToPixel(PointF gridLoc)
        {
            return new Point((int)Math.Round(gridLoc.X * GridSize.Width), (int)Math.Round(gridLoc.Y * GridSize.Height));
        }
        public static Size GridToPixel(SizeF gridSize)
        {
            return new Size((int)Math.Round(gridSize.Width * GridSize.Width), (int)Math.Round(gridSize.Height * GridSize.Height));
        }
        public static int GridToPixelX(float gridX)
        {
            return (int)Math.Round(gridX * GridSize.Width);
        }

        public static int GridToPixelY(float gridY)
        {
            return (int)Math.Round(gridY * GridSize.Height);
        }
        public static PointF PixelToGrid(Point pixPt)
        {
            return new PointF((float)pixPt.X / (float)GridSize.Width,
                (float)pixPt.Y / (float)GridSize.Height);
        }
        public static float PixelToGridX(int pixX)
        {
            return (float)pixX / (float)GridSize.Width;
        }
        public static float PixelToGridY(int pixY)
        {
            return (float)pixY / (float)GridSize.Height;
        }

        public static PointF PixelToGridSnap(Point pixPt)
        {
            PointF ptF = new PointF((float)(pixPt.X / GridSize.Width) + 0.5f,
                              (float)(pixPt.Y / GridSize.Height) + 0.5f);
            return ptF;
        }
        #endregion Static helpers


        #region Public Properties

        public ISelectable CurrentSel
        {
            get
            {
                return _currentSel;
            }
            set
            {
                if (EditMode)
                {
                    if (_currentSel != null)
                    {
                        _currentSel.SMSelected = false;
                    }

                    _currentSel = value;
                    if (_currentSel != null)
                    {
                        _currentSel.SMSelected = true;
                    }
                }
            }
        }
        /// <summary>
        /// Get the master control
        /// </summary>
        public SMFlowChartCtlBasic FlowChartCtlBasic
        {
            get { return _flowChartCtlBasic; }
        }

        /// <summary>
        /// Get the SMFlowContainer reference
        /// </summary>
        public SMFlowContainer FlowContainer
        {
            get { return _flowContainer; }
        }

        /// <summary>
        /// Indicates if in Edit mde
        /// </summary>
        public bool EditMode
        {
            get
            {
                return _flowContainer.IsEditing(Name);
            }
            set
            {
                if (value != _flowContainer.IsEditing(Name))
                {
                    if (value)
                    {
                        if (_flowContainer.RegisterEdit(Name))
                        {
                            BackgroundImage = global::MCore.Comp.SMLib.SMFlowChart.Properties.Resources.GridBackground;
                            _flowChartCtlBasic.OnEditMode = true;
                            Redraw();
                        }
                    }
                    else
                    {
                        if (_flowContainer.UnregisterEdit(Name))
                        {
                            BackgroundImage = global::MCore.Comp.SMLib.SMFlowChart.Properties.Resources.NoGridBackground;
                            _flowChartCtlBasic.OnEditMode = false;
                            Redraw();
                        }
                    }
                }
            }
        }
        #endregion Public Properties
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="flowChartCtlBasic"></param>
        /// <param name="flowContainer"></param>
        /// <param name="offsetY"></param>
        public SMContainerPanel(SMFlowChartCtlBasic flowChartCtlBasic, SMFlowContainer flowContainer, int offsetY)
            : base()
        {
            DoubleBuffered = true;
            _flowChartCtlBasic = flowChartCtlBasic;
            _flowContainer = flowContainer;
            Name = flowContainer.Nickname;
            TabIndex = 0;
            MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
            MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnLeftClick);
            MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            Paint += new PaintEventHandler(OnPaint);


            _flowItemCursor = CustomCursor.Create(global::MCore.Comp.SMLib.SMFlowChart.Properties.Resources.FlowItemCursor);
            BackgroundImage = global::MCore.Comp.SMLib.SMFlowChart.Properties.Resources.NoGridBackground;

            GridSize = BackgroundImage.Size;

            // Set the size of the panel
            Size = GridToPixel(_flowContainer.GridSize);

            // Set the location of the panel
            Point ptLoc = GridToPixel(_flowContainer.GridCorner);
            ptLoc.Offset(0, offsetY);
            Location = ptLoc;
            Rebuild();
        }

        void OnPaint(object sender, PaintEventArgs e)
        {
            if (!string.IsNullOrEmpty(_flowChartCtlBasic.RefStateMachine.LockText))
            {
                e.Graphics.DrawString(_flowChartCtlBasic.RefStateMachine.LockText, new Font("Arial", 30), new SolidBrush(Color.FromArgb(128, 0, 0)), new Point(40, 40));
            }
        }
        /// <summary>
        /// Initial Creation
        /// </summary>
        public void Rebuild()
        {

            Control[] controls = new Control[Controls.Count];
            Controls.CopyTo(controls, 0);
            foreach (Control ctl in controls)
            {
                SMCtlBase ctlBase = ctl as SMCtlBase;
                if (ctlBase != null)
                {
                    Dispose(ctlBase);
                }
            }


            foreach (SMFlowBase flowItem in _flowContainer.FilterByType<SMFlowBase>())
            {
                Build(flowItem);
            }

            Redraw();
        }

        public void Dispose(SMCtlBase ctlBase)
        {
            ctlBase.Dispose();
            Controls.Remove(ctlBase);
        }
        public void Dispose(SMFlowBase flowItem)
        {
            SMCtlBase ctlBase = GetFlowCtl(flowItem);
            if (ctlBase != null)
            {
                Dispose(ctlBase);
            }
        }
        private void Build(SMFlowBase flowItem)
        {
            if (flowItem is SMStart || flowItem is SMExit)
            {
                new StartStopCtl(this, flowItem);
            }
            else if (flowItem is SMActionFlow)
            {
                new ActionCtl(this, flowItem);
            }
            else if (flowItem is SMDecision)
            {
                new DecisionCtl(this, flowItem);
            }
            else if (flowItem is SMSubroutine)
            {
                new SubroutineCtl(this, flowItem as SMSubroutine);
            }
            else
            {
                throw new Exception(string.Format("'{0}' is not yet supported", flowItem.GetType().Name));
            }
        }

        public void Redraw()
        {
            _flowContainer.DetermineAllChildTargets();

            foreach (Control ctl in this.Controls)
            {
                SMCtlBase ctlBase = ctl as SMCtlBase;
                if (ctlBase != null)
                {
                    ctlBase.MoveItem();
                }
            }

            //foreach (SMFlowBase flowItem in _flowContainer.FilterByType<SMFlowBase>())
            //{
            //    SMCtlBase ctlItem = GetFlowCtl(flowItem);
            //    if (ctlItem != null)
            //    {
            //        ctlItem.MoveItem();
            //    }
            //}
        }

        public void AddNewFlowItem(Type flowItemType, PointF ptGridPt, string text)
        {
            SMFlowBase flowItem = Activator.CreateInstance(flowItemType, string.Empty) as SMFlowBase;
            flowItem.Text = text;
            flowItem.GridLoc = ptGridPt;
            _flowContainer.AddFlowItem(flowItem);
            X_CoreS.LogChangeAdded(string.Format("{0}.{1}", flowItem.Nickname, text));
            Build(flowItem);
            Redraw(flowItem);
        }


        /// <summary>
        /// Entry into a flow item
        /// </summary>
        public void RefreshFlowItem(SMFlowBase currentFlowItem)
        {
            SMCtlBase ctlBase = GetFlowCtl(currentFlowItem);
            if (ctlBase != null)
            {
                ctlBase.OnChanged();
                if (currentFlowItem.IncomingPath != null)
                {
                    SMFlowBase pathFlowItem = currentFlowItem.IncomingPath.Owner;
                    ctlBase = GetFlowCtl(pathFlowItem);
                    if (ctlBase != null)
                    {
                        ctlBase.MoveIt(currentFlowItem.IncomingPath);
                    }
                }
            }
        }


        public void Redraw(SMFlowBase flowItem)
        {
            if (flowItem != null)
            {
                flowItem.DetermineAllPathTargets();
                SMCtlBase ctlBase = GetFlowCtl(flowItem);
                if (ctlBase != null)
                {
                    ctlBase.MoveItem();
                    ctlBase.OnChanged();
                }
            }
        }

        public void DeleteFlowItem(SMFlowBase flowItem)
        {
            X_CoreS.LogChangeRemoved(flowItem.Nickname);
            _flowChartCtlBasic.RemoveFlowContainer(flowItem as SMFlowContainer);
            flowItem.Delete();
            Dispose(flowItem);
            Redraw();
        }
        public SMCtlBase GetFlowCtl(SMFlowBase flowItem)
        {
            if (flowItem == null)
                return null;
            return GetFlowCtl(flowItem.Name);
        }
        /// <summary>
        /// Get the FlowItem Ctl
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SMCtlBase GetFlowCtl(string name)
        {
            if (!string.IsNullOrEmpty(name) && this.Controls.ContainsKey(name))
            {
                return this.Controls[name] as SMCtlBase;
            }
            return null;
        }
        public event MouseEventHandler onMouseMove;
        private bool _emptyFlowSpot = false;
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
//            System.Diagnostics.Debug.WriteLine(string.Format("FlowChart MouseMove"));
            if (onMouseMove != null)
            {
  //              System.Diagnostics.Debug.WriteLine(string.Format("Fire FlowChart MouseMove event"));
                onMouseMove(this, e);
                return;
            }

            SMContainerPanel panel = sender as SMContainerPanel;
            if (panel == null)
                return;

            if (e.Button == MouseButtons.Left)
            {
                if (_emptyFlowSpot)
                {
                    // turn off New Flow Item readiness
                    panel.Cursor = Cursors.Hand;
                    _emptyFlowSpot = false;
                }
                if (!_lastMousePos.IsEmpty)
                {
                    // Dragging
                    Point newPos = _flowChartCtlBasic.PointToClient(MousePosition);
                    Point deltaMoved = newPos;

                    deltaMoved.Offset(-_lastMousePos.X, -_lastMousePos.Y);
                    panel.Top += deltaMoved.Y;
                    panel.Left += deltaMoved.X;
                    _lastMousePos = newPos;
                }
            }
            else if (EditMode)
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (!_lastMousePos.IsEmpty)
                    {
                        // Dragging
                        Point newPos = _flowChartCtlBasic.PointToClient(MousePosition);
                        Point deltaMoved = newPos;

                        deltaMoved.Offset(-_lastMousePos.X, -_lastMousePos.Y);

                        if (Math.Abs(deltaMoved.X) > GridToPixelX(1.0f))
                        {
                            // Change in X
                            if (deltaMoved.X > 0)
                            {
                                _flowContainer.MoveAll(new PointF(1f, 0f));
                            }
                            else
                            {
                                _flowContainer.MoveAll(new PointF(-1f, 0f));
                            }
                            _lastMousePos = newPos;
                            Redraw();
                        }
                        else if (Math.Abs(deltaMoved.Y) > GridToPixelY(1.0f))
                        {
                            // Chang in Y
                            if (deltaMoved.Y > 0)
                            {
                                _flowContainer.MoveAll(new PointF(0f, 1f));
                            }
                            else
                            {
                                _flowContainer.MoveAll(new PointF(0f, -1f));
                            }
                            _lastMousePos = newPos;
                            Redraw();
                        }
                    }
                }
                else
                {
                    // If over an empty space, show
                    PointF gridPos = PixelToGrid(panel.PointToClient(MousePosition));
                    double x = gridPos.X - (int)gridPos.X;
                    double y = gridPos.Y - (int)gridPos.Y;

                    if (x < 0.7 && x > 0.3 && y < 0.6 && y > 0.4)
                    {
                        _emptyFlowSpot = true;
                        panel.Cursor = _flowItemCursor;
                    }
                    else
                    {
                        _emptyFlowSpot = false;
                        panel.Cursor = Cursors.Default;
                    }
                }
            }
        }

        private Point _lastMousePos = Point.Empty;

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                _lastMousePos = _flowChartCtlBasic.PointToClient(MousePosition);
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            SMContainerPanel panel = sender as SMContainerPanel;
            if (panel != null)
            {
                _lastMousePos = Point.Empty;
                panel.Cursor = Cursors.Default;
            }
        }

        private void OnLeftClick(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(_flowChartCtlBasic.RefStateMachine.LockText))
            {
                return;
            }
            if (!EditMode)
            {
                EditMode = true;
            }
            else
            {
                SMContainerPanel panel = sender as SMContainerPanel;
                CurrentSel = null;
                if (_emptyFlowSpot && panel != null)
                {
                    new NewItemForm(this, PixelToGridSnap(panel.PointToClient(MousePosition))).ShowDialog();
                }
            }
        }
    }
}
