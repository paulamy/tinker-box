using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneClient.Models
{
    public class Card
    {
        public string Front { get; set; }

        public string Back { get; set; }
        public int TypeID { get; set; }

        public int Difficulty { get; set; }

        public List<string> Tags { get; set; }
    }
}
