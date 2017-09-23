using Business_Management.Business.Models.FirstTimeSetup;
using Business_Management.Business.Setup;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Business_Management.UserInterface.Admin.ViewModels.Setup
{
	public class FirstTimeSetupViewModel : ViewModelBase
	{
		#region fields
		private FirstTimeSetupBanksViewModel banks;
		private FirstTimeSetupProductTypesViewModel productTypes;
		private FirstTimeSetupServiceIntervalsViewModel serviceIntervals;
		private FirstTimeSetupServiceTypesViewModel serviceTypes;
		private FirstTimeSetupUserRolesViewModel userRoles;
		#endregion

		public FirstTimeSetupBanksViewModel Banks
		{
			get
			{
				return banks ?? (banks = new FirstTimeSetupBanksViewModel());
			}
			set
			{
				banks = value;
			}
		}
		public FirstTimeSetupProductTypesViewModel ProductTypes
		{
			get
			{
				return productTypes ?? (productTypes = new FirstTimeSetupProductTypesViewModel());
			}
			set
			{
				productTypes = value;
			}
		}
		public FirstTimeSetupServiceIntervalsViewModel ServiceIntervals
		{
			get
			{
				return serviceIntervals ?? (serviceIntervals = new FirstTimeSetupServiceIntervalsViewModel());
			}
			set
			{
				serviceIntervals = value;
			}
		}
		public FirstTimeSetupServiceTypesViewModel ServiceTypes
		{
			get
			{
				return serviceTypes ?? (serviceTypes = new FirstTimeSetupServiceTypesViewModel());
			}
			set
			{
				serviceTypes = value;
			}
		}
		public FirstTimeSetupUserRolesViewModel UserRoles
		{
			get
			{
				return userRoles ?? (userRoles = new FirstTimeSetupUserRolesViewModel());
			}
			set
			{
				userRoles = value;
			}
		}

		public ICommand SaveCommand { get { return new RelayCommand<Window>(Save, window => { return CanSave(window); }); } }
		public ICommand CancelCommand { get { return new RelayCommand<Window>(window => { window.Close(); }, window => { return true; }); } }

		public bool CanSave(Window window)
		{
			return true;
		}
		private void Save(Window window)
		{
			using (var db = FirstTimeSetup.Instance)
			{
				var result = db.SaveFirstTimeSetup(new FirstTimeSetupData
				{
					Banks = Banks.List.Select(i => i).ToList(),
					ProductTypes = ProductTypes.List.Select(i => i).ToList(),
					ServiceIntervals = ServiceIntervals.List.Select(i => i).ToList(),
					ServiceTypes = ServiceTypes.List.Select(i => i).ToList(),
					UserRoles = UserRoles.List.Select(i => i).ToList(),
				});
				if (result.IsSuccessful) window.Close();
				else MessageBox.Show(window, result.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

	}
}
