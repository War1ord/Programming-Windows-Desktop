using Business_Management.Business.Extentions;
using Business_Management.Business.Models;
using Business_Management.Business.Models.FirstTimeSetup;
using Business_Management.Business.Setup;
using Business_Management.UserInterface.Admin.ViewModels.Setup;
using GalaSoft.MvvmLight;
using System;

namespace Business_Management.UserInterface.Admin.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		public MainWindowViewModel()
		{
			using (var db = ConnectionSetup.Instance)
			{
				if (db.CheckIfConnectionSetupIsNeeded())
				{
					var result = RunConnectionSetup(db);
					//var exists = db.CheckIfDatabaseExists();
				}
			}
			//var initialize = DataContext.InitializeDatabase();
			using (var db = FirstTimeSetup.Instance)
			{
				// check setting if first time setup has been run or needs to be run
				var firstTimeSetup = db.IsFirstTimeSetup();
				if (firstTimeSetup.Entity.HasValue && firstTimeSetup.Entity.Value)
				{
					var result = RunFirstTimeSetup(db);
				}
			}
		}

		private Result RunConnectionSetup(ConnectionSetup db)
		{
			try
			{
				var setup = new Windows.Setup.ConnectionSetup() { DataContext = new ConnectionSetupViewModel() };
				var result = setup.ShowDialog();
				if (result.HasValue && result == true)
				{
					var model = setup.DataContext as ConnectionSetupViewModel;
					if (model.IsSet())
					{
						return db.SaveConnectionSetup(new ConnectionSetupData
						{

						});
					}
					else
					{
						return Results.ErrorResult("First Time Setup View Model is invalid.");
					}
				}
				else
				{
					return Results.InvalidResult("User cancelled connection setup.");
				}
			}
			catch (Exception e)
			{
				return Results.ErrorResult(e);
			}
		}

		private Result RunFirstTimeSetup(FirstTimeSetup db)
		{
			try
			{
				var setup = new Windows.Setup.FirstTimeSetup() { DataContext = new FirstTimeSetupViewModel() };
				var result = setup.ShowDialog();
				if (result.HasValue && result == true)
				{
					var model = setup.DataContext as FirstTimeSetupViewModel;
					if (model.IsSet())
					{
						return db.SaveFirstTimeSetup(new FirstTimeSetupData
						{

						});
					}
					else
					{
						return Results.ErrorResult("First Time Setup View Model is invalid.");
					}
				}
				else
				{
					return Results.InvalidResult("User cancelled first time setup.");
				}
			}
			catch (Exception e)
			{
				return Results.ErrorResult(e);
			}
		}

	}
}
