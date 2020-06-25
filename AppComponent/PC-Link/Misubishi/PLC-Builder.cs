//using Ai_PCSystem.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AiComp.Misubishis.Divice.PLC
{
    public class PLC_Builder : PLC_Commu
    {
        public PLC_Builder() { }
        public PLC_Builder(string name) : base(name) { }
        public override void Initialization()
        {
            base.Initialization();
        }

        public override void InitializeIDReferences()
        {
            base.InitializeIDReferences();          
        }
        /// <summary>
        /// 
        /// </summary>
        private const int ELEMENT_SIZE_WORD_WRITE = 10;
        /// <summary>
        /// 
        /// </summary>
        //private const int ELEMENT_SIZE_WORD_READ = 450;
        /// <summary>
        /// 
        /// </summary>
        private const int ELEMENT_SIZE_32BITINTEGER = 2;
        /// <summary>
        /// 
        /// </summary>
        private const int ELEMENT_SIZE_REALNUMBER = 2;
        /// <summary>
        /// 
        /// </summary>
        private static Encoding objAsciiCodePageEncoding = Encoding.Default;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sDeviceName"></param>
        /// <param name="sNumberOfData"></param>
        /// <param name="sValue"></param>
        /// <returns></returns>
        public  int WriteDeviceRandom2(string sDeviceName, string sNumberOfData, TextBox sValue)
        {
            int iReturnCode;				//Return code
            String szDeviceName = "";		//List data for 'DeviceName'
            int iNumberOfData = 0;			//Data for 'DeviceSize'
            short[] arrDeviceValue;		    //Data for 'DeviceValue'
            int iNumber = 0;					//Loop counter        
            //Get the list of 'DeviceName'.
            //  Join each line(StringType array) of 'DeviceName' by the separator '\n',
            //  and create a joined string data.
            szDeviceName = String.Join("\n", sDeviceName);

            //Check the 'DeviceSize'.(If succeeded, the value is gotten.)
            if (!this.GetIntValue(sNumberOfData, out iNumberOfData)) {
                //If failed, this process is end.	
                return -1;
            }

            //Assign the array for 'DeviceValue'.
            arrDeviceValue = new short[iNumberOfData];
            //Check the 'DeviceValue'.(If succeeded, the value is gotten.)
            arrDeviceValue = new short[iNumberOfData];
            if (!this.GetShortArray(sValue, out arrDeviceValue)) {
                //If failed, this process is end.	
                return -1;
            }

            //Set the 'DeviceValue'.
            for (iNumber = 0; iNumber < iNumberOfData; iNumber++) {
                arrDeviceValue[iNumber] = arrDeviceValue[iNumber];
            }
            //
            //Processing of WriteDeviceRandom2 method
            //
            try {

                //The WriteDeviceRandom2 method is executed.
                iReturnCode = this.mActUtlTypeClass.WriteDeviceRandom2(szDeviceName,
                                                              iNumberOfData,
                                                              ref arrDeviceValue[0]);
            }

            //Exception processing			
            catch (Exception exception) {
                MessageBox.Show(exception.Message, MethodBase.GetCurrentMethod().Name,
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            return iReturnCode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sDeviceName"></param>
        /// <param name="sNumberOfData"></param>
        /// <param name="sValue"></param>
        /// <returns></returns>
        public int ReadDeviceRandom2(TextBox sDeviceName, string sNumberOfData ,out string[] OutValue )
        {
            int iReturnCode;				//Return code
            String szDeviceName = "";		//List data for 'DeviceName'
            int iNumberOfData = 0;			//Data for 'DeviceSize'
            short[] arrDeviceValue;		    //Data for 'DeviceValue'
            int iNumber;					//Loop counter
            //System.String[] arrData;	    //Array for 'Data'
            OutValue = null;

            //Get the list of 'DeviceName'.
            //  Join each line(StringType array) of 'DeviceName' by the separator '\n',
            //  and create a joined string data.
            szDeviceName = String.Join("\n", sDeviceName.Lines);

            //Check the 'DeviceSize'.(If succeeded, the value is gotten.)
            if (!GetIntValue(sNumberOfData, out iNumberOfData)) {
                //If failed, this process is end.	
                return -1;
            }
            //Assign the array for 'DeviceValue'.
            arrDeviceValue = new short[iNumberOfData];//iNumberOfData

            try {
                //The ReadDeviceBlock2 method is executed.
                //The ReadDeviceRandom2 method is executed.
                iReturnCode = this.mActUtlTypeClass.ReadDeviceRandom2(szDeviceName,
                                                                iNumberOfData,
                                                                out arrDeviceValue[0]);
            }        
            //Exception processing			
            catch (Exception exception) {
                MessageBox.Show(exception.Message, MethodBase.GetCurrentMethod().Name,
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

            //Display the read data
            //
            //When the ReadDeviceRandom2 method is succeeded, display the read data.
            if (iReturnCode == 0) {
                //Assign the array for the read data.
                OutValue = new System.String[iNumberOfData];

                //Copy the read data to the 'arrData'.
                for (iNumber = 0; iNumber < iNumberOfData; iNumber++) {
                    OutValue[iNumber] = arrDeviceValue[iNumber].ToString();
                    
                }
            }
                return iReturnCode;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strWriteWord"></param>
        /// <returns></returns>

        public int WriteMuiltiWordData(string WordStart ="D1000" ,string strWriteWord ="",int ELEMENT_SIZE_WORD = 40)
        {
            try {

                int iReturnCode = 0; //Return code
                byte[] byarrBufferByte = null; //Array for using BitConverter/Encoding class
                short[] sharrBufferForDeviceValue = new short[ELEMENT_SIZE_WORD]; //Array for writing data to the PLC
                int iLengthOfBuffer = 0; //Size of encoded 'Word' data
                int iNumber = 0; //Loop counter

                //Convert the TextBox data to ASCII Code Page.
                byarrBufferByte = objAsciiCodePageEncoding.GetBytes(strWriteWord);

                //Set the size of encoded 'Word' data.(Maximum 20 bytes<for D0-D9>) 
                iLengthOfBuffer = Math.Min(byarrBufferByte.Length, ELEMENT_SIZE_WORD *2);

                //Convert the 'byarrBufferByte' to the array for writing to the PLC.
                //  Step 2                :To copy 2 bytes data to a element of ShortType array.
                //  iLengthOfBuffer - 2   :Not to refer to out of 'byarrBufferByte', when the 'iLengthOfBuffer' is odd.
                for (iNumber = 0; iNumber <= iLengthOfBuffer - 2; iNumber += 2) {
                    sharrBufferForDeviceValue[iNumber / 2] = BitConverter.ToInt16(byarrBufferByte, iNumber);
                }
                //Process the remained character, when the 'iLengthOfBuffer' is odd.
                if ((iLengthOfBuffer % 2) == 1) {
                    sharrBufferForDeviceValue[(int)(iLengthOfBuffer / 2)] = byarrBufferByte[iLengthOfBuffer - 1];
                }
                //The WriteDeviceBlock2 method is executed.(to D0-D9)//Str_Attributes.GetEnumDescription(PLC_Define.WORDMEM.STARTWORD)
                iReturnCode = this.mActUtlTypeClass.WriteDeviceBlock2(WordStart, ELEMENT_SIZE_WORD, ref sharrBufferForDeviceValue[0]);// "R32000"

                //When ActUtlType returns error code, display error message.
                if (iReturnCode != 0) {
                    DisplayErrorMessage(iReturnCode);
                    return iReturnCode;
                }
                //return;
                return iReturnCode;
            }
            catch (Exception ex) {

                MessageBox.Show(ex.ToString(), MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

        }
        /// <summary>
        /// Defile D7000
        /// </summary>
        /// <param name="strDataWord"></param>
        /// <returns></returns>
        public int ReadMuiltiWordData(int ElementSize, string address, out string strDataWord )
        {
            //450
            try {

                strDataWord = "";
                int iReturnCode = 0; //Return code
                byte[] byarrBufferByte = new byte[ElementSize * 2]; //Array for using BitConverter/Encoding class
                short[] sharrBufferForDeviceValue = new short[ElementSize]; //Array for reading to the PLC
                byte[] byarrTemp = null; //Temporary array for copying data
                int iNumber = 0; //Loop counter
                //string Alldata = "";

                iReturnCode = this.mActUtlTypeClass.ReadDeviceBlock2(address, ElementSize, out sharrBufferForDeviceValue[0]);
                //When ActUtlType returns error code, display error message.
                if (iReturnCode != 0) {
                    DisplayErrorMessage(iReturnCode);
                    return 0;
                }
                //Convert the 'sharrBufferForDeviceValue' to the array for using BitConverter/Encoding class.
                for (iNumber = 0; iNumber < ElementSize; iNumber++) {
                    byarrTemp = BitConverter.GetBytes(sharrBufferForDeviceValue[iNumber]);
                    byarrBufferByte[iNumber * 2] = byarrTemp[0];
                    byarrBufferByte[iNumber * 2 + 1] = byarrTemp[1];
                }
                Thread.Sleep(0);
                //Convert to Unicode, and set the data to the TextBox.
                // txt_ReadWord.Text = objAsciiCodePageEncoding.GetString(byarrBufferByte);
                strDataWord = objAsciiCodePageEncoding.GetString(byarrBufferByte);
                return iReturnCode;
            }
            catch (Exception ex) {

                MessageBox.Show(ex.ToString(), MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                strDataWord = null;
                return -1;
            }

        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lptxt_SourceOfShortArray"></param>
        /// <param name="lplpshShortArrayValue"></param>
        /// <returns></returns>
        private bool GetShortArray(TextBox lptxt_SourceOfShortArray, out short[] lplpshShortArrayValue)
        {
            int iSizeOfShortArray;		//Size of ShortType array
            int iNumber;				//Loop counter

            //Get the size of ShortType array.
            iSizeOfShortArray = lptxt_SourceOfShortArray.Lines.Length;
            lplpshShortArrayValue = new short[iSizeOfShortArray];

            //Get each element of ShortType array.
            for (iNumber = 0; iNumber < iSizeOfShortArray; iNumber++) {
                try {
                    if (lptxt_SourceOfShortArray.Lines[iNumber] != "") {
                        lplpshShortArrayValue[iNumber]
                            = Convert.ToInt16(lptxt_SourceOfShortArray.Lines[iNumber]);
                    }
                }

                //Exception processing
                catch (Exception exExcepion) {
                    MessageBox.Show(exExcepion.Message,
                                      MethodBase.GetCurrentMethod().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            //Normal End
            return true;
        }
       

    }
}

