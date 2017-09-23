using System;
using System.IO;

namespace Windows_Run_Shortcodes
{
    public class ShortcodeApplication
    {
        private const string SystemPath = @"C:\Windows\System32";

        public string Shortcode { get; private set; }
        public string Link { get; private set; }

        public ShortcodeApplication(string shortcode, string link)
        {
            Link = link;
            Shortcode = shortcode;
        }

        public void Setup_and_Copy_Shortcode_Application()
        {
            try
            {
                //pre-build Shortcodes.exe application with config files, need to be copied and renamed 
                var source = new FileInfo(GetType().Assembly.Location).Directory.FullName;
                var config = string.Format("{0}.exe.config", Shortcode);
                var target_exe = new FileInfo(Path.Combine(source, @"app\Shortcodes.exe"))
                    .CopyTo(Path.Combine(SystemPath, string.Format("{0}.exe", Shortcode)), true);
                var target_config = new FileInfo(Path.Combine(source, @"app\Shortcodes.exe.config"))
                    .CopyTo(Path.Combine(SystemPath, config), true);
                //.config file need to be pulled and updated
                var format = File.ReadAllText(target_config.FullName);
                var config_xml = string.Format(format, Link.Trim());
                File.WriteAllText(target_config.FullName, config_xml);
            }
            catch (Exception e)
            {
                
            }
        }
    }
}