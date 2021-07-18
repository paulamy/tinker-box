using System;
using System.Collections.Generic;
using System.Text;

namespace GamesAndPuzzles.Classes
{
    public class Deck
    {
        public int NumberOfCardsInDeck { get; set; }
        private List<Card> AllCards { get; set; } = new List<Card>();

        public Deck()
        {
            foreach (string suit in Card.SuitNames)
            {
                for (int i = 1; i < 14; i++)
                {
                    Card tempCard = new Card(suit, i);
                    AllCards.Add(tempCard);
                    NumberOfCardsInDeck++;
                }
            }
        }
        public Card Draw()
        {
            Card cardToBeDrawn = AllCards[0];
            AllCards.RemoveAt(0);
            return cardToBeDrawn;
        }
        public void Shuffle()
        {
            Random r = new Random();
            for (int i = 0; i < 1000000; i++)
            {
                int randomSpot1 = r.Next(AllCards.Count);
                int randomSpot2 = r.Next(AllCards.Count);
                Card tempCard = AllCards[randomSpot1];
                AllCards[randomSpot1] = AllCards[randomSpot2];
                AllCards[randomSpot2] = tempCard;
            }
        }
    }
}
