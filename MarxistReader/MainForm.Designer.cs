/*
 * Created by SharpDevelop.
 * User: User
 * Date: 21-May-20
 * Time: 16:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace MarxistReader
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ListView openWorksListView;
		private System.Windows.Forms.ImageList iconsList;
		private System.Windows.Forms.ComboBox comboBoxLanguage;
		private System.Windows.Forms.Button openURLButton;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ComboBox comboBoxColorScheme;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.openWorksListView = new System.Windows.Forms.ListView();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.iconsList = new System.Windows.Forms.ImageList(this.components);
			this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
			this.openURLButton = new System.Windows.Forms.Button();
			this.comboBoxColorScheme = new System.Windows.Forms.ComboBox();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// openWorksListView
			// 
			this.openWorksListView.ContextMenuStrip = this.contextMenuStrip1;
			this.openWorksListView.LargeImageList = this.iconsList;
			this.openWorksListView.Location = new System.Drawing.Point(23, 12);
			this.openWorksListView.Name = "openWorksListView";
			this.openWorksListView.Size = new System.Drawing.Size(773, 455);
			this.openWorksListView.SmallImageList = this.iconsList;
			this.openWorksListView.TabIndex = 0;
			this.openWorksListView.UseCompatibleStateImageBehavior = false;
			this.openWorksListView.View = System.Windows.Forms.View.List;
			this.openWorksListView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OpenWorksListViewKeyUp);
			this.openWorksListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OpenWorksListViewMouseDoubleClick);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripMenuItem1});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(123, 28);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(122, 24);
			this.toolStripMenuItem1.Text = "Delete";
			this.toolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuItem1Click);
			// 
			// iconsList
			// 
			this.iconsList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iconsList.ImageStream")));
			this.iconsList.TransparentColor = System.Drawing.Color.Transparent;
			this.iconsList.Images.SetKeyName(0, "marx_icon");
			this.iconsList.Images.SetKeyName(1, "lenin_icon");
			this.iconsList.Images.SetKeyName(2, "luxemburg_icon");
			this.iconsList.Images.SetKeyName(3, "trotsky_icon");
			this.iconsList.Images.SetKeyName(4, "engels_icon");
			// 
			// comboBoxLanguage
			// 
			this.comboBoxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxLanguage.FormattingEnabled = true;
			this.comboBoxLanguage.Location = new System.Drawing.Point(23, 492);
			this.comboBoxLanguage.Name = "comboBoxLanguage";
			this.comboBoxLanguage.Size = new System.Drawing.Size(167, 24);
			this.comboBoxLanguage.TabIndex = 1;
			this.comboBoxLanguage.SelectedIndexChanged += new System.EventHandler(this.ComboBoxLanguageSelectedIndexChanged);
			// 
			// openURLButton
			// 
			this.openURLButton.Location = new System.Drawing.Point(23, 522);
			this.openURLButton.Name = "openURLButton";
			this.openURLButton.Size = new System.Drawing.Size(101, 23);
			this.openURLButton.TabIndex = 3;
			this.openURLButton.Text = "Custom URL";
			this.openURLButton.UseVisualStyleBackColor = true;
			this.openURLButton.Click += new System.EventHandler(this.OpenURLButtonClick);
			// 
			// comboBoxColorScheme
			// 
			this.comboBoxColorScheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxColorScheme.FormattingEnabled = true;
			this.comboBoxColorScheme.Items.AddRange(new object[] {
			"<Color scheme>",
			"White on black",
			"Black on white"});
			this.comboBoxColorScheme.Location = new System.Drawing.Point(196, 492);
			this.comboBoxColorScheme.Name = "comboBoxColorScheme";
			this.comboBoxColorScheme.Size = new System.Drawing.Size(167, 24);
			this.comboBoxColorScheme.TabIndex = 2;
			this.comboBoxColorScheme.SelectedIndexChanged += new System.EventHandler(this.ComboBoxColorSchemeSelectedIndexChanged);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(847, 570);
			this.Controls.Add(this.comboBoxColorScheme);
			this.Controls.Add(this.openURLButton);
			this.Controls.Add(this.comboBoxLanguage);
			this.Controls.Add(this.openWorksListView);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "MarxistReader";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
