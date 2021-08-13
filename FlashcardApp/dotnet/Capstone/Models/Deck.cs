using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Deck
    {
        public List<Card> Cards { get; set; }   

        public string Name { get; set; }

        public int DeckID { get; set; }

        public string Description { get; set; }

        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public int UserID { get; set; }

        public bool Public { get; set; }
    }
}
