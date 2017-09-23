using System.Windows.Forms;

namespace WebView.UI
{
	public sealed partial class FormWebView : Form
	{
	    public FormWebView()
	    {
            InitializeComponent();
        }

	    public FormWebView(string url)
		{
			InitializeComponent();
            webBrowser.Navigate(url);
        }

	    private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var w = (sender as WebBrowser);
            if (w != null)
            {
                Text = w.DocumentTitle;
                /*if (w.Document != null)
                {
                    if (w.Document.Window != null)
                    {
                        w.Document.Window.Alert("test form web browser"); 
                    } 
                }*/
            }
        }

        private void webBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            Text = @"Waiting for website...";
        }

		private void tsMenuCreateLink_Click(object sender, System.EventArgs e)
		{
			new FormCreateLink().Show(this);
		}

		private void tsMenuOpen_Click(object sender, System.EventArgs e)
		{
			new FormOpenLinks().Show(this);
		}

		private void toolStripSplitButton1_ButtonClick(object sender, System.EventArgs e)
		{
			new FormCreateLink().Show(this);
		}

	}
}
