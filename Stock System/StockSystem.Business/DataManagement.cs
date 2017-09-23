using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using StockSystem.Data;
using StockSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockSystem.Business
{
	public static class DataManagement
	{
		public static void Initialize()
		{
			DataContext.Initialize();
		}
	}

	public static class DataManagement<T> where T : Models.Base, new()
	{
		private static Result ErrorUnknown { get { return new Result("Unknown Error", ResultType.Error); } }
		private static Result Success { get { return new Result("Saved", ResultType.Success); } }

		public static T GetById(Guid id, bool isDeleted = false)
		{
			try
			{
				using (var db = new DataContext())
				{
					return db.Set<T>().FirstOrDefault(i => i.IsDeleted == isDeleted && i.Id == id);
				}
			}
			catch (Exception)
			{
				return new T {Result = ErrorUnknown};
			}
		}
		public static List<T> GetList(bool isDeleted = false)
		{
			try
			{
				using (var db = new DataContext())
				{
					return db.Set<T>().Where(i => i.IsDeleted == isDeleted).ToList();
				}
			}
			catch (Exception)
			{
				return new List<T> { new T { Result = ErrorUnknown } };
			}
		}
		public static Result AddItem(T item)
		{
			try
			{
				using (var db = new DataContext())
				{
					db.Entry(db.Set<T>().Attach(item)).State = EntityState.Added;
					db.SaveChanges();
					return Success;
				}
			}
			catch (Exception)
			{
				return ErrorUnknown;
			}
		}
		public static Result AddList(List<T> list)
		{
			try
			{
				using (var db = new DataContext())
				{
					list.ForEach(item => db.Entry(db.Set<T>().Attach(item)).State = EntityState.Added);
					db.SaveChanges();
					return Success;
				}
			}
			catch (Exception)
			{
				return ErrorUnknown;
			}
		}
		public static Result UpdateItem(T item)
		{
			try
			{
				using (var db = new DataContext())
				{
					db.Entry(db.Set<T>().Attach(item)).State = EntityState.Modified;
					db.SaveChanges();
					return Success;
				}
			}
			catch (Exception)
			{
				return ErrorUnknown;
			}
		}
		public static Result UpdateList(List<T> list)
		{
			try
			{
				using (var db = new DataContext())
				{
					list.ForEach(item => db.Entry(db.Set<T>().Attach(item)).State = EntityState.Modified);
					db.SaveChanges();
					return Success;
				}
			}
			catch (Exception)
			{
				return ErrorUnknown;
			}
		}
		public static Result DeleteItem(T item)
		{
			try
			{
				using (var db = new DataContext())
				{
					item.IsDeleted = true;
					db.Entry(db.Set<T>().Attach(item)).State = EntityState.Modified;
					db.SaveChanges();
					return Success;
				}
			}
			catch (Exception)
			{
				return ErrorUnknown;
			}
		}
		public static Result DeleteList(List<T> list)
		{
			try
			{
				using (var db = new DataContext())
				{
					list.ForEach(item =>
					{
						item.IsDeleted = true;
						db.Entry(db.Set<T>().Attach(item)).State = EntityState.Modified;
					});
					db.SaveChanges();
					return Success;
				}
			}
			catch (Exception)
			{
				return ErrorUnknown;
			}
		}

		public static DbEntityValidationResult GetResult(T item)
		{
			try
			{
				using (var db = new DataContext())
				{
					return db.Entry(item).GetValidationResult();
				}
			}
			catch (SqlException sqlException)
			{
				return null;
			}
			catch (Exception exception)
			{
				return null;
			}
		}

	}
}