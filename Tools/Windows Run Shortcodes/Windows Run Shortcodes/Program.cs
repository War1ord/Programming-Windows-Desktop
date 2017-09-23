using System;

namespace Windows_Run_Shortcodes
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                RegistrySettings.ConfigExeFileRightClickMenu();
                if (args != null && args.Length > 0)
                {
                    string shortcode;
                    do
                    {
                        Console.Write("Enter application's Shortcode? ");
                        shortcode = Console.ReadLine();
                        Console.WriteLine();
                    } while (string.IsNullOrEmpty(shortcode));
                    var application = new ShortcodeApplication(shortcode, args[0]);
                    application.Setup_and_Copy_Shortcode_Application();
                }
            }
            catch (Exception e)
            {
                //TODO: log exception
            }
        }
    }
}
