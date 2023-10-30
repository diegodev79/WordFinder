using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WordFinder.Models
{
    public class SearchResultsModel
    {
        public List<(string, bool)> WordsFound { get; set; }
    }

}