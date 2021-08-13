using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneClient.Models
{
    public class Deck
    {
        public string Name { get; set; }

        public int DeckID { get; set; }

        public string Description { get; set; }

        public int CategoryID { get; set; }

        public int UserID { get; set; }

        public bool Public { get; set; }
    }
}
