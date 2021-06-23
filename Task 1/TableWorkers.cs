namespace Task_1
{
    /// <summary>
    /// A class that implements data stored in a table
    /// </summary>
    internal class TableWorkers
    {
        public string Text { get; set; }
        public uint WordsCount { get; set; }
        public uint VowelsCount { get; set; }

        public TableWorkers(string text)
        {
            Text = text;
            WordsCount = WordCounter();
            VowelsCount = VolwesCounter();
        }

        /// <summary>
        /// Counts the number of words in the text
        /// </summary>
        /// <returns>Number of words</returns>
        private uint WordCounter()
        {
            return (uint)Text.Split(" ").Length;
        }

        /// <summary>
        /// Counts the number of vowels in the text
        /// </summary>
        /// <returns>Тumber of vowels</returns>
        private uint VolwesCounter()
        {
            Vowels vowels = new Vowels();
            uint counter = 0;
            foreach (char letter in Text)
            {
                if (vowels.isVowel(letter))
                {
                    counter++;
                }
            }

            return counter;
        }
    }
}
