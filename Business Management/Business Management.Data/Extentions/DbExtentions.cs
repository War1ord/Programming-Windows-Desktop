using Business_Management.Models.Base;
using System.Data.Entity;
using System.Linq;

namespace Business_Management.Data.Extentions
{
	public static class DbExtentions
	{
		public static T Save<T>(this DataContext dataContext, T entity) where T : ModelBase
		{
			if (dataContext.Set<T>().Count(item => item.Id == entity.Id) == 0)
			{
				dataContext.Entry<T>(entity).State = EntityState.Added;
			}
			else
			{
				dataContext.Entry<T>(entity).State = EntityState.Modified;
			}
			return entity;
		}
		public static T Add<T>(this DataContext dataContext, T entity) where T : ModelBase
		{
			if (dataContext.Set<T>().Count(item => item.Id == entity.Id) == 0)
			{
				dataContext.Entry<T>(entity).State = EntityState.Added;
			}
			return entity;
		}
	}
}
