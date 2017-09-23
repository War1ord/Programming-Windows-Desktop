using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using TimeTracking.Business.Extensions;
using TimeTracking.Business.Models;
using TimeTracking.Models;

namespace TimeTracking.Business.Base
{
	/// <summary>
	/// This is a CRUD entity manager
	/// </summary>
	public abstract class DataManagerBase : BusinessBase
	{
		protected DataManagerBase(Guid? userId) : base(userId) { }
		protected DataManagerBase(TimeTracking.Data.DataContext db, Guid? userId) : base(db, userId) { }

		public virtual Result CreateEntity<T>(T entity) where T : ModelBase
		{
			try
			{
				var result = GetValidationResult(entity);
				if (result.IsValid)
				{
					Add(entity);
					Db.SaveChanges();
					return Results.SuccessResult();
				}
				else
				{
					return Results.ErrorResult(result.ValidationErrors.ToHtmlValidMultiLineString());
				}
			}
			catch (Exception e)
			{
				/*LogException(e);*/
				return Results.ErrorResult();
			}
		}

		public virtual Result<T> GetEntity<T>(Guid id, bool isDeleted = false) where T : ModelBase, new()
		{
			try
			{
				var entity = Db.Set<T>().FirstOrDefault(i => i.IsDeleted == isDeleted && i.Guid == id);
				return Results.NoneResult(entity);
			}
			catch (Exception e)
			{
				/*LogException(e);*/
				return Results.ErrorResult<T>();
			}
		}
		public virtual Result<T> GetEntity<T>(Expression<Func<T, bool>> filter, bool isDeleted = false) where T : ModelBase, new()
		{
			try
			{
				var entity = Db.Set<T>().Where(i => i.IsDeleted == isDeleted).FirstOrDefault(filter);
				return Results.NoneResult(entity);
			}
			catch (Exception e)
			{
				/*LogException(e);*/
				return Results.ErrorResult<T>();
			}
		}
		public virtual Result<List<T>> GetEntityList<T>(bool isDeleted = false) where T : ModelBase
		{
			try
			{
				var list = Db.Set<T>().Where(i => i.IsDeleted == isDeleted).ToList();
				return Results.NoneResult(list);
			}
			catch (Exception e)
			{
				/*LogException(e);*/
				return Results.ErrorResult<List<T>>();
			}
		}
		public virtual Result<List<T>> GetEntityList<T>(Expression<Func<T, bool>> filter, bool isDeleted = false) where T : ModelBase
		{
			try
			{
				var list = Db.Set<T>().Where(i => i.IsDeleted == isDeleted).Where(filter).ToList();
				return Results.NoneResult(list);
			}
			catch (Exception e)
			{
				/*LogException(e);*/
				return Results.ErrorResult<List<T>>();
			}
		}
		public virtual Result<List<T>> GetEntityList<T, TProperty>(Expression<Func<T, bool>> filter, Expression<Func<T, TProperty>> sort, bool isSortDescending, bool isDeleted = false) where T : ModelBase
		{
			try
			{
				if (isSortDescending)
				{
					var queryable = Db.Set<T>().Where(i => i.IsDeleted == isDeleted).Where(filter).OrderByDescending(sort);
/*
					var filtertest = filter.ToString();
					var sqltest = queryable.ToString();
*/
					var list = queryable.ToList();
					return Results.NoneResult(list);
				}
				else
				{
					var queryable = Db.Set<T>().Where(i => i.IsDeleted == isDeleted).Where(filter).OrderBy(sort);
/*
					var filtertest = filter.ToString();
					var sqltest = queryable.ToString();
*/
					var list = queryable.ToList();
					return Results.NoneResult(list);
				}
			}
			catch (Exception e)
			{
				/*LogException(e);*/
				return Results.ErrorResult<List<T>>();
			}
		}
		public virtual Result<List<T>> GetEntityList<T, TProperty>(Expression<Func<T, TProperty>> sort, bool isSortDescending, bool isDeleted = false) where T : ModelBase
		{
			try
			{
				if (isSortDescending)
				{
					var list = Db.Set<T>().Where(i => i.IsDeleted == isDeleted).OrderByDescending(sort).ToList();
					return Results.NoneResult(list);
				}
				else
				{
					var list = Db.Set<T>().Where(i => i.IsDeleted == isDeleted).OrderBy(sort).ToList();
					return Results.NoneResult(list);
				}
			}
			catch (Exception e)
			{
				/*LogException(e);*/
				return Results.ErrorResult<List<T>>();
			}
		}
		public virtual Result<List<EntityDisplay>> GetEntityDisplayList<T>(Expression<Func<T, EntityDisplay>> selector, bool isDeleted = false) where T : ModelBase
		{
			try
			{
				var list = Db.Set<T>().Where(i => i.IsDeleted == isDeleted).Select(selector).ToList();
				return Results.NoneResult(list);
			}
			catch (Exception e)
			{
				/*LogException(e);*/
				return Results.ErrorResult(new List<EntityDisplay>());
			}
		}
		public virtual Result<List<EntityDisplay>> GetEntityDisplayList<T, TProperty>(Expression<Func<T, EntityDisplay>> selector, Expression<Func<T, TProperty>> sortExpression, bool isDeleted = false) where T : ModelBase
		{
			try
			{
				var list = Db.Set<T>().Where(i => i.IsDeleted == isDeleted).OrderBy(sortExpression).Select(selector).ToList();
				return Results.NoneResult(list);
			}
			catch (Exception e)
			{
				/*LogException(e);*/
				return Results.ErrorResult(new List<EntityDisplay>());
			}
		}
		public virtual Result<List<EntityDisplay>> GetEntityDisplayList<T, TProperty>(Expression<Func<T, EntityDisplay>> selector, Expression<Func<T, bool>> filter, Expression<Func<T, TProperty>> sortExpression, bool isDeleted = false) where T : ModelBase
		{
			try
			{
				var list = Db.Set<T>().Where(i => i.IsDeleted == isDeleted).Where(filter).OrderBy(sortExpression).Select(selector).ToList();
				return Results.NoneResult(list);
			}
			catch (Exception e)
			{
				/*LogException(e);*/
				return Results.ErrorResult(new List<EntityDisplay>());
			}
		}

		public virtual Result UpdateEntity<T>(T entity) where T : ModelBase
		{
			try
			{
				var result = GetValidationResult(entity);
				if (result.IsValid)
				{
					Update(entity);
					Db.SaveChanges();
					return Results.SuccessResult();
				}
				else
				{
					return Results.ErrorResult(result.ValidationErrors.ToHtmlValidMultiLineString());
				}
			}
			catch (Exception e)
			{
				/*LogException(e);*/
				return Results.ErrorResult();
			}
		}
		public virtual Result UpdateEntityProperty<TEntity, TProperty>(Guid entityID, TProperty propertyValue, Expression<Func<TEntity, TProperty>> propertyExpression, Expression<Func<TEntity, bool>> conditions = null) where TEntity : ModelBase
		{
			try
			{
				if ((conditions != null && Db.Set<TEntity>().Any(conditions))
					|| (conditions == null))
				{
					var entity = Db.Set<TEntity>().FirstOrDefault(i => i.Guid == entityID);
					if (entity != null)
					{
						Db.Entry(entity).Property(propertyExpression).CurrentValue = propertyValue;
						var validationResult = Db.Entry(entity).GetValidationResult();
						if (validationResult.IsValid)
						{
							Db.Entry(Db.Set<TEntity>().Attach(entity)).State = EntityState.Modified;
							Db.SaveChanges();
							return Results.SuccessResult();
						}
						else
						{
							var error = validationResult.ToMultilineString();
							return Results.ErrorResult(error);
						}
					}
					else
					{
						return Results.ErrorResult("Unexpected Error");
					}
				}
				else
				{
					return Results.ErrorResult("As per conditions update is not allowed.");
				}
			}
			catch (Exception e)
			{
				/*LogException(e);*/
				return Results.ErrorResult("Unexpected Error");
			}
		}
		public virtual Result UpdateEntityProperties<TEntity>(Guid entityID, Expression<Func<TEntity, TEntity>> updateExpression, Expression<Func<TEntity, bool>> conditions = null) where TEntity : ModelBase
		{
			try
			{
				if ((conditions != null && Db.Set<TEntity>().Any(conditions))
					|| (conditions == null))
				{
					Db.Set<TEntity>()
						.Where(i => i.Guid == entityID)
						.Update(updateExpression);
					return Results.SuccessResult();
				}
				else
				{
					return Results.ErrorResult("As per conditions update is not allowed.");
				}
			}
			catch (Exception e)
			{
				/*LogException(e);*/
				return Results.ErrorResult("Unexpected Error");
			}
		}
		public virtual Result UpdateEntityProperties<TEntity>(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, TEntity>> updateExpression, Expression<Func<TEntity, bool>> conditions = null) where TEntity : ModelBase
		{
			try
			{
				if ((conditions != null && Db.Set<TEntity>().Any(conditions))
					|| (conditions == null))
				{
					Db.Set<TEntity>()
						.Where(filterExpression)
						.Update(updateExpression);
					return Results.SuccessResult();
				}
				else
				{
					return Results.ErrorResult("As per conditions update is not allowed.");
				}
			}
			catch (Exception e)
			{
				/*LogException(e);*/
				return Results.ErrorResult("Unexpected Error");
			}
		}

		public virtual Result DeleteEntity<T>(Guid id) where T : ModelBase, new()
		{
			try
			{
				var entity = Db.Set<T>().FirstOrDefault(e => e.Guid == id);
				if (entity != null)
				{
					Delete<T>(id);
					//// for .Where().Update() there is no need to call SaveChanges() as the .Update() is not tracked by change tracking.
					//Db.SaveChanges();
					return Results.SuccessResult();
				}
				else
				{
					return Results.ErrorResult();
				}
			}
			catch (Exception e)
			{
				/*LogException(e);*/
				return Results.ErrorResult();
			}
		}

		protected DbEntityValidationResult GetValidationResult<T>(T entity) where T : ModelBase
		{
			return Db.Entry(Db.Set<T>().Attach(entity)).GetValidationResult();
		}

		public virtual void Add<T>(T entity) where T : ModelBase
		{
			Db.Entry(Db.Set<T>().Attach(entity)).State = EntityState.Added;
		}
		public virtual void Update<T>(T entity) where T : ModelBase
		{
			Db.Entry(Db.Set<T>().Attach(entity)).State = EntityState.Modified;
		}
		public virtual void Delete<T>(Guid id) where T : ModelBase, new()
		{
			//Db.Entry(Db.Set<T>().Attach(Db.Set<T>().FirstOrDefault(i => i.Id == id)))
			//	.Property(i => i.IsDeleted).CurrentValue = true;
			Db.Set<T>().Where(i => i.Guid == id).Update(e => new T {IsDeleted = true});
		}
	}
}