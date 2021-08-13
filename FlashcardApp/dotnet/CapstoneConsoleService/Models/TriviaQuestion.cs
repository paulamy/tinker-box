using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneClient.Models
{
    public class TriviaQuestion
    {
        public string Type { get; set; }
        public string Difficulty { get; set; }
        public string Question { get; set; }

        public string CorrectAnswer { get; set; }

        public List<string> IncorrectAnswers { get; set; }
    }
}
