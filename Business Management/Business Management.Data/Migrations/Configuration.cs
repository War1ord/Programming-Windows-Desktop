namespace Business_Management.Data.Migrations
{
	using Data;
	using System.Data.Entity.Migrations;

	internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(DataContext context)
		{
		}
	}
}
