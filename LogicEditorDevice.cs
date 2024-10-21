using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace DetermineDummyGroupAddresses
{
    public class LogicEditorDevice : DependentDevice
    {
        public static readonly string Manufacturer = "BAB TECHNOLOGIE GmbH";
        public static readonly ushort[] Fingerprints = { 0xB78D };

        public string Source { get; set; } = "http://192.168.0.123/logic-editor/login";

        public string Username { get; set; } = "admin";
        public string Password { get; set; }

        public override Control CreateControl(Project project)
        {
            return new LogicEditorControl() { Project = project, Device = this };
        }

        public void ParseConfig(string json)
        {
            var importGroupAddresses = new HashSet<GroupAddress>();

            var config = JsonSerializer.Deserialize<Config>(json);

            foreach (var registerItemConfig in config.RegisterItems)
            {
                foreach (var groupAddressConfig in registerItemConfig.Properties.GroupAddresses)
                {
                    var groupAddress = new GroupAddress(groupAddressConfig.Address);
                    importGroupAddresses.Add(groupAddress);
                }
            }

            DependentGroupAddresses = importGroupAddresses;
            DependentGroupAddressesChanged = DateTime.Now;
        }

        public struct Config
        {
            [JsonPropertyName("registerItems")]
            public RegisterItemConfig[] RegisterItems { get; set; }

            public struct RegisterItemConfig
            {
                [JsonPropertyName("name")]
                public string Name { get; set; }
                [JsonPropertyName("description")]
                public string Description { get; set; }

                [JsonPropertyName("properties")]
                public PropertiesConfig Properties { get; set; }

                public struct PropertiesConfig
                {
                    [JsonPropertyName("groupaddresses")]
                    public GroupAddressConfig[] GroupAddresses { get; set; }

                    public struct GroupAddressConfig
                    {
                        [JsonPropertyName("address")]
                        public ushort Address { get; set; }

                        [JsonPropertyName("title")]
                        public string Title { get; set; }
                        [JsonPropertyName("description")]
                        public string Description { get; set; }

                        [JsonPropertyName("dpt")]
                        public int DatapointType { get; set; }
                    }
                }
            }
        }
    }
}
