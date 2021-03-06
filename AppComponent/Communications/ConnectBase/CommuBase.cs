﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X_Core.CompElement;

namespace AiComp.ConnectType.Commu
{
    public delegate void DataRecievedEventHandler(string text);
    public partial class CommuBase : CompBase
    {

        /// <summary>
        /// Event to handle receiving of data
        /// </summary>
        public event DataRecievedEventHandler OnDataReceived = null;

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public CommuBase()
        {
        }

        /// <summary>
        /// Manual creation constructor
        /// </summary>
        /// <param name="childName"></param>
        public CommuBase(string childName) 
            : base (childName)
        {
        }

        /// <summary>
        /// Fire the data received event
        /// </summary>
        /// <param name="text"></param>
        protected void FireDataRecieved(string text)
        {
            if (OnDataReceived != null && !string.IsNullOrEmpty(text))
            {
                foreach (DataRecievedEventHandler handler in this.OnDataReceived.GetInvocationList())
                {
                    handler.BeginInvoke(text, null, null);
                }
            }
        }

        /// <summary>
        /// Send a string command and return a response
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual void WriteLine(string cmd, params object[] args)
        {
        }

        /// <summary>
        /// Send a string command and return a response
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual void Write(string cmd, params object[] args)
        {
        }

        #endregion Constructors
    }
}
