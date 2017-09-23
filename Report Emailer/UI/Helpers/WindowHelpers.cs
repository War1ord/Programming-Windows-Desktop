using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using UI.Views;

namespace UI.Helpers
{
    /// <summary>
    /// a class containing helper methods for windows and dialogs
    /// </summary>
    public static class WindowHelpers
    {
        /// <summary>
        /// Shows the data grid.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        public static void ShowDataGrid(DataTable dataTable)
        {
            new Window
            {
                Content = new DataGrid
                {
                    ItemsSource = dataTable.DefaultView,
                }
            }.Show();
        }
        /// <summary>
        /// Shows the data grid.
        /// </summary>
        /// <param name="dataSet">The data set.</param>
        public static void ShowDataGrid(DataSet dataSet)
        {
            foreach (DataTable table in dataSet.Tables)
            {
                new ViewDataTable
                {
                    DataContext = table
                }.Show();
            }
        }
        /// <summary>
        /// Shows the select list dialog.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>a dialog result of keyvalue</returns>
        public static Models.DialogResult<global::Models.KeyValue<string, string>> ShowSelectListDialog(List<global::Models.KeyValue<string,string>> list)
        {
            var dialog = new SelectListDialog(list);
            var result = dialog.ShowDialog();
            if (result ?? false)
            {
                var selectedItem = dialog.List.SelectedItem as global::Models.KeyValue<string, string>;
                if (selectedItem != null)
                {
                    return new Models.DialogResult<global::Models.KeyValue<string, string>>
                    {
                        Success = true,
                        Result = selectedItem,
                    };
                }
                else
                {
                    return new Models.DialogResult<global::Models.KeyValue<string, string>>
                    {
                        Success = false,
                        Result = null,
                    };
                }
            }
            else
            {
                return new Models.DialogResult<global::Models.KeyValue<string, string>>
                {
                    Success = false,
                    Result = null,
                };
            }
        }
    }
}