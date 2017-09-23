namespace TimeTracking.Data.Migrations
{
	using System.Data.Entity.Migrations;

	internal sealed class Configuration : DbMigrationsConfiguration<TimeTracking.Data.DataContext>
    {
	    public Configuration()
	    {
	        AutomaticMigrationsEnabled = true;
	        AutomaticMigrationDataLossAllowed = true;
	    }

        protected override void Seed(DataContext context)
        {
            
        }
    }
}
