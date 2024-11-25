using System.Collections.Generic;
using System.Xml.Linq;

namespace DetermineDummyGroupAddresses
{
    public class GroupAddressInfo
    {
        public GroupAddress GroupAddress { get; set; }
        public DatapointType DatapointType { get; set; }

        public string DatapointName { get; set; }

        /// <summary>
        /// Segments in the ETS topology in which this group address is used.
        /// </summary>
        public HashSet<PhysicalAddress> UsedInSegments { get; set; } = new();

        /// <summary>
        /// Is this the primary group address assignment to this datapoint?
        /// </summary>
        public bool IsPrimary { get; set; }

        /// <summary>
        /// Chain of group address declarations in case there is more than one for the same group address.
        /// </summary>
        public GroupAddressInfo Next { get; set; }

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

        public override string ToString()
        {
            string description = IsPrimary
                ? $"{GroupAddress} {DatapointName} ({DatapointType}) [primary]"
                : $"{GroupAddress} {DatapointName} ({DatapointType})";

            if (Next == null)
            {
                return description;
            }

            return $"{description} + {Next.ToString()}";
        }
    }
}
