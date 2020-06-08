﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using MCore.Comp.SMLib.Path;
using MDouble;

namespace MCore.Comp.SMLib.Flow
{
    public class SMFlowContainer : SMFlowBase
    {
        #region Private properties
        private SMFlowBase _currentFlowItem = null;
        private int _decisionTimeout = -1;
        private string _prevScope = string.Empty;
        #endregion Private properties

        public const string NOSCOPE = "**No Scope**";

        #region Serialize properties
        /// <summary>
        /// Upper left corner used for current scroll position
        /// </summary>
        public Point GridCorner 
        {
            get { return GetPropValue(() => GridCorner, Point.Empty); }
            set { SetPropValue(() => GridCorner, value); }
        }
        /// <summary>
        /// The total size of the grid drawing
        /// </summary>
        public Size GridSize
        {
            get { return GetPropValue(() => GridSize, new Size(10, 20)); }
            set { SetPropValue(() => GridSize, value); }
        }

        /// <summary>
        /// The scope ID
        /// </summary>
        public string ScopeID
        {
            get { return GetPropValue(() => ScopeID, string.Empty); }
            set { SetPropValue(() => ScopeID, value); }
        }
        #endregion
        /// <summary>
        /// The Prev scope ID
        /// </summary>
        public string PrevScopeID
        {
            get { return _prevScope; }
            set { _prevScope = value; }
        }

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SMFlowContainer()
        {
        }
        /// <summary>
        /// Manual Creation Constructor
        /// </summary>
        /// <param name="name"></name>
        public SMFlowContainer(string name)
            : base(name)
        {
        }
        #endregion Constructors

        #region overrides
        public override void Initialize()
        {
            U.RegisterOnChanged(() => ScopeID, OnScopeChanged);
            base.Initialize();
            _prevScope = ScopeID;
        }

        public override void Destroy()
        {
            base.Destroy();
            U.UnRegisterOnChanged(() => ScopeID, OnScopeChanged);
        }
        #endregion overrides


        private void OnScopeChanged(string scopeID)
        {
            foreach(SMFlowBase flowBase in ChildArray)
            {
                flowBase.Rebuild();
            }
            _prevScope = ScopeID;
        }
        /// <summary>
        /// Add Flow Item
        /// </summary>
        /// <param name="flowItemAdded"></param>
        public void AddFlowItem(SMFlowBase flowItemAdded)
        {
            Add(flowItemAdded);
            flowItemAdded.Initialize();
        }
        /// <summary>
        /// Get the flow item for the target in the specified pathOut
        /// </summary>
        /// <param name="pathOut"></param>
        /// <returns></returns>
        public SMFlowBase GetFlowTarget(SMPathOut pathOut)
        {
            if (pathOut.HasTargetID)
            {
                SMFlowBase[] list = FilterByType<SMFlowBase>();
                if (list.Length > 0)
                {
                    try
                    {
                        return list.First(c => c.Name == pathOut.TargetID);
                    }
                    catch
                    {
                        pathOut.DeletedTarget();
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Copy certain non-unique properties
        /// </summary>
        /// <param name="compTo"></param>
        public override void ShallowCopyTo(CompBase compTo)
        {
            base.ShallowCopyTo(compTo);
        }
        /// <summary>
        /// Clone this component
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bRecursivley"></param>
        /// <returns></returns>
        public override CompBase Clone(string name, bool bRecursivley)
        {
            SMFlowContainer newComp = base.Clone(name, bRecursivley) as SMFlowContainer;
            newComp.ScopeID = ScopeID;
            newComp.GridCorner = GridCorner;
            newComp.GridSize = GridSize;

            return newComp;
        }

        private void FindStart()
        {
            _currentFlowItem = ChildArray.ToList().Find(c => c is SMStart) as SMFlowBase;
            if (_currentFlowItem == null)
            {
                throw new Exception("Unable to find the Start Flow chart element");
            }
        }

        private bool FindStop()
        {
            SMFlowBase exitFlowItem = ChildArray.ToList().Find(c => c is SMReturnStop) as SMFlowBase;
            if (exitFlowItem != null)
            {
                _currentFlowItem = exitFlowItem;
                return true;
            }
            return false;
        }

        
        /// <summary>
        /// Move everything in this level
        /// </summary>
        /// <param name="moveDist"></param>
        public void MoveAll(PointF moveDist)
        {
            foreach (SMFlowBase flowItem in ChildArray)
            {
                if (!flowItem.GridLoc.IsEmpty)
                {
                    flowItem.GridLoc = new PointF(flowItem.GridLoc.X + moveDist.X, flowItem.GridLoc.Y + moveDist.Y); 
                }
            }
        }
        /// <summary>
        /// Find the target at the specified endpoint
        /// </summary>
        /// <param name="searcher"></param>
        /// <param name="endGridPt"></param>
        /// <returns></returns>
        public SMFlowBase FindTarget(SMFlowBase searcher, PointF endGridPt)
        {
            foreach (SMFlowBase flowItem in ChildArray)
            {
                if (!object.ReferenceEquals(searcher, flowItem) && flowItem.Contains(endGridPt))
                {
                    return flowItem;
                }

            }
            return null;
        }

        public void AutoScope(bool autoScope)
        {
            string oldScope = ScopeID;
            string newScope = string.Empty;
            if (autoScope)
            {
                if (string.IsNullOrEmpty(ScopeID))
                {

                    foreach (SMFlowBase flowBase in ChildArray)
                    {
                        newScope = flowBase.ScopeCheck(newScope);
                    }
                    if (newScope == NOSCOPE)
                    {
                        newScope = string.Empty;
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                newScope = string.Empty;
            }
            if (oldScope != newScope)
            {
                ScopeID = newScope;
                U.LogChange(string.Format("{0}.Scope", Nickname), oldScope, newScope); 
            }

        }

        public static string DetermineScope(string proposedScope, string path)
        {

            if (string.IsNullOrEmpty(proposedScope))
            {
                // First occurance
                return path;
            }

            // pathA.pathB.id
            // pathA.PathB.PathC (proposed)
            string newProposed = NOSCOPE;
            for (int i = 0; i >= 0 && i < proposedScope.Length;)
            {
                string check = string.Empty;
                i = proposedScope.IndexOf('.', i);
                if (i < 0)
                {
                    check = proposedScope;
                }
                else
                {
                    check = proposedScope.Substring(0, i++);
                }
                if (!path.StartsWith(check))
                {
                    return newProposed;
                }
                newProposed = check;
            }


            return newProposed;
        }
        /// <summary>
        /// Evaluate all the Path Segments and look for targets
        /// </summary>
        public void DetermineAllChildTargets()
        {

            ChildArray.ToList().ForEach(c => (c as SMFlowBase).DetermineAllPathTargets());
        }

        private string _editRegisterationID = String.Empty;

        /// <summary>
        /// Register the editing
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool RegisterEdit(string ID)
        {
            if (string.IsNullOrEmpty(_editRegisterationID))
            {
                _editRegisterationID = ID;
                U.LogChange(string.Format("{0}, Start editing", Nickname));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if editing
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool IsEditing(string ID)
        {
            return _editRegisterationID == ID;
        }
        /// <summary>
        /// Unregister the editing
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool UnregisterEdit(string ID)
        {
            if (_editRegisterationID == ID)
            {
                U.LogChange(string.Format("{0}, Finish editing", Nickname));
                _editRegisterationID = string.Empty;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Enter the path delay if it is less than any others
        /// </summary>
        /// <param name="mSec"></param>
        public void EnterPathDelay(int mSec)
        {
            if (mSec > 0)
            {
                if (_decisionTimeout == -1)
                {
                    _decisionTimeout = 0x7fffffff;
                }
                _decisionTimeout = Math.Min(_decisionTimeout, mSec);
            }
        }

        private void EnterFlowItem()
        {
            StateMachine.EnterFlowItem(_currentFlowItem);
        }
        private void ExitFlowItem()
        {
            StateMachine.ExitFlowItem(_currentFlowItem);
        }
        /// <summary>
        /// Run method for the main state machine thread
        /// </summary>
        /// <returns></returns>
        public override SMPathOut Run()
        {
            SMStateMachine stateMachine = StateMachine;
            try
            {
                ManualResetEvent waitHandle = stateMachine.WaitHandle;
                ManualResetEvent decisionWaitHandle = stateMachine.DecisionWaitHandle;
                List<SMDecision> decisionLoopList = stateMachine.DecisionLoopList;
                SMPathOut pathOut = null;
                SMFlowBase flowItemForPathOut = null;
                _decisionTimeout = -1;
                ExitFlowItem();
                FindStart();
                // Hightlight start
                EnterFlowItem();
                while (true)
                {
                    Thread.Sleep(0);
                    // If Reset will block thread (Pause) until Step or Run (Set)
                    if (stateMachine.Mode == SMStateMachine.eMode.Pause)
                    {
                        waitHandle.Reset();
                        // Wait until(Pause) until Step or Run (Set)
                        EnterFlowItem();
                        waitHandle.WaitOne();
                        ExitFlowItem();
                    }

                    if (_currentFlowItem is SMDecision)
                    {
                        SMDecision decisionItem = _currentFlowItem as SMDecision;
                        if (decisionLoopList.Contains(decisionItem))
                        {
                            // We have closed the loop of nothing but decision items
                            EnterFlowItem();
                            //stateMachine.EnterPathItem(flowItemForPathOut, flowItemForPathOut.OutgoingPath);
                            decisionWaitHandle.WaitOne(_decisionTimeout);
                            _decisionTimeout = -1;
                            //stateMachine.ExitPathItem(flowItemForPathOut, flowItemForPathOut.OutgoingPath);
                            ExitFlowItem();
                            stateMachine.ClearDecisionList();
                        }
                        else
                        {
                            if (decisionLoopList.Count == 0)
                            {
                                // First one.  Reset early in case something changes quickly
                                decisionWaitHandle.Reset();
                            }
                            decisionItem.AddNotifier(StateMachine.OnDecisionChanged);
                            stateMachine.AddToDecisionList(decisionItem, flowItemForPathOut, pathOut);
                        }
                    }
                    else
                    {
                        stateMachine.ClearDecisionList();
                    }
                    //
                    //  Run this item
                    //
                    EnterFlowItem();

                    pathOut = _currentFlowItem.Run();
                    ExitFlowItem();

                    if (stateMachine.ReceivedStop)
                    {
                        stateMachine.ReceivedStop = false;
                        // Redirect the path to the PathOutStop
                        pathOut = _currentFlowItem[typeof(SMPathOutStop)];
                    }

                    if (pathOut == null)
                    {
                        // Will stop the whole State Machine
                        return null;
                    }

                    SMPathOutError pathOutError = pathOut as SMPathOutError;
                    if (pathOutError != null)
                    {
                        pathOutError.ProcessErrors();
                    }

                    if (pathOut.HasTargetID)
                    {
                        flowItemForPathOut = _currentFlowItem;

                        // We are on the target path.
                        _currentFlowItem = GetFlowTarget(pathOut);
                        _currentFlowItem.IncomingPath = pathOut;
                    }
                    else if ((_currentFlowItem is SMDecision) && !(pathOut is SMPathOutStop) && !(pathOut is SMPathOutError))
                    {
                        // Keep same current flow item. Let it loop to itself
                    }
                    else
                    {
                        // No target to go to
                        // Will stop the whole State Machine

                        if ((pathOut is SMPathOutStop) || (pathOut is SMPathOutError))
                        {
                            if (!FindStop())
                                FindStart();
                            EnterFlowItem();
                        }
                        _currentFlowItem.IncomingPath = null;
                        return null;
                    }

                    if (_currentFlowItem == null)
                    {
                        throw new Exception(string.Format("Could not locate Flowitem from ID '{0}' in StateMachine '{1}'.  State Machine has paused.",
                           pathOut.TargetID, Text));
                    }
                    if (_currentFlowItem is SMExit)
                    {
                        EnterFlowItem();
                        if (this is SMDecision)
                        {
                            foreach (SMPathOut path in PathArray)
                            {
                                if (_currentFlowItem is SMReturnNo)
                                {
                                    if (path is SMPathOutBool && !(path as SMPathOutBool).True)
                                    {
                                        return path;
                                    }
                                }
                                else if (_currentFlowItem is SMReturnYes)
                                {
                                    if (path is SMPathOutBool && (path as SMPathOutBool).True)
                                    {
                                        return path;
                                    }
                                }
                                else if (_currentFlowItem is SMReturnStop)
                                {
                                    if (path is SMPathOutStop)
                                    {
                                        return path;
                                    }
                                }
                            }
                        }
                        if (this is SMSubroutine && _currentFlowItem is SMReturnStop)
                        {
                            return this[typeof(SMPathOutStop)];
                        }
                        return this[typeof(SMPathOut)];
                    }
                }
            }
            finally
            {
                stateMachine.ClearDecisionList();
            }
        }
    }
}
