using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PalApp.Enums;
using PalApp.Helpers;
using System.Linq;

namespace PalApp.Providers
{
	public class PlainJSONProvider
	{
		public const string Separator = ",";
		public const string JsonDivider = ":";
		public void SaveDataToFile(string filename, Dictionary<string, SmellType> data)
		{
			FileHelpers.EnsureFileDeleted(filename);

			var dirname = Path.GetDirectoryName(filename);

			FileHelpers.EnsureDirExists(dirname);

			StringBuilder sb = new StringBuilder();
			sb.AppendLine("[");
			foreach (var person in data)
			{
				sb.Append($"{{\"{Constants.Atribute1}\":\"{person.Key}\"{Separator}\"{Constants.Atribute2}\":\"{(int)person.Value}\"}}");
				if (person.Key!=data.Keys.Last())
				{
					sb.AppendLine(",");
				}
			}
			sb.AppendLine("\n]");
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
				if (line == "[" || line == "]")
				{
					continue;
				}

				var temp = line;
				char[] charsToRemove = { '"','{', '}', ' '};
				foreach (char c in charsToRemove)
				{
					temp = temp.Replace(c.ToString(), String.Empty);
				}
				string part1 = Convert.ToString(temp.Split(Separator)[0]);
				string part2 = Convert.ToString(temp.Split(Separator)[1]);
				string savedName = part1.Split(JsonDivider)[1];
				string savedSmells = part2.Split(JsonDivider)[1];
				SmellType convertedSavedSmells = (SmellType)Convert.ToInt32(savedSmells);

				people.Add(savedName, convertedSavedSmells);
			}

			return people;
		}
	}
}
