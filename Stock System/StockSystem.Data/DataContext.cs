using System;
using System.Configuration;
using System.Data.Entity;
using StockSystem.Models;

namespace StockSystem.Data
{
	public class DataContext : DbContext
	{
		public static string ConnectionString
		{
			get
			{
				ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["DefaultConnection"];
				if (settings == null)
				{
					throw new Exception("Cannot resolve connection setting to the database.");
				}
				return settings.ConnectionString;
			}
		}

		public DataContext() : base(ConnectionString) { }
		public DataContext(string nameOrConnectionString) : base(nameOrConnectionString){}

		public DbSet<Area> Areas { get; set; }
		public DbSet<AreaType> AreaTypes { get; set; }
		public DbSet<StockItem> StockItems { get; set; }
		public DbSet<StockType> StockTypes { get; set; }
		public DbSet<StockTypeGroup> StockTypeGroups { get; set; }

		public static void Initialize()
		{
			using (var db = new Data.DataContext())
			{
				db.Database.CreateIfNotExists();
			}
		}
	}
}