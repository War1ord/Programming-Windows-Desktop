using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace TestApplication.Data
{
	public class DataContext : DbContext
	{
		static DataContext(){}
		public DataContext() : base("DefaultConnection"){}
		public static void Initialize()
		{
			using (var db = new DataContext())
			{
				new MigrateDatabaseToLatestVersion<DataContext, Migrations.Configuration>().InitializeDatabase(db);
			}
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			ConfigureModels<RulesEngine.Models.ModelBase>(modelBuilder);
			ConfigureModels<Models.ModelBase>(modelBuilder);
			base.OnModelCreating(modelBuilder);
		}

		/// <summary>
		/// Configures the models.
		/// </summary>
		/// <typeparam name="T">Configures models based on type</typeparam>
		/// <param name="modelBuilder">The model builder.</param>
		public void ConfigureModels<T>(DbModelBuilder modelBuilder)
		{
			Assembly assembly = Assembly.GetAssembly(typeof(T));
			var entityMethod = typeof(DbModelBuilder).GetMethod("Entity");
			var results = assembly.GetTypes().Where(i => i.BaseType == typeof(T)).OrderBy(i => i.Name)
				.Select(i => new { ReturnValue = entityMethod.MakeGenericMethod(i).Invoke(modelBuilder, new object[] { }), i.Namespace, i.Name, i.FullName, i.AssemblyQualifiedName })
				.ToList();
		}
	}
}