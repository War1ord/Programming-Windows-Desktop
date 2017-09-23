using System;
using System.Windows.Forms;
using System.Windows.Input;
using Business;
using FoldersLinkRightClickMenu.Singletons;

namespace ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            LinkFolder = ApplicationSingleton.Instance.IsLinkFolderValid
                ? ApplicationSingleton.Instance.LinkFolder
                : string.Empty;
        }

        private string _returnText;
        private string _targetFolder;
        private string _linkFolder;
        private string _linkFolderSymbol;

        public string LinkFolderFull
        {
            get { return IsLinkFolderValid ? string.Format("\"{0}\"", LinkFolder + @"\" + LinkFolderSymbol) : string.Empty; }
        }
        public string LinkFolderSymbol
        {
            get { return _linkFolderSymbol; }
            private set { _linkFolderSymbol = value; RaisePropertyChanged(() => LinkFolderSymbol); }
        }
        public string LinkFolderFullWithQuotes
        {
            get { return string.Format("\"{0}\"", LinkFolderFull); }
        }
        public string LinkFolder
        {
            get { return _linkFolder; }
            set
            {
                _linkFolder = value;
                RaisePropertyChanged(() => LinkFolder);
            }
        }
        public bool IsLinkFolderValid
        {
            get { return !string.IsNullOrWhiteSpace(LinkFolder); }
        }
        public string TargetFolder
        {
            get { return _targetFolder; }
            set
            {
                _targetFolder = value;
                RaisePropertyChanged(() => TargetFolder);
            }
        }
        public bool IsTaregtFolderValid
        {
            get { return !string.IsNullOrWhiteSpace(TargetFolder); }
        }
        public string TargetFolderWithQuotes
        {
            get { return string.Format("\"{0}\"", _targetFolder); }
        }
        public bool IsValid
        {
            get { return IsLinkFolderValid && IsTaregtFolderValid; }
        }
        public string ReturnText
        {
            get { return _returnText; }
            set
            {
                _returnText = value;
                RaisePropertyChanged(() => ReturnText);
            }
        }
        public Environment.SpecialFolder RootFolder
        {
            get { return Environment.SpecialFolder.Desktop; }
        }

        public ICommand BrowseLinkFolderCommand
        {
            get { return new Common.Helpers.Action(BrowseLinkFolder); }
        }
        private void BrowseLinkFolder()
        {
            LinkFolder = GetPath();
        }

        public ICommand BrowseTargetFolderCommand
        {
            get { return new Common.Helpers.Action(BrowseTargetFolder); }
        }
        private void BrowseTargetFolder()
        {
            TargetFolder = GetPath();
        }

        public ICommand CreateCommand
        {
            get { return new Common.Helpers.Action(Create, () => IsValid); }
        }
        private void Create()
        {
            var manager = new CmdManager
            {
                CmdString = string.Format(Commands.CreateSymbolicLinkStringFormat, LinkFolderFull, TargetFolderWithQuotes)
            };
            manager.Action.Execute(null);
            ReturnText = manager.CmdReturn;
        }

        #region Helpers

        private string GetPath()
        {
            var dialog = new FolderBrowserDialog {ShowNewFolderButton = true, RootFolder = RootFolder};
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.Yes || result == DialogResult.OK)
            {
                return dialog.SelectedPath;
            }
            return string.Empty;
        }

        #endregion

    }
}