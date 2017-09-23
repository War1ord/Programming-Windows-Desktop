using Models;
using System;
using System.Collections.Generic;
using System.Windows;

namespace UI.Views
{
    /// <summary>
    /// Interaction logic for SelectListDialog.xaml
    /// </summary>
    public partial class SelectListDialog : Window
    {
        public SelectListDialog(List<KeyValue<string, string>> list)
        {
            InitializeComponent();
            DataContext = list;
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
