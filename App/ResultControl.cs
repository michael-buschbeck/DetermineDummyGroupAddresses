using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DetermineDummyGroupAddresses
{
    public partial class ResultControl : UserControl
    {
        private readonly ListViewGroup ResultListView_ToAdd_ListViewGroup;
        private readonly ListViewGroup ResultListView_ToRemove_ListViewGroup;
        private readonly ListViewGroup ResultListView_ToKeep_ListViewGroup;

        public ResultControl()
        {
            InitializeComponent();

            ResultListView_ToAdd_ListViewGroup = ResultListView.Groups["ToAdd"];
            ResultListView_ToRemove_ListViewGroup = ResultListView.Groups["ToRemove"];
            ResultListView_ToKeep_ListViewGroup = ResultListView.Groups["ToKeep"];
        }

        public void ClearContents()
        {
            ResultListView.Items.Clear();
        }

        public void RefreshContents(Project project, DependentDevice dependentDevice)
        {
            if (project == null || dependentDevice == null)
            {
                ClearContents();
                return;
            }

            var expectedGroupAddressInfos = new Dictionary<GroupAddress, GroupAddressInfo>();

            foreach (GroupAddress dependentGroupAddress in dependentDevice.DependentGroupAddressInfos.Keys)
            {
                if (project.GroupAddressInfos.TryGetValue(dependentGroupAddress, out var dependentGroupAddressInfo) && dependentGroupAddressInfo.IsUsedInSegmentsOtherThan(dependentDevice.SegmentAddress))
                {
                    expectedGroupAddressInfos.Add(
                        dependentGroupAddress,
                        dependentGroupAddressInfo);
                }
            }

            ResultListView.SuspendLayout();
            ResultListView.Items.Clear();

            foreach (var expectedGroupAddressInfo in expectedGroupAddressInfos.Values.OrderBy(groupAddressInfo => groupAddressInfo.GroupAddress))
            {
                var expectedGroupAddress = expectedGroupAddressInfo.GroupAddress;

                var resultListViewItem = new ListViewItem()
                {
                    Text = expectedGroupAddress.ToString(),
                    SubItems = { expectedGroupAddressInfo.DatapointName },

                    Group = dependentDevice.AssignedGroupAddresses.Contains(expectedGroupAddress)
                        ? ResultListView_ToKeep_ListViewGroup
                        : ResultListView_ToAdd_ListViewGroup
                };

                ResultListView.Items.Add(resultListViewItem);
            }

            foreach (var assignedGroupAddress in dependentDevice.AssignedGroupAddresses.OrderBy(groupAddress => groupAddress))
            {
                if (expectedGroupAddressInfos.ContainsKey(assignedGroupAddress))
                {
                    continue;
                }

                var resultListViewItem = new ListViewItem()
                {
                    Text = assignedGroupAddress.ToString(),
                    Group = ResultListView_ToRemove_ListViewGroup
                };

                if (project.GroupAddressInfos.TryGetValue(assignedGroupAddress, out var assignedGroupAddressInfo))
                {
                    resultListViewItem.SubItems.Add(assignedGroupAddressInfo.DatapointName);
                }

                ResultListView.Items.Add(resultListViewItem);
            }

            ResultListView.ResumeLayout();
        }

        private void ResultListView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            AdjustResultListViewLastColumnSize(e.ColumnIndex);
        }

        private void ResultListView_SizeChanged(object sender, EventArgs e)
        {
            AdjustResultListViewLastColumnSize();
        }

        private void AdjustResultListViewLastColumnSize(int changedColumnIndex = int.MaxValue)
        {
            int lastColumnIndex = ResultListView.Columns.Count - 1;

            if (changedColumnIndex != lastColumnIndex)
            {
                int totalColumnWidth = Enumerable.Range(0, lastColumnIndex).Sum(columnIndex => ResultListView.Columns[columnIndex].Width);
                ResultListView.Columns[lastColumnIndex].Width = Math.Max(30, ResultListView.ClientSize.Width - totalColumnWidth);
            }
        }
    }
}
