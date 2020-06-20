using Pc2Pc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using X_Core.CompElement;
using X_Unit;

namespace AiComp.ConnectType.Pc2Pc.Inf
{
    interface IPc2Pc
    {
        Pc2PcModel Pc2PcModels { get; set; }
        void InitializeComport();
        JigCommand Pc2PcSetCommand(JigCommand jigStation);
        string OnSendPortCommand(string cmdSend, Miliseconds timout, bool isNeedReply = true);
        //string OnSendPortCommand(string command, Miliseconds timout);

    }
}
