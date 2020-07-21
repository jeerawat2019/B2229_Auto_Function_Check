using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AppMachine.AppControlBase;
using X_Core;
using B2229_AT_FuncCheck.Dev_AppStation.Data;

namespace B2229_AT_FuncCheck.Display.Production
{
    public partial class UserProduction : AppUserControlBase
    {
        public LoggingResult mLogResult = null;
        /// <summary>
        /// 
        /// </summary>
        public UserProduction()
        {
            //InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void Initializing()
        {
            base.Initializing();
            ///
            InitializeComponent();
            ///
            InitializeComponentPart();
            ///
            dgvResultPart.CellPainting += DgvResultPart_CellPainting;
            ///


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvResultPart_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                //dgvResultPart.ToList().ForEach(x =>
                //{
                //if (x != null)
                //{
                for (int i = 0; i < dgvResultPart.ColumnCount; i++)
                {
                    if (e.RowIndex == -1 && e.ColumnIndex == i)
                    {   ///
                        e.PaintBackground(e.CellBounds, true);
                        ///
                        e.Graphics.TranslateTransform(e.CellBounds.Left, e.CellBounds.Bottom);
                        ///
                        e.Graphics.RotateTransform(270);
                        ///
                        e.Graphics.DrawString(e.FormattedValue.ToString(), e.CellStyle.Font, Brushes.Black, 5, 5);
                        ///
                        e.Graphics.ResetTransform();
                        ///
                        e.Handled = true;
                    }
                }
                txtRead2DCode.Focus();
                //    }
                //});
            }
            catch (Exception ex)
            {
                //Assembly.CRLine21.Group.DataHitoryLogging.ShowHistoryDisplay(this.Name, MethodBase.GetCurrentMethod().Name, ex.ToString());
                MessageBox.Show("Dgv_CellPainting can't success:>" + ex.ToString(), "frmStationLogs",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                ///
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeComponentPart()
        {
            JenerateParamPart();
            ///
            Dev_AppMachine.Machine.This.PartJigColSfit1View = appPartJigColSFitViewss1;
            ///
            Dev_AppMachine.Machine.This.PartJigColSfit2View = appPartJigColSFitViewss2;
            ///
            Dev_AppMachine.Machine.This.PartJigColAngingView = appPartJigColAngingView1;
            ///
            Dev_AppMachine.Machine.This.PartJigColWDView = appPartJigColWDView1;
            ///
            SetMemMemControlPart();
            ///
            SetMemMemConfirmPart();
            ///
            Dev_AppMachine.Machine.This.UserControlPart = new Dictionary<string, AppUserControlBase>()
            {
                { "PartJigColSfit1",appPartJigColSFitViewss1},
                { "PartJigColSfit2",appPartJigColSFitViewss2},
                { "PartJigColAnging",appPartJigColAngingView1},
                { "PartJigColWD",appPartJigColWDView1}
            };
            ///
            GetAllComponentSystem();
        }
        /// <summary>
        /// 
        /// </summary>
        private Dev_Component.ComuPLCLink mComPLCLink = null;
        ///
        private Dev_AppStation.TesterStation.PC1_SFIT mPC1_SFIT = null;
        ///
        private Dev_AppStation.TesterStation.PC2_SFIT mPC2_SFIT = null;
        ///
        private Dev_AppStation.TesterStation.PC3_AGING mPC3_ANGING = null;
        ///
        private Dev_AppStation.TesterStation.PC5_WD mPC5_WD = null;
        /// <summary>
        /// 
        /// </summary>
        private void GetAllComponentSystem()
        {
            ///
            mLogResult = X_CoreS.GetComponent(Dev_AppMachine.StaticName.DATA_LOGRESULT) as Dev_AppStation.Data.LoggingResult;
            ///
            //Dev_AppMachine.Machine.This.LogsHeader = mLogResult.Partheader;
            ///
            DgvInitializing();
            ///
            mComPLCLink = X_CoreS.GetComponent(Dev_AppMachine.StaticName.PLC_TATTURN) as Dev_Component.ComuPLCLink;
            ///
            mPC1_SFIT = X_CoreS.GetComponent(Dev_AppMachine.StaticName.ST_PC1_SFIT) as Dev_AppStation.TesterStation.PC1_SFIT;
            ///
            mPC2_SFIT = X_CoreS.GetComponent(Dev_AppMachine.StaticName.ST_PC2_SFIT) as Dev_AppStation.TesterStation.PC2_SFIT;

            mPC3_ANGING = X_CoreS.GetComponent(Dev_AppMachine.StaticName.ST_PC3_AGING) as Dev_AppStation.TesterStation.PC3_AGING;
            ///
            mPC5_WD = X_CoreS.GetComponent(Dev_AppMachine.StaticName.ST_PC4_WD) as Dev_AppStation.TesterStation.PC5_WD;
            ///
            X_CoreS.RegisterOnChanged(() => mPC1_SFIT.CerrentResult, OnChangedUpDateDataGridView);
            ///
            X_CoreS.RegisterOnChanged(() => mPC2_SFIT.CerrentResult, OnChangedUpDateDataGridView);
            ///
            X_CoreS.RegisterOnChanged(() => mPC3_ANGING.CerrentResult, OnChangedUpDateDataGridView);
            ///
            X_CoreS.RegisterOnChanged(() => mPC5_WD.CerrentResult, OnChangedUpDateDataGridView);
        }
        //char[] x = { 'A', 'B', 'C', 'D', 'E' };
        //var index = x.Select((c, i) => new { c, i }).SingleOrDefault(c => c.Equals('D')).i
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newVal"></param>
        private void OnChangedUpDateDataGridView(string result)
        {
            ///
            lock (this)
            {
                ///
                var srt2Dcode = result.Split(',')[2]; var strStationId = result.Split(',')[3];
                ///
                if (string.IsNullOrEmpty(srt2Dcode))
                    ///
                    X_CoreS.LogAlarmPopup("Null data 2dCode",this.Name);
                ///
                dgvResultPart.Invoke(new EventHandler(delegate
                {
                    var row = MatchingRowIndex(dgvResultPart, dgvResultPart.Columns[3].HeaderText, srt2Dcode);
                    ///
                    if (row == null)
                    {
                        /// Add New data rows
                        result = string.Format("{0},{1}", (dgvResultPart.Rows.Count + 1).ToString(), result); this.dgvResultPart.Rows.Add(result.Split(','));
                    }
                    else if(row.Index == -1)                   
                    {
                        /// Add New data rows
                        result = string.Format("{0},{1}", "1", result); this.dgvResultPart.Rows.Add(result.Split(',')); 
                    }
                    else
                    {   ///
                        int index = 0; int resultIndex = (strStationId == "01") ? 0 : 3;
                        switch (strStationId)
                        {
                            case "01":
                                index = 1;
                                break;
                            case "02":
                                index = 8;
                                break;
                            case "03":
                                index = 12;
                                break;
                            case "04":
                                index = 16;
                                break;
                        }
                        ///
                        for (int i = 0; i < result.Split(',').Length - resultIndex; i++)
                            ///
                            dgvResultPart.Rows[row.Index].Cells[index + i].Value = result.Split(',')[resultIndex + i];                        
                    }                                      
                })); dgvResultPart.Update(); dgvResultPart.Refresh();
            }           
        }

        /// <summary>
        /// 
        /// </summary>
        private void JenerateParamPart()
        {
            int i = 0;
            int ofset = 20;
            appPartJigColSFitViewss1.PartJigViews.All((x) =>
            {
                x.CDPlayer.Mem2DCode = (1010 + (i * ofset));               
                x.CDPlayer.MemResult = (1020 + (i * ofset));
                x.CDPlayer.PartId = i++;
                return true;
            });
            i = 0;
            appPartJigColSFitViewss2.PartJigViews.All((x) =>
            {
                x.CDPlayer.Mem2DCode = (1120 + (i * ofset));
                x.CDPlayer.MemResult = (1130+ (i * ofset));
                x.CDPlayer.PartId = i++;
                return true;
            });
            i = 0;
            appPartJigColAngingView1.PartJigViews.All((x) =>
            {
                x.CDPlayer.Mem2DCode = (1220 + (i * ofset));
                x.CDPlayer.MemResult = (1230 + (i * ofset));
                x.CDPlayer.PartId = i++;
                return true;
            });
            i = 0;
            appPartJigColWDView1.PartJigViews.All((x) =>
            {
                x.CDPlayer.Mem2DCode = (1520 + (i * ofset));
                x.CDPlayer.MemResult = (1530 + (i * ofset));
                x.CDPlayer.PartId = i++;
                return true;
            });
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetMemMemControlPart()
        {
            Dev_AppMachine.Machine.This.PartJigColSfit1View.MemControlPart = 1002;
            ///
            Dev_AppMachine.Machine.This.PartJigColSfit2View.MemControlPart = 1112;
            ///
            Dev_AppMachine.Machine.This.PartJigColAngingView.MemControlPart = 1212;
            ///
            Dev_AppMachine.Machine.This.PartJigColWDView.MemControlPart = 1512;
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetMemMemConfirmPart()
        {
            Dev_AppMachine.Machine.This.PartJigColSfit1View.MemConfirmPart = 1000;
            ///
            Dev_AppMachine.Machine.This.PartJigColSfit2View.MemConfirmPart = 1110;
            ///
            Dev_AppMachine.Machine.This.PartJigColAngingView.MemConfirmPart = 1210;
            ///
            Dev_AppMachine.Machine.This.PartJigColWDView.MemConfirmPart = 1510;
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetMemStatusJigEmpty()
        {
            Dev_AppMachine.Machine.This.PartJigColSfit1View.MemStatusJigEmpty = 1006;
            ///
            Dev_AppMachine.Machine.This.PartJigColSfit2View.MemStatusJigEmpty = 1116;
            ///
            Dev_AppMachine.Machine.This.PartJigColAngingView.MemStatusJigEmpty = 1216;
            ///
            Dev_AppMachine.Machine.This.PartJigColWDView.MemStatusJigEmpty = 1516;
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetMemNotUseJig()
        {
            Dev_AppMachine.Machine.This.PartJigColSfit1View.MemNotUseJig = 1007;
            ///
            Dev_AppMachine.Machine.This.PartJigColSfit2View.MemNotUseJig = 1117;
            ///
            Dev_AppMachine.Machine.This.PartJigColAngingView.MemNotUseJig = 1217;
            ///
            Dev_AppMachine.Machine.This.PartJigColWDView.MemNotUseJig = 1517;
        }
        /// <summary>
        /// 
        /// </summary>
        private void DgvInitializing()
        {
            //if (x != null)
            //{
            dgvResultPart.AutoGenerateColumns = false;
            ///
            dgvResultPart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            //throw new Exception("Error");
            ///
            DataGridViewTextBoxColumn dgvTbColumns;
            ///
            int i = 0;
            ///
            mLogResult.Partheader.ToList().ForEach(HeaderName =>
            {
                dgvTbColumns = new DataGridViewTextBoxColumn();
                ///
                dgvTbColumns.HeaderText = HeaderName.ToString();
                ///
                dgvTbColumns.Width = 50;
                ///
                dgvResultPart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                ///
                dgvResultPart.ColumnHeadersHeight = 200;
                ///
                dgvResultPart.Columns.Add(dgvTbColumns);
                ///
                if (HeaderName.IndexOf("FinalResult") > -1)
                {
                    DataGridViewColumn dataGridViewColumn = dgvResultPart.Columns[i];
                    ///
                    dataGridViewColumn.HeaderCell.Style.BackColor = Color.Yellow;
                    ///
                    dataGridViewColumn.HeaderCell.Style.ForeColor = Color.Green;
                }
                dgvResultPart.EnableHeadersVisualStyles = false;

                //dataGridViews[(int)x.StationNo].Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                ///
                i++;
            }); dgvResultPart.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MainTableDisplayHeader"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetAllDataStationByDataGrid(Dictionary<string, string> MainTableDisplayHeader, string searchValue)
        {
            try
            {
                if (MainTableDisplayHeader == null)
                    return null;
                ///
                //dataGridViews.ToList().ForEach(x =>
                //{
                    if (dgvResultPart != null)
                    {
                        var row = MatchingRowIndex(dgvResultPart, dgvResultPart.Columns[0].HeaderText, searchValue);

                        if (row != null)
                        {
                            for (int i = 0; i < dgvResultPart.ColumnCount; i++)
                            {
                                MainTableDisplayHeader[dgvResultPart.Columns[i].HeaderText.ToString()] = row.Cells[i].Value.ToString();//x.Columns[i].HeaderText.ToString()
                            }
                        }
                    }
                    //});
                    return MainTableDisplayHeader;
            }
              catch (Exception ex)
            {
                return MainTableDisplayHeader;

            }
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="columnName"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static DataGridViewRow MatchingRowIndex(DataGridView dgv, string columnName, string searchValue)
        {
            DataGridViewRow row = new DataGridViewRow();
            ///
            int rowIndex = -1;
            ///
            bool tempAllowUserToAddRows = dgv.AllowUserToAddRows;
            ///
            dgv.AllowUserToAddRows = false; // Turn off or .Value below will throw null exception
            ///
            if (dgv.Rows.Count > 0 && dgv.Columns.Count > 0 && dgv.Columns[3].HeaderText != null)//&& dgv.Columns[columnName] != null
            {
                row = dgv.Rows
               .Cast<DataGridViewRow>()
               .FirstOrDefault(r => r.Cells[3].Value.ToString().Equals(searchValue));

                //rowIndex = row.Index;
            }
            ///
            dgv.AllowUserToAddRows = tempAllowUserToAddRows;
            ///
            return row;
        }
        /// <summary>
        /// 
        /// </summary>
        private int counter = 0;
        ///
        private string str2dcode = "";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butEnter_Click(object sender, EventArgs e)
        {
            str2dcode = txtRead2DCode.Text.Trim();
            ///
            if (string.IsNullOrEmpty(str2dcode)) return;

            var strSub = str2dcode.Substring(0, str2dcode.Length - 3);
            ///
            counter++;
            ///
            var strCount = counter.ToString();
            ///
            if (strCount.Length != 3)
            {
                var num = strCount.Length;
                ///
                for (int i = 0; i < (3 - num); i++)
                    ///
                    strCount = string.Format("{0}{1}", "0", strCount);
                ///
                strSub += strCount;
                ///
                str2dcode = strSub;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRead2DCode_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {              
                if (txtRead2DCode != null)
                {
                    ///
                    butEnter_Click(sender, e);
                    ///
                    rtbStr2DList.AppendText(str2dcode + Environment.NewLine);
                    ///
                    txtRead2DCode.Clear(); txtRead2DCode.Refresh(); txtRead2DCode.Focus();
                    ///
                    Confirm2DCodeByServer(str2dcode);
                }
            }
        }
        bool ServerConfiem = true;
        /// <summary>
        /// 
        /// </summary>
        private void Confirm2DCodeByServer(string str2dcode)
        {
            if(ServerConfiem)
            {
                mComPLCLink.SetCsvFileNameDownloadToPLC("R1610",str2dcode,10);
            }
        }
    }
}
