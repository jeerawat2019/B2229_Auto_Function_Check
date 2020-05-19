using System;

namespace X_Core
{
    /// <summary>
    /// Exception derived class to handle data validation
    /// </summary>
    public class X_CoreExceptionAlert : X_CoreException
    {
        /// <summary>
        /// Construct with innerException, procedure and message. Default is (Error), (Alert, Pause), (DumpOptions).
        /// </summary>
        /// <param name="innerException"></param>
        /// <param name="msg">String that contins error message to be dispayed</param>
        /// <param name="args">optional argument for string.Format()</param>
        public X_CoreExceptionAlert(Exception innerException, string msg, params object[] args) : base(innerException, LogSeverity.Alert, msg, args) { }

        /// <summary>
        /// Constructor. Default is (Error), (Alert, Pause), (NoDump).
        /// </summary>
        /// <param name="msg">String that contins error message to be dispayed</param>
        /// <param name="args">optional argument for string.Format()</param>
        public X_CoreExceptionAlert(string msg, params object[] args) : this(null, msg, args) { }
    }
    //#endregion

}
