using System;
using System.Xml.Schema;
using System.Xml;
using System.Xml.Serialization;

namespace DetermineDummyGroupAddresses
{
    public struct DatapointType : IEquatable<DatapointType>, IXmlSerializable
    {
        public ushort Main { get; private set; }
        public ushort Sub { get; private set; }

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
            (Main, Sub) = ParseType(type);
        }

        private static (ushort, ushort) ParseType(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                return (0, 0);
            }

            if (type.EndsWith(".*") || type.EndsWith(".x"))
            {
                return (ushort.Parse(type.Substring(0, type.Length - ".*".Length)), 0);
            }

            int dotOffset = type.IndexOf('.');

            return (
                ushort.Parse(type.Substring(0, dotOffset)),
                ushort.Parse(type.Substring(dotOffset + 1)));
        }

        public bool IsCompatibleTo(DatapointType otherDatapointType)
        {
            return Main == otherDatapointType.Main;
        }

        public override string ToString()
        {
            return (Sub == 0)
                ? $"{Main}.*"
                : $"{Main}.{Sub:D3}";
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

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            (Main, Sub) = ParseType(reader.GetAttribute("Type"));
            reader.Read();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("Type", ToString());
        }
    }
}
