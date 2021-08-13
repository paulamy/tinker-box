using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneClient.Models
{
    public class TriviaQuestionCount
    {
        public int CategoryID { get; set; }

        public List<TriviaTotalQuestionCount> CategoryQuestionCount { get; set; }

        public TriviaQuestionCount()
        {
            CategoryQuestionCount = new List<TriviaTotalQuestionCount>();
        }
    }
}
