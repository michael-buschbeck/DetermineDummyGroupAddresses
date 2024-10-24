using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DetermineDummyGroupAddresses
{
    public class HomeServerDevice : DependentDevice
    {
        public static readonly string Manufacturer = "GIRA Giersiepen";
        public static readonly ushort[] Fingerprints = { 0x0CC4, 0x7C49, 0x9542, 0xEA29 };

        public string ProjectFile { get; set; }

        public override Control CreateControl(Project project)
        {
            return new HomeServerControl() { Project = project, Device = this };
        }

        // knxdatapoints\$10e0c30a-f7a5-4c65-8d39-b1c77bff7c45.xml
        private static readonly Regex datapointConfigNameRegex =
            new(@"/knxdatapoints/\$[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}\.xml$",
                RegexOptions.Compiled);

        public void ParseProject()
        {
            var importGroupAddresses = new HashSet<GroupAddress>();

            using (var projectFileStream = new FileStream(ProjectFile, FileMode.Open, FileAccess.Read))
            using (var projectFileArchive = new ZipArchive(projectFileStream, ZipArchiveMode.Read))
            {
                var datapointSerializer = new XmlSerializer(typeof(DatapointConfig));

                foreach (var projectFileEntry in projectFileArchive.Entries)
                {
                    var datapointConfigNameMatch = datapointConfigNameRegex.Match(projectFileEntry.FullName);

                    if (datapointConfigNameMatch.Success)
                    {
                        using (var projectFileEntryStream = projectFileEntry.Open())
                        {
                            var datapointConfig = (DatapointConfig)datapointSerializer.Deserialize(projectFileEntryStream);

                            void AddGroupAddress(string groupAddressString)
                            {
                                if (!string.IsNullOrEmpty(groupAddressString))
                                {
                                    var groupAddressValue = ushort.Parse(groupAddressString);
                                    importGroupAddresses.Add(new GroupAddress(groupAddressValue));
                                }
                            }

                            AddGroupAddress(datapointConfig.ReadGroupAddress);
                            AddGroupAddress(datapointConfig.WriteGroupAddress);
                            AddGroupAddress(datapointConfig.DisplayGroupAddress);
                        }
                    }
                }
            }

            DependentGroupAddresses = importGroupAddresses;
            DependentGroupAddressesChanged = DateTime.Now;
        }

        [XmlRoot("KnxDataPoint", Namespace = "http://service.schema.gira.de/configuration")]
        public struct DatapointConfig
        {
            [XmlElement("EntityName")]
            public string Name;

            [XmlElement("ReadGroupAddress")]
            public string ReadGroupAddress;
            [XmlElement("WriteGroupAddress")]
            public string WriteGroupAddress;
            [XmlElement("DisplayGroupAddress")]
            public string DisplayGroupAddress;

            [XmlElement("DataTypeKnx")]
            public string DatapointType;
        }
    }
}
