
namespace Business
{
    public static class DataContextDbCreate
    {
        public static bool Initialized { get; set; }

		public static void Initialize()
        {
            Data.DataContext.Initialize();
            Initialized = Data.DataContext.Initialized;
            //using (var db = new Data.DataContext())
            //{
            //    var exists = db.Database.Exists();
            //    if (exists)
            //    {
            //        Initialized = true;
            //    }
            //    else
            //    {
            //        Data.DataContext.Initialize();
            //        Initialized = Data.DataContext.Initialized;
            //        Thread.Sleep(new TimeSpan(0, 0, 0, 10));
            //    }
            //}
        }

		public static void DropDataBase()
		{
			bool deleted = Data.DataContext.DropDataBase();
		}
    }
}