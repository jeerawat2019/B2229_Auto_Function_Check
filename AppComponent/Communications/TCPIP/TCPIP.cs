using AiComp.ConnectType.Commu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Xml.Serialization;



namespace AiComp.ConnectType.Commu
{
    public class TCPIP : CommuBase
    {

        #region private Data

        #endregion private Data


        #region Public Browsable Properties

        /// <summary>
        /// Get the Global IP address
        /// </summary>
        /// <returns></returns>
        [Browsable(true)]
        [Category("TCPIP")]
        public string IPAddress
        {
            get { return GetPropValue(() => IPAddress); }
            set { SetPropValue(() => IPAddress, value); }
        }

        /// <summary>
        /// Get the local PI address
        /// </summary>
        /// <returns></returns>
        [Browsable(true)]
        [Category("TCPIP")]
        public string LocalIPAddress
        {
            get { return GetPropValue(() => LocalIPAddress, "192.168.1.10"); }
            set { SetPropValue(() => LocalIPAddress, value); }
        }

        /// <summary>
        /// Get the Comm Port
        /// </summary>
        /// <returns></returns>
        [Browsable(true)]
        [Category("TCPIP")]
        public ushort Port
        {
            get { return GetPropValue(() => Port); }
            set { SetPropValue(() => Port, value); }
        }

        #endregion Public Browsable Properties



        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public TCPIP()
        {
        }

        /// <summary>
        /// Manual creation constructor
        /// </summary>
        /// <param name="name"></param>
        public TCPIP(string name)
            : base(name)
        {
        }
        #endregion Constructors


        #region Public Methods
        /// <summary>
        /// Initialize this component
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }


        /// <summary>
        /// Destroy the object
        /// </summary>
        public override void Destroy()
        {
            base.Destroy();
        }

        public byte[] ToByteArray()
        {
            string[] split = IPAddress.Split('.');

            return new byte[]
			{
			    Convert.ToByte(split[0]),
				Convert.ToByte(split[1]),
				Convert.ToByte(split[2]),
				Convert.ToByte(split[3])
            };

        }

        #endregion
    }
}
