using System.Collections.Generic;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml;

namespace DetermineDummyGroupAddresses
{
    public class GroupAddressInfoDictionary : Dictionary<GroupAddress, GroupAddressInfo>, IXmlSerializable
    {
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
