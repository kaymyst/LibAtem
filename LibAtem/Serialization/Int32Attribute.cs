using System;
using System.Linq;
using System.Reflection;

namespace LibAtem.Serialization
{
    public class Int32Attribute : SerializableAttributeBase
    {
        public override void Serialize(bool reverseBytes, byte[] data, uint start, object val)
        {
            byte[] bytes = BitConverter.GetBytes((int)val);
            data[start] = bytes[reverseBytes ? 3 : 0];
            data[start + 1] = bytes[reverseBytes ? 2 : 1];
            data[start + 2] = bytes[reverseBytes ? 1 : 2];
            data[start + 3] = bytes[reverseBytes ? 0 : 3];
        }

        public override object Deserialize(bool reverseBytes, byte[] data, uint start, PropertyInfo prop)
        {

            return (int)BitConverter.ToInt32(ReverseBytes(reverseBytes, data.Skip((int)start).Take(4)), 0);
        }

        public override bool AreEqual(object val1, object val2)
        {
            return Equals(val1, val2);
        }
    }

    public class Int32DAttribute : Int32Attribute, IRandomGeneratorAttribute
    {
        public double Scale { get; }
        public int ScaledMin { get; }
        public int ScaledMax { get; }

        public Int32DAttribute(double scale, int scaledMin, int scaledMax)
        {
            Scale = scale;
            ScaledMin = scaledMin;
            ScaledMax = scaledMax;

            if (scaledMin >= scaledMax)
                throw new ArgumentException("Min must be less than Max");
        }

        public override void Serialize(bool reverseBytes, byte[] data, uint start, object val)
        {
            int value = (int)Math.Round((double)val * Scale);
            base.Serialize(reverseBytes, data, start, value);
        }

        public override object Deserialize(bool reverseBytes, byte[] data, uint start, PropertyInfo prop)
        {
            int val = (int)base.Deserialize(reverseBytes, data, start, prop);

            if (val < ScaledMin)
                return ScaledMin;
            if (val > ScaledMax)
                return ScaledMax;

            return val / Scale;
        }

        public object GetRandom(Random random)
        {
            int range = ScaledMax - ScaledMin;
            return (random.NextDouble() * range + ScaledMin) / Scale;
        }

        public bool IsValid(object obj)
        {
            return (double)obj >= ScaledMin && (double)obj <= ScaledMax;
        }

        public override bool AreEqual(object val1, object val2)
        {
            double tolerance = 1 / (2 * Scale);
            return Math.Abs((double)val1 - (double)val2) <= tolerance;
        }
    }
}