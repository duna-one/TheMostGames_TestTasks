using System.Collections.Generic;

namespace Task_3
{
    internal class Result
    {
        /// <summary>
        /// A class that describes the data for output of the result
        /// </summary>
        /// 
        public string RussianString { get; private set; }
        public List<string> EnglishStrings { get; private set; }

        public Result(string rusString)
        {
            RussianString = rusString;
            EnglishStrings = new List<string>();
        }

        /// <summary>
        /// Adds an English string
        /// </summary>
        /// <param name="engString">English string</param>
        public void AddEnglishString(string engString)
        {
            EnglishStrings.Add(engString);
        }

        public override string ToString()
        {
            string output = "Russian string:\n" + RussianString + "\n";
            foreach (string str in EnglishStrings)
            {
                output += str + "\n";
            }
            return output;
        }
    }
}
