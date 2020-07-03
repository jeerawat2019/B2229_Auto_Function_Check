using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ActSupportMsgLib;
using ActUtlTypeLib;
using AiComp.ConnectType.Commu;
using X_Core.CompElement;

namespace B2229_AT_FuncCheck.Dev_Component//AiComp.Misubishis.Divice.PLC
{
    public class PLC_Commu : CompBase//PLC_Base
    {
        /// <summary>
        /// Part Id
        /// </summary>
        [Category("Communication"), Browsable(true), Description("mAxActMLUtlType")]
        public ActUtlTypeClass mActUtlTypeClass = null;
        /// <summary>
        /// Part Id
        /// </summary>
        [Category("Communication"), Browsable(true), Description("ActMLSupportMsg")]
        public ActSupportMsgClass mActSupportMsgClass = null;
        //[Category("Communication"), Browsable(true), Description("ActMLSupportMsg")]
        //public ActProgTypeLib.ActMLProgTypeClass mActMLProgTypeClass = null;
        /// <summary>
        /// Part Id
        /// </summary>
        [Category("Communication"), Browsable(true), Description("Ping")]
        private Ping mPing = null;
        /// <summary>
        /// Part Id
        /// </summary>
        [Category("Communication"), Browsable(true), Description("iStstionNumber")]
        public int iStstionNumber
        {
            get;
            set;
        } = 5;
        /// <summary>
        /// Part Id
        /// </summary>
        [Category("Communication"), Browsable(true), Description("IsNetworkConnect")]
        public bool IsNetworkConnect
        {
            get
            {
                //if (string.IsNullOrEmpty(this.IPAddress))
                    return false;// throw new PLC_Exception("PLC IP Address found!");

                mPing = new Ping();
                ///
                //PingReply pingReply = mPing.Send(this.IPAddress, 10000);
                ///
                //Status = pingReply.Status.ToString() != "Success" ? Error.CommuFail : Error.Normal;
                ///
                //return (Status == Error.Normal) ? true : false;

            }
        }
        public PLC_Commu(string name) : base(name) { }
        public override void Initialize()
        {
            base.Initialize();
        }
        /// <summary>
        /// 
        /// </summary>
        public override void InitializeIDReferences()
        {
            base.InitializeIDReferences();
            ///
            this.mActSupportMsgClass = new ActSupportMsgLib.ActSupportMsgClass();
            ///
            this.mActUtlTypeClass = new ActUtlTypeLib.ActUtlTypeClass();
        }
        //public static PLC_Commu PLC_Setting { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int AxActOpen()
        {
            try
            {


                if (!this.IsNetworkConnect)
                {
                    throw new PLC_Exception("");
                }
                int iLogicalStationNumber;		//LogicalStationNumber for ActUtlType
                int iReturnCode = 0; //Return code
                                     //Error Handler
                                     //Set the value of 'LogicalStationNumber' to the property.
                                     //Check the 'LogicalStationNumber'.(If succeeded, the value is gotten.)
                if (GetIntValue(this.iStstionNumber.ToString(), out iLogicalStationNumber) != true)
                {
                    //If failed, this process is end.			
                    return -1;
                }
                this.mActUtlTypeClass.ActLogicalStationNumber = iLogicalStationNumber;
                //The Open method is executed.
                iReturnCode = this.mActUtlTypeClass.Open();
                //When ActUtlType returns error code, display error message.
                if (iReturnCode != 0)
                {
                    DisplayErrorMessage(iReturnCode);
                    return iReturnCode;
                }
                return iReturnCode;
            }
            //Exception processing			
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, MethodBase.GetCurrentMethod().Name,
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

        }
        public bool GetIntValue(string lptxt_SourceOfIntValue, out int iGottenIntValue)
        {
            iGottenIntValue = 0;
            //Get the value as 32bit integer from a TextBox
            try
            {
                iGottenIntValue = Convert.ToInt32(lptxt_SourceOfIntValue);
            }

            //When the value is nothing or out of the range, the exception is processed.
            catch (Exception exExcepion)
            {
                MessageBox.Show(exExcepion.Message,
                                  MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Normal End
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iReturnCode"></param>
        public void DisplayErrorMessage(int iReturnCode)
        {
            string sActErrorMessage = null; //Message as the return code of ActUtlType
            ///
            int iSupportReturnCode = 0; //Return code of ActSupportMsg
            //The GetErrorMessage method is executed
            iSupportReturnCode = this.mActSupportMsgClass.GetErrorMessage(iReturnCode, out sActErrorMessage);
            //When ActSupportMsg returns error code, display error code of ActUtlType.
            if (iSupportReturnCode != 0)
            {
                MessageBox.Show("Cannot get the string data of error message." + "\n" + "  Error code = 0x" + Convert.ToString(iReturnCode, 16).ToUpper(), string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(sActErrorMessage, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
