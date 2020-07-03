using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2229_AT_FuncCheck.Dev_AppMachine
{
    public class StaticName
    {
        internal const string AppLogingMachine = "AppLogAppMachine";
        ///
        internal const string MainApp = "MainApp";
        ///
        internal const string AllStation = "AllStation";
        ///
        internal const string AllStateMachine = "AllStateMachine";
        ///
        internal const string AllTesttingSolution = "AllTesterSolution";
        ///
        internal const string AllController = "AllController";

        #region Station static State Machine name
        /// <summary>
        /// 
        /// </summary>
        internal const string SMHomeRes = "SM_HomeRes";
        ///
        internal const string SMMain = "SM_Main";
        /// <summary>
        /// 
        /// </summary>
        internal const string SMController = "SM_Controller";
        ///
        internal const string SMPC1_SFIT = "SM_PC1_Sfit";
        ///
        internal const string SMPC2_SFIT = "SM_PC2_Sfit";
        ///
        internal const string SMPC3_AGING = "SM_PC3_Aging";
        ///
        internal const string SMPC4_WD = "SM_PC4_WD";
        ///
         #endregion
        /// <summary>
        ///
        /// </summary>
        #region Station static Station Sequence Name

        internal static string ST_PC1_SFIT = "ST_PC1_Sfit";
        ///
        internal static string ST_PC2_SFIT = "ST_PC2_Sfit";
        ///
        internal static string ST_PC3_AGING = "ST_PC3_Anging";
        ///
        internal static string ST_PC4_WD = "ST_PC1_WD";

        #endregion

        #region Station Testting Solution name
        /// <summary>
        /// 
        /// </summary>
        internal static string T_SFIT_NO01_NO04 = "T_PC1_Sfit";
        ///
        internal static string T_SFIT_NO05_NO08 = "T_PC2_Sfit";
        ///
        internal static string T_ANGIN_NO01_NO13 = "T_PC3_Anging";
        ///
        internal static string T_WD_NO01 = "T_PC4_WD";
        #endregion

        #region Station PLC Controller
        /// <summary>
        /// 
        /// </summary>
        internal static string PLC_TATTURN = "PLC_Patturn";
        /// 
        internal static string PLC_Q02CPU = "PLC_Q02Cpu";
        #endregion
    }
}
