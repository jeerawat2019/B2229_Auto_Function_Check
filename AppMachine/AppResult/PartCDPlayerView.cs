using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMachine.AppResult
{
    public class PartCDPlayerView : PartResultBase
    {
        public PartCDPlayerView() { }
        public PartCDPlayerView(string name, int partId)
            : base(name, partId)
        {
            this.PartId = partId;
        }
    }
}
