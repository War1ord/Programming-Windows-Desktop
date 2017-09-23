using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TimeTracking.Data.Migrations;

namespace TimeTracking.Data
{
    public class DataContext : DbContext
    {
        public static void Initialize()
        {
            using (var db = new DataContext())
            {
                new MigrateDatabaseToLatestVersion<DataContext, Configuration>().InitializeDatabase(db);
            }
        }

        public DataContext() : base("DefaultConnection"){}
        public DataContext(string nameOrConnectionString) : base(nameOrConnectionString){}

        public DbSet<Models.WorkItem> WorkItems { get; set; }
        public DbSet<Models.ClientOrProject> ClientsOrProjects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Remove Cascade Delete Convention
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

    }
}