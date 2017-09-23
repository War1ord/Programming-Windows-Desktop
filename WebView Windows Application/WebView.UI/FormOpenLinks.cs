using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WebView.UI
{
	public partial class FormOpenLinks : Form
	{
		public FormOpenLinks()
		{
			InitializeComponent();
			for (int i = 0; i < 10; i++)
			{
				fWebViewLinks.Controls.Add(new UserControlWebViewLink("www.google.co.za"));
			}
		}
	}
}
