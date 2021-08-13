using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Card
    {
        public List<string> Tags { get; set; }

        public int CardID { get; set; }

        public int TypeID { get; set; }

        public int Difficulty { get; set; }

        public string Front { get; set; }

        public string Back { get; set; }
    }
}
