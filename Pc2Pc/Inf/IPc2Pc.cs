using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pc2Pc.Model;
using X_Unit;

namespace Pc2Pc.Inf
{
    interface IPc2Pc
    {
        Pc2PcModel mPc2PcModel { get; set; }
        void InitializeComport();
        JigCommand Pc2PcSetCommand(JigCommand jigStation);
        string OnSendPortCommand(string cmdSend, Miliseconds timout, bool isNeedReply = true);
        //string OnSendPortCommand(string command, Miliseconds timout);

    }
}
