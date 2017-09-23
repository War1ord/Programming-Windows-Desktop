using System;
using System.Windows.Forms;

namespace HTML2JSON
{
	public partial class frmHTMLToJsonConverter : Form
	{
		public frmHTMLToJsonConverter()
		{
			InitializeComponent();
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			var url = new Uri(txtAddress.Text);
			txtAddress.Text = url.ToString();
			wbMain.Url = url;
		}

		private void btnConvert_Click(object sender, EventArgs e)
		{
			if (rbFromHtml.Checked)
			{
				txtJson.Text = new Business.HtmlToJsonConverter(wbMain.DocumentText).ToJson();
			}
			else if (rbFromWebAddress.Checked)
			{
				
			}
		}
	}
}