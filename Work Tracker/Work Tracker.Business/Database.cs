using Work_Tracker.Data;

namespace Work_Tracker.Business
{
    public static class Database
    {
        public static void Init()
        {
            try
            {
                WorkTrackerDataContext.Init();
            }
            catch
            {

            }
        }
    }
}