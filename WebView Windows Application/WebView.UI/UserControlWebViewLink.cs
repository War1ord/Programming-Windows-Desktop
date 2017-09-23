using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WebView.UI
{
	public partial class UserControlWebViewLink : UserControl
	{
		public UserControlWebViewLink()
		{
			InitializeComponent();
		}

		public UserControlWebViewLink(string url)
		{
			InitializeComponent();
			webBrowser.Navigate(url);
		}
	}
}
