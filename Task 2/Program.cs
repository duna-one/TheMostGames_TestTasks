using System;
using System.IO;

namespace Task_2
{
    internal class Program
    {
        private static string fileSrc="";   // File source
        private static string outputDir=""; // Output directory
        private static string fileName="";  // File name

        private static void Main(string[] args)
        {
            // File source input and check
            while (fileSrc == "")
            {
                Console.WriteLine("Enter file source:");
                fileSrc = Console.ReadLine();
                if (!File.Exists(fileSrc))
                {
                    Console.WriteLine("file not Exists");
                }
            }

            // Output directory input and check
            while (outputDir == "")
            {
                Console.WriteLine("Enter output directory:");
                outputDir = Console.ReadLine();
                if (!Directory.Exists(outputDir))
                {
                    // Ask for create if not exist
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

            // File name input
            while (fileName=="")
            {
                Console.WriteLine("Enter output file name");
                fileName = Console.ReadLine();
            }           

            // Main process inside constructor
            new Parcer(fileSrc, outputDir, fileName);


            // Notification of work completion and indication of the place where the output file was saved
            Console.WriteLine("The parser has finished working. Output file:");
            Console.WriteLine(outputDir + fileName);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
