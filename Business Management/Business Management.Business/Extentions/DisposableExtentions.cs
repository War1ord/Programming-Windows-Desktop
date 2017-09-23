using System;

namespace Business_Management.Business.Extentions
{
	public static class DisposableExtentions
	{
		public static void SafeDispose(this IDisposable obj)
		{
			if (obj.IsSet()) obj.Dispose();
			obj = null;
		}
	}
}
