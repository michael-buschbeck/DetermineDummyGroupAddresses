using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DetermineDummyGroupAddresses
{
    [XmlInclude(typeof(LogicEditorDevice))]
    [XmlInclude(typeof(HomeServerDevice))]
    public abstract class DependentDevice
    {
        public PhysicalAddress SegmentAddress { get; set; }
        public PhysicalAddress PhysicalAddress { get; set; }

        public HashSet<GroupAddress> AssignedGroupAddresses { get; set; } = new();
        public HashSet<GroupAddress> DependentGroupAddresses { get; set; } = new();

        public DateTime DependentGroupAddressesChanged { get; set; }

        public abstract Control CreateControl(Project project);
    }

    public interface IDependentDeviceControl
    {
        public void RefreshResults();
    }
}
