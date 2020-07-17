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
            JeneratePartId();
            ///
            Dev_AppMachine.Machine.This.PartJigColSfit1 = appPartJigColSFitViewss1;
            ///
            Dev_AppMachine.Machine.This.PartJigColSfit2 = appPartJigColSFitViewss2;
            ///
            Dev_AppMachine.Machine.This.PartJigColAngingView = appPartJigColAngingView1;
            ///
            Dev_AppMachine.Machine.This.PartJigColWDView = appPartJigColWDView1;
            ///
            SetMemConfigPart();
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
            DgvInitializing();
            ///
            mComPLCLink = X_CoreS.GetComponent(Dev_AppMachine.StaticName.PLC_TATTURN) as Dev_Component.ComuPLCLink;
            ///
            mPC1_SFIT = X_CoreS.GetComponent(Dev_AppMachine.StaticName.ST_PC1_SFIT) as Dev_AppStation.TesterStation.PC1_SFIT;
            ///
            X_CoreS.RegisterOnChanged(() => mPC1_SFIT.CerrentResult, OnChangedUpDateDataGridView);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newVal"></param>
        private void OnChangedUpDateDataGridView(string newVal)
        {
           
        }

        /// <summary>
        /// 
        /// </summary>
        private void JeneratePartId()
        {
            int i = 0;
            int ofset = 20;
            appPartJigColSFitViewss1.PartJigViews.All((x) =>
            {
                x.CDPlayer.PartDataMemory = (1010 + (i * ofset));
                x.CDPlayer.PartId = i++;
               
                return true;
            });
            i = 0;
            appPartJigColSFitViewss2.PartJigViews.All((x) =>
            {
                x.CDPlayer.PartDataMemory = (1120 + (i * ofset));
                x.CDPlayer.PartId = i++;
                return true;
            });
            i = 0;
            appPartJigColAngingView1.PartJigViews.All((x) =>
            {
                x.CDPlayer.PartDataMemory = (1220 + (i * ofset));
                x.CDPlayer.PartId = i++;
                return true;
            });
            i = 0;
            appPartJigColWDView1.PartJigViews.All((x) =>
            {
                x.CDPlayer.PartDataMemory = (1520 + (i * ofset));
                x.CDPlayer.PartId = i++;
                return true;
            });
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetMemConfigPart()
        {
            Dev_AppMachine.Machine.This.PartJigColSfit1.MemControlPart = 1000;
            ///
            Dev_AppMachine.Machine.This.PartJigColSfit2.MemControlPart = 1110;
            ///
            Dev_AppMachine.Machine.This.PartJigColAngingView.MemControlPart = 1210;
            ///
            Dev_AppMachine.Machine.This.PartJigColWDView.MemControlPart = 1510;
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
            //if(strData2D.Length == 12)
            //{
                var strSub = str2dcode.Substring(0, str2dcode.Length-3);
                ///
                counter++;
                ///
                var strCount = counter.ToString();
                ///
                if(strCount.Length != 3)
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
            //}
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
