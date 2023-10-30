using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFinderConsole.Core;

namespace WordFinderConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<string> matrix = InitializeMatrixAsInPDF();
            IEnumerable<string> wordstream = new List<string> { "COLD", "WIND", "SNOW", "CHILL" };
            // Create an instance of WordFinder
            WordFinder wordFinder = new WordFinder(matrix);
            // Find words in the matrix
            IEnumerable<string> results = wordFinder.Find(wordstream);

            // Display results
            Console.WriteLine("Word Finder Results:");
            foreach (string result in results)
            {
                Console.WriteLine(result);
            }
            Console.WriteLine("");
            Console.WriteLine("===============================");
            Console.WriteLine("Second sample with RANDOM data");
            matrix = InitializeMatrixRandom(32);
            wordstream = new List<string> { "COLD", "WIND", "SNOW", "CHILL", "BO", "BU", "BA" };
            // Create an instance of WordFinder
            wordFinder = new WordFinder(matrix);
            // Find words in the matrix
            results = wordFinder.Find(wordstream);

            // Display results
            Console.WriteLine("Word Finder Results:");
            foreach (string result in results)
            {
                Console.WriteLine(result);
            }


            Console.WriteLine("Press any key to close");
            var k = Console.ReadKey();
        }

        private static IEnumerable<string> InitializeMatrixRandom(int size)
        {
            //Make a random matrix of size x size.           
            Random random = new Random();

            // Define the characters you want to include in the matrix
            string allowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            List<string> matrix = new List<string>();

            for (int i = 0; i < size; i++)
            {
                // Generate a random string of random characters of size 'size'
                string randomString = new string(Enumerable.Range(0, size)
                    .Select(_ => allowedCharacters[random.Next(0, allowedCharacters.Length)])
                    .ToArray());

                matrix.Add(randomString);
            }

            return matrix;
        }

        // Initialize the character matrix with the sample in the PDF.
        private static IEnumerable<string> InitializeMatrixAsInPDF()
        {
            return new List<string>
            {
                "ABCDC",
                "FGWIO",
                "CHILL",
                "KLNND",
                "KLDNO"
            };
        }
    }
}
