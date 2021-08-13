using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface ICardDao
    {
        List<Card> GetPublicCards();

        List<Card> GetPublicCardsByTag(string tag);

        Card GetCardByID(int userID, int cardID);

        Card AddCard(int userID, int deckID, Card newCard);

        Card ModifyCard(int userID, int cardID, Card cardToEdit);

        bool DeleteCard(int userID, int cardID);
    }
}
