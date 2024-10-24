using System.Collections.Generic;

namespace DetermineDummyGroupAddresses
{
    public class GroupAddressInfo
    {
        public GroupAddress GroupAddress { get; set; }

        public string Name { get; set; }
        public string DatapointType { get; set; }

        public HashSet<PhysicalAddress> UsedInSegments { get; set; } = new();

        public bool IsUsedInSegmentsOtherThan(PhysicalAddress segmentAddress)
        {
            if (UsedInSegments == null ||
                UsedInSegments.Count == 0)
            {
                return false;
            }

            if (UsedInSegments.Count == 1)
            {
                return !UsedInSegments.Contains(segmentAddress);
            }

            return true;
        }
    }
}
