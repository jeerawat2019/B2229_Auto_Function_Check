using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace X_Unit
{
    /// <summary>
    /// Base class for Weight
    /// </summary>
    public abstract class X_Weight : X_UnitBase
    {
        /// <summary>
        /// Conversion factor to base value
        /// </summary>
        public abstract double GramsPerUnit
        {
            get;
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        public X_Weight()
        {
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        /// <param name="initialVal"></param>
        public X_Weight(double initialVal)
            :base(initialVal)
        {
        }
        /// <summary>
        /// Convert the number to the base number (grams)
        /// </summary>
        public double ToGrams
        {
            get { return Val * GramsPerUnit; }
        }
    }
    /// <summary>
    /// Grams (gm)
    /// </summary>
    [TypeConverterAttribute(typeof(X_DoubleConverter<Grams>))]
    public class Grams : X_Weight
    {
        /// <summary>
        /// Conversion factor to base value
        /// </summary>
        public override double GramsPerUnit
        {
            get { return 1.0; }
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        public Grams()
        {
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        /// <param name="initialVal"></param>
        public Grams(double initialVal)
            :base(initialVal)
        {
        }
        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="val"></param>
        public Grams(X_Weight val)
        {
            _val = val.ToGrams;
        }
        /// <summary>
        ///  Get the units
        /// </summary>
        public override string UnitText
        {
            get { return "gm"; }
        }
        /// <summary>
        /// Convert from a double
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator Grams(double val)
        {
            Grams newVal = new Grams();
            newVal._val = val;
            return newVal;
        }
        /// <summary>
        /// Convert from Milligrams
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator Grams(Milligrams val)
        {
            Grams newVal = new Grams();
            newVal._val = val.ToGrams;
            return newVal;
        }
        /// <summary>
        /// Addition
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public static Grams operator +(Grams val1, X_Weight val2)
        {
            Grams newVal = new Grams();
            newVal._val = val1.ToGrams + val2.ToGrams;
            return newVal;
        }
        /// <summary>
        /// Subtraction
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public static Grams operator -(Grams val1, X_Weight val2)
        {
            Grams newVal = new Grams();
            newVal._val = val1.ToGrams - val2.ToGrams;
            return newVal;
        }
    }
    /// <summary>
    /// Milligrams (mg)
    /// </summary>
    [TypeConverterAttribute(typeof(X_DoubleConverter<Milligrams>))]
    public class Milligrams : X_Weight
    {
        /// <summary>
        /// Conversion factor to base value
        /// </summary>
        public override double GramsPerUnit
        {
            get { return 0.001; }
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        public Milligrams()
        {
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        /// <param name="initialVal"></param>
        public Milligrams(double initialVal)
            : base(initialVal)
        {
        }
        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="val"></param>
        public Milligrams(X_Weight val)
        {
            _val = val.ToGrams / GramsPerUnit;
        }
        /// <summary>
        ///  Get the units
        /// </summary>
        public override string UnitText
        {
            get { return "mg"; }
        }
        /// <summary>
        /// Convert from a double
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator Milligrams(double val)
        {
            Milligrams newVal = new Milligrams();
            newVal._val = val;
            return newVal;
        }
        /// <summary>
        /// Convert from Grams
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator Milligrams(Grams val)
        {
            Milligrams newVal = new Milligrams();
            newVal._val = val.ToGrams / newVal.GramsPerUnit;
            return newVal;
        }
        /// <summary>
        /// Addition
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public static Milligrams operator +(Milligrams val1, X_Weight val2)
        {
            Milligrams newVal = new Milligrams();
            newVal._val = (val1.ToGrams + val2.ToGrams) / newVal.GramsPerUnit;
            return newVal;
        }
        /// <summary>
        /// Subtraction
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public static Milligrams operator -(Milligrams val1, X_Weight val2)
        {
            Milligrams newVal = new Milligrams();
            newVal._val = (val1.ToGrams - val2.ToGrams) / newVal.GramsPerUnit;
            return newVal;
        }
    }
}
