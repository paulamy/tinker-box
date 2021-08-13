using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Session
    {
        public int UserID { get; set; }

        public List<Deck> Decks { get; set; }

        public int SessionID { get; set; }

        public int SessionTypeID { get; set; }

        public string SessionDescription { get; set; }

        public string TimeCreated { get; set; }

        public List<Card> ReviewCards { get; set; }

        public List<Card> MasteredCards { get; set; }

        public double Score { get; set; }

        public string TimeCompleted { get; set; }
    }
}
