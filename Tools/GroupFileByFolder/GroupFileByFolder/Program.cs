using GroupFileByFolder.Properties;
using System.IO;
using System.Linq;

namespace GroupFileByFolder
{
	class Program
	{
		static void Main(string[] args)
		{
			foreach (var file in Directory.GetFiles(Settings.Default.From, "*.*", SearchOption.AllDirectories))
			{
				var fi = new FileInfo(file);

				var fileNameSplit = fi.Name.Split('_');
				var folderGroup = fileNameSplit.First();
				var subFolderGroup = fi.Directory.Name;

				var destFilePath = Path.Combine(Settings.Default.To, folderGroup, subFolderGroup);
				if (!Directory.Exists(destFilePath)) Directory.CreateDirectory(destFilePath);
				var destFileName = Path.Combine(destFilePath, fi.Name);
				//fi.CopyTo(destFileName);
				File.Copy(file, destFileName);
			}
		}
	}
}
