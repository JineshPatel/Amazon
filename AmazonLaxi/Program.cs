using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AmazonLaxi
{
    class Program
    {
        static void Main(string[] args)
        {
            string test = "Jack and Jill went to market to buy bread and cheese.Cheese is Jack's and Jill's favorite food";
            List<string> wordToExclude = new List<string>();
            wordToExclude.Add("and");
            wordToExclude.Add("he");
            wordToExclude.Add("the");
            wordToExclude.Add("to");
            wordToExclude.Add("is");
            //  wordToExclude.Add("Jack");
            //   wordToExclude.Add("Jill");
            List<string> s = retrieveMostFrequentlyUsedWords(test, wordToExclude);
            foreach (var item in s)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }


        public static List<String> retrieveMostFrequentlyUsedWords(String literatureText,
                                               List<String> wordsToExclude)
        {
            string[] words = Regex.Split(literatureText, @"\W");
            var occurrences = new Dictionary<string, int>();

            foreach (var word in words)
            {
                string lowerWord = word.ToLowerInvariant();
                if (!occurrences.ContainsKey(lowerWord))
                    occurrences.Add(lowerWord, 1);
                else
                    occurrences[lowerWord]++;
            }
            var result = (from wp in occurrences.OrderByDescending(kvp => kvp.Value) select wp).ToDictionary(kw => kw.Key, kw => kw.Value);
            foreach (var item in wordsToExclude)
            {
                result.Remove(item.ToLowerInvariant());
            }
            return result.Where(k => k.Value >= result.Values.Max()).ToDictionary(kw => kw.Key, ke => ke.Value).Keys.ToList();
        }
    }
}
