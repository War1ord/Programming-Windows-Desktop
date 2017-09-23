using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using StockSystem.Models;

namespace StockSystem.Business
{
	public class BusinessBase<T> : Base where T : Models.Base, new()
	{
		public T GetById(Guid id, bool isDeleted = false)
		{
			try
			{
				return Db.Set<T>().FirstOrDefault(i => i.IsDeleted == isDeleted && i.Id == id);
			}
			catch (Exception)
			{
				return new T {Result = ErrorUnknown};
			}
		}
		public List<T> GetList(bool isDeleted = false)
		{
			try
			{
				return Db.Set<T>().Where(i => i.IsDeleted == isDeleted).ToList();
			}
			catch (Exception)
			{
				return new List<T> { new T() { Result = ErrorUnknown } };
			}
		}
		public Result Add(T item)
		{
			try
			{
				Db.Entry(Db.Set<T>().Attach(item)).State = EntityState.Added;
				Db.SaveChanges();
				return Success;
			}
			catch (Exception)
			{
				return ErrorUnknown;
			}
		}
		public Result Add(List<T> list)
		{
			try
			{
				list.ForEach(item => Db.Entry<T>(Db.Set<T>().Attach(item)).State = EntityState.Added);
				Db.SaveChanges();
				return Success;
			}
			catch (Exception)
			{
				return ErrorUnknown;
			}
		}
		public Result Update(T item)
		{
			try
			{
				Db.Entry(Db.Set<T>().Attach(item)).State = EntityState.Modified;
				Db.SaveChanges();
				return Success;
			}
			catch (Exception)
			{
				return ErrorUnknown;
			}
		}
		public Result Update(List<T> list)
		{
			try
			{
				list.ForEach(item => Db.Entry(Db.Set<T>().Attach(item)).State = EntityState.Modified);
				Db.SaveChanges();
				return Success;
			}
			catch (Exception)
			{
				return ErrorUnknown;
			}
		}
		public Result Delete(T item)
		{
			try
			{
				item.IsDeleted = true;
				Db.Entry(Db.Set<T>().Attach(item)).State = EntityState.Modified;
				Db.SaveChanges();
				return Success;
			}
			catch (Exception)
			{
				return ErrorUnknown;
			}
		}
		public Result Delete(List<T> list)
		{
			try
			{
				list.ForEach(item =>
				{
					item.IsDeleted = true;
					Db.Entry(Db.Set<T>().Attach(item)).State = EntityState.Modified;
				});
				Db.SaveChanges();
				return Success;
			}
			catch (Exception)
			{
				return ErrorUnknown;
			}
		}
	}
}