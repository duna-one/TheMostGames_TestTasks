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

            // File source input and check
            while (inputFilePath == "")
            {
                Console.WriteLine("Enter file source:");
                inputFilePath = Console.ReadLine();
                if (!File.Exists(inputFilePath))
                {
                    Console.WriteLine("file not Exists");
                }
            }

            // Sorting the strings into English and Russian
            SortStrings(inputFilePath);

            // Getting the result
            GetResults();

            // Waiting until all secondary threads are completed
            while (RunningThreads != 0);

            // Displaying the result on the screen
            foreach (Result result in Results)
            {
                Console.WriteLine(result.ToString());
            }
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

        }

        /// <summary>
        /// Reads from a file and sorts the rows into 2 lists
        /// </summary>
        /// <param name="inputFilePath">Input file path</param>
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

        /// <summary>
        /// Checks whether the string is English
        /// </summary>
        /// <param name="input">The string to check</param>
        /// <returns>True if the string is English and a false if it is Russian</returns>
        private static bool isEnglishString(string input)
        {
            return new Regex("[a-z]|[A-Z]", RegexOptions.Compiled).IsMatch(input[0]+"");
        }

        /// <summary>
        /// Creates the result of the program
        /// </summary>
        private static void GetResults()
        {
            while (RussianStrings.Count != 0)
            {
                for (int i = 0; i < RussianStrings.Count; i++)
                {
                    // Сheck whether the index calculation operation is completed
                    if (RussianStrings[i].ThreadRunning)
                    {
                        continue;
                    }
                    else
                    {
                        // Adding the result to the list
                        Results.Add(new Result(RussianStrings[i].Str));
                        // We give the search for the corresponding rows to a secondary thread
                        new Thread(() => FoundRight(new StringData(RussianStrings[i]), i)).Start();
                        RussianStrings.RemoveAt(i);
                        i--;
                        // Increasing the number of running secondary threads
                        RunningThreads++;
                    }
                }
            }
        }

        /// <summary>
        /// Search for matches by the Petrenko index
        /// </summary>
        /// <param name="string">String</param>
        /// <param name="index">Index in the list of results</param>
        private static void FoundRight(StringData @string, int index)
        {
            foreach(StringData data in EnglishStrings)
            {
                // A simple equality check
                if (@string.PetrencoIndex == data.PetrencoIndex)
                {
                    Results[index].AddEnglishString(data.Str);
                }
            }

            // Reducing the number of running threads
            RunningThreads--;
        }
    }
}
