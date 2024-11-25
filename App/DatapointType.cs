using System;

namespace DetermineDummyGroupAddresses
{
    public struct DatapointType : IEquatable<DatapointType>
    {
        public ushort Main { get; }
        public ushort Sub { get; }

        public DatapointType(ushort main, ushort sub = 0)
        {
            Main = main;
            Sub = sub;
        }

        public DatapointType(uint type)
        {
            Main = (ushort)(type >> 16);
            Sub = (ushort)(type & 0xFFFF);
        }

        public DatapointType(string type)
        {
            if (type.EndsWith(".*") || type.EndsWith(".x"))
            {
                Main = ushort.Parse(type.Substring(0, type.Length - ".*".Length));
                Sub = 0;
            }
            else
            {
                int dotOffset = type.IndexOf('.');

                Main = ushort.Parse(type.Substring(0, dotOffset));
                Sub = ushort.Parse(type.Substring(dotOffset + 1));
            }
        }

        public bool IsCompatibleTo(DatapointType otherDatapointType)
        {
            return Main == otherDatapointType.Main;
        }

        public static bool operator ==(DatapointType datapointTypeA, DatapointType datapointTypeB) => datapointTypeA.Equals(datapointTypeB);
        public static bool operator !=(DatapointType datapointTypeA, DatapointType datapointTypeB) => !datapointTypeA.Equals(datapointTypeB);

        public bool Equals(DatapointType otherDatapointType)
        {
            return Main == otherDatapointType.Main && Sub == otherDatapointType.Sub;
        }

        public override bool Equals(object other)
        {
            return other is DatapointType otherDatapointType && Equals(otherDatapointType);
        }

        public override int GetHashCode()
        {
            return (Main, Sub).GetHashCode();
        }

        public override string ToString()
        {
            return (Sub == 0)
                ? $"{Main}.*"
                : $"{Main}.{Sub:D3}";
        }
    }
}
