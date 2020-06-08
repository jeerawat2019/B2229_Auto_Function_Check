﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using X_Core.Comp.SMLib.Path;

namespace X_Core.Comp.SMLib.Flow
{
    public class SMReturnYes : SMExit
    {
        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SMReturnYes()
        {
        }
        /// <summary>
        /// Manual Creation Constructor
        /// </summary>
        /// <param name="text"></name>
        public SMReturnYes(string text)
            : base(text)
        {
            if (string.IsNullOrEmpty(text))
            {
                Text = "Return Yes";
            }
        }
        #endregion Constructors
        /// <summary>
        /// Text to be displayed in cursor
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Return Yes";
        }
    }
}