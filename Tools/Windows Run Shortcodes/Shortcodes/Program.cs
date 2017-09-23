using System;
using System.Configuration;
using System.Diagnostics;

namespace Shortcodes
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            try
            {
                Process.Start(ConfigurationManager.AppSettings.Get("Link"));
            }
            catch (Exception e)
            {
                
            }
        }
    }
}
