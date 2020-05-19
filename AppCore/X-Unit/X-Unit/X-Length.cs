using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace X_Unit
{
    public delegate void MillimeterEventHandler(Millimeters mm);
    public delegate void MicronEventHandler(Microns um);


    /// <summary>
    /// Delegate for Millimeters/MillimetersPerSecond
    /// </summary>
    /// <param name="mm"></param>
    /// <param name="mmps"></param>
    public delegate void delVoid_MillimeterMillimeterPerSec(Millimeters mm, MillimetersPerSecond mmps);


    /// <summary>
    /// Base class for Length
    /// </summary>
    public abstract class X_Length : X_UnitBase
    {
        /// <summary>
        /// Conversion factor to base value
        /// </summary>
        public abstract double MicronsPerUnit
        {
            get;
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        public X_Length()
        {
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        /// <param name="initialVal"></param>
        public X_Length(double initialVal)
            :base(initialVal)
        {
        }
        /// <summary>
        /// Conversion from string constructor
        /// </summary>
        /// <param name="sVal"></param>
        public X_Length(string sVal)
            : base(sVal)
        {

        }
        /// <summary>
        /// Convert the number to the base number (microns)
        /// </summary>
        public double ToMicrons
        {
            get { return Val * MicronsPerUnit; }
        }
        /// <summary>
        /// Convert the number to the base number (microns)
        /// </summary>
        public double ToMM
        {
            get { return ToMicrons / 1000.0; }
        }
    }
    /// <summary>
    /// Microns (um)
    /// </summary>
    [TypeConverterAttribute(typeof(X_DoubleConverter<Microns>))]
    public class Microns : X_Length
    {
        /// <summary>
        /// Conversion factor to base value
        /// </summary>
        public override double MicronsPerUnit
        {
            get { return 1.0; }
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        public Microns()
        {
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        /// <param name="initialVal"></param>
        public Microns(double initialVal)
            :base(initialVal)
        {
        }
        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="val"></param>
        public Microns(X_Length val)
        {
            _val = val.ToMicrons;
        }
        /// <summary>
        ///  Get the units
        /// </summary>
        public override string UnitText
        {
            get { return "um"; }
        }

        /// <summary>
        /// Convert from a double
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator Microns(double val)
        {
            Microns newVal = new Microns();
            newVal._val = val;
            return newVal;
        }
        /// <summary>
        /// Convert from a milimeters
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator Microns(Millimeters val)
        {
            Microns newVal = new Microns();
            newVal._val = val.ToMicrons;
            return newVal;
        }
        /// <summary>
        /// Convert from a Nanometers
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator Microns(Nanometers val)
        {
            Microns newVal = new Microns();
            newVal._val = val.ToMicrons;
            return newVal;
        }
        /// <summary>
        /// Addition
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public static Microns operator +(Microns val1, X_Length val2)
        {
            Microns newVal = new Microns();
            newVal._val = val1.ToMicrons + val2.ToMicrons;
            return newVal;
        }
        /// <summary>
        /// Subtraction
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public static Microns operator -(Microns val1, X_Length val2)
        {
            Microns newVal = new Microns();
            newVal._val = val1.ToMicrons - val2.ToMicrons;
            return newVal;
        }
    }
    /// <summary>
    /// Milimeters (mm)
    /// </summary>
    [TypeConverterAttribute(typeof(X_DoubleConverter<Millimeters>))]
    public class Millimeters : X_Length
    {
        /// <summary>
        /// Conversion factor to base value
        /// </summary>
        public override double MicronsPerUnit
        {
            get { return 1000.0; }
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        public Millimeters()
        {
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        /// <param name="initialVal"></param>
        public Millimeters(double initialVal)
            :base(initialVal)
        {
        }
        /// <summary>
        /// Conversion from string constructor
        /// </summary>
        /// <param name="sVal"></param>
        public Millimeters(string sVal)
            : base(sVal)
        {

        }
        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="val"></param>
        public Millimeters(X_Length val)
        {
            _val = val.ToMicrons / MicronsPerUnit;
        }
        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="val"></param>
        public Millimeters(X_LengthSpeed val)
        {
            _val = val.ToMicronsPerSecond / MicronsPerUnit;
        }
        /// <summary>
        ///  Get the units
        /// </summary>
        public override string UnitText
        {
            get { return "mm"; }
        }

        /// <summary>
        /// Convert from a double
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator Millimeters(double val)
        {
            Millimeters newVal = new Millimeters();
            newVal._val = val;
            return newVal;
        }
        /// <summary>
        /// Convert from a Microns
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator Millimeters(Microns val)
        {
            Millimeters newVal = new Millimeters();
            newVal._val = val.Val / newVal.MicronsPerUnit;
            return newVal;
        }
        /// <summary>
        /// Convert from a Nanometers
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator Millimeters(Nanometers val)
        {
            Millimeters newVal = new Millimeters();
            newVal._val = val.ToMicrons / newVal.MicronsPerUnit;
            return newVal;
        }
        /// <summary>
        /// Addition
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public static Millimeters operator +(Millimeters val1, X_Length val2)
        {
            Millimeters newVal = new Millimeters();
            newVal._val = (val1.ToMicrons + val2.ToMicrons) / newVal.MicronsPerUnit;
            return newVal;
        }
        /// <summary>
        /// Subtraction
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public static Millimeters operator -(Millimeters val1, X_Length val2)
        {
            Millimeters newVal = new Millimeters();
            newVal._val = (val1.ToMicrons - val2.ToMicrons) / newVal.MicronsPerUnit;
            return newVal;
        }
    }
    /// <summary>
    /// Nano-Meters (nm)
    /// </summary>
    [TypeConverterAttribute(typeof(X_DoubleConverter<Nanometers>))]
    public class Nanometers : X_Length
    {
        /// <summary>
        /// Conversion factor to base value
        /// </summary>
        public override double MicronsPerUnit
        {
            get { return 0.001; }
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        public Nanometers()
        {
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        /// <param name="initialVal"></param>
        public Nanometers(double initialVal)
            :base(initialVal)
        {
        }
        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="val"></param>
        public Nanometers(X_Length val)
        {
            _val = val.ToMicrons / MicronsPerUnit;
        }
        /// <summary>
        ///  Get the units
        /// </summary>
        public override string UnitText
        {
            get { return "nm"; }
        }

        /// <summary>
        /// Convert from a double
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator Nanometers(double val)
        {
            Nanometers newVal = new Nanometers();
            newVal._val = val;
            return newVal;
        }
        /// <summary>
        /// Convert from a milimeters
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator Nanometers(Millimeters val)
        {
            Nanometers newVal = new Nanometers();
            newVal._val = val.ToMicrons / newVal.MicronsPerUnit;
            return newVal;
        }
        /// <summary>
        /// Convert from a Microns
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator Nanometers(Microns val)
        {
            Nanometers newVal = new Nanometers();
            newVal._val = val.ToMicrons / newVal.MicronsPerUnit;
            return newVal;
        }
        /// <summary>
        /// Addition
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public static Nanometers operator +(Nanometers val1, X_Length val2)
        {
            Nanometers newVal = new Nanometers();
            newVal._val = (val1.ToMicrons + val2.ToMicrons) / newVal.MicronsPerUnit;
            return newVal;
        }
        /// <summary>
        /// Subtraction
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public static Nanometers operator -(Nanometers val1, X_Length val2)
        {
            Nanometers newVal = new Nanometers();
            newVal._val = (val1.ToMicrons - val2.ToMicrons) / newVal.MicronsPerUnit;
            return newVal;
        }
    }
}
