using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace Task_3
{   
    class Program
    {
        static List<StringData> RussianStrings = new List<StringData>();
        static List<StringData> EnglishStrings = new List<StringData>();
        static List<Result> Results = new List<Result>();
        static uint RunningThreads = 0;

        static void Main(string[] args)
        {
            string inputFilePath = "";

            while (inputFilePath == "")
            {
                Console.WriteLine("Enter file source:");
                inputFilePath = Console.ReadLine();
                if (!File.Exists(inputFilePath))
                {
                    Console.WriteLine("file not Exists");
                }
            }

            SortStrings(inputFilePath);            
            GetResults();

            while (RunningThreads != 0);
            foreach(Result result in Results)
            {
                Console.WriteLine(result.ToString());
            }
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

        }

        private static void SortStrings(string inputFilePath)
        {
            StreamReader streamReader = new StreamReader(inputFilePath);
            string buffer = "";

            while (!streamReader.EndOfStream)
            {
                buffer = streamReader.ReadLine();
                if (isEnglishString(buffer))
                {
                    EnglishStrings.Add(new StringData(buffer));
                }
                else
                {
                    RussianStrings.Add(new StringData(buffer));
                }
            }
        }

        private static bool isEnglishString(string input)
        {
            return new Regex("[a-z]|[A-Z]", RegexOptions.Compiled).IsMatch(input[0]+"");
        }

        private static void GetResults()
        {
            while (RussianStrings.Count != 0)
            {
                for (int i = 0; i < RussianStrings.Count; i++)
                {
                    if (RussianStrings[i].ThreadRunning)
                    {
                        continue;
                    }
                    else
                    {
                        Results.Add(new Result(RussianStrings[i].Str));
                        new Thread(() => FoundRight(new StringData(RussianStrings[i]), i)).Start();
                        RussianStrings.RemoveAt(i);
                        i--;
                        RunningThreads++;
                    }
                }
            }
        }

        private static void FoundRight(StringData @string, int index)
        {
            foreach(StringData data in EnglishStrings)
            {
                if(@string.PetrencoIndex == data.PetrencoIndex)
                {
                    Results[index].AddEnglishString(data.Str);
                }
            }
            RunningThreads--;
        }
    }
}
