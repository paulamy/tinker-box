using CapstoneClient.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneClient.API_Services
{
    public class CardService
    {
        private static List<Card> cards = new List<Card>();

        public List<Card> GetTriviaCards(TriviaResult results)
        {
            cards = new List<Card>();
            foreach (TriviaQuestion question in results.Results)
            {
                Card tempCard = new Card();
                tempCard.Front = question.Question;
                tempCard.Back = question.CorrectAnswer;
                tempCard.TypeID = 2;
                tempCard.Tags = new List<string>() { "None" };
                if (question.Difficulty == "easy")
                {
                    tempCard.Difficulty = 1;
                }
                else if (question.Difficulty == "medium")
                {
                    tempCard.Difficulty = 3;
                }
                else if (question.Difficulty == "hard")
                {
                    tempCard.Difficulty = 5;
                }
                cards.Add(tempCard);
            }

            return cards;
        }

        public List<Card> GetDefinitionCards(Dictionary<string,OwlBotDefinition> definitions)
        {
            cards = new List<Card>();
            foreach (KeyValuePair<string, OwlBotDefinition> kvp in definitions)
            {
                Card tempCard = new Card();
                tempCard.Front = kvp.Key;
                tempCard.Back = kvp.Value.Definition;
                tempCard.TypeID = 3;
                tempCard.Tags = new List<string>() { "None" };
                tempCard.Difficulty = 3;
                cards.Add(tempCard);
            }
            return cards;
        }

        public List<Card> GetImageCards(List<MetMuseumObject> objectList)
        {
            cards = new List<Card>();
            foreach (MetMuseumObject item in objectList)
            {
                Card tempCard = new Card();
                tempCard.Front = item.PrimaryImageSmall;
                tempCard.Back = $"Artist: {item.ArtistDisplayName} | Title: {item.Title}";
                tempCard.TypeID = 4;
                tempCard.Tags = new List<string>() { "None" };
                tempCard.Difficulty = 5;
                cards.Add(tempCard);
            }

            return cards;
        }

    }
}
