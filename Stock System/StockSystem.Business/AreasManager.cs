using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using StockSystem.Models;

namespace StockSystem.Business
{
	public class AreasManager : BusinessBase<Area>
	{
		public List<Area> GetByType(Area type, bool isDeleted = false)
		{
			try
			{
				var typeId = type.Id;
				return Db.Set<Area>()
					.Include(item => item.Type)
					.Where(i => i.IsDeleted == isDeleted && i.Type.Id == typeId)
					.ToList();
			}
			catch (Exception)
			{
				return new List<Area> { new Area { Result = ErrorUnknown } };
			}
		}

	}
}