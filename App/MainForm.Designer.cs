namespace DetermineDummyGroupAddresses
{
    partial class MainForm
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label ETS_ProjectFileCaptionLabel;
            System.Windows.Forms.Label ETS_ChangedCaptionLabel;
            this.ETS_GroupBox = new System.Windows.Forms.GroupBox();
            this.ETS_ProjectFileBrowseButton = new System.Windows.Forms.Button();
            this.ETS_ProjectFileTextBox = new System.Windows.Forms.TextBox();
            this.ETS_ProjectImportButton = new System.Windows.Forms.Button();
            this.ETS_ChangedLabel = new System.Windows.Forms.Label();
            this.ETS_ProjectOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.DependentDevicesPanel = new System.Windows.Forms.Panel();
            ETS_ProjectFileCaptionLabel = new System.Windows.Forms.Label();
            ETS_ChangedCaptionLabel = new System.Windows.Forms.Label();
            this.ETS_GroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ETS_ProjectFileCaptionLabel
            // 
            ETS_ProjectFileCaptionLabel.AutoSize = true;
            ETS_ProjectFileCaptionLabel.Location = new System.Drawing.Point(11, 25);
            ETS_ProjectFileCaptionLabel.Name = "ETS_ProjectFileCaptionLabel";
            ETS_ProjectFileCaptionLabel.Size = new System.Drawing.Size(69, 13);
            ETS_ProjectFileCaptionLabel.TabIndex = 1;
            ETS_ProjectFileCaptionLabel.Text = "Projektdatei";
            // 
            // ETS_ChangedCaptionLabel
            // 
            ETS_ChangedCaptionLabel.AutoSize = true;
            ETS_ChangedCaptionLabel.Location = new System.Drawing.Point(11, 54);
            ETS_ChangedCaptionLabel.Name = "ETS_ChangedCaptionLabel";
            ETS_ChangedCaptionLabel.Size = new System.Drawing.Size(67, 13);
            ETS_ChangedCaptionLabel.TabIndex = 1;
            ETS_ChangedCaptionLabel.Text = "Datenstand";
            // 
            // ETS_GroupBox
            // 
            this.ETS_GroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ETS_GroupBox.Controls.Add(this.ETS_ProjectFileBrowseButton);
            this.ETS_GroupBox.Controls.Add(this.ETS_ProjectFileTextBox);
            this.ETS_GroupBox.Controls.Add(this.ETS_ProjectImportButton);
            this.ETS_GroupBox.Controls.Add(this.ETS_ChangedLabel);
            this.ETS_GroupBox.Controls.Add(ETS_ChangedCaptionLabel);
            this.ETS_GroupBox.Controls.Add(ETS_ProjectFileCaptionLabel);
            this.ETS_GroupBox.Location = new System.Drawing.Point(13, 12);
            this.ETS_GroupBox.Name = "ETS_GroupBox";
            this.ETS_GroupBox.Padding = new System.Windows.Forms.Padding(11);
            this.ETS_GroupBox.Size = new System.Drawing.Size(1159, 116);
            this.ETS_GroupBox.TabIndex = 4;
            this.ETS_GroupBox.TabStop = false;
            this.ETS_GroupBox.Text = "ETS";
            // 
            // ETS_ProjectFileBrowseButton
            // 
            this.ETS_ProjectFileBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ETS_ProjectFileBrowseButton.Location = new System.Drawing.Point(1039, 20);
            this.ETS_ProjectFileBrowseButton.Name = "ETS_ProjectFileBrowseButton";
            this.ETS_ProjectFileBrowseButton.Size = new System.Drawing.Size(106, 23);
            this.ETS_ProjectFileBrowseButton.TabIndex = 2;
            this.ETS_ProjectFileBrowseButton.Text = "Durchsuchen...";
            this.ETS_ProjectFileBrowseButton.UseVisualStyleBackColor = true;
            this.ETS_ProjectFileBrowseButton.Click += new System.EventHandler(this.ETS_ProjectFileBrowseButton_Click);
            // 
            // ETS_ProjectFileTextBox
            // 
            this.ETS_ProjectFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ETS_ProjectFileTextBox.Location = new System.Drawing.Point(102, 20);
            this.ETS_ProjectFileTextBox.Name = "ETS_ProjectFileTextBox";
            this.ETS_ProjectFileTextBox.Size = new System.Drawing.Size(931, 22);
            this.ETS_ProjectFileTextBox.TabIndex = 0;
            this.ETS_ProjectFileTextBox.TextChanged += new System.EventHandler(this.ETS_ProjectFileTextBox_TextChanged);
            // 
            // ETS_ProjectImportButton
            // 
            this.ETS_ProjectImportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ETS_ProjectImportButton.Location = new System.Drawing.Point(102, 79);
            this.ETS_ProjectImportButton.Name = "ETS_ProjectImportButton";
            this.ETS_ProjectImportButton.Size = new System.Drawing.Size(280, 23);
            this.ETS_ProjectImportButton.TabIndex = 2;
            this.ETS_ProjectImportButton.Text = "Topologie und Gruppenadressen laden";
            this.ETS_ProjectImportButton.UseVisualStyleBackColor = true;
            this.ETS_ProjectImportButton.Click += new System.EventHandler(this.ETS_ProjectImportButton_Click);
            // 
            // ETS_ChangedLabel
            // 
            this.ETS_ChangedLabel.AutoSize = true;
            this.ETS_ChangedLabel.Location = new System.Drawing.Point(102, 54);
            this.ETS_ChangedLabel.Name = "ETS_ChangedLabel";
            this.ETS_ChangedLabel.Size = new System.Drawing.Size(143, 13);
            this.ETS_ChangedLabel.TabIndex = 1;
            this.ETS_ChangedLabel.Text = "noch keine Daten geladen";
            // 
            // ETS_ProjectOpenFileDialog
            // 
            this.ETS_ProjectOpenFileDialog.DefaultExt = "knxproj";
            this.ETS_ProjectOpenFileDialog.Filter = "ETS-Projektdateien|*.knxproj";
            this.ETS_ProjectOpenFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.ETS_ProjectOpenFileDialog_FileOk);
            // 
            // DependentDevicesPanel
            // 
            this.DependentDevicesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DependentDevicesPanel.AutoScroll = true;
            this.DependentDevicesPanel.Location = new System.Drawing.Point(13, 134);
            this.DependentDevicesPanel.Name = "DependentDevicesPanel";
            this.DependentDevicesPanel.Size = new System.Drawing.Size(1159, 615);
            this.DependentDevicesPanel.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.DependentDevicesPanel);
            this.Controls.Add(this.ETS_GroupBox);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainForm";
            this.Text = "Dummy-Gruppenadressen abgleichen";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.ETS_GroupBox.ResumeLayout(false);
            this.ETS_GroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox ETS_GroupBox;
        private System.Windows.Forms.OpenFileDialog ETS_ProjectOpenFileDialog;
        private System.Windows.Forms.Button ETS_ProjectFileBrowseButton;
        private System.Windows.Forms.TextBox ETS_ProjectFileTextBox;
        private System.Windows.Forms.Button ETS_ProjectImportButton;
        private System.Windows.Forms.Panel DependentDevicesPanel;
        private System.Windows.Forms.Label ETS_ChangedLabel;
    }
}

