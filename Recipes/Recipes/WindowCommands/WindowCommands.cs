using System.Windows.Input;

namespace Recipes.WindowCommands
{
	public static class WindowCommands
	{
		public static ICommand WindowCloseCommand { get; private set; }
		public static ICommand WindowMaximizeCommand { get; private set; }
		public static ICommand WindowMinimizeCommand { get; private set; }
		public static ICommand WindowsDragCommand { get; private set; }

		static WindowCommands()
		{
			WindowCloseCommand = new WindowCloseCommand();
			WindowMaximizeCommand = new WindowMaximizeCommand();
			WindowMinimizeCommand = new WindowMinimizeCommand();
			WindowsDragCommand = new WindowsDragCommand();
		}
	}
}