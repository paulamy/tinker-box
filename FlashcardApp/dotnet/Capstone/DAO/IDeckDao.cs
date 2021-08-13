using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface IDeckDao
    {
        List<Deck> GetPublicDecks();

        List<Deck> GetUserDecks(int userID);

        Deck GetDeckByName(string name);

        Deck GetDeckByID(int userID, int deckID);

        Deck AddDeck(int userID, Deck newDeck);

        Deck ModifyDeck(int userID, int deckID, Deck deckToEdit);

        bool DeleteDeck(int userID, int deckID);
    }
}
