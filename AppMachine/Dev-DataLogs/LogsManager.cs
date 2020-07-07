using AiComp.Logs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_Core;
using X_Core.CompElement;



namespace B2229_AT_FuncCheck.Dev_DataLogs
{
    public class LogsManager : DefaultLogger
    {
        //[System.ComponentModel.Editor(
        //typeof(System.Windows.Forms.Design.FolderNameEditor),
        //typeof(System.Drawing.Design.UITypeEditor))]
        [Browsable(true)]
        [Category("Result"),]
        public string ResultFileName
        {
            get;
            set;
        } = "Result";
        //[System.ComponentModel.Editor(
        //typeof(System.Windows.Forms.Design.FolderNameEditor),
        //typeof(System.Drawing.Design.UITypeEditor))]
        [Browsable(true)]
        [Category("Result"),]
        public string ResultPath
        {
            get;
            set;
        } = @"C:\";
        [Browsable(true)]
        [Category("Result"),]
        public List<string> ColunmsHeader
        {
            get;
            set;
        }
        public LogsManager() { }
        public LogsManager(string name) : base(name) { }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void InitializeIDReferences()
        {
            base.InitializeIDReferences();
        }

        public void CreateLogFile()
        {
            // filePath usually comes from the App.config file. I've written the value explicitly here for demo purposes.
            var filePath = "C:\\Logs";

            // Append a backslash if one is not present at the end of the file path.
            if (!filePath.EndsWith("\\"))
            {
                filePath += "\\";
            }

            // Create the path if it doesn't exist.
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            // Create the file name with a calendar sortable date on the end.
            var now = DateTime.Now;
            filePath += string.Format("Daily Log [{0}-{1}-{2}].txt", now.Year, now.Month, now.Day);

            // Check if the file that is about to be created already exists. If so, append a number to the end.
            if (File.Exists(filePath))
            {
                var counter = 1;
                filePath = filePath.Replace(".txt", " (" + counter + ").txt");
                while (File.Exists(filePath))
                {
                    filePath = filePath.Replace("(" + counter + ").txt", "(" + (counter + 1) + ").txt");
                    counter++;
                }
            }

            // Note that after the file is created, the file stream is still open. It needs to be closed
            // once it is created if other methods need to access it.
            using (var file = File.Create(filePath))
            {
                file.Close();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [StateMachineEnabled]
        public void CreateResultFile()
        {
            try
            {
                string _currentFile = string.Format("{0}.csv", ResultFileName);
                var currentPath = string.Format(@"{0}\{1}", this.ResultPath, DateTime.Now.ToString("ddMMyyyy"));

                if (!Directory.Exists(currentPath))
                {
                    Directory.CreateDirectory(currentPath);
                    Directory.CreateDirectory(string.Format(@"{0}\Production", currentPath));
                }

                if (!File.Exists(string.Format(@"{0}\Production\{1}", currentPath, _currentFile)))
                {
                    var fullPath = string.Format(@"{0}\Production\{1}", currentPath, _currentFile);
                    File.Create(fullPath).Dispose();
                    this.AppenDataHeader();
                }
            }
            catch (Exception)
            {

                return;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void AppenDataHeader()
        {
            try
            {
                if (ColunmsHeader == null)
                    throw new Exception("null object (FileHeader)");
                // Dump data to current file
                string currentFile = string.Format("{0}.csv", ResultFileName);
                var currentPath = string.Format(@"{0}\{1}\Production", this.ResultPath, DateTime.Now.ToString("ddMMyyyy"));

                var filePath = string.Format(@"{0}\{1}", currentPath, currentFile);

                if (File.Exists(filePath))
                {
                    var csv = new StringBuilder();
                    ColunmsHeader.ForEach(x => csv.Append(string.Format("{0},", x)));
                    File.AppendAllText(filePath, csv.ToString() + Environment.NewLine);
                }

            }
            catch (Exception ex)
            {
                X_CoreS.LogError(ex, $"TimeOut waiting for SetStatusProcess '{this.Nickname}'");
                return;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        [StateMachineEnabled]
        public void AppenAlimentTrayCam1Job1_1TrayResult(object result)
        {
            try
            {
                lock (this)
                {
                    if (string.IsNullOrEmpty("AS".Trim())) return;

                    string currentFile = string.Format("{0}.csv", ResultFileName);
                    var currentPath = string.Format(@"{0}\{1}\Production", this.ResultPath, DateTime.Now.ToString("ddMMyyyy"));


                    if (File.Exists(currentPath))
                    {
                        var count = 1;
                        for (int col = 0; col < 10; col++)
                        {
                            File.AppendAllText(currentPath, string.Format("{0},{1},{2},{3},{4},{5}",
                            count,
                            DateTime.Now.ToString("ddMMyyyy"),
                            DateTime.Now.ToString("hh:mm:ss")
                            //result.VisionResult[col].AligmentResult,
                            //result.VisionResult[col].MixTabResult,
                            //result.VisionResult[col].MixModelResult
                       ) + Environment.NewLine);
                            count++;
                        }
                    }
                }
            }
            catch (Exception)
            {

                return;
            }
        }
    }
}
