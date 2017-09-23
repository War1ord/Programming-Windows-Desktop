using System.Windows;
using Work_Tracker.ViewModel;

namespace Work_Tracker.Windows
{
    /// <summary>
    /// Interaction logic for EnterDescription.xaml
    /// </summary>
    public partial class EnterDescription : Window
    {
        public EnterDescriptionViewModel _data;
        public EnterDescriptionViewModel Data { get { return _data ?? (_data = DataContext as EnterDescriptionViewModel); } }

        public EnterDescription(EnterDescriptionViewModel model)
        {
            InitializeComponent();
            DataContext = model;
            Description.Focus();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
