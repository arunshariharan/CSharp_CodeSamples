using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSamples
{
    public class YieldReturnCodeSample
    {
        private IEnumerable<string> listOfNames;
        public YieldReturnCodeSample()
        {
            // The following will have different values for each FindNames call
            // This tells the beauty of yield - multiple invocations will result in multiple iterations.
            // Enable the following line to see 2 different set of random words
            // Good explanation here- https://www.kenneth-truyers.net/2016/05/12/yield-return-in-c/
            
            // listOfNames = GenerateRandomWords_WithYield(4);

            // The following will have a static list of names 
            // Enable the following to have one list of names that will be iterated by FindNames methods

            listOfNames = GenerateRandomWords_WithNoYield(4);
        }

        private IEnumerable<string> GenerateRandomWords_WithYield(int numberOfWords)
        {
            var lengthOfWords = 4;
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_)(*&^%$#@!.";
            for(int i = 0; i < numberOfWords; i++)
            {
                yield return new string(
                    Enumerable.Repeat(chars, lengthOfWords)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray());
            }
        }

        private IEnumerable<string> GenerateRandomWords_WithNoYield(int numberOfWords)
        {
            var lengthOfWords = 4;
            var random = new Random();
            var randomWords = new List<string>();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_)(*&^%$#@!.";
            for (int i = 0; i < numberOfWords; i++)
            {
                randomWords.Add(new string(
                    Enumerable.Repeat(chars, lengthOfWords)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray()));
            }
            return randomWords;
        }

        public void ExecuteMethods()
        {
            char letterInput = PrintWelcomeMessageAndGetUserInput();

            Console.WriteLine("\n Normal method results: \n");
            var resultWithNoYield = FindNamesWithLetter_NoYieldReturn(letterInput);
            foreach (var name in resultWithNoYield)
            {
                Console.WriteLine(name);
            }

            Console.WriteLine("\n Yield method results: \n");
            var resultWithYield = FindNamesWithLetter_WithYieldReturn(letterInput);
            foreach (var name in resultWithYield)
            {
                Console.WriteLine(name);
            }
        }

        public IEnumerable<string> FindNamesWithLetter_NoYieldReturn(char letter)
        {
            var namesList = new List<string>();
            foreach(var name in listOfNames)
            {
                Console.WriteLine($"Processing the name: {name} ");
                if (name.Contains(letter, StringComparison.CurrentCultureIgnoreCase))
                {
                    namesList.Add(name);
                }
            }
            return namesList;
        }

        public IEnumerable<string> FindNamesWithLetter_WithYieldReturn(char letter)
        {
            foreach(var name in listOfNames)
            {
                Console.WriteLine($"Processing the name: {name} ");
                if (name.Contains(letter, StringComparison.CurrentCultureIgnoreCase))
                    yield return name;
            }
        }

        private char PrintWelcomeMessageAndGetUserInput()
        {
            Console.WriteLine($"Given a list of initial names, print the names that contains a letter:");
            foreach (var name in listOfNames)
                Console.WriteLine(name);
            Console.WriteLine($"We will call 2 methods - \n 1. A normal method that stroes names in a list and returns \n 2. A method with 'yield return'. \n Result as follows. \n ");

            Console.WriteLine("Enter a letter to find the names that contain the letter: ");
            var letterInput = Console.ReadLine().Trim().ToCharArray().First();
            return letterInput;
        }
    }
}
