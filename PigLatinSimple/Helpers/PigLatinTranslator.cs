using PigLatinSimple.Models;
using System.Globalization;
using System.Text;

namespace PigLatinSimple.Helpers
{
    public class PigLatinTranslator
    {
        int vowelIndex = 0;

        List<char> vowels = new List<char>();
        List<int> capitalIndex = new List<int>();

        Dictionary<int, string> punctuations = new Dictionary<int, string>();


        public string ProcessPhrase(User user)
        {
            string translatedPhrase = "";
                        
            String[] phrase = user.inputtedPhrase.Split(' ');
            
            List<string> translatedWords = new List<string>();

            vowels.Add('a');
            vowels.Add('e');
            vowels.Add('i');
            vowels.Add('o');
            vowels.Add('u');
            vowels.Add('A');
            vowels.Add('E');
            vowels.Add('I');
            vowels.Add('O');
            vowels.Add('U');

            foreach (string word in phrase)
            {
                Char[] wordCharArray = word.ToCharArray();
                      
                translatedWords.Add(Translator(word, user));
                
            }

            for(int i = 0; i < translatedWords.Count; i++)
            {
                translatedPhrase = i==0 ? translatedWords[i] : translatedPhrase + " " + translatedWords[i];
            }

            return translatedPhrase;
        }

        private string Translator(string word, User user)
        {
            int iCNT = 0;
            
            string translatedWord = "";

            bool startsWithVowel = false;                       

            List<char> vowels = new List<char>();
            vowels.Add('a');
            vowels.Add('e');
            vowels.Add('i');
            vowels.Add('o');
            vowels.Add('u');
            vowels.Add('A');
            vowels.Add('E');
            vowels.Add('I');
            vowels.Add('O');
            vowels.Add('U');

            if (word.Any(c => (c >= 34 && c <= 38)) ||
                word.Any(c => (c >= 40 && c <= 45)) ||
                word.Any(c => (c >= 47 && c <= 62)) ||
                word.Any(c => (c == 64)))
            {
                return word;
            }

            translatedWord = PrepareWord(word, user, ref startsWithVowel);


            if (startsWithVowel) 
            { 
                translatedWord += "way"; 
            }
            else
            {
                translatedWord = SplitWord(translatedWord);

                translatedWord += "ay";
                
            }

            translatedWord = ResetPunctuationAndCapitals(word, translatedWord);

            punctuations.Clear();
            capitalIndex.Clear();

            return translatedWord;

        }

        private string PrepareWord(string word, User user, ref bool startsWithVowel)
        {
            int iCNT = 0;

            string translatedWord = "";

            foreach (char c in word)
            {
                if (Char.IsUpper(c))
                {
                    capitalIndex.Add(iCNT);
                }

                if (iCNT == 0 && vowels.Contains(c)) { startsWithVowel = true; }

                vowelIndex = vowelIndex == 0 && vowels.Contains(c) ? iCNT : vowelIndex;

                if (c == 33 || c == 39 || c == 46 || c == 63)
                {
                    punctuations.Add(iCNT, c.ToString());

                    if (translatedWord == "")
                    {
                        translatedWord = word.Substring(0, iCNT) + word.Substring(iCNT + 1);
                    }
                    else if (translatedWord.Length != iCNT)
                    {
                        translatedWord = translatedWord.Substring(0, iCNT) + translatedWord.Substring(iCNT + 1);
                    }
                    else if (word.Length == iCNT + 1)
                    {
                        translatedWord = translatedWord.Remove(word.Length - punctuations.Count);

                    }

                }

                iCNT++;
            }

            translatedWord = translatedWord == "" ? word : translatedWord;
            
            return translatedWord;
        }

        private string SplitWord(string word)
        {
            string translatedWord = "";

            if (translatedWord == "")
            {
                translatedWord = word.Substring(vowelIndex) + word.Substring(0, vowelIndex);
            }
            else
            {
                translatedWord = translatedWord.Substring(vowelIndex) + translatedWord.Substring(0, vowelIndex);
            }

            return translatedWord;

        }

        private string ResetPunctuationAndCapitals(string word, string translatedWord)
        {
            int punctuationDifference;

            foreach (var entry in punctuations)
            {
                punctuationDifference = translatedWord.Length - word.Length;

                if (entry.Key == word.Length - 1)
                    translatedWord = translatedWord + entry.Value;
                else
                {
                    translatedWord = translatedWord.Substring(0, (word.Length + punctuationDifference) - 1) +
                                     entry.Value +
                                     translatedWord.Substring(word.Length + punctuationDifference - 1);
                }

            }

            translatedWord =  SetCapitals(translatedWord);

            return translatedWord;
        }

        private string SetCapitals(string translatedWord)
        {
            for (int i = 0; i < translatedWord.Length; i++)
            {
                if (capitalIndex.Contains(i))
                {
                    translatedWord = translatedWord.Remove(i, 1).Insert(i, Char.ToUpper(translatedWord[i]).ToString());
                }
                else
                {
                    translatedWord = translatedWord.Remove(i, 1).Insert(i, Char.ToLower(translatedWord[i]).ToString());
                }
            }

            return translatedWord;
        }

    }

}
