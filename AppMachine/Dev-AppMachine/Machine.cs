using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using X_Core;
using X_Core.CompElement;
using X_Core.ControlElement;

namespace B2229_AT_FuncCheck.Dev_AppMachine
{
    public class Machine : CompBase
    {
        public Machine() { }
        public Machine(string name) : base(name) { }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void InitializeIDReferences()
        {
            base.InitializeIDReferences();
        }
    }
}
