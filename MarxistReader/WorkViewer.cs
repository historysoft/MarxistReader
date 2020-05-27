using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace MarxistReader
{
	public partial class WorkViewer : Form
	{
		public WorkViewer()
		{
			InitializeComponent();
			webBrowser1.DownloadControlFlags = (int)(WebBrowserDownloadControlFlags.NO_JAVA
			                                         | WebBrowserDownloadControlFlags.NO_BEHAVIORS
			                                         | WebBrowserDownloadControlFlags.NO_DLACTIVEXCTLS
			                                         | WebBrowserDownloadControlFlags.NO_RUNACTIVEXCTLS
			                                         | WebBrowserDownloadControlFlags.NO_SCRIPTS
			                                         | WebBrowserDownloadControlFlags.DLIMAGES);
			webBrowser1.Url = new Uri("https://www.marxists.org/");
		}
		
		
		public string DocumentTitle{
			get{
				if(webBrowser1.DocumentTitle == null) return "";
				return webBrowser1.DocumentTitle;
			}
		}
		
		public string Uri{
			get{
				if(webBrowser1.Url == null) return "";
				return webBrowser1.Url.ToString();
			}
			
			set{
				webBrowser1.Url = new Uri(value);
			}
		}
		
		public int defaultScrollY = 0;
		
		public int scrollY{
			get{
				if(webBrowser1.Document == null) return 0;
				int y = webBrowser1.Document.GetElementsByTagName("html")[0].ScrollTop;
				if(y == 0)
					y = webBrowser1.Document.Body.ScrollTop;
				return y;
			}
		}
		
		void WorkViewerLoad(object sender, EventArgs e){
			if (WorkViewerSettings.Default.Maximised){
				WindowState = FormWindowState.Maximized;
			}
			
			Location = WorkViewerSettings.Default.Location;
			Size = WorkViewerSettings.Default.Size;
		}
		
		void WorkViewerFormClosing(object sender, FormClosingEventArgs e)
		{
			if(e.CloseReason == CloseReason.UserClosing){
				e.Cancel = true;
				
				if (WindowState == FormWindowState.Maximized)
				{
					WorkViewerSettings.Default.Location = RestoreBounds.Location;
					WorkViewerSettings.Default.Size = RestoreBounds.Size;
					WorkViewerSettings.Default.Maximised = true;
				}
				else if (WindowState == FormWindowState.Normal)
				{
					WorkViewerSettings.Default.Location = Location;
					WorkViewerSettings.Default.Size = Size;
					WorkViewerSettings.Default.Maximised = false;
				}else{
					WorkViewerSettings.Default.Location = RestoreBounds.Location;
					WorkViewerSettings.Default.Size = RestoreBounds.Size;
					WorkViewerSettings.Default.Maximised = false;
				}
				
				WorkViewerSettings.Default.Save();
				
				Hide();
			}
		}
		void WebBrowser1DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			webBrowser1.Document.Window.ScrollTo(0, defaultScrollY);
			defaultScrollY = 0;
		}
		
		void WorkViewerKeyUp(object sender, KeyEventArgs e){
		}
		
		void WebBrowser1PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if(e.KeyCode == Keys.F11){
				ToggleFullscreen();
			}
		}
		
		void ToggleFullscreen(){
			if(FormBorderStyle == FormBorderStyle.None){
				WindowState = FormWindowState.Normal;
				FormBorderStyle = FormBorderStyle.FixedSingle;
			}else{
				WindowState = FormWindowState.Maximized;
				FormBorderStyle = FormBorderStyle.None;
			}
		}
	}
}
