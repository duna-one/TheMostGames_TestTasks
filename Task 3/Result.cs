using System.Collections.Generic;

namespace Task_3
{
    internal class Result
    {
        public string RussianString { get; private set; }
        public List<string> EnglishStrings { get; private set; }

        public Result(string rusString)
        {
            RussianString = rusString;
            EnglishStrings = new List<string>();
        }

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
