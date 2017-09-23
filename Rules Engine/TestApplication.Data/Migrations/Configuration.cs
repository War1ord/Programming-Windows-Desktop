namespace TestApplication.Data.Migrations
{
	using System.Data.Entity.Migrations;

	internal sealed class Configuration : DbMigrationsConfiguration<TestApplication.Data.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
	        AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TestApplication.Data.DataContext context)
        {
        }
    }
}
