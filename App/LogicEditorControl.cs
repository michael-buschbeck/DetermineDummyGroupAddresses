using System;
using System.IO;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;

namespace DetermineDummyGroupAddresses
{
    public partial class LogicEditorControl : UserControl, IDependentDeviceControl
    {
        public Project Project { get; set; }

        private LogicEditorDevice device;

        public LogicEditorDevice Device
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

        public LogicEditorControl()
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

                AddressTextBox.Text = string.Empty;
                UsernameTextBox.Text = string.Empty;
                PasswordTextBox.Text = string.Empty;

                AddressTextBox.Enabled = false;
                UsernameTextBox.Enabled = false;
                PasswordTextBox.Enabled = false;

                ChangedLabel.Text = initialChangedLabelText;
            }
            else
            {
                MainGroupBox.Text = $"{initialMainGroupBoxText} ({Device.PhysicalAddress})";

                AddressTextBox.Text = Device.Source;
                UsernameTextBox.Text = Device.Username;
                PasswordTextBox.Text = Device.Password;

                AddressTextBox.Enabled = true;
                UsernameTextBox.Enabled = true;
                PasswordTextBox.Enabled = true;

                ChangedLabel.Text = (Device.DependentGroupAddressesChanged == default) ? initialChangedLabelText : Device.DependentGroupAddressesChanged.ToString("F");
            }

            RefreshImportButtonState();
            RefreshResults();
        }

        public void RefreshResults()
        {
            ResultControl.RefreshContents(Project, Device);
        }

        private void AddressTextBox_TextChanged(object sender, EventArgs e)
        {
            if (Device != null)
            {
                Device.Source = AddressTextBox.Text;
                RefreshImportButtonState();
            }
        }

        private void UsernameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (Device != null)
            {
                Device.Username = UsernameTextBox.Text;
                RefreshImportButtonState();
            }
        }

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            if (Device != null)
            {
                Device.Password = PasswordTextBox.Text;
                RefreshImportButtonState();
            }
        }

        private void RefreshImportButtonState()
        {
            ImportButton.Enabled = Device != null &&
                !string.IsNullOrWhiteSpace(AddressTextBox.Text) &&
                !string.IsNullOrWhiteSpace(UsernameTextBox.Text) &&
                !string.IsNullOrWhiteSpace(PasswordTextBox.Text);
        }

        private void WebView_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            WebView.CoreWebView2.WebResourceResponseReceived += LogicEditor_WebResourceResponseReceived;
        }

        private bool attemptedLogicEditorLogin;

        private void ImportButton_Click(object sender, EventArgs e)
        {
            ChangedLabel.Text = "\u2026";  // horizontal ellipsis

            attemptedLogicEditorLogin = false;
            WebView.Source = new Uri(AddressTextBox.Text);
        }

        private async void WebView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (WebView.Source.AbsolutePath != "/logic-editor/login")
            {
                return;
            }

            if (attemptedLogicEditorLogin)
            {
                // avoid repeated failing login
                return;
            }

            attemptedLogicEditorLogin = true;

            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;

            async Task FormFieldSetValue(string fieldId, string fieldValue)
            {
                await WebView.CoreWebView2.ExecuteScriptAsync($@"
                    var field = document.getElementById('{fieldId}');
                    field.value = '{fieldValue}';
                    field.dispatchEvent(new Event('input', {{ bubbles: true, cancelable: true }}));
                ");
            }

            async Task FormFieldPressKey(string fieldId, string keyName, int keyCode)
            {
                await WebView.CoreWebView2.ExecuteScriptAsync($@"
                    var field = document.getElementById('{fieldId}');
                    field.dispatchEvent(new KeyboardEvent('keypress', {{ bubbles: true, cancelable: true, key: '{keyName}', keyCode: {keyCode}, which: {keyCode} }}));
                ");
            }

            await FormFieldSetValue("username", username);
            await FormFieldSetValue("password", password);

            await FormFieldPressKey("password", "Enter", 13);
        }

        private async void LogicEditor_WebResourceResponseReceived(object sender, CoreWebView2WebResourceResponseReceivedEventArgs e)
        {
            if (new Uri(e.Request.Uri).AbsolutePath != "/web.logics/LogicServlet/sync/connectors/00000000-0000-0000-0000-000000000000")
            {
                return;
            }

            if (e.Response.StatusCode != 200)
            {
                WebView.CoreWebView2.NavigateToString($"Datenpunkte konnten nicht geladen werden (HTTP-Status: {e.Response.StatusCode})");
                return;
            }

            var contentType = new ContentType(e.Response.Headers.GetHeader("Content-Type"));

            if (contentType.MediaType != "application/json")
            {
                WebView.CoreWebView2.NavigateToString($"Datenpunkte konnten nicht geladen werden (nicht unterstützter Medientyp: {contentType.MediaType})");
                return;
            }

            var contentStream = await e.Response.GetContentAsync();

            using (var contentReader = new StreamReader(contentStream, Encoding.GetEncoding(contentType.CharSet)))
            {
                var content = await contentReader.ReadToEndAsync();
                Device.ParseConfig(content);
            }

            WebView.CoreWebView2.NavigateToString("Datenpunkte geladen!");

            RefreshContents();
        }
    }
}
