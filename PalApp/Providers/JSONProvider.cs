using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using CsvHelper;
using PalApp.Enums;

namespace PalApp.Providers
{
	public class Records
	{
		public string Name { get; set; }
		public SmellType SmellLevel { get; set; }
	}

	public class JSONProvider
	{
		public void SaveDataToFile(string filename, Dictionary<string, SmellType> data)
		{
			using var writer = new StreamWriter(filename);
			using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
			var list = new List<Record>();
			foreach (var item in data)
			{
				list.Add(new Record() { Name = item.Key, SmellLevel = item.Value });
			}

			csv.WriteRecords(list);
		}
		public Dictionary<string, SmellType> LoadDataFromFile(string filename)
		{
			if (!File.Exists(filename))
				return null;

			using var reader = new StreamReader(filename);
			using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
			var records = csv.GetRecords<Record>().ToList();

			return records.ToDictionary(x => x.Name, x => x.SmellLevel);
		}
	}

}
