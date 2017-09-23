using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Toggl_To_Axosoft_Integration.Business;

namespace Toggl_To_Axosoft_Integration
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            CefSharp.Cef.Initialize(new CefSharp.CefSettings());
            var templateEngine = TemplateEngine.Instance;
        }
    }
}
