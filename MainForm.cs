using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace DetermineDummyGroupAddresses
{
    public partial class MainForm : Form
    {
        public Project Project { get; }

        private readonly string initialChangedLabelText;

        public MainForm()
        {
            InitializeComponent();

            initialChangedLabelText = ETS_ChangedLabel.Text;

            Project = Properties.Settings.Default.Project ??= new();

            ETS_RefreshContents();

            RefreshDependentDevicesPanel();
        }

        public void ETS_RefreshContents()
        {
            ETS_ProjectFileTextBox.Text = Project.ProjectFile;
            ETS_ChangedLabel.Text = (Project.ProjectChanged == default) ? initialChangedLabelText : Project.ProjectChanged.ToString("F");

            ETS_RefreshProjectImportButtonState();
        }

        private void ETS_RefreshProjectImportButtonState()
        {
            ETS_ProjectImportButton.Enabled = File.Exists(Project.ProjectFile);
        }

        private void RefreshDependentDevicesPanel()
        {
            DependentDevicesPanel.SuspendLayout();
            DependentDevicesPanel.Controls.Clear();

            int dependentDeviceLocationY = 0;

            foreach (var dependentDevice in Project.DependentDevices)
            {
                var dependentDeviceControl = dependentDevice.CreateControl(Project);

                if (dependentDeviceLocationY > 0)
                {
                    dependentDeviceLocationY += dependentDeviceControl.Margin.Top;
                }

                dependentDeviceControl.Location = new(0, dependentDeviceLocationY);
                dependentDeviceControl.Size = new(DependentDevicesPanel.Width, dependentDeviceControl.Size.Height);
                dependentDeviceControl.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;

                DependentDevicesPanel.Controls.Add(dependentDeviceControl);

                dependentDeviceLocationY += dependentDeviceControl.Size.Height;
                dependentDeviceLocationY += dependentDeviceControl.Margin.Bottom;
            }

            foreach (var dependentDeviceControl in DependentDevicesPanel.Controls.OfType<IDependentDeviceControl>())
            {
                dependentDeviceControl.RefreshResults();
            }

            DependentDevicesPanel.ResumeLayout();
        }

        private void ETS_ProjectFileBrowseButton_Click(object sender, EventArgs e)
        {
            ETS_ProjectOpenFileDialog.FileName = ETS_ProjectFileTextBox.Text;
            ETS_ProjectOpenFileDialog.ShowDialog();
        }

        private void ETS_ProjectOpenFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ETS_ProjectFileTextBox.Text = ETS_ProjectOpenFileDialog.FileName;
        }

        private void ETS_ProjectFileTextBox_TextChanged(object sender, EventArgs e)
        {
            Project.ProjectFile = ETS_ProjectFileTextBox.Text;

            ETS_RefreshProjectImportButtonState();
        }

        private void ETS_ProjectImportButton_Click(object sender, EventArgs e)
        {
            Project.ParseProject();

            ETS_RefreshContents();
            RefreshDependentDevicesPanel();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }

    public class Project
    {
        public string ProjectFile { get; set; }
        public DateTime ProjectChanged { get; set; }

        public GroupAddressInfoDictionary GroupAddressInfos { get; set; } = new();

        public List<DependentDevice> DependentDevices { get; } = new();

        private static readonly Regex projectConfigNameRegex =
            new(@"^(?<ProjectId>P-[0-9A-F]{4})/(?<InstallationId>[0-9]+)\.xml$",
                RegexOptions.Compiled);

        private static readonly Regex deviceHardwareRegex =
            new(@"^(?<ManufacturerId>M-[0-9A-F]{4})_.*(?<Fingerprint>[0-9A-F]{4})$",
                RegexOptions.Compiled);

        private static readonly Regex datapointTypeIdRegex =
            new(@"^DPT-(?<MainNumber>[0-9]+)$|^DPST-(?<MainNumber>[0-9]+)-(?<SubNumber>[0-9]+)$",
                RegexOptions.Compiled);

        private class DeviceInfo
        {
            public PhysicalAddress SegmentAddress { get; set; }
            public PhysicalAddress DeviceAddress { get; set; }

            public string HardwareManufacturerId { get; set; }
            public ushort HardwareFingerprint { get; set; }

            public HashSet<GroupAddress> AssignedGroupAddresses { get; } = new();
        }

        public void ParseProject()
        {
            var deviceInfos = new List<DeviceInfo>();
            var importGroupAddressInfos = new GroupAddressInfoDictionary();

            var manufacturerNameById = new Dictionary<string, string>();

            using (var projectFileStream = new FileStream(ProjectFile, FileMode.Open, FileAccess.Read))
            using (var projectFileArchive = new ZipArchive(projectFileStream, ZipArchiveMode.Read))
            {
                static T[] EmptyIfNull<T>(T[] items) => items ?? Array.Empty<T>();

                var projectMasterSerializer = new XmlSerializer(typeof(KNXMaster));
                var projectConfigSerializer = new XmlSerializer(typeof(KNXConfig));

                foreach (var projectFileEntry in projectFileArchive.Entries)
                {
                    if (projectFileEntry.FullName == "knx_master.xml")
                    {
                        using (var projectFileEntryStream = projectFileEntry.Open())
                        {
                            var projectMaster = (KNXMaster)projectMasterSerializer.Deserialize(projectFileEntryStream);

                            foreach (var manufacturerConfig in EmptyIfNull(projectMaster.MasterData.Manufacturers))
                            {
                                manufacturerNameById.Add(
                                    manufacturerConfig.Id,
                                    manufacturerConfig.Name);
                            }
                        }
                    }

                    var projectConfigNameMatch = projectConfigNameRegex.Match(projectFileEntry.FullName);

                    if (projectConfigNameMatch.Success)
                    {
                        string idPrefix = string.Format("{0}-{1}",
                            projectConfigNameMatch.Groups["ProjectId"],
                            projectConfigNameMatch.Groups["InstallationId"]);

                        using (var projectFileEntryStream = projectFileEntry.Open())
                        {
                            var projectConfig = (KNXConfig)projectConfigSerializer.Deserialize(projectFileEntryStream);

                            foreach (var installationConfig in projectConfig.Project.Installations)
                            {
                                var importGroupAddressInfoById = new Dictionary<string, GroupAddressInfo>();

                                var groupAddressConfigs =
                                    EmptyIfNull(installationConfig.GroupAddresses)
                                        .SelectMany(groupAddressesConfig => EmptyIfNull(groupAddressesConfig.MainGroupRanges)
                                        .SelectMany(mainGroupRangeConfig => EmptyIfNull(mainGroupRangeConfig.MiddleGroupRanges)
                                        .SelectMany(middleGroupRangeConfig => EmptyIfNull(middleGroupRangeConfig.GroupAddresses))));

                                foreach (var groupAddressConfig in groupAddressConfigs)
                                {
                                    var groupAddress = new GroupAddress(groupAddressConfig.Address);
                                
                                    var datapointType = default(string);
                                    var datapointTypeIdMatch = datapointTypeIdRegex.Match(groupAddressConfig.DatapointTypeId);
                                
                                    if (datapointTypeIdMatch.Success)
                                    {
                                        if (datapointTypeIdMatch.Groups["SubNumber"].Success)
                                        {
                                            datapointType = string.Format("{0}.{1:D3}",
                                                ushort.Parse(datapointTypeIdMatch.Groups["MainNumber"].Value),
                                                ushort.Parse(datapointTypeIdMatch.Groups["SubNumber"].Value));
                                        }
                                        else
                                        {
                                            datapointType = string.Format("{0}.*",
                                                ushort.Parse(datapointTypeIdMatch.Groups["MainNumber"].Value));
                                        }
                                    }

                                    var groupAddressInfo = new GroupAddressInfo() 
                                    {
                                        GroupAddress = groupAddress,
                                        Name = groupAddressConfig.Name,
                                        DatapointType = datapointType,
                                    };

                                    importGroupAddressInfos.Add(groupAddress, groupAddressInfo);
                                    importGroupAddressInfoById.Add(groupAddressConfig.Id, groupAddressInfo);
                                }

                                foreach (var areaConfig in EmptyIfNull(installationConfig.Topology.Areas))
                                {
                                    foreach (var lineConfig in EmptyIfNull(areaConfig.Lines))
                                    {
                                        foreach (var segmentConfig in EmptyIfNull(lineConfig.Segments))
                                        {
                                            if (segmentConfig.DeviceInstances == null ||
                                                segmentConfig.DeviceInstances.Length == 0)
                                            {
                                                continue;
                                            }

                                            var segmentAddress = new PhysicalAddress
                                            (
                                                areaConfig.Address,
                                                lineConfig.Address,
                                                segmentConfig.Number == 0 ? (byte)0 : new PhysicalAddress(segmentConfig.DeviceInstances.First().Address).Device
                                            );

                                            foreach (var deviceInstanceConfig in EmptyIfNull(segmentConfig.DeviceInstances))
                                            {
                                                var deviceInfo = default(DeviceInfo);
                                                var deviceHardwareMatch = deviceHardwareRegex.Match(deviceInstanceConfig.Hardware2ProgramRefId);

                                                if (deviceHardwareMatch.Success)
                                                {
                                                    deviceInfo = new DeviceInfo()
                                                    {
                                                        SegmentAddress = segmentAddress,

                                                        DeviceAddress = new PhysicalAddress
                                                        (
                                                            areaConfig.Address,
                                                            lineConfig.Address,
                                                            deviceInstanceConfig.Address
                                                        ),

                                                        HardwareManufacturerId = deviceHardwareMatch.Groups["ManufacturerId"].Value,
                                                        HardwareFingerprint = Convert.ToUInt16(deviceHardwareMatch.Groups["Fingerprint"].Value, 0x10),
                                                    };

                                                    deviceInfos.Add(deviceInfo);
                                                }

                                                var groupAddressRefs =
                                                    EmptyIfNull(deviceInstanceConfig.ComObjectInstanceRefs)
                                                        .SelectMany(comObjectInstanceRef => EmptyIfNull(comObjectInstanceRef.Links?.Split()));

                                                foreach (var groupAddressRef in groupAddressRefs)
                                                {
                                                    var groupAddressId = idPrefix + "_" + groupAddressRef;

                                                    if (importGroupAddressInfoById.TryGetValue(groupAddressId, out var groupAddressInfo))
                                                    {
                                                        groupAddressInfo.UsedInSegments.Add(segmentAddress);
                                                        deviceInfo?.AssignedGroupAddresses.Add(groupAddressInfo.GroupAddress);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            var importDependentDevices = new Dictionary<PhysicalAddress, DependentDevice>();

            foreach (var deviceInfo in deviceInfos)
            {
                manufacturerNameById.TryGetValue(deviceInfo.HardwareManufacturerId, out var manufacturerName);

                var importDependentDevice = default(DependentDevice);

                if (LogicEditorDevice.Manufacturer == manufacturerName &&
                    LogicEditorDevice.Fingerprints.Contains(deviceInfo.HardwareFingerprint))
                {
                    importDependentDevice = new LogicEditorDevice();
                }

                if (HomeServerDevice.Manufacturer == manufacturerName &&
                    HomeServerDevice.Fingerprints.Contains(deviceInfo.HardwareFingerprint))
                {
                    importDependentDevice = new HomeServerDevice();
                }

                if (importDependentDevice != null)
                {
                    importDependentDevice.SegmentAddress = deviceInfo.SegmentAddress;
                    importDependentDevice.PhysicalAddress = deviceInfo.DeviceAddress;

                    importDependentDevice.AssignedGroupAddresses = deviceInfo.AssignedGroupAddresses;

                    importDependentDevices.Add(
                        importDependentDevice.PhysicalAddress,
                        importDependentDevice);
                }
            }

            GroupAddressInfos = importGroupAddressInfos;

            DependentDevices.RemoveAll(
                dependentDevice => !(importDependentDevices.TryGetValue(dependentDevice.PhysicalAddress, out var importDependentDevice) && dependentDevice.GetType() == importDependentDevice.GetType()));

            foreach (var dependentDevice in DependentDevices)
            {
                dependentDevice.AssignedGroupAddresses = importDependentDevices[dependentDevice.PhysicalAddress].AssignedGroupAddresses;
                importDependentDevices.Remove(dependentDevice.PhysicalAddress);
            }

            DependentDevices.AddRange(importDependentDevices.Values);
            DependentDevices.Sort(static (dependentDeviceA, dependentDeviceB) => dependentDeviceA.PhysicalAddress.CompareTo(dependentDeviceB.PhysicalAddress));

            ProjectChanged = DateTime.Now;
        }

        [XmlRoot("KNX", Namespace = "http://knx.org/xml/project/23")]
        public struct KNXMaster
        {
            [XmlElement("MasterData")]
            public MasterDataConfig MasterData;

            public struct MasterDataConfig
            {
                [XmlArray("Manufacturers")]
                [XmlArrayItem("Manufacturer")]
                public ManufacturerConfig[] Manufacturers;

                public struct ManufacturerConfig
                {
                    [XmlAttribute("Id")]
                    public string Id;
                    [XmlAttribute("Name")]
                    public string Name;
                }
            }
        }

        [XmlRoot("KNX", Namespace = "http://knx.org/xml/project/23")]
        public struct KNXConfig
        {
            [XmlElement("Project")]
            public ProjectConfig Project;

            public struct ProjectConfig
            {
                [XmlAttribute("Id")]
                public string Id;

                [XmlArray("Installations")]
                [XmlArrayItem("Installation")]
                public InstallationConfig[] Installations;

                public struct InstallationConfig
                {
                    [XmlElement("Topology")]
                    public TopologyConfig Topology;

                    [XmlElement("GroupAddresses")]
                    public GroupAddressesConfig[] GroupAddresses;

                    public struct TopologyConfig
                    {
                        [XmlElement("Area")]
                        public AreaConfig[] Areas;

                        public struct AreaConfig
                        {
                            [XmlAttribute("Address")]
                            public byte Address;

                            [XmlElement("Line")]
                            public LineConfig[] Lines;

                            public struct LineConfig
                            {
                                [XmlAttribute("Address")]
                                public byte Address;

                                [XmlElement("Segment")]
                                public SegmentConfig[] Segments;

                                public struct SegmentConfig
                                {
                                    [XmlAttribute("Number")]
                                    public byte Number;

                                    [XmlElement("DeviceInstance")]
                                    public DeviceInstanceConfig[] DeviceInstances;

                                    public struct DeviceInstanceConfig
                                    {
                                        [XmlAttribute("Address")]
                                        public byte Address;

                                        [XmlAttribute("Name")]
                                        public string Name;
                                        [XmlAttribute("Description")]
                                        public string Description;

                                        [XmlAttribute("Hardware2ProgramRefId")]
                                        public string Hardware2ProgramRefId;

                                        [XmlArray("ComObjectInstanceRefs")]
                                        [XmlArrayItem("ComObjectInstanceRef")]
                                        public ComObjectInstanceRefConfig[] ComObjectInstanceRefs;

                                        public struct ComObjectInstanceRefConfig
                                        {
                                            [XmlAttribute("Links")]
                                            public string Links;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    public struct GroupAddressesConfig
                    {
                        [XmlArray("GroupRanges")]
                        [XmlArrayItem("GroupRange")]
                        public MainGroupRangeConfig[] MainGroupRanges;

                        public struct MainGroupRangeConfig
                        {
                            [XmlElement("GroupRange")]
                            public MiddleGroupRangeConfig[] MiddleGroupRanges;

                            public struct MiddleGroupRangeConfig
                            {
                                [XmlElement("GroupAddress")]
                                public GroupAddressConfig[] GroupAddresses;

                                public struct GroupAddressConfig
                                {
                                    [XmlAttribute("Id")]
                                    public string Id;

                                    [XmlAttribute("Address")]
                                    public ushort Address;

                                    [XmlAttribute("Name")]
                                    public string Name;
                                    [XmlAttribute("Description")]
                                    public string Description;

                                    [XmlAttribute("DatapointType")]
                                    public string DatapointTypeId;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
