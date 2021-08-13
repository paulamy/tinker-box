using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneClient.Models
{
    public class TriviaCategoryList
    {
        public List<TriviaCategories> TriviaCategories { get; set; }
        public TriviaCategoryList()
        {
            TriviaCategories = new List<TriviaCategories>();
        }
    }
}
