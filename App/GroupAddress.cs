using System;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml;
using System.CodeDom;

namespace DetermineDummyGroupAddresses
{
    public struct GroupAddress : IEquatable<GroupAddress>, IComparable<GroupAddress>, IXmlSerializable
    {
        public ushort Value { get; private set; }

        public byte Main => (byte)((Value >> 11) & 0x1F);
        public byte Middle => (byte)((Value >> 8) & 0x07);
        public byte Sub => (byte)(Value & 0xFF);

        public GroupAddress(ushort value)
        {
            Value = value;
        }

        public GroupAddress(byte main, byte sub)
        {
            Value = CalcValue(main, sub);
        }

        public GroupAddress(byte main, byte middle, byte sub)
        {
            Value = CalcValue(main, middle, sub);
        }

        public GroupAddress(string address)
        {
            Value = ParseAddress(address);
        }

        private static ushort CalcValue(byte main, ushort sub)
        {
            return (ushort)((main & 0x1F) << 11 | (sub & 0x07FF));
        }

        private static ushort CalcValue(byte main, byte middle, byte sub)
        {
            return (ushort)((main & 0x1F) << 11 | (middle & 0x07) << 8 | sub);
        }

        private static ushort ParseAddress(string address)
        {
            int firstSlashOffset = address.IndexOf('/');

            if (firstSlashOffset < 0)
            {
                return ushort.Parse(address);
            }

            int secondSlashOffset = address.IndexOf("/", firstSlashOffset + 1);

            if (secondSlashOffset < 0)
            {
                return CalcValue(
                    byte.Parse(address.Substring(0, firstSlashOffset)),
                    ushort.Parse(address.Substring(firstSlashOffset + 1)));
            }

            return CalcValue(
                byte.Parse(address.Substring(0, firstSlashOffset)),
                byte.Parse(address.Substring(firstSlashOffset + 1, secondSlashOffset - firstSlashOffset - 1)),
                byte.Parse(address.Substring(secondSlashOffset + 1)));
        }

        public override string ToString()
        {
            return $"{Main}/{Middle}/{Sub}";
        }

        public static bool operator==(GroupAddress groupAddressA, GroupAddress groupAddressB) => groupAddressA.Equals(groupAddressB);
        public static bool operator!=(GroupAddress groupAddressA, GroupAddress groupAddressB) => !groupAddressA.Equals(groupAddressB);

        public bool Equals(GroupAddress otherGroupAddress)
        {
            return Value == otherGroupAddress.Value;
        }

        public override bool Equals(object other)
        {
            return other is GroupAddress otherGroupAddress && Equals(otherGroupAddress);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public int CompareTo(GroupAddress otherGroupAddress)
        {
            return Value.CompareTo(otherGroupAddress.Value);
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
