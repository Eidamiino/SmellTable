using System.IO;

namespace PalApp.Helpers
{
	public class FileHelpers
	{
		public static void EnsureDirExists(string dirname)
		{
			if (!Directory.Exists(dirname))
				Directory.CreateDirectory(dirname);
		}

		public static void EnsureFileDeleted(string filename)
		{
			if (File.Exists(filename))
				File.Delete(filename);
		}
	}
}
