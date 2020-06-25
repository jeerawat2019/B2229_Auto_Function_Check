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
using AppMachine.AppResult;

namespace B2229_AT_FuncCheck.AppResult.AppConsignePart
{
    public partial class AppPartJigView : AppUserControlBase//UserControl
    {
        public PartCDPlayerView CDPlayer = new PartCDPlayerView();
        /// <summary>
        /// 
        /// </summary>
        public AppPartJigView()
        {
            InitializeComponent();
        }
        public AppPartJigView(PartCDPlayerView cdplayer)
        {
            CDPlayer = cdplayer;
        }
        protected override void Initializing()
        {
            lblPartId.Text = (CDPlayer.PartId + 1).ToString();
            //OnStatusChanged(_part.PartStatus);
            X_CoreS.RegisterOnChanged(() => CDPlayer.PartStatus, OnStatusChanged);
            X_CoreS.RegisterOnChanged(() => CDPlayer.IsProcess, OnProcessChanged);
            X_CoreS.RegisterOnChanged(() => CDPlayer.PartId, OnPartIdChanged);
        }

        private void OnPartIdChanged(int newVal)
        {
            lblPartId.Text = (newVal + 1).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newVal"></param>
        private void OnProcessChanged(Part.Process newVal)
        {
            switch(newVal)
            {
                case Part.Process.Empty:               
                    lblResult.BackColor = Color.Yellow;
                    break;
                case Part.Process.Start:
                    lblResult.BackColor = Color.Brown;
                    break;
                case Part.Process.Testting:

                    lblResult.BackColor = Color.Red;
                    break;
                case Part.Process.Finnish:

                    lblResult.BackColor = Color.GreenYellow;
                    break;
            }   
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        private void OnStatusChanged(string status)
        {
            switch (status)
            {
                case "N/A":
                    lblPartId.BackColor = Color.Yellow;
                    this.Update();
                    break;
                case "NG":
                    lblPartId.BackColor = Color.Red;
                    this.Update();
                    break;
                case "OK":
                    lblPartId.BackColor = Color.Lime;
                    this.Update();
                    break;
                case "MISSING":
                default:
                    lblPartId.BackColor = Color.DimGray;
                    this.Update();
                    break;

            }
        }
    }
}
