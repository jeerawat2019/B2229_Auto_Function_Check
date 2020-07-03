﻿using System;

namespace B2229_AT_FuncCheck.Dev_Component
{
    /// <summary>
    /// Error code PC-Base  { "SetPCErrorToPLC-L","D8765"}, { "SetPCErrorToPLC-R","D18765"},
    /// </summary>
    public enum SequenceError
    {
        Normal = 0x0000,
        IsConfSetResultPartToPLC = 0x0001,
        IsData2DReady = 0x0010,
        IsTrayInPosition = 0x0011,
        IsTrayOutPosition = 0x0100,
        GetDataResultFormPLC = 0x0101,
        GetPartTotal = 0x0110,
        GetPartModel = 0x0111,
        GetData2DCode = 0x1000,
        SetConfRead2DCode = 0x1001,
        SetResultPartToPLC = 0x1010,
        PC_LinkCommu = 0x1011,
        PCWriteDataResultToPLC = 0x1100,
        IsPlcConnect = 0x1101,
        FirstWork2DFail = 0x1110,
        CSVFileNotFound = 0x1111,
        GetFileList = 0x1112,
        InputParameterNull = 0x1113,
        FTPDownLoadDataServer = 0x1114,
        FTPUpLoadDataServer = 0x1115,
        DeleteFileOnFtpServer = 0x1116,
        DumpResultCSV = 0x1117,
        CalculateError = 0x1118,
    }
    public class PLCConvert
    {
        public static string DecimalToBinary(string data)
        {
            string error = string.Empty;
            string result = string.Empty;
            int rem = 0;
            try
            {
                if (!X_Core.X_CoreS.IsNumber(data))
                    error = "Invalid Value - This is not a numeric value";
                else
                {
                    int num = int.Parse(data);
                    while (num > 0)
                    {
                        rem = num % 2;
                        num = num / 2;
                        result = rem.ToString() + result;
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return result;
        }
        public static bool IsNumeric(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c) && c != '.')
                {
                    return false;
                }
            }

            return true;
        }
    }
}