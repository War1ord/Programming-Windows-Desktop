namespace Data.Migrations
{
    using Static;
    using System.Data.Entity.Migrations;

	internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
		}

		protected override void Seed(DataContext db)
		{
			StaticDataCreator.UnitNames(db);
			db.SaveChanges();
		}
	}
}
