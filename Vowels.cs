using System.Text.RegularExpressions;

namespace Task_1
{
    class Vowels
    {
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

        public Vowels()
        {
            Pattern = "[" + Russian + English + Bulgarian + Greek +
                Danish + Irish + Spanish + Italian + Latvian + Lithuanian + Maltese +
                German + Dutch + Polish + Romanian + Slovak + Slovenian + Finnish + French +
                Croatian + Czech + Swedish + Estonian + Ukrain + "]";
        }

        public bool isVowel(char letter)
        {
            Regex regex = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return regex.IsMatch("" + letter);
        }
    }
}
