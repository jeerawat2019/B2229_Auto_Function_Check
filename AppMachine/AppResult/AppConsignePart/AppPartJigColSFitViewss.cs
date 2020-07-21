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
using System.Xml.Serialization;
using X_Core;
using X_Core.CompElement;

namespace B2229_AT_FuncCheck.AppResult.AppConsignePart
{
    public partial class AppPartJigColSFitViewss : AppUserControlBase 
    {
        private AppPartJigView[] mPartJigView = null;

        [XmlIgnore]
        public AppPartJigView[] PartJigViews
        {
            get 
            { 
                Initializing();
                return mPartJigView;
            }
            set { mPartJigView = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        //[XmlIgnore]
        //public int MemControlPart
        //{
        //    get;
        //    set;
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //[XmlIgnore]
        //public string MemBinProcess
        //{
        //    get;
        //    set;
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //[XmlIgnore]
        //public int MemRang
        //{
        //    get;
        //    set;
        //} = 10;
        /// <summary>
        /// 
        /// </summary>
        public int MemConfirmPart { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public AppPartJigColSFitViewss()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void Initializing()
        {

            AddPart();
            //appPartJigView1.CDPlayer.PartStatus = true;
        }
        private void AddPart()
        {
            mPartJigView = new AppPartJigView[]
            {
                appPartJigView1,
                appPartJigView2,
                appPartJigView3,
                appPartJigView4

            };
        }
    }
}
