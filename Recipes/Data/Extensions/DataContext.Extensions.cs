using Models;
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Data.Extensions
{
	public static class DataContextExtensions
	{
		/// <summary>
		/// Saves the specified data context.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="db">The data context.</param>
		/// <param name="entity">The entity.</param>
		public static void Save<T>(this DataContext db, T entity) where T : Base
		{
			db.Entry<T>(entity).State = 
				db.Set<T>().Any(item => item.Id == entity.Id)
				? EntityState.Modified
				: EntityState.Added;
		}

		/// <summary>
		/// Helper for the Include function of entity framework to include the full path of the property.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="source">The source.</param>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public static IQueryable<T> IncludeFullPath<T, TProperty>(this IQueryable<T> source, Expression<Func<T, TProperty>> path) where T : class
		{
			string fullString = path.ToString();
			int index = fullString.IndexOf(".", StringComparison.Ordinal);
			var pathString = fullString.Substring(index >= 0 ? index + 1 : 0);
			return source.Include(pathString);
		}

		/// <summary>
		/// Helper for the Include function of entity framework to include the full path of the properties, using a params array argument.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="source">The source.</param>
		/// <param name="paths">The paths.</param>
		/// <returns></returns>
		public static IQueryable<T> IncludeFullPath<T, TProperty>(this IQueryable<T> source, params Expression<Func<T, TProperty>>[] paths) where T : class
		{
			foreach (var path in paths)
			{
				string fullString = path.ToString();
				int index = fullString.IndexOf(".", StringComparison.Ordinal);
				var pathString = fullString.Substring(index >= 0 ? index + 1 : 0);
				source = source.Include(pathString);
			}
			return source;
		}

		/// <summary>
		/// Includes the specified queryable.
		/// </summary>
		/// <param name="queryable">The queryable.</param>
		/// <param name="includes">The includes.</param>
		/// <returns></returns>
		public static IQueryable Include(this IQueryable queryable, params string[] includes)
		{
			var includemethod = queryable.GetType().GetMethod("Include", new Type[] { typeof(string) });
			if (includemethod != null)
				foreach (var include in includes)
					queryable = includemethod.Invoke(queryable, new object[] { include }) as IQueryable;
			return queryable;
		}

		/// <summary>
		/// Includes the specified queryable.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="queryable">The queryable.</param>
		/// <param name="includes">The includes.</param>
		/// <returns></returns>
		public static IQueryable<T> Include<T>(this IQueryable<T> queryable, params string[] includes)
		{
			var includemethod = queryable.GetType().GetMethod("Include", new Type[] { typeof(string) });
			if (includemethod != null)
				foreach (var include in includes)
					queryable = includemethod.Invoke(queryable, new object[] { include }) as IQueryable<T>;
			return queryable;
		}
	}
}
