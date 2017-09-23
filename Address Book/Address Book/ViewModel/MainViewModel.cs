using System.Linq;
using Address_Book.Extentions;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using Microsoft.Win32;

namespace Address_Book.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Private Fields
        private ObservableCollection<Models.Address> _list;
        private Models.Address _selectedAddress;
        private Enums.SortBy _sortBy;
        private ObservableCollection<Models.Message> _messageList; 
        #endregion

        public static string ApplicationFolder;
        public static string XmlFileFullName;

        static MainViewModel()
        {
            ApplicationFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Assembly.GetEntryAssembly().GetName().Name);
            XmlFileFullName = Path.Combine(ApplicationFolder, "Addresses.xml");
            if (!Directory.Exists(ApplicationFolder))
            {
                Directory.CreateDirectory(ApplicationFolder);
            }
        }

        public MainViewModel()
        {
            NewLists();
            if (File.Exists(XmlFileFullName))
            {
                try
                {
                    List = new ObservableCollection<Models.Address>(
                        File.ReadAllText(XmlFileFullName)
                            .ToObject<List<Models.Address>>());
                }
                catch (Exception e)
                {
                    MessageList.Add(new Models.Message(e));
                }
            }
        }
        private void NewLists()
        {
            MessageList = new ObservableCollection<Models.Message>();
            List = new ObservableCollection<Models.Address>();
        }

        public ObservableCollection<Models.Address> List
        {
            get { return _list; }
            set
            {
                _list = value;
                RaisePropertyChanged(() => List);
            }
        }
        public Models.Address SelectedAddress
        {
            get { return _selectedAddress; }
            set
            {
                _selectedAddress = value;
                RaisePropertyChanged(() => SelectedAddress);
            }
        }

        public ObservableCollection<Models.Message> MessageList
        {
            get { return _messageList; }
            set
            {
                _messageList = value;
                RaisePropertyChanged(() => MessageList);
            }
        }

        public bool IsAddressValid { get { return SelectedAddress != null; } }

        public Enums.SortBy SortBy
        {
            get { return _sortBy; }
            set
            {
                _sortBy = value;
                RaisePropertyChanged(() => SortBy);
                switch (value)
                {
                    case Enums.SortBy.AddressType:
                        List = new ObservableCollection<Models.Address>(List.OrderBy(i => i.Type).ToList());
                        break;
                    case Enums.SortBy.Province:
                        List = new ObservableCollection<Models.Address>(List.OrderBy(i => i.Province).ToList());
                        break;
                    case Enums.SortBy.City:
                        List = new ObservableCollection<Models.Address>(List.OrderBy(i => i.City).ToList());
                        break;
                    case Enums.SortBy.Suburb:
                        List = new ObservableCollection<Models.Address>(List.OrderBy(i => i.Suburb).ToList());
                        break;
                    case Enums.SortBy.PostalCode:
                        List = new ObservableCollection<Models.Address>(List.OrderBy(i => i.PostalCode).ToList());
                        break;
                    default:
                        List = new ObservableCollection<Models.Address>(List.OrderBy(i => i.Display).ToList());
                        break;
                }
            }
        }

        public ICommand AddCommand
        {
            get
            {
                return new RelayCommand(canExecute: () => List != null, execute: () =>
                {
                    try
                    {
                        List.Add(new Models.Address());
                        MessageList.Insert(0, new Models.Message("New Address Added."));
                    }
                    catch (Exception e)
                    {
                        MessageList.Insert(0, new Models.Message(e));
                    }
                });
            }
        }
        public ICommand DuplicateCommand
        {
            get
            {
                return new RelayCommand(canExecute: () => List != null && SelectedAddress != null, execute: () =>
                {
                    try
                    {
                        List.Add(new Models.Address(SelectedAddress));
                        MessageList.Insert(0, new Models.Message("Address Duplicated."));
                    }
                    catch (Exception e)
                    {
                        MessageList.Insert(0, new Models.Message(e));
                    }
                });
            }
        }
        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(canExecute: () => List.Any(), execute: () =>
                {
                    try
                    {
                        if (List.Contains(SelectedAddress))
                        {
                            List.Remove(SelectedAddress);
                            MessageList.Insert(0, new Models.Message("Address Removed.")); 
                        }
                        else
                        {
                            MessageList.Insert(0, new Models.Message("Address does not exists.")); 
                        }
                    }
                    catch (Exception e)
                    {
                        MessageList.Insert(0, new Models.Message(e));
                    }
                });
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(canExecute: () => List.Any(), execute: () =>
                {
                    try
                    {
                        File.WriteAllText(XmlFileFullName, List.ToXml());
                        MessageList.Insert(0, new Models.Message("Addresses Saved."));
                    }
                    catch (Exception e)
                    {
                        MessageList.Insert(0, new Models.Message(e));
                    }
                });
            }
        }

        public ICommand ExportToExcelCommand
        {
            get
            {
                return new RelayCommand(canExecute: () => List.Any(), execute: () =>
                {
                    try
                    {
                        var dialog = new SaveFileDialog
                        {
                            Filter = "Excel file|*.xlsx"
                        };
                        var result = dialog.ShowDialog();
                        if (result != true)
                        {
                            MessageList.Add(new Models.Message("Cancelled."));
                            return;
                        }
                        using (var memory = new MemoryStream())
                        using (var excel = new ExcelPackage(memory))
                        {
                            excel.Compression = CompressionLevel.BestCompression;
                            var sheet = excel.Workbook.Worksheets.Add("Sheet 1");
                            var filter = new[] { "Type", "Province", "City", "Suburb", "PostalCode", "StreetName", "StreetNumber", "Extra" };
                            var properties = typeof(Models.Address).GetProperties()
                                .Where(i => filter.Contains(i.Name))
                                .Cast<MemberInfo>()
                                .ToArray();
                            sheet.Cells.LoadFromCollection(List, true, TableStyles.Medium2, BindingFlags.GetProperty, properties);
                            sheet.Cells.AutoFitColumns();
                            File.WriteAllBytes(dialog.FileName, excel.GetAsByteArray());
                            MessageList.Insert(0, new Models.Message("Addresses Exported to Excel."));
                        }
                    }
                    catch (Exception e)
                    {
                        MessageList.Insert(0, new Models.Message(e));
                    }
                });
            }
        }
        public ICommand ExportToXmlCommand
        {
            get
            {
                return new RelayCommand(canExecute: () => List.Any(), execute: () =>
                {
                    try
                    {
                        var dialog = new SaveFileDialog
                        {
                            Filter = "Xml file|*.xml"
                        };
                        var result = dialog.ShowDialog();
                        if (result != true)
                        {
                            MessageList.Add(new Models.Message("Cancelled."));
                            return;
                        }
                        File.WriteAllText(dialog.FileName, List.ToXml());
                        MessageList.Insert(0, new Models.Message("Addresses Exported to Xml."));
                    }
                    catch (Exception e)
                    {
                        MessageList.Insert(0, new Models.Message(e));
                    }
                });
            }
        }

        public ICommand ImportFromExcelCommand
        {
            get
            {
                return new RelayCommand(canExecute: () => List != null, execute: () =>
                {
                    try
                    {
                        var dialog = new OpenFileDialog
                        {
                            Filter = "Excel files|*.xlsx",
                            Multiselect = false
                        };
                        var result = dialog.ShowDialog();
                        if (result != true)
                        {
                            MessageList.Add(new Models.Message("Cancelled."));
                            return;
                        }
                        using (var file = new FileStream(dialog.FileName, FileMode.Open))
                        using (var excel = new ExcelPackage(file))
                        {
                            foreach (var sheet in excel.Workbook.Worksheets)
                            {
                                try
                                {
                                    var rows = sheet.Cells[1, 1, sheet.Cells.Rows, 1]
                                        .Count(i => !string.IsNullOrWhiteSpace(i.GetValue<string>()));
                                    const int start = 2;
                                    foreach (var i in Enumerable.Range(start, rows - start + 1))
                                    {
                                        List.Add(new Models.Address
                                        {
                                            Type = sheet.Cells[i, 1].GetValue<Enums.AddressType>(),
                                            Province = sheet.Cells[i, 2].GetValue<string>(),
                                            City = sheet.Cells[i, 3].GetValue<string>(),
                                            Suburb = sheet.Cells[i, 4].GetValue<string>(),
                                            PostalCode = sheet.Cells[i, 5].GetValue<string>(),
                                            StreetName = sheet.Cells[i, 6].GetValue<string>(),
                                            StreetNumber = sheet.Cells[i, 7].GetValue<string>(),
                                            Extra = sheet.Cells[i, 8].GetValue<string>(),
                                        });
                                    }
                                    MessageList.Insert(0, new Models.Message("Addresses Imported from Excel."));
                                }
                                catch (Exception e)
                                {
                                    MessageList.Insert(0, new Models.Message(e));
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        MessageList.Insert(0, new Models.Message(e));
                    }
                });
            }
        }
        public ICommand ImportFromXmlCommand
        {
            get
            {
                return new RelayCommand(canExecute: () => List != null, execute: () =>
                {
                    try
                    {
                        var dialog = new OpenFileDialog
                        {
                            Filter = "Xml files|*.xml",
                            Multiselect = false
                        };
                        var result = dialog.ShowDialog();
                        if (result != true)
                        {
                            MessageList.Add(new Models.Message("Cancelled."));
                            return;
                        }
                        var xml = File.ReadAllText(dialog.FileName);
                        if (string.IsNullOrWhiteSpace(xml))
                        {
                            MessageList.Add(new Models.Message("The xml file is empty."));
                            return;
                        }
                        xml.ToObject<List<Models.Address>>().ForEach(i => List.Add(i));
                        MessageList.Insert(0, new Models.Message("Addresses Imported from Xml."));
                    }
                    catch (Exception e)
                    {
                        MessageList.Insert(0, new Models.Message(e));
                    }
                });
            }
        }

    }
}