using System.Collections.Generic;
using System.Windows;

namespace Framework.Wpf.Mvvm.UI.Windows
{
    /// <summary>
    /// Interaction logic for StyleTestWindow.xaml
    /// </summary>
    public partial class StyleTestWindow : Window
    {
        public StyleTestWindow()
        {
            InitializeComponent();

            List<string> names = new List<string>();
            names.Add("Anand");
            names.Add("Arun");
            names.Add("Arjun");
            names.Add("Arul");
            names.Add("Arula");
            names.Add("Arule");
            names.Add("Bala");
            names.Add("Balaji");
            names.Add("Barani");
            names.Add("Buji");
            names.Add("Brasil");
/*
            autoComplete.ItemsSource = names;
*/
        }

        static int Clicks = 0;
        private void RepeatButton_Click(object sender, RoutedEventArgs e)
        {
            Clicks += 1;
            clickTextBlock.Text = "Number of Clicks: " + Clicks;
        }
    }
}
