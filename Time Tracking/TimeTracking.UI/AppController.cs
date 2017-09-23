namespace TimeTracking.UI
{
	public sealed class AppController
	{
		private static AppController _instance;
		private static System.Object _object = new object();

		private AppController() {}

		public static AppController Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_object)
					{
						if (_instance == null)
						{
							_instance = new AppController();
						}
					}
				}
				return _instance;
			}
		}

		public System.Windows.Forms.NotifyIcon Notification;

	}
}