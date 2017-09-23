using Business_Management.Business.Config;
using Business_Management.Business.Extentions;
using Business_Management.Business.Models;
using Business_Management.Business.Models.FirstTimeSetup;
using Business_Management.Models.User;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Business_Management.Business.Setup
{
	public class FirstTimeSetup : Base.BusinessBase
	{
		public static FirstTimeSetup Instance { get { return new FirstTimeSetup(); } }

		public Result<bool?> IsFirstTimeSetup()
		{
			var value = ConfigurationManager.AppSettings.Get("FirstTimeSetupCheck");
			return value.IsSet()
				? Results.SuccessResult<bool?>(value.AsBool())
				: Results.ErrorResult<bool?>();
		}

		public Result SetFirstTimeSetupCheck(bool value)
		{
			return EditConfigFile.UpdateAppSettings(GetType().Assembly.Location
				, new KeyValuePair<string, string>("FirstTimeSetupCheck", value.ToString()));
		}

		public Result SaveFirstTimeSetup(FirstTimeSetupData data)
		{
			if (data.IsNotSet()) throw new ArgumentNullException(nameof(data));
			// save FirstTimeSetupData
			var sysadmin = CreateIfNotExistsSysAdmin();
			foreach (var item in data.Banks) item.CreatedBy = sysadmin;
			foreach (var item in data.ProductTypes) item.CreatedBy = sysadmin;
			foreach (var item in data.ServiceIntervals) item.CreatedBy = sysadmin;
			foreach (var item in data.ServiceTypes) item.CreatedBy = sysadmin;
			foreach (var item in data.UserRoles) item.CreatedBy = sysadmin;
			Db.Banks.AddRange(data.Banks);
			Db.ProductTypes.AddRange(data.ProductTypes);
			Db.ServiceIntervals.AddRange(data.ServiceIntervals);
			Db.ServiceTypes.AddRange(data.ServiceTypes);
			Db.UserRoles.AddRange(data.UserRoles);

			int result;
			try
			{
				result = Db.SaveChanges();
			}
			catch (Exception e)
			{
				return e.ToResult();
			}

			var result_check = SetFirstTimeSetupCheck(false);
			return result > 0 && result_check.IsSuccessful
				? Results.SuccessResult("Saved first time setup data.")
				: result_check.IsError
					? result_check
					: Results.ErrorResult("Some problem with saving of the first time setup data, there is maybe no data to save or other error.");
		}

		private User CreateIfNotExistsSysAdmin()
		{
			var sysAdmin = Db.Users.FirstOrDefault(i => i.Username.Equals(User.sysadmin.username));
			if (sysAdmin.IsSet())
			{
				return sysAdmin;
			}
			else
			{
				var admin = new User
				{
					Username = User.sysadmin.username,
					Password = User.sysadmin.password,
					FirstName = User.sysadmin.username,
				};
				admin.CreatedBy = admin;
				Db.Users.Add(admin);
				Db.SaveChanges();
				return admin;
			}
		}

	}
}
