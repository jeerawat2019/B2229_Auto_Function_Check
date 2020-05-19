using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace X_Unit
{
    /// <summary>
    /// Base class for Pressure
    /// </summary>
    public abstract class X_Pressure : X_UnitBase
    {
        /// <summary>
        /// Conversion factor to base value
        /// </summary>
        public abstract double PascalsPerUnit
        {
            get;
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        public X_Pressure()
        {
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        /// <param name="initialVal"></param>
        public X_Pressure(double initialVal)
            :base(initialVal)
        {
        }
        /// <summary>
        /// Convert the number to the base number (grams)
        /// </summary>
        public double ToPascals
        {
            get { return Val * PascalsPerUnit; }
        }
    }
    /// <summary>
    /// Grams (gm)
    /// </summary>
    [TypeConverterAttribute(typeof(X_DoubleConverter<Grams>))]
    public class Pascal : X_Pressure
    {
        /// <summary>
        /// Conversion factor to base value
        /// </summary>
        public override double PascalsPerUnit
        {
            get { return 1.0; }
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        public Pascal()
        {
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        /// <param name="initialVal"></param>
        public Pascal(double initialVal)
            :base(initialVal)
        {
        }
        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="val"></param>
        public Pascal(X_Pressure val)
        {
            _val = val.ToPascals;
        }
        /// <summary>
        /// Convert the number to the base number (pascals)
        /// </summary>
        public double ToPascals
        {
            get { return Val * PascalsPerUnit; }
        }
        /// <summary>
        ///  Get the units
        /// </summary>
        public override string UnitText
        {
            get { return "Pa"; }
        }
        /// <summary>
        /// Convert from a double
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator Pascal(double val)
        {
            Pascal newVal = new Pascal();
            newVal._val = val;
            return newVal;
        }
        /// <summary>
        /// Convert from KiloPascal
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator Pascal(KiloPascal val)
        {
            Pascal newVal = new Pascal();
            newVal._val = val.ToPascals;
            return newVal;
        }
        /// <summary>
        /// Addition
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public static Pascal operator +(Pascal val1, X_Pressure val2)
        {
            Pascal newVal = new Pascal();
            newVal._val = val1.ToPascals + val2.ToPascals;
            return newVal;
        }
        /// <summary>
        /// Subtraction
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public static Pascal operator -(Pascal val1, X_Pressure val2)
        {
            Pascal newVal = new Pascal();
            newVal._val = val1.ToPascals - val2.ToPascals;
            return newVal;
        }
    }
    /// <summary>
    /// KiloPascal (kPa)
    /// </summary>
    [TypeConverterAttribute(typeof(X_DoubleConverter<KiloPascal>))]
    public class KiloPascal : X_Pressure
    {
        /// <summary>
        /// Conversion factor to base value
        /// </summary>
        public override double PascalsPerUnit
        {
            get { return 1000.0; }
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        public KiloPascal()
        {
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        /// <param name="initialVal"></param>
        public KiloPascal(double initialVal)
            : base(initialVal)
        {
        }
        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="val"></param>
        public KiloPascal(X_Pressure val)
        {
            _val = val.ToPascals / PascalsPerUnit;
        }
        /// <summary>
        ///  Get the units
        /// </summary>
        public override string UnitText
        {
            get { return "kPa"; }
        }
        /// <summary>
        /// Convert from a double
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator KiloPascal(double val)
        {
            KiloPascal newVal = new KiloPascal();
            newVal._val = val;
            return newVal;
        }
        ///// <summary>
        ///// Convert from Grams
        ///// </summary>
        ///// <param name="val"></param>
        ///// <returns></returns>
        //public static implicit operator KiloPascal(Grams val)
        //{
        //    KiloPascal newVal = new KiloPascal();
        //    newVal._val = val.ToGrams / newVal.PascalsPerUnit;
        //    return newVal;
        //}
        /// <summary>
        /// Addition
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public static KiloPascal operator +(KiloPascal val1, X_Pressure val2)
        {
            KiloPascal newVal = new KiloPascal();
            newVal._val = (val1.ToPascals + val2.ToPascals) / newVal.PascalsPerUnit;
            return newVal;
        }
        /// <summary>
        /// Subtraction
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public static KiloPascal operator -(KiloPascal val1, X_Pressure val2)
        {
            KiloPascal newVal = new KiloPascal();
            newVal._val = (val1.ToPascals - val2.ToPascals) / newVal.PascalsPerUnit;
            return newVal;
        }
    }
}
