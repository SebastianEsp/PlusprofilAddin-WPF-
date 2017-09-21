namespace EACustomWindow
{
	partial class EAUserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.masterTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.topTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.uriLabel = new System.Windows.Forms.Label();
            this.uriTextBox = new System.Windows.Forms.TextBox();
            this.umlLabel = new System.Windows.Forms.Label();
            this.umlTextBox = new System.Windows.Forms.TextBox();
            this.aliasLabel = new System.Windows.Forms.Label();
            this.aliasTextBox = new System.Windows.Forms.TextBox();
            this.langTabControl = new System.Windows.Forms.TabControl();
            this.danishTabPage = new System.Windows.Forms.TabPage();
            this.danishTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.englishTabPage = new System.Windows.Forms.TabPage();
            this.englishTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.stereotypeTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.masterTableLayoutPanel.SuspendLayout();
            this.topTableLayoutPanel.SuspendLayout();
            this.langTabControl.SuspendLayout();
            this.danishTabPage.SuspendLayout();
            this.englishTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // masterTableLayoutPanel
            // 
            this.masterTableLayoutPanel.ColumnCount = 1;
            this.masterTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.masterTableLayoutPanel.Controls.Add(this.topTableLayoutPanel, 0, 0);
            this.masterTableLayoutPanel.Controls.Add(this.langTabControl, 0, 1);
            this.masterTableLayoutPanel.Controls.Add(this.stereotypeTableLayoutPanel, 0, 2);
            this.masterTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.masterTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.masterTableLayoutPanel.Margin = new System.Windows.Forms.Padding(4);
            this.masterTableLayoutPanel.Name = "masterTableLayoutPanel";
            this.masterTableLayoutPanel.RowCount = 3;
            this.masterTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.masterTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.masterTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.masterTableLayoutPanel.Size = new System.Drawing.Size(620, 738);
            this.masterTableLayoutPanel.TabIndex = 0;
            // 
            // topTableLayoutPanel
            // 
            this.topTableLayoutPanel.AutoSize = true;
            this.topTableLayoutPanel.ColumnCount = 2;
            this.topTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.topTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.topTableLayoutPanel.Controls.Add(this.uriLabel);
            this.topTableLayoutPanel.Controls.Add(this.uriTextBox);
            this.topTableLayoutPanel.Controls.Add(this.umlLabel);
            this.topTableLayoutPanel.Controls.Add(this.umlTextBox);
            this.topTableLayoutPanel.Controls.Add(this.aliasLabel);
            this.topTableLayoutPanel.Controls.Add(this.aliasTextBox);
            this.topTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topTableLayoutPanel.Location = new System.Drawing.Point(4, 4);
            this.topTableLayoutPanel.Margin = new System.Windows.Forms.Padding(4);
            this.topTableLayoutPanel.Name = "topTableLayoutPanel";
            this.topTableLayoutPanel.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.topTableLayoutPanel.RowCount = 3;
            this.topTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.topTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.topTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.topTableLayoutPanel.Size = new System.Drawing.Size(612, 106);
            this.topTableLayoutPanel.TabIndex = 0;
            this.topTableLayoutPanel.Text = "URI";
            // 
            // uriLabel
            // 
            this.uriLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uriLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.uriLabel.Location = new System.Drawing.Point(11, 6);
            this.uriLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.uriLabel.Name = "uriLabel";
            this.uriLabel.Size = new System.Drawing.Size(70, 34);
            this.uriLabel.TabIndex = 0;
            this.uriLabel.Text = "URI";
            this.uriLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uriTextBox
            // 
            this.uriTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uriTextBox.Location = new System.Drawing.Point(89, 10);
            this.uriTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.uriTextBox.Name = "uriTextBox";
            this.uriTextBox.Size = new System.Drawing.Size(512, 22);
            this.uriTextBox.TabIndex = 1;
            // 
            // umlLabel
            // 
            this.umlLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.umlLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.umlLabel.Location = new System.Drawing.Point(11, 40);
            this.umlLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.umlLabel.Name = "umlLabel";
            this.umlLabel.Size = new System.Drawing.Size(70, 30);
            this.umlLabel.TabIndex = 2;
            this.umlLabel.Text = "UML Name";
            this.umlLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // umlTextBox
            // 
            this.umlTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.umlTextBox.Location = new System.Drawing.Point(89, 44);
            this.umlTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.umlTextBox.Name = "umlTextBox";
            this.umlTextBox.Size = new System.Drawing.Size(512, 22);
            this.umlTextBox.TabIndex = 3;
            // 
            // aliasLabel
            // 
            this.aliasLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aliasLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.aliasLabel.Location = new System.Drawing.Point(11, 70);
            this.aliasLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.aliasLabel.Name = "aliasLabel";
            this.aliasLabel.Size = new System.Drawing.Size(70, 30);
            this.aliasLabel.TabIndex = 4;
            this.aliasLabel.Text = "Alias";
            this.aliasLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // aliasTextBox
            // 
            this.aliasTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aliasTextBox.Location = new System.Drawing.Point(89, 74);
            this.aliasTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.aliasTextBox.Name = "aliasTextBox";
            this.aliasTextBox.Size = new System.Drawing.Size(512, 22);
            this.aliasTextBox.TabIndex = 5;
            // 
            // langTabControl
            // 
            this.langTabControl.Controls.Add(this.danishTabPage);
            this.langTabControl.Controls.Add(this.englishTabPage);
            this.langTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.langTabControl.Location = new System.Drawing.Point(4, 118);
            this.langTabControl.Margin = new System.Windows.Forms.Padding(4);
            this.langTabControl.Name = "langTabControl";
            this.langTabControl.SelectedIndex = 0;
            this.langTabControl.Size = new System.Drawing.Size(612, 304);
            this.langTabControl.TabIndex = 1;
            // 
            // danishTabPage
            // 
            this.danishTabPage.Controls.Add(this.danishTableLayoutPanel);
            this.danishTabPage.Location = new System.Drawing.Point(4, 25);
            this.danishTabPage.Margin = new System.Windows.Forms.Padding(4);
            this.danishTabPage.Name = "danishTabPage";
            this.danishTabPage.Padding = new System.Windows.Forms.Padding(4);
            this.danishTabPage.Size = new System.Drawing.Size(604, 275);
            this.danishTabPage.TabIndex = 0;
            this.danishTabPage.Text = "Danish Annotations";
            this.danishTabPage.UseVisualStyleBackColor = true;
            // 
            // danishTableLayoutPanel
            // 
            this.danishTableLayoutPanel.ColumnCount = 1;
            this.danishTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.danishTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.danishTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.danishTableLayoutPanel.Location = new System.Drawing.Point(4, 4);
            this.danishTableLayoutPanel.Margin = new System.Windows.Forms.Padding(4);
            this.danishTableLayoutPanel.Name = "danishTableLayoutPanel";
            this.danishTableLayoutPanel.Padding = new System.Windows.Forms.Padding(5);
            this.danishTableLayoutPanel.RowCount = 1;
            this.danishTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.danishTableLayoutPanel.Size = new System.Drawing.Size(596, 267);
            this.danishTableLayoutPanel.TabIndex = 0;
            // 
            // englishTabPage
            // 
            this.englishTabPage.Controls.Add(this.englishTableLayoutPanel);
            this.englishTabPage.Location = new System.Drawing.Point(4, 25);
            this.englishTabPage.Margin = new System.Windows.Forms.Padding(4);
            this.englishTabPage.Name = "englishTabPage";
            this.englishTabPage.Padding = new System.Windows.Forms.Padding(4);
            this.englishTabPage.Size = new System.Drawing.Size(604, 275);
            this.englishTabPage.TabIndex = 1;
            this.englishTabPage.Text = "English Annotations";
            this.englishTabPage.UseVisualStyleBackColor = true;
            // 
            // englishTableLayoutPanel
            // 
            this.englishTableLayoutPanel.ColumnCount = 1;
            this.englishTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.englishTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.englishTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.englishTableLayoutPanel.Location = new System.Drawing.Point(4, 4);
            this.englishTableLayoutPanel.Margin = new System.Windows.Forms.Padding(4);
            this.englishTableLayoutPanel.Name = "englishTableLayoutPanel";
            this.englishTableLayoutPanel.Padding = new System.Windows.Forms.Padding(0, 6, 7, 0);
            this.englishTableLayoutPanel.RowCount = 1;
            this.englishTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.englishTableLayoutPanel.Size = new System.Drawing.Size(596, 267);
            this.englishTableLayoutPanel.TabIndex = 0;
            // 
            // stereotypeTableLayoutPanel
            // 
            this.stereotypeTableLayoutPanel.ColumnCount = 1;
            this.stereotypeTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.stereotypeTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.stereotypeTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stereotypeTableLayoutPanel.Location = new System.Drawing.Point(4, 430);
            this.stereotypeTableLayoutPanel.Margin = new System.Windows.Forms.Padding(4);
            this.stereotypeTableLayoutPanel.Name = "stereotypeTableLayoutPanel";
            this.stereotypeTableLayoutPanel.RowCount = 1;
            this.stereotypeTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.stereotypeTableLayoutPanel.Size = new System.Drawing.Size(612, 304);
            this.stereotypeTableLayoutPanel.TabIndex = 1;
            // 
            // EAUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.masterTableLayoutPanel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "EAUserControl";
            this.Size = new System.Drawing.Size(620, 738);
            this.masterTableLayoutPanel.ResumeLayout(false);
            this.masterTableLayoutPanel.PerformLayout();
            this.topTableLayoutPanel.ResumeLayout(false);
            this.topTableLayoutPanel.PerformLayout();
            this.langTabControl.ResumeLayout(false);
            this.danishTabPage.ResumeLayout(false);
            this.englishTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel masterTableLayoutPanel;
		private System.Windows.Forms.TableLayoutPanel topTableLayoutPanel;
		private System.Windows.Forms.TabControl langTabControl;
		private System.Windows.Forms.TabPage danishTabPage;
		private System.Windows.Forms.TabPage englishTabPage;
		private System.Windows.Forms.TableLayoutPanel englishTableLayoutPanel;
		private System.Windows.Forms.TableLayoutPanel danishTableLayoutPanel;
		private System.Windows.Forms.TableLayoutPanel stereotypeTableLayoutPanel;
		private System.Windows.Forms.Label uriLabel;
		private System.Windows.Forms.TextBox uriTextBox;
        private System.Windows.Forms.Label umlLabel;
        private System.Windows.Forms.TextBox umlTextBox;
        private System.Windows.Forms.Label aliasLabel;
        private System.Windows.Forms.TextBox aliasTextBox;
    }
}