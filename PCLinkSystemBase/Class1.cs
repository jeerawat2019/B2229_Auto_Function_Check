using AiComp.ConnectType.Commu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X_Core.CompElement;

namespace AiComp.ConnectType.PCLinkSystemBase
{
    public class DataLink : CompBase
    {
        CommuBase mCommu = null;
        public DataLink() { }
        public DataLink(string name) : base(name) { }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void InitializeIDReferences()
        {
            base.InitializeIDReferences();
            mCommu = GetParent<CommuBase>();
        }
    }
}
