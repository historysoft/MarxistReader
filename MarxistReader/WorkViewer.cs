using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Diagnostics;

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
				CanSaveURL = false;
				webBrowser1.Url = new Uri(value);
			}
		}
		
		public int defaultScrollY = 0;
		public int defaultScrollElementIndex = -1;
		
		public int scrollX{
			get{
				int x = 0;
				if(webBrowser1.Document == null) return -1;
				if(webBrowser1.Document.GetElementsByTagName("html").Count >= 1){
					x = webBrowser1.Document.GetElementsByTagName("html")[0].ScrollLeft;
				}
				
				if(x == 0){
					x = webBrowser1.Document.Body.ScrollLeft;
				}
				
				return x;
			}
		}
		
		public int scrollY{
			get{
				int y = 0;
				if(webBrowser1.Document == null) return -1;
				if(webBrowser1.Document.GetElementsByTagName("html").Count >= 1){
					y = webBrowser1.Document.GetElementsByTagName("html")[0].ScrollTop;
				}
				
				if(y == 0){
					y = webBrowser1.Document.Body.ScrollTop;
				}
				
				return y;
			}
		}
		
		public int scrollTopElementIndex{
			get{
				int _scrollY = scrollY;
				
				for(int i = 0; i < webBrowser1.Document.All.Count; i++){
					HtmlElement el = webBrowser1.Document.All[i];
					
					if(getVerticalOffsetOfHtmlElement(el) > _scrollY){
						return i;
					}
				}
				
				return 0;
			}
		}
		
		public bool CanSaveURL = false;
		
		int getVerticalOffsetOfHtmlElement(HtmlElement el)
		{
			//get element pos
			int yPos = el.OffsetRectangle.Top;

			//get the parents pos
			HtmlElement tempEl = el.OffsetParent;
			while (tempEl != null)
			{
				yPos += tempEl.OffsetRectangle.Top;
				tempEl = tempEl.OffsetParent;
			}

			return yPos;
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
				
				if(MainForm.tts != null){
					MainForm.tts.SpeakAsyncCancelAll();
				}
				
				Hide();
			}
		}
		void WebBrowser1DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			if(WorkViewerSettings.Default.ColorScheme == 1){
				MSHTML.IHTMLDocument2 doc = (MSHTML.IHTMLDocument2)webBrowser1.Document.DomDocument;
				MSHTML.IHTMLStyleSheet css = doc.createStyleSheet("", 0);
				css.cssText = @"*{background: #000000 !important; color: #FFFFFF !important;} a:link{color: orange !important;} a:visited{color: red !important;}";
			}
			
			if(defaultScrollElementIndex != -1){
				if(webBrowser1.Document.All.Count > defaultScrollElementIndex){
					if(defaultScrollElementIndex > 0){
						defaultScrollElementIndex -= 1;
					}
					
					HtmlElement el = webBrowser1.Document.All[defaultScrollElementIndex];
					webBrowser1.Document.Window.ScrollTo(scrollX, getVerticalOffsetOfHtmlElement(el));
				}
				
				defaultScrollElementIndex = -1;
			}else if(defaultScrollY != 0){
				webBrowser1.Document.Window.ScrollTo(0, defaultScrollY);
				defaultScrollY = 0;
			}
			
			CanSaveURL = true;
			webBrowser1.Document.Body.KeyDown += webBrowser1_Document_Body_KeyDown;;
			webBrowser1.Document.Body.KeyUp += webBrowser1_Document_Body_KeyUp;
			webBrowser1.Focus();
		}
		void webBrowser1_Document_Body_KeyDown(object sender, HtmlElementEventArgs e)
		{
			if(e.KeyPressedCode == (int) Keys.Tab){
				e.ReturnValue = false;
			}else if(e.KeyPressedCode == (int) Keys.F1){
				e.ReturnValue = false;
				MSHTML.IHTMLDocument2 htmlDocument = webBrowser1.Document.DomDocument as MSHTML.IHTMLDocument2;
				MSHTML.IHTMLSelectionObject currentSelection = htmlDocument.selection;
				
				if (currentSelection != null){
					MSHTML.IHTMLTxtRange range= currentSelection.createRange() as MSHTML.IHTMLTxtRange;

					if (range != null){
						if(range.text != null){
							if(MainForm.tts == null){
								MainForm.tts = new SpeechSynthesizer();
								MainForm.tts.SetOutputToDefaultAudioDevice();
							}
							
							MainForm.tts.SpeakAsyncCancelAll();
							MainForm.tts.SpeakAsync(range.text);
						}
					}
				}
			}
		}
		
		void webBrowser1_Document_Body_KeyUp(object sender, HtmlElementEventArgs e){
			if(e.KeyPressedCode == (int) Keys.F11){
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
