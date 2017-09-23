using System;
using System.Windows.Forms;

namespace WebView.UI
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
		    if (args != null && args.Length > 0 && !string.IsNullOrEmpty(args[0]))
		    {
		        Application.Run(new FormWebView(args[0]));
		    }
		    else
		    {
                Application.Run(new FormWebView());
		    }
		}
	}
}
