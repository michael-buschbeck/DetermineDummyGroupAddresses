namespace DetermineDummyGroupAddresses
{
    partial class LogicEditorControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ColumnHeader GroupAddress;
            System.Windows.Forms.ColumnHeader Name;
            System.Windows.Forms.Label PasswordCaptionLabel;
            System.Windows.Forms.Label UsernameCaptionLabel;
            System.Windows.Forms.Label AddressCaptionLabel;
            System.Windows.Forms.Label ChangedCaptionLabel;
            this.WebView = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.ImportButton = new System.Windows.Forms.Button();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.UsernameTextBox = new System.Windows.Forms.TextBox();
            this.AddressTextBox = new System.Windows.Forms.TextBox();
            this.MainGroupBox = new System.Windows.Forms.GroupBox();
            this.ResultControl = new DetermineDummyGroupAddresses.ResultControl();
            this.ChangedLabel = new System.Windows.Forms.Label();
            GroupAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            PasswordCaptionLabel = new System.Windows.Forms.Label();
            UsernameCaptionLabel = new System.Windows.Forms.Label();
            AddressCaptionLabel = new System.Windows.Forms.Label();
            ChangedCaptionLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.WebView)).BeginInit();
            this.MainGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupAddress
            // 
            GroupAddress.Text = "Adresse";
            GroupAddress.Width = 80;
            // 
            // Name
            // 
            Name.Text = "Name";
            Name.Width = 200;
            // 
            // PasswordCaptionLabel
            // 
            PasswordCaptionLabel.AutoSize = true;
            PasswordCaptionLabel.Location = new System.Drawing.Point(11, 79);
            PasswordCaptionLabel.Name = "PasswordCaptionLabel";
            PasswordCaptionLabel.Size = new System.Drawing.Size(53, 13);
            PasswordCaptionLabel.TabIndex = 4;
            PasswordCaptionLabel.Text = "Passwort";
            // 
            // UsernameCaptionLabel
            // 
            UsernameCaptionLabel.AutoSize = true;
            UsernameCaptionLabel.Location = new System.Drawing.Point(11, 51);
            UsernameCaptionLabel.Name = "UsernameCaptionLabel";
            UsernameCaptionLabel.Size = new System.Drawing.Size(80, 13);
            UsernameCaptionLabel.TabIndex = 2;
            UsernameCaptionLabel.Text = "Benutzername";
            // 
            // AddressCaptionLabel
            // 
            AddressCaptionLabel.AutoSize = true;
            AddressCaptionLabel.Location = new System.Drawing.Point(11, 23);
            AddressCaptionLabel.Name = "AddressCaptionLabel";
            AddressCaptionLabel.Size = new System.Drawing.Size(47, 13);
            AddressCaptionLabel.TabIndex = 0;
            AddressCaptionLabel.Text = "Adresse";
            // 
            // ChangedCaptionLabel
            // 
            ChangedCaptionLabel.AutoSize = true;
            ChangedCaptionLabel.Location = new System.Drawing.Point(11, 107);
            ChangedCaptionLabel.Name = "ChangedCaptionLabel";
            ChangedCaptionLabel.Size = new System.Drawing.Size(67, 13);
            ChangedCaptionLabel.TabIndex = 6;
            ChangedCaptionLabel.Text = "Datenstand";
            // 
            // WebView
            // 
            this.WebView.AllowExternalDrop = true;
            this.WebView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.WebView.CreationProperties = null;
            this.WebView.DefaultBackgroundColor = System.Drawing.SystemColors.Control;
            this.WebView.Location = new System.Drawing.Point(14, 163);
            this.WebView.Name = "WebView";
            this.WebView.Size = new System.Drawing.Size(72, 19);
            this.WebView.Source = new System.Uri("about:blank", System.UriKind.Absolute);
            this.WebView.TabIndex = 0;
            this.WebView.TabStop = false;
            this.WebView.Visible = false;
            this.WebView.ZoomFactor = 1D;
            this.WebView.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.WebView_CoreWebView2InitializationCompleted);
            this.WebView.NavigationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs>(this.WebView_NavigationCompleted);
            // 
            // ImportButton
            // 
            this.ImportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ImportButton.Location = new System.Drawing.Point(102, 163);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(280, 23);
            this.ImportButton.TabIndex = 8;
            this.ImportButton.Text = "Verwendete Gruppenadressen laden";
            this.ImportButton.UseVisualStyleBackColor = true;
            this.ImportButton.Click += new System.EventHandler(this.ImportButton_Click);
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(102, 76);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(280, 22);
            this.PasswordTextBox.TabIndex = 5;
            this.PasswordTextBox.UseSystemPasswordChar = true;
            this.PasswordTextBox.TextChanged += new System.EventHandler(this.PasswordTextBox_TextChanged);
            // 
            // UsernameTextBox
            // 
            this.UsernameTextBox.Location = new System.Drawing.Point(102, 48);
            this.UsernameTextBox.Name = "UsernameTextBox";
            this.UsernameTextBox.Size = new System.Drawing.Size(280, 22);
            this.UsernameTextBox.TabIndex = 3;
            this.UsernameTextBox.TextChanged += new System.EventHandler(this.UsernameTextBox_TextChanged);
            // 
            // AddressTextBox
            // 
            this.AddressTextBox.Location = new System.Drawing.Point(102, 20);
            this.AddressTextBox.Name = "AddressTextBox";
            this.AddressTextBox.Size = new System.Drawing.Size(280, 22);
            this.AddressTextBox.TabIndex = 1;
            this.AddressTextBox.TextChanged += new System.EventHandler(this.AddressTextBox_TextChanged);
            // 
            // MainGroupBox
            // 
            this.MainGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainGroupBox.Controls.Add(this.ResultControl);
            this.MainGroupBox.Controls.Add(this.WebView);
            this.MainGroupBox.Controls.Add(this.ImportButton);
            this.MainGroupBox.Controls.Add(this.ChangedLabel);
            this.MainGroupBox.Controls.Add(ChangedCaptionLabel);
            this.MainGroupBox.Controls.Add(PasswordCaptionLabel);
            this.MainGroupBox.Controls.Add(UsernameCaptionLabel);
            this.MainGroupBox.Controls.Add(AddressCaptionLabel);
            this.MainGroupBox.Controls.Add(this.PasswordTextBox);
            this.MainGroupBox.Controls.Add(this.UsernameTextBox);
            this.MainGroupBox.Controls.Add(this.AddressTextBox);
            this.MainGroupBox.Location = new System.Drawing.Point(0, 0);
            this.MainGroupBox.Name = "MainGroupBox";
            this.MainGroupBox.Padding = new System.Windows.Forms.Padding(8);
            this.MainGroupBox.Size = new System.Drawing.Size(1000, 200);
            this.MainGroupBox.TabIndex = 3;
            this.MainGroupBox.TabStop = false;
            this.MainGroupBox.Text = "EIBPort Logik Editor";
            // 
            // ResultControl
            // 
            this.ResultControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultControl.Location = new System.Drawing.Point(398, 20);
            this.ResultControl.Name = "ResultControl";
            this.ResultControl.Size = new System.Drawing.Size(591, 166);
            this.ResultControl.TabIndex = 9;
            // 
            // ChangedLabel
            // 
            this.ChangedLabel.AutoSize = true;
            this.ChangedLabel.Location = new System.Drawing.Point(102, 107);
            this.ChangedLabel.Name = "ChangedLabel";
            this.ChangedLabel.Size = new System.Drawing.Size(143, 13);
            this.ChangedLabel.TabIndex = 7;
            this.ChangedLabel.Text = "noch keine Daten geladen";
            // 
            // LogicEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainGroupBox);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(560, 165);
            this.Name = "LogicEditorControl";
            this.Size = new System.Drawing.Size(1000, 200);
            ((System.ComponentModel.ISupportInitialize)(this.WebView)).EndInit();
            this.MainGroupBox.ResumeLayout(false);
            this.MainGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Microsoft.Web.WebView2.WinForms.WebView2 WebView;
        private System.Windows.Forms.Button ImportButton;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.TextBox UsernameTextBox;
        private System.Windows.Forms.TextBox AddressTextBox;
        private System.Windows.Forms.GroupBox MainGroupBox;
        private ResultControl ResultControl;
        private System.Windows.Forms.Label ChangedLabel;
    }
}
