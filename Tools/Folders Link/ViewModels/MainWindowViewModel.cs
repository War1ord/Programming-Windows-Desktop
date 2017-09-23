using System;
using System.Windows.Forms;
using Business;
using System.Windows.Input;
using Action = Common.Helpers.Action;

namespace ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		#region Fields

		private string _returnText;
		private string _targetFolder;
		private string _linkFolder;
		private string _linkFolderSymbol;

		#endregion

		#region Properties

		public string LinkFolderFull
		{
			get { return IsLinkFolderValid ? LinkFolder + @"\" + LinkFolderSymbol : string.Empty; }
		}
		public string LinkFolderSymbol
		{
			get { return _linkFolderSymbol; }
			private set { _linkFolderSymbol = value; RaisePropertyChanged(() => LinkFolderSymbol); }
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
		public Environment.SpecialFolder DefaultFolder
		{
			get { return Environment.SpecialFolder.MyComputer; }
		}

		#endregion

		#region Commands

		public ICommand CreateCommand
		{
			get { return new Action(Create, () => IsValid); }
		}
		private void Create()
		{
			var manager = new CmdManager
			{
				CmdString = string.Format(Commands.CreateSymbolicLinkStringFormat, LinkFolderFull, TargetFolder)
			};
			manager.Action.Execute(null);
			ReturnText = manager.CmdReturn;
		}

		public ICommand BrowseLinkFolderCommand
		{
			get { return new Action(BrowseLinkFolder); }
		}
		private void BrowseLinkFolder()
		{
			var dialog = new FolderBrowserDialog {ShowNewFolderButton = true, RootFolder = DefaultFolder}; 
			DialogResult result = dialog.ShowDialog();
			if (result == DialogResult.Yes || result == DialogResult.OK)
			{
				LinkFolder = dialog.SelectedPath;
			}
		}

		public ICommand BrowseTargetFolderCommand
		{
			get { return new Action(BrowseTargetFolder); }
		}

		private void BrowseTargetFolder()
		{
			var dialog = new FolderBrowserDialog { ShowNewFolderButton = true, RootFolder = DefaultFolder };  
			DialogResult result = dialog.ShowDialog();
			if (result == DialogResult.Yes || result == DialogResult.OK)
			{
				TargetFolder = dialog.SelectedPath;
			}
		}

		#endregion
	}
}