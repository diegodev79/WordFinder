using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WordFinder.Models;
using static System.Web.Razor.Parser.SyntaxConstants;

namespace WordFinder.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private List<List<string>> matrix;
        public List<List<string>> Matrix
        {
            get
            {
                if (matrix == null)
                {
                    matrix = InitializeMatrix();
                }
                return matrix;
            }
            set
            {
                matrix = value;
            }
        }

        private List<List<string>> InitializeMatrix()
        {
            var initializedMatrix = new List<List<string>>
            {
                new List<string> { "A", "B", "C", "D", "C" },
                new List<string> { "F", "G", "W", "I", "O" },
                new List<string> { "C", "H", "I", "L", "L" },
                new List<string> { "K", "L", "N", "N", "D" },
                new List<string> { "K", "L", "D", "N", "O" }
            };
            return initializedMatrix;
        }

        public ActionResult Index()
        {
            int rows = 5;
            int cols = 5;
            bool empty = false;

            if (empty)
            {
                Matrix = emptyMatrix(cols, rows);
            }

            ViewBag.Matrix = Matrix;

            return View();
        }

        public List<List<string>> emptyMatrix(int cols, int rows)
        {
            List<List<string>> matrix = new List<List<string>>();

            for (int i = 0; i < rows; i++)
            {
                List<string> row = new List<string>();
                for (int j = 0; j < cols; j++)
                {
                    row.Add(""); // Initialize each cell with an empty string
                }
                matrix.Add(row);
            }

            return matrix;
        }

        public PartialViewResult StartSearch(string words)
        {
            string[] wordsArray = words.Split(',');

            List<(string, bool)> searchResults = PerformSearch(wordsArray);

            return PartialView("_SearchResult", searchResults);
        }

        private List<(string, bool)> PerformSearch(string[] wordsArray)
        {
            List<(string, bool)> res = new List<(string, bool)>();
            foreach (string word in wordsArray)
            {
                res.Add((word, searchWord(word)));
            }
            return res;
        }

        private bool searchWord(string word)
        {
            bool found = false;
            
            // Search for the word in each row
            foreach (var row in Matrix)
            {
                if (SearchWordInRow(row, word.ToUpper().Trim()))
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                for (int column = 0; column < Matrix[0].Count; column++)
                {
                    if (SearchWordInColumn(word.ToUpper().Trim(), column))
                    {
                        found = true;
                        break;
                    }
                }
            }
            return found;
        }

        private bool SearchWordInRow(List<string> row, string word)
        {
            string rowString = string.Join("", row).ToUpper();
            return rowString.Contains(word);
        }
        private bool SearchWordInColumn(string word, int column)
        {
            StringBuilder columnContent = new StringBuilder();
            foreach (var row in matrix)
            {
                if (row.Count > column)
                {
                    columnContent.Append(row[column]); 
                }
            }
            string concatenatedColumn = columnContent.ToString(); 
            
            return concatenatedColumn.Contains(word); 

        }
    }
}
