using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PalApp.Enums;
using PalApp.Helpers;

namespace PalApp.Providers
{
	public class PlainCSVProvider
	{
		public const string Separator = ";";
		public void SaveDataToFile(string filename, Dictionary<string, SmellType> data)
		{
			FileHelpers.EnsureFileDeleted(filename);

			var dirName = Path.GetDirectoryName(filename);

			FileHelpers.EnsureDirExists(dirName);

			StringBuilder sb = new StringBuilder();
			sb.AppendLine($"\"name\"{Separator}\"smellLevel\"");
			foreach (var person in data)
			{
				sb.AppendLine($"\"{person.Key}\"{Separator}\"{(int)person.Value}\"");
			}

			File.AppendAllText(filename, sb.ToString());
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

				var parts = line.Split(Separator);

				string savedName = parts[0].Trim('"');
				SmellType savedSmells = (SmellType)Convert.ToInt32(parts[1].Trim('"'));
				people.Add(savedName, savedSmells);
			}

			return people;
		}
	}
}
