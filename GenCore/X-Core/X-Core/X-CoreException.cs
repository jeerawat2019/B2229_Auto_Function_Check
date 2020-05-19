using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace X_Core
{
    #region Base Exceptions
    /// <summary>
    /// The general MCL Exception
    /// </summary>
    public class X_CoreException : ApplicationException
    {
        private string _procedureName;
        private LogSeverity _logSeverity = LogSeverity.Error;

        /// <summary>
        /// Get the log severity
        /// </summary>
        public LogSeverity Severity
        {
            get { return _logSeverity; }
        }

        /// <summary>
        /// Get the Procedure
        /// </summary>
        public string Procedure
        {
            get { return _procedureName; }
        }
        /// <summary>
        /// Contructor with params and inner exceptions
        /// </summary>
        /// <param name="innerException"></param>
        /// <param name="severity"></param>
        /// <param name="msg">String that contins error message to be dispayed</param>
        /// <param name="args">optional argument for string.Format()</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public X_CoreException(Exception innerException,
            LogSeverity severity,
            string msg, params object[] args)
            : base(string.Format(msg, args), innerException)
        {
            _logSeverity = severity;
            _procedureName = "Unknown Source";
            StackTrace st = new StackTrace();
            foreach (StackFrame frame in st.GetFrames())
            {
                MethodBase methodBase = frame.GetMethod();
                if (!methodBase.DeclaringType.IsSubclassOf(typeof(Exception)))
                {
                    _procedureName = string.Format("{0}.{1}", methodBase.DeclaringType.Name, methodBase.Name);
                    break;
                }
            }
        }
        /// <summary>
        /// Contructor with params
        /// </summary>
        /// <param name="severity"></param>
        /// <param name="msg">String that contins error message to be dispayed</param>
        /// <param name="args">optional argument for string.Format()</param>
        public X_CoreException(LogSeverity severity,
            string text, params object[] args)
            : this(null, severity, text, args)
        {
        }

        
    }

    /// <summary>
    /// Exception derived class to handle data validation
    /// </summary>
    public class X_CoreExceptionPopup : X_CoreException
    {
        /// <summary>
        /// Construct with innerException, procedure and message. Default is (Error), (Popup), (DumpOptions).
        /// </summary>
        /// <param name="innerException"></param>
        /// <param name="msg">String that contins error message to be dispayed</param>
        /// <param name="args">optional argument for string.Format()</param>
        public X_CoreExceptionPopup(Exception innerException, string msg, params object[] args) : base(innerException, LogSeverity.Popup, msg, args) { }
        /// <summary>
        /// Constructor. Default is (Error), (Popup, Pause), (DumpOptions).
        /// </summary>
        /// <param name="msg">String that contins error message to be dispayed</param>
        /// <param name="args">optional argument for string.Format()</param>
        public X_CoreExceptionPopup(string msg, params object[] args) : this(null, msg, args) { }

    }

    /// <summary>
    /// Exception derived class to handle non-pops and alert Errors
    /// </summary>
    public class X_CoreExceptionError : X_CoreException
    {
        /// <summary>
        /// Construct with innerException, procedure and message. Default is (Error), (Alert, Pause), (DumpOptions).
        /// </summary>
        /// <param name="innerException"></param>
        /// <param name="msg">String that contins error message to be dispayed</param>
        /// <param name="args">optional argument for string.Format()</param>
        public X_CoreExceptionError(Exception innerException, string msg, params object[] args) : base(innerException, LogSeverity.Error, msg, args) { }

        /// <summary>
        /// Constructor. Default is (Error), (Alert, Pause), (NoDump).
        /// </summary>
        /// <param name="msg">String that contins error message to be dispayed</param>
        /// <param name="args">optional argument for string.Format()</param>
        public X_CoreExceptionError(string msg, params object[] args) : this(null, msg, args) { }
    }

    /// <summary>
    /// Exception derived class to handle non-pops and alert Warnings
    /// </summary>
    public class X_CoreExceptionWarning : X_CoreException
    {
        /// <summary>
        /// Construct with innerException, procedure and message. Default is (Error), (Alert, Pause), (DumpOptions).
        /// </summary>
        /// <param name="innerException"></param>
        /// <param name="msg">String that contins error message to be dispayed</param>
        /// <param name="args">optional argument for string.Format()</param>
        public X_CoreExceptionWarning(Exception innerException, string msg, params object[] args) : base(innerException, LogSeverity.Warning, msg, args) { }

        /// <summary>
        /// Constructor. Default is (Error), (Alert, Pause), (NoDump).
        /// </summary>
        /// <param name="msg">String that contins error message to be dispayed</param>
        /// <param name="args">optional argument for string.Format()</param>
        public X_CoreExceptionWarning(string msg, params object[] args) : this(null, msg, args) { }
    }

    /// <summary>
    /// Exception derived class to handle non-pops and alert Debugs
    /// </summary>
    public class X_CoreExceptionDebug : X_CoreException
    {
        /// <summary>
        /// Construct with innerException, procedure and message. Default is (Error), (Alert, Pause), (DumpOptions).
        /// </summary>
        /// <param name="innerException"></param>
        /// <param name="msg">String that contins error message to be dispayed</param>
        /// <param name="args">optional argument for string.Format()</param>
        public X_CoreExceptionDebug(Exception innerException, string msg, params object[] args) : base(innerException, LogSeverity.Debug, msg, args) { }

        /// <summary>
        /// Constructor. Default is (Error), (Alert, Pause), (NoDump).
        /// </summary>
        /// <param name="msg">String that contins error message to be dispayed</param>
        /// <param name="args">optional argument for string.Format()</param>
        public X_CoreExceptionDebug(string msg, params object[] args) : this(null, msg, args) { }
    }

    /// <summary>
    /// Exception derived class to handle non-pops and alerts
    /// </summary>
    public class X_CoreExceptionInfo : X_CoreException
    {
        /// <summary>
        /// Construct with innerException, procedure and message. Default is (Error), (Alert, Pause), (DumpOptions).
        /// </summary>
        /// <param name="innerException"></param>
        /// <param name="msg">String that contins error message to be dispayed</param>
        /// <param name="args">optional argument for string.Format()</param>
        public X_CoreExceptionInfo(Exception innerException, string msg, params object[] args) : base(innerException, LogSeverity.Info, msg, args) { }

        /// <summary>
        /// Constructor. Default is (Error), (Alert, Pause), (NoDump).
        /// </summary>
        /// <param name="msg">String that contins error message to be dispayed</param>
        /// <param name="args">optional argument for string.Format()</param>
        public X_CoreExceptionInfo(string msg, params object[] args) : this(null, msg, args) { }
    }

    #endregion


    #region Controller Initialize Fail
    /// <summary>
    ///    Simulate Due to Initialize Fail by replacing class with Sim
    /// </summary>
    public class ForceSimulateException : X_CoreException
    {
        /// <summary>
        /// Full constructor
        /// </summary>
        public ForceSimulateException(string text, params object[] args)
            : base(LogSeverity.Warning, text, args)
        {                        
        }
        public ForceSimulateException(Exception ex)
            : base(ex, LogSeverity.Warning, ex.Message)
        {
        }
        public ForceSimulateException(Exception ex, string msg, params object[] args)
            : base(ex, LogSeverity.Warning, msg, args)
        {
        }

    }
    #endregion

}
