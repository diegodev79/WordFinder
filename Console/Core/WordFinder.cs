using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFinderConsole.Core
{
    public class WordFinder
    {
        private readonly IEnumerable<string> matrix;

        public WordFinder(IEnumerable<string> matrix)
        {
            if (matrix.FirstOrDefault()?.Length > 64)
            {
                throw new ArgumentOutOfRangeException("Matrix strings cannot exceed 64 characters.");
            }

            this.matrix = matrix;
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            var results = new List<string>();

            foreach (string word in wordstream)
            {
                bool found = false;

                foreach (string row in matrix)
                {
                    if (row.Contains(word))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    for (int column = 0; column < matrix.First().Length; column++)
                    {
                        string columnContent = string.Join("", matrix.Select(row => row[column]));
                        if (columnContent.Contains(word))
                        {
                            found = true;
                            break;
                        }
                    }
                }

                results.Add(word + ": " + (found ? "found" : "not found"));
            }

            return results;
        }
    }
}
