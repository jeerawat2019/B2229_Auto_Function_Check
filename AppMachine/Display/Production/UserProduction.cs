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

namespace B2229_AT_FuncCheck.Display.Production
{
    public partial class UserProduction : AppUserControlBase
    {
        public UserProduction()
        {
            //InitializeComponent();
        }
        ///
        protected override void Initializing()
        {
            base.Initializing();
            ///
            InitializeComponent();
            ///
            InitializeComponentPart();
           
        }

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
        }
        private void JeneratePartId()
        {
            int i = 0;
            appPartJigColSFitViewss1.PartJigViews.All((x) =>
            {
                x.CDPlayer.PartId = i++;
                return true;
            });
            appPartJigColSFitViewss2.PartJigViews.All((x) =>
            {
                x.CDPlayer.PartId = i++;
                return true;
            });
            appPartJigColAngingView1.PartJigViews.All((x) =>
            {
                x.CDPlayer.PartId = i++;
                return true;
            });
            appPartJigColWDView1.PartJigViews.All((x) =>
            {
                x.CDPlayer.PartId = i++;
                return true;
            });
        }
    }
}
