using System.Text.RegularExpressions;
using System.Threading;

namespace Task_3
{
    internal class StringData
    {
        /// <summary>
        /// The class describing the input data
        /// </summary>

        public string Str { get; private set; }
        public double PetrencoIndex { get; private set; }

        public bool ThreadRunning { get; private set; }

        public StringData(string str, bool isEnglish = false)
        {
            Str = str;

            //If the received string is English
            if (!isEnglish)
            {
                // Set a marker that the thread is running and give the index count to a parallel thread
                ThreadRunning = true;
                new Thread(() => CountIndex(Str)).Start();
            }
            else
            {
                // Set a marker that the thread is running and give the index count to a parallel thread
                ThreadRunning = true;
                new Thread(() => CountIndex(Str.Substring(0, Str.IndexOf("|") + 1))).Start();
            }
        }

        /// <summary>
        /// Clones the received instance
        /// </summary>
        /// <param name="stringData">Instance for cloning</param>
        public StringData(StringData stringData)
        {
            Str = stringData.Str;
            PetrencoIndex = stringData.PetrencoIndex;
        }

        /// <summary>
        /// Considers the petrenko index
        /// </summary>
        /// <param name="input">Input string</param>
        private void CountIndex(string input)
        {
            Regex regex = new Regex("[а-я]|[А-Я]|[a-z]|[A-Z]", RegexOptions.Compiled);
            double index = 0;

            for (int i = 0; i < input.Length; i++)
            {
                // If the character is not a letter
                if (!regex.IsMatch(input[i] + ""))
                {
                    // Deleting it
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

            // Setting the thread completion marker
            ThreadRunning = false;
        }
    }

}
