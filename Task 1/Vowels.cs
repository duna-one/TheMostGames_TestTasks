using System.Text.RegularExpressions;

namespace Task_1
{
    class Vowels
    {
        /// <summary>
        /// All symbols denoting vowel sounds of European languages
        /// </summary>
        private readonly string Russian = "ауоыиэяюёе";
        private readonly string English = "aeiouy";
        private readonly string Bulgarian = "иеъауо";
        private readonly string Hungarian = "aeioöuüáéíóőúű";
        private readonly string Greek = "αεηιουω";
        private readonly string Danish = "aeiouyæøå";
        private readonly string Irish = "iíeéaáoóuú";
        private readonly string Spanish = "aеiоu";
        private readonly string Italian = "aeiou";
        private readonly string Latvian = "aāeēiīouū";
        private readonly string Lithuanian = "aąeęiįyouųū";
        private readonly string Maltese = "aeiouaeieiou";
        private readonly string German = "aeiouäöü";
        private readonly string Dutch = "aeiou";
        private readonly string Polish = "aeiouóyąę";
        //private readonly string Portuguese = "";
        private readonly string Romanian = "aăâeiîou";
        private readonly string Slovak = "aáäeéiíoóôuúyý";
        private readonly string Slovenian = "iauoe";
        private readonly string Finnish = "ieyuoaäö";
        private readonly string French = "аеiïouéêё";
        private readonly string Croatian = "iīeēaāoōuū";
        private readonly string Czech = "аáеéěiíoóuúůyý";
        private readonly string Swedish = "aåäöeiouy";
        private readonly string Estonian = "аоиеiодцй";
        private readonly string Ukrain = "АЕЄИІЇОУЮЯ";

        private string Pattern { get; set; }

        /// <summary>
        /// When you run the constructor, it creates a template that is then used in Regex
        /// </summary>
        public Vowels()
        {
            Pattern = "[" + Russian + English + Bulgarian + Hungarian + Greek +
                Danish + Irish + Spanish + Italian + Latvian + Lithuanian + Maltese +
                German + Dutch + Polish + Romanian + Slovak + Slovenian + Finnish + French +
                Croatian + Czech + Swedish + Estonian + Ukrain + "]";
        }

        /// <summary>
        /// Checks whether the specified character is a vowel letter
        /// </summary>
        /// <param name="letter">The character being checked</param>
        /// <returns>
        /// Returns true if the character is a vowel letter
        /// Returns false if the character is not a vowel letter
        /// </returns>
        public bool isVowel(char letter)
        {
            Regex regex = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return regex.IsMatch("" + letter); // And here we have a stupid conversion of a char to string
        }
    }
}
