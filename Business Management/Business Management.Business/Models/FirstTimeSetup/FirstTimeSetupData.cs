using Business_Management.Models.Bank;
using Business_Management.Models.Product;
using Business_Management.Models.Service;
using Business_Management.Models.User;
using System.Collections.Generic;

namespace Business_Management.Business.Models.FirstTimeSetup
{
	public class FirstTimeSetupData : BusinessModelBase
	{
		private List<Bank> banks;
		private List<ProductType> productTypes;
		private List<ServiceInterval> serviceIntervals;
		private List<ServiceType> serviceTypes;
		private List<UserRole> userRoles;

		public List<Bank> Banks
		{
			get
			{
				return banks ?? (banks = new List<Bank>());
			}
			set
			{
				banks = value;
			}
		}
		public List<ProductType> ProductTypes
		{
			get
			{
				return productTypes ?? (productTypes = new List<ProductType>());
			}
			set
			{
				productTypes = value;
			}
		}
		public List<ServiceInterval> ServiceIntervals
		{
			get
			{
				return serviceIntervals ?? (serviceIntervals = new List<ServiceInterval>());
			}
			set
			{
				serviceIntervals = value;
			}
		}
		public List<ServiceType> ServiceTypes
		{
			get
			{
				return serviceTypes ?? (serviceTypes = new List<ServiceType>());
			}
			set
			{
				serviceTypes = value;
			}
		}
		public List<UserRole> UserRoles
		{
			get
			{
				return userRoles ?? (userRoles = new List<UserRole>());
			}
			set
			{
				userRoles = value;
			}
		}
	}
}
