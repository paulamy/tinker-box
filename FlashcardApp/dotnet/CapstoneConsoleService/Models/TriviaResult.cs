using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneClient.Models
{
    public class TriviaResult
    {
        public int ResponseCode { get; set; }
        public List<TriviaQuestion> Results { get; set; }

        public TriviaResult()
        {
            Results = new List<TriviaQuestion>();
        }
    }
}
