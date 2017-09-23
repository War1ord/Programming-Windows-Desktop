using System;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using Enums;
using Models;

namespace Data
{
	public static class StaticDataCreator
	{
		public static void Save(DataContext db) { db.SaveChanges(); }

		public static void UnitNames(DataContext db)
		{
			foreach (UnitNames item in Enum.GetValues(typeof(UnitNames)))
			{
				string name = Enum.GetName(typeof(UnitNames), item);
				DescriptionAttribute descriptionAttribute = ((DescriptionAttribute[])item.GetType().GetField(item.ToString())
					.GetCustomAttributes(typeof(DescriptionAttribute), false))[0];
				string description = descriptionAttribute != null ? descriptionAttribute.Description : item.ToString();
				db.Units.AddOrUpdate(i => i.Key, new Unit { Key = item, Name = name, Group = description });
			}
		}
	}
}