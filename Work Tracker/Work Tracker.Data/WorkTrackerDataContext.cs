using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Work_Tracker.Business.Models;

namespace Work_Tracker.Data
{
    public class WorkTrackerDataContext : DbContext
    {
        private static string _connectionStringName;

        public static string ConnectionStringName
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionStringName))
                {
                    var appSetting = ConfigurationManager.AppSettings.Get("ConnectionSettingsName");
                    return _connectionStringName = string.IsNullOrEmpty(appSetting) ? "DefaultConnection" : appSetting;
                }
                else
                {
                    return _connectionStringName;
                }
            }
        }

        public static void Init()
        {
            using (var db = new WorkTrackerDataContext())
            {
                db.Database.CreateIfNotExists();
            }
        }
        
        public WorkTrackerDataContext() : base(ConnectionStringName) { }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            // Remove Cascade Delete Convention
            builder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            builder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            base.OnModelCreating(builder);
        }

        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Check> Checks { get; set; }
        public DbSet<Extra> Extras { get; set; }

    }
}