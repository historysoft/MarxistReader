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
			this.iconsList = new System.Windows.Forms.ImageList(this.components);
			this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// openWorksListView
			// 
			this.openWorksListView.LargeImageList = this.iconsList;
			this.openWorksListView.Location = new System.Drawing.Point(23, 12);
			this.openWorksListView.Name = "openWorksListView";
			this.openWorksListView.Size = new System.Drawing.Size(701, 455);
			this.openWorksListView.SmallImageList = this.iconsList;
			this.openWorksListView.TabIndex = 0;
			this.openWorksListView.UseCompatibleStateImageBehavior = false;
			this.openWorksListView.View = System.Windows.Forms.View.List;
			this.openWorksListView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OpenWorksListViewKeyUp);
			this.openWorksListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OpenWorksListViewMouseDoubleClick);
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
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(847, 570);
			this.Controls.Add(this.comboBoxLanguage);
			this.Controls.Add(this.openWorksListView);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "MarxistReader";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.ResumeLayout(false);

		}
	}
}
