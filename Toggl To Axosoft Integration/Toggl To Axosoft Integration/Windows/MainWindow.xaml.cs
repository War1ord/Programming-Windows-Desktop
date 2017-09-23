using System.Windows;
using CefSharp;

namespace Toggl_To_Axosoft_Integration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ViewModels.MainViewModel(Browser);

        }
        
    }
}
