using System.Text.RegularExpressions;
using System.Threading;

namespace Task_3
{
    internal class StringData
    {
        public string Str { get; private set; }
        public double PetrencoIndex { get; private set; }

        public bool ThreadRunning { get; private set; }

        public StringData(string str, bool isEnglish = false)
        {
            Str = str;
            if (!isEnglish)
            {
                ThreadRunning = true;
                new Thread(() => CountIndex(Str)).Start();
            }
            else
            {
                ThreadRunning = true;
                new Thread(() => CountIndex(Str.Substring(0, Str.IndexOf("|") + 1))).Start();
            }
        }

        public StringData(StringData stringData)
        {
            Str = stringData.Str;
            PetrencoIndex = stringData.PetrencoIndex;
        }

        private void CountIndex(string input)
        {
            Regex regex = new Regex("[а-я]|[А-Я]|[a-z]|[A-Z]", RegexOptions.Compiled);
            double index = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (!regex.IsMatch(input[i] + ""))
                {
                    input = input.Replace(input[i] + "", "");
                    i--;
                }
                else
                {
                    index += i + 0.5;
                }
            }
            index *= Str.Length;
            PetrencoIndex = index;
            ThreadRunning = false;
        }
    }

}
