using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace Data
{
	/// <summary>
	/// Helper to add index
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class IndexInitializer<T> : IDatabaseInitializer<T> where T : DbContext
	{
		private const string ColumnNameTemplate = "{0}";
		private const string TableNameTemplate = "{1}";
		private const string IndexNameTemplate = "{2}";
		private const string UniqueTemplate = "{3}";

		/// <summary>
		/// The create index query template
		/// </summary>
		private const string CreateIndexQueryTemplate =
			"CREATE " + UniqueTemplate +
			" INDEX " + IndexNameTemplate +
			" ON " + TableNameTemplate +
			" (" + ColumnNameTemplate + ")";

		public void InitializeDatabase(T context)
		{
			const BindingFlags publicInstance = BindingFlags.Public | BindingFlags.Instance;
			foreach ( var dataSetProperty in typeof(T).GetProperties(publicInstance).Where(p => p.PropertyType.Name == typeof(DbSet<>).Name))
			{
				Type entityType = dataSetProperty.PropertyType.GetGenericArguments().Single();
				var tableAttributes = (TableAttribute[])entityType.GetCustomAttributes(typeof(TableAttribute), false);
				foreach (PropertyInfo property in entityType.GetProperties(publicInstance))
				{
					var indexAttributes = (IndexAttribute[])property.GetCustomAttributes(typeof(IndexAttribute), false);
					var notMappedAttributes = (NotMappedAttribute[])property.GetCustomAttributes(typeof(NotMappedAttribute), false);
					if (indexAttributes.Length > 0 && notMappedAttributes.Length == 0)
					{
						var columnAttributes = (ColumnAttribute[])property.GetCustomAttributes(typeof(ColumnAttribute), false);
						foreach (IndexAttribute indexAttribute in indexAttributes)
						{
							string query = string.Format(CreateIndexQueryTemplate,
								indexAttribute.Name
								,tableAttributes.Length != 0 ? tableAttributes[0].Name : dataSetProperty.Name
								,columnAttributes.Length != 0 ? columnAttributes[0].Name : property.Name
								,indexAttribute.IsUnique ? "UNIQUE" : string.Empty);
							context.Database.CreateIfNotExists();
							context.Database.ExecuteSqlCommand(query);
						}
					}
				}
			}
		}
	}
}