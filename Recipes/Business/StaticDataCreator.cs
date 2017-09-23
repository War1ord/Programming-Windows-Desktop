using Data;

namespace Business
{
	public static class StaticDataCreator
	{
		public static void Create()
		{
			using (var db = new DataContext())
			{
				UnitNames(db);
				Save(db);
			}
		}
		public static void Save(DataContext db) { db.SaveChanges(); }
		public static void UnitNames(DataContext db) { Data.Static.StaticDataCreator.UnitNames(db); }
	}
}
