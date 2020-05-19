using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace X_Unit
{
    /// <summary>
    /// Base class for Angle
    /// </summary>
    public abstract class X_Angle : X_UnitBase
    {
        /// <summary>
        /// Conversion factor to base value
        /// </summary>
        public abstract double DegreesPerUnit
        {
            get;
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        public X_Angle()
        {
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        /// <param name="initialVal"></param>
        public X_Angle(double initialVal)
            :base(initialVal)
        {
        }
        /// <summary>
        /// Conversion from string constructor
        /// </summary>
        /// <param name="sVal"></param>
        public X_Angle(string sVal)
            : base(sVal)
        {

        }
        /// <summary>
        /// Convert the number to degrees
        /// </summary>
        public double ToDegrees
        {
            get { return Val * DegreesPerUnit; }
        }

        /// <summary>
        /// Convert rad to degrees
        /// </summary>
        /// <param name="rad"></param>
        /// <returns></returns>
        public static double RadToDegrees(double rad)
        {
            return rad *180.0 / Math.PI;
        }
        /// <summary>
        /// Convert degrees to radians
        /// </summary>
        /// <param name="deg"></param>
        /// <returns></returns>
        public static double DegToRadians(double deg)
        {
            return deg * Math.PI / 180.0;
        }
    }
    /// <summary>
    /// Degrees (deg)
    /// </summary>
    [TypeConverterAttribute(typeof(X_DoubleConverter<Degrees>))]
    public class Degrees : X_Angle
    {
        /// <summary>
        /// Convert degrees to units
        /// </summary>
        public override double DegreesPerUnit
        {
            get { return 1.0; }
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        public Degrees()
        {
        }
        /// <summary>
        /// double constructor
        /// </summary>
        /// <param name="initialVal"></param>
        public Degrees(double initialVal)
            : base(initialVal)
        {
        }
        /// <summary>
        /// string constructor
        /// </summary>
        /// <param name="initialVal"></param>
        public Degrees(string initialVal)
            : base(initialVal)
        {
        }
        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="val"></param>
        public Degrees(X_Angle val)
        {
            _val = val.ToDegrees / DegreesPerUnit;
        }
        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="val"></param>
        public Degrees(X_RotarySpeed val)
        {
            _val = val.ToDegreesPerSecond / DegreesPerUnit;
        }
        /// <summary>
        /// return radians (rd)
        /// </summary>
        public double ToRadians
        {
            get { return DegToRadians(_val); }
        }
        /// <summary>
        ///  Get the units
        /// </summary>
        public override string UnitText
        {
            get { return "deg"; }
        }
        /// <summary>
        /// Convert from a double
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator Degrees(double val)
        {
            Degrees newVal = new Degrees();
            newVal._val = val;
            return newVal;
        }
        /// <summary>
        /// Convert from Radians
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator Degrees(Radians val)
        {
            Degrees newVal = new Degrees();
            newVal._val = val.ToDegrees;
            return newVal;
        }
        /// <summary>
        /// Convert from Cycles
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator Degrees(Cycles val)
        {
            Degrees newVal = new Degrees();
            newVal._val = val.ToDegrees;
            return newVal;
        }
        /// <summary>
        /// Addition
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public static Degrees operator +(Degrees val1, X_Angle val2)
        {
            Degrees newVal = new Degrees();
            newVal._val = val1.ToDegrees + val2.ToDegrees;
            return newVal;
        }
        /// <summary>
        /// Subtraction
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public static Degrees operator -(Degrees val1, X_Angle val2)
        {
            Degrees newVal = new Degrees();
            newVal._val = val1.ToDegrees - val2.ToDegrees;
            return newVal;
        }
    }
    /// <summary>
    /// Radians (rd)
    /// </summary>
    [TypeConverterAttribute(typeof(X_DoubleConverter<Radians>))]
    public class Radians : X_Angle
    {
        /// <summary>
        /// Convert degrees to units
        /// </summary>
        public override double DegreesPerUnit
        {
            get { return 180.0 / Math.PI; }
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        public Radians()
        {
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        /// <param name="initialVal"></param>
        public Radians(double initialVal)
            :base(initialVal)
        {
        }
        /// <summary>
        /// string constructor
        /// </summary>
        /// <param name="initialVal"></param>
        public Radians(string initialVal)
            : base(initialVal)
        {
        }
        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="val"></param>
        public Radians(X_Angle val)
        {
            _val = val.ToDegrees / DegreesPerUnit;
        }
        /// <summary>
        ///  Get the units
        /// </summary>
        public override string UnitText
        {
            get { return "rd"; }
        }
        /// <summary>
        /// Convert from a double
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator Radians(double val)
        {
            Radians newVal = new Radians();
            newVal._val = val;
            return newVal;
        }
        /// <summary>
        /// Convert from Degrees
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator Radians(Degrees val)
        {
            Radians newVal = new Radians();
            newVal._val = val.Val / newVal.DegreesPerUnit;
            return newVal;
        }
        /// <summary>
        /// Convert from Cycles
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator Radians(Cycles val)
        {
            Radians newVal = new Radians();
            newVal._val = val.ToDegrees / newVal.DegreesPerUnit;
            return newVal;
        }
        /// <summary>
        /// Addition
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public static Radians operator +(Radians val1, X_Angle val2)
        {
            Radians newVal = new Radians();
            newVal._val = (val1.ToDegrees + val2.ToDegrees) / newVal.DegreesPerUnit;
            return newVal;
        }
        /// <summary>
        /// Subtraction
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public static Radians operator -(Radians val1, X_Angle val2)
        {
            Radians newVal = new Radians();
            newVal._val = (val1.ToDegrees - val2.ToDegrees) / newVal.DegreesPerUnit;
            return newVal;
        }
    }
    /// <summary>
    /// cycles (cycles)
    /// </summary>
    [TypeConverterAttribute(typeof(X_DoubleConverter<Cycles>))]
    public class Cycles : X_Angle
    {
        /// <summary>
        /// Convert degrees to units
        /// </summary>
        public override double DegreesPerUnit
        {
            get { return 360.0; }
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        public Cycles()
        {
        }
        /// <summary>
        /// Serializing Constructor
        /// </summary>
        /// <param name="initialVal"></param>
        public Cycles(double initialVal)
            :base(initialVal)
        {
        }
        /// <summary>
        /// string constructor
        /// </summary>
        /// <param name="initialVal"></param>
        public Cycles(string initialVal)
            : base(initialVal)
        {
        }
        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="val"></param>
        public Cycles(X_Angle val)
        {
            _val = val.ToDegrees / DegreesPerUnit;
        }
        /// <summary>
        ///  Get the units
        /// </summary>
        public override string UnitText
        {
            get { return "cycles"; }
        }
        /// <summary>
        /// Convert from a double
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator Cycles(double val)
        {
            Cycles newVal = new Cycles();
            newVal._val = val;
            return newVal;
        }
        /// <summary>
        /// Convert from Radians
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator Cycles(Radians val)
        {
            Cycles newVal = new Cycles();
            newVal._val = val.ToDegrees / newVal.DegreesPerUnit;
            return newVal;
        }
        /// <summary>
        /// Convert from Degrees
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static implicit operator Cycles(Degrees val)
        {
            Cycles newVal = new Cycles();
            newVal._val = val.Val / newVal.DegreesPerUnit;
            return newVal;
        }
        /// <summary>
        /// Addition
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public static Cycles operator +(Cycles val1, X_Angle val2)
        {
            Cycles newVal = new Cycles();
            newVal._val = (val1.ToDegrees + val2.ToDegrees) / newVal.DegreesPerUnit;
            return newVal;
        }
        /// <summary>
        /// Subtraction
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public static Cycles operator -(Cycles val1, X_Angle val2)
        {
            Cycles newVal = new Cycles();
            newVal._val = (val1.ToDegrees - val2.ToDegrees) / newVal.DegreesPerUnit;
            return newVal;
        }
    }
}
