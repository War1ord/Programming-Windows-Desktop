using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Business_Management.Data
{
	public class DataContext : DbContext
	{
		public DataContext() : base(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString) { }
		public DataContext(string connectionString) : base(connectionString) { }

		public static DataContext Instance { get { return new DataContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString); } }

		public static void InitializeDatabase()
		{
			InitializeDatabase(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
		}
		public static void InitializeDatabase(string connectionString)
		{
			using (var db = new DataContext(connectionString))
			{
				new MigrateDatabaseToLatestVersion<DataContext, Migrations.Configuration>().InitializeDatabase(db);
			}
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			// Remove Cascade Delete Convention
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
			modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
			base.OnModelCreating(modelBuilder);
		}

		public DbSet<Models.Bank.Bank> Banks { get; set; }
		public DbSet<Models.Client.Address.ClientAddress> ClientAddresses { get; set; }
		public DbSet<Models.Client.Address.ClientAddressType> ClientAddressTypes { get; set; }
		public DbSet<Models.Client.Category.ClientCategory> ClientCategories { get; set; }
		public DbSet<Models.Client.Category.ClientCategoryLink> ClientCategoryLinks { get; set; }
		public DbSet<Models.Client.Contact.ClientContact> ClientContacts { get; set; }
		public DbSet<Models.Client.Contact.ClientContactType> ClientContactTypes { get; set; }
		public DbSet<Models.Client.Client> Clients { get; set; }
		public DbSet<Models.ClientTransaction.ClientTransaction> ClientTransactions { get; set; }
		public DbSet<Models.ClientTransaction.ClientTransactionType> ClientTransactionTypes { get; set; }
		public DbSet<Models.General.Title> Titles { get; set; }
		public DbSet<Models.Product.Product> Products { get; set; }
		public DbSet<Models.Product.ProductType> ProductTypes { get; set; }
		public DbSet<Models.Service.Service> Services { get; set; }
		public DbSet<Models.Service.ServiceInterval> ServiceIntervals { get; set; }
		public DbSet<Models.Service.ServiceType> ServiceTypes { get; set; }
		public DbSet<Models.User.User> Users { get; set; }
		public DbSet<Models.User.UserRole> UserRoles { get; set; }
		public DbSet<Models.User.UserRoleLink> UserRoleLinks { get; set; }
		public DbSet<Models.Financial.Invoice> Invoices { get; set; }
		public DbSet<Models.Financial.ProductSale> ProductSales { get; set; }
		public DbSet<Models.Financial.ServiceTimeLog> ServiceTimeLogs { get; set; }
		public DbSet<Models.General.Setting> Settings { get; set; }
	}
}
