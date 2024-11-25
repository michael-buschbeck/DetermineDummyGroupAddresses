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

        /// <summary>
        /// Group addresses assigned to the dummy device in the ETS project.
        /// </summary>
        public HashSet<GroupAddress> AssignedGroupAddresses { get; set; } = new();

        /// <summary>
        /// Group addresses declared and used within the dependent device configuration,
        /// along with the name and datapoint type they were declared with if available.
        /// </summary>
        public GroupAddressInfoDictionary DependentGroupAddressInfos { get; set; } = new();

        public DateTime DependentGroupAddressesChanged { get; set; }

        public abstract Control CreateControl(Project project);
    }

    public interface IDependentDeviceControl
    {
        public void RefreshResults();
    }
}
