using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Speech.Synthesis;
using Newtonsoft.Json;

namespace MarxistReader
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
			InitializeComponent();
		}
		
		WorkViewer wv = new WorkViewer();
		int OpenedIndex = -1;
		WebClient wc = new WebClient();
		
		struct WorkItem{
			public string uri;
			public string title;
			public string iconKey;
			public int scrollY;
			public int defaultScrollElementIndex;
		}
		
		struct WebsiteLanguage{
			public string href;
			public string name;
		}
		
		public const string BASE_URL = "https://www.marxists.org/";
		
		List<WorkItem> WorkItemList = new List<WorkItem>();
		List<WebsiteLanguage> WebsiteLanguageList = new List<WebsiteLanguage>();
		public static SpeechSynthesizer tts = null;
		
		void MainFormLoad(object sender, EventArgs e){
			openWorksListView.Items.Add("< New > ", "marx_icon");
			wv.FormClosing += wv_FormClosing;
			if(File.Exists("worksOpen.xml")){
				XmlDocument xml = new XmlDocument();
				
				try{
					xml.Load("worksOpen.xml");
					foreach(XmlNode listNode in xml.DocumentElement.SelectSingleNode("/WorkItems").ChildNodes){
						if(listNode.Name == "WorkItem"){
							WorkItem wi = new WorkItem();
							wi.scrollY = 0;
							wi.defaultScrollElementIndex = -1;
							
							foreach(XmlNode wiNode in listNode.ChildNodes){
								
								switch(wiNode.Name){
									case "Uri":
										wi.uri = wiNode.InnerText;
										break;
									case "Title":
										wi.title = wiNode.InnerText;
										break;
									case "IconKey":
										wi.iconKey = wiNode.InnerText;
										break;
									case "ScrollY":
										int.TryParse(wiNode.InnerText, out wi.scrollY);
										break;
									case "DefaultScrollElementIndex":
										int.TryParse(wiNode.InnerText, out wi.defaultScrollElementIndex);
										break;
								}
							}
							
							WorkItemList.Add(wi);
							openWorksListView.Items.Add(wi.title, wi.iconKey);
						}
					}
				}catch(Exception ex){
					MessageBox.Show(ex.ToString());
				}
				
				wc.DownloadStringCompleted += wc_DownloadStringCompleted;
				wc.Encoding = Encoding.UTF8;
				
				try{
					wc.DownloadStringAsync(new Uri("https://www.marxists.org/admin/js/data/sections.json"));
				}catch(Exception ex){
					MessageBox.Show(ex.ToString());
				}
			}
			
			comboBoxLanguage.Items.Add("< Language >");
			comboBoxLanguage.SelectedIndex = 0;
			
			comboBoxColorScheme.SelectedIndex = 0;
		}
		
		void OpenSelectedWork(){
			if(openWorksListView.SelectedItems.Count == 1){
				if(OpenedIndex == -1){
					OpenedIndex = openWorksListView.SelectedItems[0].Index;
					Hide();
					wv.Show();
					
					if(openWorksListView.SelectedItems[0].Index == 0){
						wv.defaultScrollY = 0;
						wv.defaultScrollElementIndex = -1;
						wv.Uri = "https://www.marxists.org/";
					}
					
					if(openWorksListView.SelectedItems[0].Index > 0){
						WorkItem wi = WorkItemList[openWorksListView.SelectedItems[0].Index - 1];
						wv.defaultScrollY = wi.scrollY;
						wv.defaultScrollElementIndex = wi.defaultScrollElementIndex;
						wv.Uri = wi.uri;
					}
				}else{
					wv.BringToFront();
				}
			}
		}
		
		void OpenWorksListViewMouseDoubleClick(object sender, MouseEventArgs e){
			OpenSelectedWork();
		}

		void wv_FormClosing(object sender, FormClosingEventArgs e){
			if(wv.Uri != "about:blank" && wv.CanSaveURL){
				WorkItem wi;
				
				int NewIndex = OpenedIndex;
				
				if(OpenedIndex == 0){
					NewIndex = WorkItemList.Count;
					wi = new WorkItem();
					WorkItemList.Add(wi);
				}else if(OpenedIndex > 0){
					NewIndex = OpenedIndex - 1;
					wi = WorkItemList[NewIndex];
				}
				
				wi.uri = wv.Uri;
				wi.title = wv.DocumentTitle;
				wi.iconKey = "marx_icon";
				
				if(wi.uri.Contains("/marx/") || wi.uri.Contains("/m-e/")){
					if(wi.title.Contains("Engels")){
						wi.iconKey = "engels_icon";
					}
				}else if(wi.uri.Contains("/luxemburg/")){
					wi.iconKey = "luxemburg_icon";
				}else if(wi.uri.Contains("/lenin/")){
					wi.iconKey = "lenin_icon";
				}else if(wi.uri.Contains("/trotsky/")){
					wi.iconKey = "trotsky_icon";
				}
				
				wi.scrollY = wv.scrollY;
				wi.defaultScrollElementIndex = wv.scrollTopElementIndex;
				WorkItemList[NewIndex] = wi;
				
				if(OpenedIndex == 0){
					openWorksListView.Items.Add(wi.title, wi.iconKey);
				}else if(OpenedIndex> 0){
					openWorksListView.Items[OpenedIndex].Text = wi.title;
					openWorksListView.Items[OpenedIndex].ImageKey = wi.iconKey;
				}
			}
			
			OpenedIndex = -1;
			Show();
		}
		
		void OpenWorksListViewKeyUp(object sender, KeyEventArgs e)
		{
			if(e.KeyData == Keys.Enter){
				OpenSelectedWork();
			}else if(e.KeyData == Keys.Delete){
				DeleteSelectedWorks();
			}
		}
		
		void DeleteSelectedWorks(){
			if(openWorksListView.SelectedItems.Count >= 1){
				if(MessageBox.Show("Delete for sure?", Text, MessageBoxButtons.YesNo) == DialogResult.Yes){
					foreach(ListViewItem item in openWorksListView.SelectedItems){
						if(item.Index != 0){
							WorkItemList.RemoveAt(item.Index - 1);
							openWorksListView.Items.Remove(item);
						}
					}
				}
			}
		}
		
		void MainFormFormClosing(object sender, FormClosingEventArgs e){
			try{
				XmlWriter xml = XmlWriter.Create("worksOpen.xml");
				xml.WriteStartElement("WorkItems");
				
				foreach(WorkItem wi in WorkItemList){
					xml.WriteStartElement("WorkItem");
					xml.WriteElementString("Uri", wi.uri.ToString());
					xml.WriteElementString("Title", wi.title);
					xml.WriteElementString("IconKey", wi.iconKey);
					xml.WriteElementString("DefaultScrollElementIndex", wi.defaultScrollElementIndex.ToString());
					xml.WriteElementString("ScrollY", wi.scrollY.ToString());
					xml.WriteEndElement();
				}
				
				xml.WriteEndElement();
				xml.Close();
			}catch(Exception ex){
				MessageBox.Show(ex.ToString());
			}
		}

		void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e){
			try{
				dynamic dynJson = JsonConvert.DeserializeObject(e.Result);
				
				
				foreach (var item in dynJson){
					WebsiteLanguage wl = new WebsiteLanguage();
					wl.name = item.name;
					wl.href = item.href;
					WebsiteLanguageList.Add(wl);
					comboBoxLanguage.Items.Add(wl.name);
				}
			}catch(Exception ex){
				MessageBox.Show(ex.ToString());
			}
		}
		
		void ComboBoxLanguageSelectedIndexChanged(object sender, EventArgs e){
			if(comboBoxLanguage.SelectedIndex > 0 && comboBoxLanguage.SelectedIndex <= WebsiteLanguageList.Count){
				OpenedIndex = 0;
				Hide();
				wv.Show();
				wv.Uri = BASE_URL + WebsiteLanguageList[comboBoxLanguage.SelectedIndex - 1].href;
			}
		}
		
		void OpenURLButtonClick(object sender, EventArgs e){
			string url =
				Microsoft.VisualBasic.Interaction.InputBox("Please enter the custom URL.",
				                                           Text,
				                                           "",
				                                           0,
				                                           0);
			if(!String.IsNullOrEmpty(url)){
				OpenedIndex = 0;
				Hide();
				wv.Show();
				wv.Uri = url;
			}
		}
		
		void ToolStripMenuItem1Click(object sender, EventArgs e)
		{
			DeleteSelectedWorks();
		}
		
		void ComboBoxColorSchemeSelectedIndexChanged(object sender, EventArgs e)
		{
			if(comboBoxColorScheme.SelectedIndex > 0){
				WorkViewerSettings.Default.ColorScheme = comboBoxColorScheme.SelectedIndex - 1;
			}
		}
	}
}
