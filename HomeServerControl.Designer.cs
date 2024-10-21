namespace DetermineDummyGroupAddresses
{
    partial class HomeServerControl
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
            System.Windows.Forms.Label ChangedCaptionLabel;
            System.Windows.Forms.Label ProjectFileCaptionLabel;
            this.MainGroupBox = new System.Windows.Forms.GroupBox();
            this.ResultControl = new DetermineDummyGroupAddresses.ResultControl();
            this.ProjectFileBrowseButton = new System.Windows.Forms.Button();
            this.ImportButton = new System.Windows.Forms.Button();
            this.ChangedLabel = new System.Windows.Forms.Label();
            this.ProjectFileTextBox = new System.Windows.Forms.TextBox();
            this.ProjectOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            ChangedCaptionLabel = new System.Windows.Forms.Label();
            ProjectFileCaptionLabel = new System.Windows.Forms.Label();
            this.MainGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChangedCaptionLabel
            // 
            ChangedCaptionLabel.AutoSize = true;
            ChangedCaptionLabel.Location = new System.Drawing.Point(11, 79);
            ChangedCaptionLabel.Name = "ChangedCaptionLabel";
            ChangedCaptionLabel.Size = new System.Drawing.Size(67, 13);
            ChangedCaptionLabel.TabIndex = 7;
            ChangedCaptionLabel.Text = "Datenstand";
            // 
            // ProjectFileCaptionLabel
            // 
            ProjectFileCaptionLabel.AutoSize = true;
            ProjectFileCaptionLabel.Location = new System.Drawing.Point(11, 23);
            ProjectFileCaptionLabel.Name = "ProjectFileCaptionLabel";
            ProjectFileCaptionLabel.Size = new System.Drawing.Size(69, 13);
            ProjectFileCaptionLabel.TabIndex = 0;
            ProjectFileCaptionLabel.Text = "Projektdatei";
            // 
            // MainGroupBox
            // 
            this.MainGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainGroupBox.Controls.Add(this.ResultControl);
            this.MainGroupBox.Controls.Add(this.ProjectFileBrowseButton);
            this.MainGroupBox.Controls.Add(this.ImportButton);
            this.MainGroupBox.Controls.Add(this.ChangedLabel);
            this.MainGroupBox.Controls.Add(ChangedCaptionLabel);
            this.MainGroupBox.Controls.Add(ProjectFileCaptionLabel);
            this.MainGroupBox.Controls.Add(this.ProjectFileTextBox);
            this.MainGroupBox.Location = new System.Drawing.Point(0, 0);
            this.MainGroupBox.Name = "MainGroupBox";
            this.MainGroupBox.Size = new System.Drawing.Size(1000, 200);
            this.MainGroupBox.TabIndex = 0;
            this.MainGroupBox.TabStop = false;
            this.MainGroupBox.Text = "Gira X1 HomeServer";
            // 
            // ResultControl
            // 
            this.ResultControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultControl.Location = new System.Drawing.Point(398, 20);
            this.ResultControl.Name = "ResultControl";
            this.ResultControl.Size = new System.Drawing.Size(591, 166);
            this.ResultControl.TabIndex = 10;
            // 
            // ProjectFileBrowseButton
            // 
            this.ProjectFileBrowseButton.Location = new System.Drawing.Point(102, 46);
            this.ProjectFileBrowseButton.Name = "ProjectFileBrowseButton";
            this.ProjectFileBrowseButton.Size = new System.Drawing.Size(280, 23);
            this.ProjectFileBrowseButton.TabIndex = 9;
            this.ProjectFileBrowseButton.Text = "Durchsuchen...";
            this.ProjectFileBrowseButton.UseVisualStyleBackColor = true;
            this.ProjectFileBrowseButton.Click += new System.EventHandler(this.ProjectFileBrowseButton_Click);
            // 
            // ImportButton
            // 
            this.ImportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ImportButton.Location = new System.Drawing.Point(102, 163);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(280, 23);
            this.ImportButton.TabIndex = 9;
            this.ImportButton.Text = "Verwendete Gruppenadressen laden";
            this.ImportButton.UseVisualStyleBackColor = true;
            this.ImportButton.Click += new System.EventHandler(this.ImportButton_Click);
            // 
            // ChangedLabel
            // 
            this.ChangedLabel.AutoSize = true;
            this.ChangedLabel.Location = new System.Drawing.Point(102, 79);
            this.ChangedLabel.Name = "ChangedLabel";
            this.ChangedLabel.Size = new System.Drawing.Size(143, 13);
            this.ChangedLabel.TabIndex = 6;
            this.ChangedLabel.Text = "noch keine Daten geladen";
            // 
            // ProjectFileTextBox
            // 
            this.ProjectFileTextBox.Location = new System.Drawing.Point(102, 20);
            this.ProjectFileTextBox.Name = "ProjectFileTextBox";
            this.ProjectFileTextBox.Size = new System.Drawing.Size(280, 22);
            this.ProjectFileTextBox.TabIndex = 1;
            this.ProjectFileTextBox.TextChanged += new System.EventHandler(this.ProjectFileTextBox_TextChanged);
            // 
            // ProjectOpenFileDialog
            // 
            this.ProjectOpenFileDialog.Filter = "GPA-Dateien|*.gpa";
            this.ProjectOpenFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.ProjectOpenFileDialog_FileOk);
            // 
            // HomeServerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainGroupBox);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "HomeServerControl";
            this.Size = new System.Drawing.Size(1000, 200);
            this.MainGroupBox.ResumeLayout(false);
            this.MainGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox MainGroupBox;
        private System.Windows.Forms.Button ImportButton;
        private System.Windows.Forms.Label ChangedLabel;
        private System.Windows.Forms.TextBox ProjectFileTextBox;
        private ResultControl ResultControl;
        private System.Windows.Forms.Button ProjectFileBrowseButton;
        private System.Windows.Forms.OpenFileDialog ProjectOpenFileDialog;
    }
}
