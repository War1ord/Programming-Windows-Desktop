using System;

namespace Framework.Wpf.Mvvm.Core.Helpers
{
	/// <summary>
	/// Code to be copied to destination UI project
	/// </summary>
	public sealed class ApplicationSingleton
	{
		#region Singleton

		private ApplicationSingleton() { }
		private static volatile ApplicationSingleton instance;
		private static object SyncRoot = new Object();

		public static ApplicationSingleton Instance
		{
			get
			{
				if (instance == null)
				{
					lock (SyncRoot)
					{
						if (instance == null)
							instance = new ApplicationSingleton();
					}
				}

				return instance;
			}
		}

		#endregion

		#region Properties (!NB use Instance. to access backing field for properties, see test string property)

		//private string _test;
		//public string Test
		//{
		//	get { return Instance._test; }
		//	set { Instance._test = value; }
		//}

		#endregion

	}
}