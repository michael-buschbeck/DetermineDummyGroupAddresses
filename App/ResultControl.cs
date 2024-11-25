using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DetermineDummyGroupAddresses
{
    public partial class ResultControl : UserControl
    {
        private readonly ListViewGroup ResultListView_IncompatibleType_ListViewGroup;
        private readonly ListViewGroup ResultListView_InconsistentType_ListViewGroup;
        private readonly ListViewGroup ResultListView_InconsistentName_ListViewGroup;

        private readonly ListViewGroup ResultListView_NotDefined_ListViewGroup;

        private readonly ListViewGroup ResultListView_ToAdd_ListViewGroup;
        private readonly ListViewGroup ResultListView_ToRemove_ListViewGroup;
        private readonly ListViewGroup ResultListView_ToKeep_ListViewGroup;

        public ResultControl()
        {
            InitializeComponent();

            ResultListView_IncompatibleType_ListViewGroup = ResultListView.Groups["IncompatibleType"];
            ResultListView_InconsistentType_ListViewGroup = ResultListView.Groups["InconsistentType"];
            ResultListView_InconsistentName_ListViewGroup = ResultListView.Groups["InconsistentName"];

            ResultListView_NotDefined_ListViewGroup = ResultListView.Groups["NotDefined"];

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
            const string arrow = "\u27A4";

            if (project == null || dependentDevice == null)
            {
                ClearContents();
                return;
            }

            ResultListView.SuspendLayout();
            ResultListView.Items.Clear();

            var expectedGroupAddressInfos = new Dictionary<GroupAddress, GroupAddressInfo>();

            foreach (var dependentGroupAddress in dependentDevice.DependentGroupAddressInfos.Keys.OrderBy(groupAddress => groupAddress))
            {
                if (project.GroupAddressInfos.TryGetValue(dependentGroupAddress, out var projectGroupAddressInfo))
                {
                    if (projectGroupAddressInfo.IsUsedInSegmentsOtherThan(dependentDevice.SegmentAddress))
                    {
                        expectedGroupAddressInfos.Add(
                            dependentGroupAddress,
                            projectGroupAddressInfo);
                    }

                    foreach (var dependentGroupAddressInfo in dependentDevice.DependentGroupAddressInfos[dependentGroupAddress].All())
                    {
                        if (projectGroupAddressInfo.DatapointName != dependentGroupAddressInfo.DatapointName && dependentGroupAddressInfo.IsPrimary)
                        {
                            var resultListViewItem = new ListViewItem()
                            {
                                Text = dependentGroupAddress.ToString(),
                                SubItems = { dependentGroupAddressInfo.DatapointName + $" {arrow} " + projectGroupAddressInfo.DatapointName },

                                Group = ResultListView_InconsistentName_ListViewGroup,
                            };

                            ResultListView.Items.Add(resultListViewItem);
                        }

                        if (projectGroupAddressInfo.DatapointType != dependentGroupAddressInfo.DatapointType)
                        {
                            var resultListViewItem = new ListViewItem()
                            {
                                Text = dependentGroupAddress.ToString(),
                                SubItems = { dependentGroupAddressInfo.DatapointName + $" ({dependentGroupAddressInfo.DatapointType} {arrow} {projectGroupAddressInfo.DatapointType})" },

                                Group = projectGroupAddressInfo.DatapointType.IsCompatibleTo(dependentGroupAddressInfo.DatapointType)
                                    ? ResultListView_InconsistentType_ListViewGroup
                                    : ResultListView_IncompatibleType_ListViewGroup,
                            };

                            if (dependentGroupAddressInfo.IsPrimary || resultListViewItem.Group == ResultListView_IncompatibleType_ListViewGroup)
                            {
                                ResultListView.Items.Add(resultListViewItem);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var dependentGroupAddressInfo in dependentDevice.DependentGroupAddressInfos[dependentGroupAddress].All())
                    {
                        var resultListViewItem = new ListViewItem()
                        {
                            Text = dependentGroupAddress.ToString(),
                            SubItems = { dependentGroupAddressInfo.DatapointName },
                            
                            Group = ResultListView_NotDefined_ListViewGroup,
                        };

                        ResultListView.Items.Add(resultListViewItem);
                    }
                }
            }

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
