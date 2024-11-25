namespace DetermineDummyGroupAddresses
{
    partial class ResultControl
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
            System.Windows.Forms.ColumnHeader GroupAddressHeader;
            System.Windows.Forms.ColumnHeader NameHeader;
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Inkonsistent benannte Datenpunkte", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Inkompatibel zugeordnete Datentypen", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Inkonsistent zugeordnete Datentypen", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Im ETS-Projekt nicht definierte Gruppenadressen", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("Fehlende Dummy-Gruppenadressen", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup6 = new System.Windows.Forms.ListViewGroup("Nicht benötigte Dummy-Gruppenadressen", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup7 = new System.Windows.Forms.ListViewGroup("Korrekt zugeordnete Dummy-Gruppenadressen", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "0/0/12",
            "Licht"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "0/1/12",
            "Licht Status"}, -1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "15/15/255",
            "Längste Gruppenadresse"}, -1);
            this.ResultListView = new System.Windows.Forms.ListView();
            GroupAddressHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            NameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // GroupAddressHeader
            // 
            GroupAddressHeader.Text = "Adresse";
            // 
            // NameHeader
            // 
            NameHeader.Text = "Name";
            NameHeader.Width = 200;
            // 
            // ResultListView
            // 
            this.ResultListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            GroupAddressHeader,
            NameHeader});
            listViewGroup1.Header = "Inkonsistent benannte Datenpunkte";
            listViewGroup1.Name = "InconsistentName";
            listViewGroup2.Header = "Inkompatibel zugeordnete Datentypen";
            listViewGroup2.Name = "IncompatibleType";
            listViewGroup3.Header = "Inkonsistent zugeordnete Datentypen";
            listViewGroup3.Name = "InconsistentType";
            listViewGroup4.Header = "Im ETS-Projekt nicht definierte Gruppenadressen";
            listViewGroup4.Name = "NotDefined";
            listViewGroup5.Header = "Fehlende Dummy-Gruppenadressen";
            listViewGroup5.Name = "ToAdd";
            listViewGroup6.Header = "Nicht benötigte Dummy-Gruppenadressen";
            listViewGroup6.Name = "ToRemove";
            listViewGroup7.Header = "Korrekt zugeordnete Dummy-Gruppenadressen";
            listViewGroup7.Name = "ToKeep";
            this.ResultListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3,
            listViewGroup4,
            listViewGroup5,
            listViewGroup6,
            listViewGroup7});
            this.ResultListView.HideSelection = false;
            listViewItem1.Group = listViewGroup5;
            listViewItem2.Group = listViewGroup5;
            listViewItem3.Group = listViewGroup7;
            this.ResultListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.ResultListView.Location = new System.Drawing.Point(0, 0);
            this.ResultListView.Name = "ResultListView";
            this.ResultListView.Size = new System.Drawing.Size(400, 200);
            this.ResultListView.TabIndex = 0;
            this.ResultListView.UseCompatibleStateImageBehavior = false;
            this.ResultListView.View = System.Windows.Forms.View.Details;
            this.ResultListView.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.ResultListView_ColumnWidthChanged);
            // 
            // ResultControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ResultListView);
            this.Name = "ResultControl";
            this.Size = new System.Drawing.Size(400, 200);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ResultListView;
    }
}
