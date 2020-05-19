using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace X_Unit
{
    public delegate void MDoubleEventHandler(X_DoubleNoUnits dNoUnit);
    /// <summary>
    /// Base class for Length
    /// </summary>
    [TypeConverterAttribute(typeof(X_DoubleConverter<X_DoubleNoUnits>))]
    public class X_DoubleNoUnits : X_UnitBase
    {
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        public  X_DoubleNoUnits()
        {
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        /// <param name="initialVal"></param>
        public X_DoubleNoUnits(double initialVal)
            :base(initialVal)
        {
        }
        /// <summary>
        /// Convert from a double
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator X_DoubleNoUnits(double val)
        {
            X_DoubleNoUnits newVal = new X_DoubleNoUnits();
            newVal._val = val;
            return newVal;
        }
        /// <summary>
        /// Convert to a string.  Show the units
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Val.ToString("##0.###");
        }
    }
}
