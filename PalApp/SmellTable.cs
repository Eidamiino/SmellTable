using System;
using System.Collections.Generic;
using System.IO;
using PalApp.Enums;
using PalApp.Providers;

namespace PalApp
{
    internal class Program
    {
	    static Dictionary<string, SmellType> nameDictionary = new Dictionary<string, SmellType>();

        static void Main(string[] args)
        {
            LoadFromFile();
            while (true) {
                var sum = 0;
                var name = GetName();

                if (Exit(name)) Environment.Exit(0);
                if (MaybeSave(name))
                {
	                Save();
                  continue;
                }
                if(IsDuplicate(name)) continue;

                sum = CalculateSum(name, sum);
                var avg = CalculateAvg(sum, name);

                SmellType smellLevel= CalculateSmellLevel(avg, name); ;
                nameDictionary.Add(name, smellLevel);

                PrintTable();
            }
        }

        private static bool IsDuplicate(string name)
        {
            if (nameDictionary.ContainsKey(name)) {
                Console.WriteLine("\nThis name has already been entered");
                return true;
            }
            return false;
        }

        private static bool Exit(string name)
        {
	        if (name.Equals(Constants.ExitKeyword, StringComparison.InvariantCultureIgnoreCase))
	        {
		        return true;
	        }
	        return false;
        }

        #region Helpers

        private static string GetName()
        {
	        string name;
	        Console.WriteLine("Input a name:");
	        name = Console.ReadLine();
	        return name;
        }
        private static int CalculateSum(string name, int sum)
        {
	        for (int i = 0; i < name.Length; i++)
	        {
		        if (name[i] == ' ')
			        continue;
		        sum += name[i];
	        }
	        return sum;
        }
        private static double CalculateAvg(int sum, string name)
        {
	        double avg;
	        avg = (double)sum / name.Length;
	        avg = Math.Round(avg);
	        return avg;
        }
        private static SmellType CalculateSmellLevel(double avg, string name)
        {
	        if (avg % 7 == 0 || name.ToUpper() == "HONZA RADA" || name.ToUpper() == "JAN RADA")
		        return SmellType.HobosFeet;
            
	        if (avg % 5 == 0)
		        return SmellType.HorseAss;
            
	        if (avg % 3 == 0)
		        return SmellType.OnionRinger;
            
	        return SmellType.Unsmeller;
        }

    #endregion

				#region FileManipulation

    private static bool MaybeSave(string name)
    {
	    if (name == Constants.SaveKeyword)
		    return true;
	    return false;
    }
    private static void Save()
    {
			var provider = new PlainJSONProvider();

			provider.SaveDataToFile(Constants.PathToFile, nameDictionary);

			Console.WriteLine("Table updated successfully");
    }
    private static void LoadFromFile()
		{
			var provider = new PlainJSONProvider();
			var data = provider.LoadDataFromFile(Constants.PathToFile);

			if (data != null)
				nameDictionary = data;

			PrintTable();
		}
		private static void PrintTable()
    {
	    Console.WriteLine($"\nThe Smell Table: \nExit:{Constants.ExitKeyword}\tSave:{Constants.SaveKeyword}\n*************************");
	    foreach (KeyValuePair<string, SmellType> ele in nameDictionary)
	    {
		    Console.Write($"{ele.Key}\t");
		    switch (ele.Value)
		    {
			    case SmellType.HobosFeet:
				    Console.ForegroundColor = ConsoleColor.Red;
				    Console.WriteLine(Constants.HobosFeet);
				    break;
			    case SmellType.HorseAss:
				    Console.ForegroundColor = ConsoleColor.DarkYellow;
				    Console.WriteLine(Constants.HorseAss);
				    break;
			    case SmellType.OnionRinger:
				    Console.ForegroundColor = ConsoleColor.Yellow;
				    Console.WriteLine(Constants.OnionRinger);
				    break;
			    case SmellType.Unsmeller:
				    Console.ForegroundColor = ConsoleColor.Green;
				    Console.WriteLine(Constants.Unsmeller);
				    break;
		    }
		    Console.ForegroundColor = ConsoleColor.Gray;
	    }
	    Console.WriteLine("\n");
    }

    #endregion
    }
}
