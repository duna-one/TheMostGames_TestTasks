namespace Task_1
{
    class TableWorkers
    {
        public string Text { get; set; }
        public uint WordsCount { get; set; }
        public uint VowelsCount { get; set; }

        TableWorkers(string text)
        {
            Text = text;
            WordsCount = WordCounter();
            VowelsCount = VolwesCounter();
        }

        private uint WordCounter()
        {
            return (uint)Text.Split(" ").Length;
        }

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
