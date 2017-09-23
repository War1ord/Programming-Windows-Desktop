using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Windows;
using Data.Filters;
using Data.Initializers;
using Models;

namespace Data
{
	public class DataContext : DbContext
	{
		#region Fields

		private const int CommandTimeout = 180;
		private const string DefaultConnectionName = "DefaultConnection";

		#endregion

		#region Properties

		public static bool Initialized { get; set; }
		public bool Online { get; set; }
		public static string ConnectionString
		{
			get
			{
				var settings = ConfigurationManager.ConnectionStrings[DefaultConnectionName];
				if (settings != null)
					return settings.ConnectionString;
				throw new Exception("Invalid connection setting to database. Please check configuration file.");
			}
		}
        protected string UserFrendlyError { get; set; }

		#endregion

		#region Constructors

		public DataContext()
		{
			try
			{
				Database.Connection.ConnectionString = ConnectionString;
				((IObjectContextAdapter) this).ObjectContext.CommandTimeout = CommandTimeout;
				Online = true;
			}
			catch (Exception e)
			{
				UserFrendlyError = string.Format("{0}\r\n{1}", 
					"The Database Connection had a error on construction.",
					e.Message);
				Online = false;
			}
		}


	    #endregion

		#region Initialize

		public static void Initialize()
		{
			try
			{
				using (var context = new DataContext())
				{
					var initializer = new DatabaseInitializer<DataContext>(new CreateDatabaseIfNotExists<DataContext>(),
					                                                       new IndexInitializer<DataContext>());
					initializer.InitializeDatabase(context);
					Initialized = true;
				}
			}
			catch (Exception e)
			{
				var message = string.Format("{0}\r\n{1}",
				                            "The Database Connection had a error on construction.",
				                            e.Message);
				MessageBox.Show(message);
				Initialized = false;
			}
		}

	    #endregion

		#region Overrides

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
			modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
			base.OnModelCreating(modelBuilder);
		}

		#endregion

		#region Entities

		public DbSet<Ingredient> Ingredients { get; set; }
		public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Category> Categories { get; set; }
		public DbSet<Unit> Units { get; set; }
		public DbSet<Error> Errors { get; set; }

		#endregion

		public static bool DropDataBase()
		{
			try
			{
				using (var context = new DataContext())
				{
					Initialized = false;
					return context.Database.CreateIfNotExists();
				}
			}
			catch (Exception e)
			{
				var message = string.Format("{0}\r\n{1}",
				                            "The Database Connection had a error on delete of database.",
				                            e.Message);
				MessageBox.Show(message);
				Initialized = false;
				return false;
			}
		}
		public static bool CreateDatabBaseIfNotExists()
		{
			try
			{
				using (var context = new DataContext())
				{
					Initialized = false;
					return context.Database.CreateIfNotExists();
				}
			}
			catch (Exception e)
			{
				var message = string.Format("{0}\r\n{1}",
											"The Database Connection had a error on delete of database.",
											e.Message);
				MessageBox.Show(message);
				Initialized = false;
				return false;
			}
		}
	}
}
