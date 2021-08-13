using CapstoneClient.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneClient.API_Services
{
    public class DeckService
    {
        const int adminID = 2;
        public Deck GetDeck(int deckCategory)
        {
            Console.Write("Please Enter a name for the deck: ");
            string deckName = Console.ReadLine();
            Console.Write("Please Enter a description for the deck: ");
            string deckDescription = Console.ReadLine();
            Deck newDeck = new Deck();
            //Always set new decks created this way to public decks owned by admin
            newDeck.UserID = adminID;
            newDeck.Public = true;
            newDeck.Name = deckName;
            newDeck.Description = deckDescription;
            newDeck.CategoryID = deckCategory;
            return newDeck;
        }
       
    }
}
