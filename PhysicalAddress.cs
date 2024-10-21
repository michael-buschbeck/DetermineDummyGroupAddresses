using System;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml;

namespace DetermineDummyGroupAddresses
{
    public struct PhysicalAddress : IEquatable<PhysicalAddress>, IComparable<PhysicalAddress>, IXmlSerializable
    {
        public ushort Value { get; private set; }

        public byte Area => (byte)((Value >> 12) & 0x0F);
        public byte Line => (byte)((Value >> 8) & 0x0F);
        public byte Device => (byte)(Value & 0xFF);

        public PhysicalAddress(ushort value)
        {
            Value = value;
        }

        public PhysicalAddress(byte area, byte line, byte device)
        {
            Value = CalcValue(area, line, device);
        }

        public PhysicalAddress(string address)
        {
            Value = ParseAddress(address);
        }

        private static ushort CalcValue(byte area, byte line, byte device)
        {
            return (ushort)((area & 0x0F) << 12 | (line & 0x0F) << 8 | device);
        }

        private static ushort ParseAddress(string address)
        {
            int firstDotOffset = address.IndexOf('.');

            if (firstDotOffset < 0)
            {
                return ushort.Parse(address);
            }

            int secondDotOffset = address.IndexOf(".", firstDotOffset + 1);

            if (secondDotOffset < 0)
            {
                throw new ArgumentException($"Invalid physical address: {address}");
            }

            return CalcValue(
                byte.Parse(address.Substring(0, firstDotOffset)),
                byte.Parse(address.Substring(firstDotOffset + 1, secondDotOffset - firstDotOffset - 1)),
                byte.Parse(address.Substring(secondDotOffset + 1)));
        }

        public override string ToString()
        {
            return $"{Area}.{Line}.{Device}";
        }

        public bool Equals(PhysicalAddress otherPhysicalAddress)
        {
            return Value == otherPhysicalAddress.Value;
        }

        public override bool Equals(object other)
        {
            return other is PhysicalAddress otherPhysicalAddress && Equals(otherPhysicalAddress);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public int CompareTo(PhysicalAddress otherPhysicalAddress)
        {
            return Value.CompareTo(otherPhysicalAddress.Value);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            Value = ParseAddress(reader.GetAttribute("Address"));
            reader.Read();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("Address", ToString());
        }
    }
}
