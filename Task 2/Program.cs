using System;
using System.IO;

namespace Task_2
{
    internal class Program
    {
        private static string fileSrc="";
        private static string outputDir="";
        private static string fileName="";

        private static void Main(string[] args)
        {
            while (fileSrc == "")
            {
                Console.WriteLine("Enter file source:");
                fileSrc = Console.ReadLine();
                if (!File.Exists(fileSrc))
                {
                    Console.WriteLine("file not Exists");
                }
            }
            while(outputDir == "")
            {
                Console.WriteLine("Enter output directory:");
                outputDir = Console.ReadLine();
                if (!Directory.Exists(outputDir))
                {
                    Console.WriteLine("Directory not exists! Create? y/n");
                    switch (Console.ReadLine())
                    {
                        case "y":
                            Directory.CreateDirectory(outputDir);
                            break;
                        case "n":
                            outputDir = "";
                            break;
                        default:
                            Console.WriteLine("Incorrect input");
                            outputDir = "";
                            break;
                    }
                }
            }

            while (fileName=="")
            {
                Console.WriteLine("Enter output file name");
                fileName = Console.ReadLine();
            }           

            new Parcer(fileSrc, outputDir, fileName);

            Console.WriteLine("The parser has finished working. Output file:");
            Console.WriteLine(outputDir + fileName);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
