using System;
using System.IO;
using System.Windows.Forms;

namespace DetermineDummyGroupAddresses
{
    public partial class HomeServerControl : UserControl
    {
        public Project Project { get; set; }

        private HomeServerDevice device;

        public HomeServerDevice Device
        {
            get => device;
            set
            {
                device = value;
                RefreshContents();
            }
        }

        private readonly string initialMainGroupBoxText;
        private readonly string initialChangedLabelText;

        public HomeServerControl()
        {
            InitializeComponent();

            initialMainGroupBoxText = MainGroupBox.Text;
            initialChangedLabelText = ChangedLabel.Text;

            RefreshContents();
        }

        public void RefreshContents()
        {
            if (Device == null)
            {
                MainGroupBox.Text = initialMainGroupBoxText;

                ProjectFileTextBox.Text = string.Empty;

                ProjectFileTextBox.Enabled = false;
                ProjectFileBrowseButton.Enabled = false;

                ChangedLabel.Text = initialChangedLabelText;
            }
            else
            {
                MainGroupBox.Text = $"{initialMainGroupBoxText} ({Device.PhysicalAddress})";

                ProjectFileTextBox.Text = GetRelativeProjectFile(Device.ProjectFile);

                ProjectFileTextBox.Enabled = true;
                ProjectFileBrowseButton.Enabled = true;

                ChangedLabel.Text = (Device.DependentGroupAddressesChanged == default) ? initialChangedLabelText : Device.DependentGroupAddressesChanged.ToString("F");
            }

            RefreshImportButtonState();
            RefreshResults();
        }

        public void RefreshResults()
        {
            ResultControl.RefreshContents(Project, Device);
        }

        private void RefreshImportButtonState()
        {
            ImportButton.Enabled = Device != null && File.Exists(Device.ProjectFile);
        }

        private void ProjectFileBrowseButton_Click(object sender, EventArgs e)
        {
            ProjectOpenFileDialog.FileName = Project.ProjectFile;
            ProjectOpenFileDialog.ShowDialog();
        }

        private void ProjectOpenFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ProjectFileTextBox.Text = GetRelativeProjectFile(ProjectOpenFileDialog.FileName);
        }

        private void ProjectFileTextBox_TextChanged(object sender, EventArgs e)
        {
            Device.ProjectFile = GetAbsoluteProjectFile(ProjectFileTextBox.Text);

            RefreshImportButtonState();
        }

        private string GetAbsoluteProjectFile(string relativeProjectFile)
        {
            if (string.IsNullOrWhiteSpace(relativeProjectFile) ||
                string.IsNullOrWhiteSpace(Project.ProjectFile))
            {
                return relativeProjectFile;
            }

            var baseUri = new Uri(Project.ProjectFile, UriKind.RelativeOrAbsolute);
            var relativeProjectUri = new Uri(relativeProjectFile, UriKind.RelativeOrAbsolute);

            return Uri.UnescapeDataString(new Uri(baseUri, relativeProjectUri).AbsolutePath).Replace('/', Path.DirectorySeparatorChar);
        }

        private string GetRelativeProjectFile(string absoluteProjectFile)
        {
            if (string.IsNullOrWhiteSpace(absoluteProjectFile) ||
                string.IsNullOrWhiteSpace(Project.ProjectFile))
            {
                return absoluteProjectFile;
            }

            var baseUri = new Uri(Project.ProjectFile, UriKind.RelativeOrAbsolute);
            var absoluteProjectUri = new Uri(absoluteProjectFile, UriKind.RelativeOrAbsolute);

            return Uri.UnescapeDataString(baseUri.MakeRelativeUri(absoluteProjectUri).ToString()).Replace('/', Path.DirectorySeparatorChar);
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            Device.ParseProject();

            RefreshContents();
        }
    }
}
