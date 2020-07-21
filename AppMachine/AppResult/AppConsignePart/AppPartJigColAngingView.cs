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

namespace B2229_AT_FuncCheck.AppResult.AppConsignePart
{
    public partial class AppPartJigColAngingView : AppUserControlBase
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
        
        public int MemConfirmPart { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public AppPartJigColAngingView()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void Initializing()
        {
            AddPart();
        }
        private void AddPart()
        {
            mPartJigView = new AppPartJigView[]
            {
                appPartJigView1,
                appPartJigView2,
                appPartJigView3,
                appPartJigView4,
                appPartJigView5,
                appPartJigView6,
                appPartJigView7,
                appPartJigView8,
                appPartJigView9,
                appPartJigView10,
                appPartJigView11,
                appPartJigView12,
                appPartJigView13,
            };
            //appPartJigView1.CDPlayer.PartStatus = true;
        }
    }
}
