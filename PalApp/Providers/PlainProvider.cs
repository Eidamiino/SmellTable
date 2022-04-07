using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.PortableExecutable;
using PalApp.Enums;
using PalApp.Helpers;

namespace PalApp.Providers
{
	public class PlainProvider
	{
		public void SaveDataToFile(string filename, Dictionary<string, SmellType> data)
		{
			FileHelpers.EnsureFileDeleted(filename);
			var dirname = Path.GetDirectoryName(filename);

			FileHelpers.EnsureDirExists(dirname);

			foreach (var person in data)
			{
				File.AppendAllText(filename, $"{person.Key} - {(int)person.Value}\n");
			}
		}
		public Dictionary<string, SmellType> LoadDataFromFile(string filename)
		{
			if (!File.Exists(filename))
			{
				return null;
			}

			var people = new Dictionary<string, SmellType>();
			var lines = File.ReadAllLines(filename);
			foreach (var line in lines)
			{
				if (line.Length == 0)
				{
					return people;
				}

				string savedName = line.Split(Constants.Divider)[0];
				SmellType savedSmells = (SmellType)Enum.Parse((typeof(SmellType)), (line.Split(Constants.Divider)[1]));
				people.Add(savedName, savedSmells);
			}

			return people;
		}
	}
}