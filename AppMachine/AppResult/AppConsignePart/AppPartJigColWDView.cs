﻿using System;
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
    public partial class AppPartJigColWDView : AppUserControlBase
    {
        private AppPartJigView[] mPartJigView = null;

        [XmlIgnore]
        public AppPartJigView[] PartJigViews
        {
            get
            {
                return mPartJigView;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public AppPartJigColWDView()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void Initializing()
        {
            mPartJigView = new AppPartJigView[]
            {
                appPartJigView1,

            };
            //appPartJigView1.CDPlayer.PartStatus = true;
        }
    }
}
