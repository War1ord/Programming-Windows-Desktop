using StockSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace StockSystem.Business
{
	public class StockItemsManager : BusinessBase<StockItem>
	{
		public List<StockItem> GetByType(StockType type, bool isDeleted = false)
		{
			try
			{
				var typeId = type.Id;
				return Db.Set<StockItem>()
					.Include(item => item.Type)
					.Where(i => i.IsDeleted == isDeleted && i.Type.Id == typeId)
					.ToList();
			}
			catch (Exception)
			{
				return new List<StockItem> { new StockItem { Result = ErrorUnknown } };
			}
		}

		public List<StockItem> GetByGroup(StockTypeGroup group, bool isDeleted = false)
		{
			try
			{
				var groupId = group.Id;
				return Db.Set<StockItem>()
					.Include(item => item.Type)
					.Include(item => item.Type.Group)
					.Where(i => i.IsDeleted == isDeleted && i.Type.Group.Id == groupId)
					.ToList();
			}
			catch (Exception)
			{
				return new List<StockItem> { new StockItem { Result = ErrorUnknown } };

			}
		}

		public List<StockItem> GetListByFilters(Area selectedArea, AreaType selectedAreaType, StockTypeGroup selectedStockTypeGroup, StockType selectedStockType, string nameFilter = null, bool isDeleted = false)
		{
			try
			{
				var queryable = Db.Set<StockItem>()
					.Include(i => i.Type)
					.Include(i => i.Type.Group)
					.Include(i => i.Area)
					.Include(i => i.Area.Type)
					.Where(i => i.IsDeleted == isDeleted);
				if (!string.IsNullOrWhiteSpace(nameFilter))
				{
					var lowerInvariant = nameFilter.ToLower();
					queryable = queryable.Where(i => i.Name.ToLower().Contains(lowerInvariant));
				}
				if (selectedArea != null)
				{
					queryable = queryable.Where(i => i.Area.Id == selectedArea.Id);
				}
				if (selectedAreaType != null)
				{
					queryable = queryable.Where(i => i.Area.Type.Id == selectedAreaType.Id);
				}
				if (selectedStockType != null)
				{
					queryable = queryable.Where(i => i.Type.Id == selectedStockType.Id);
				}
				if (selectedStockTypeGroup != null)
				{
					queryable = queryable.Where(i => i.Type.Group.Id == selectedStockTypeGroup.Id);
				}
				return queryable.ToList();
			}
			catch (Exception)
			{
				return new List<StockItem> { new StockItem { Result = ErrorUnknown } };
			}
		}

		public List<StockTypeGroup> GetStockTypeGroupsIncludingStockTypes(bool isDeleted = false)
		{
			try
			{
				return Db.Set<StockTypeGroup>()
					.Where(i => i.IsDeleted == isDeleted)
					.Include(i => i.StockTypes)
					.ToList();
			}
			catch (Exception)
			{
				return new List<StockTypeGroup> { new StockTypeGroup { Result = ErrorUnknown } };
			}
		}

		public List<AreaType> GetAreaTypesIncludingAreas(bool isDeleted = false)
		{
			try
			{
				return Db.Set<AreaType>()
					.Where(i => i.IsDeleted == isDeleted)
					.Include(i => i.Areas)
					.ToList();
			}
			catch (Exception)
			{
				return new List<AreaType> { new AreaType { Result = ErrorUnknown } };
			}
		}
	}
}