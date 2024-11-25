using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml;

namespace DetermineDummyGroupAddresses
{
    public class GroupAddressInfoDictionary : Dictionary<GroupAddress, GroupAddressInfo>, IXmlSerializable
    {
        public void Add(GroupAddressInfo groupAddressInfo)
        {
            Debug.Assert(groupAddressInfo.Next == null);

            var prevGroupAddressInfo = default(GroupAddressInfo);

            if (TryGetValue(groupAddressInfo.GroupAddress, out var nextGroupAddressInfo))
            {
                if (groupAddressInfo.IsPrimary)
                {
                    while (nextGroupAddressInfo != null && nextGroupAddressInfo.IsPrimary)
                    {
                        prevGroupAddressInfo = nextGroupAddressInfo;
                        nextGroupAddressInfo = nextGroupAddressInfo.Next;
                    }
                }
                else
                {
                    while (nextGroupAddressInfo != null)
                    {
                        prevGroupAddressInfo = nextGroupAddressInfo;
                        nextGroupAddressInfo = nextGroupAddressInfo.Next;
                    }
                }
            }

            if (prevGroupAddressInfo == null)
            {
                this[groupAddressInfo.GroupAddress] = groupAddressInfo;
            }
            else
            {
                prevGroupAddressInfo.Next = groupAddressInfo;
            }

            groupAddressInfo.Next = nextGroupAddressInfo;
        }

        public XmlSchema GetSchema() => null;

        public void ReadXml(XmlReader reader)
        {
            bool wasEmpty = reader.IsEmptyElement;

            reader.Read();

            if (wasEmpty)
            {
                return;
            }

            var groupAddressInfoSerializer = new XmlSerializer(typeof(GroupAddressInfo));

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                GroupAddressInfo groupAddressInfo = (GroupAddressInfo)groupAddressInfoSerializer.Deserialize(reader);
                Add(groupAddressInfo.GroupAddress, groupAddressInfo);
            }

            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            var groupAddressInfoSerializer = new XmlSerializer(typeof(GroupAddressInfo));

            foreach (GroupAddressInfo groupAddressInfo in Values)
            {
                groupAddressInfoSerializer.Serialize(writer, groupAddressInfo);
            }
        }
    }
}
