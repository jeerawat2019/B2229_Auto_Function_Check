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
        }
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
    }
}
