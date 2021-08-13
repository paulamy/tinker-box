using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class GetObjectFromReader
    {
        public Deck GetDeckFromReader(SqlDataReader reader)
        {
            Deck deck = new Deck()
            {
                DeckID = Convert.ToInt32(reader["deck_id"]),
                Name = Convert.ToString(reader["name"]),
                Description = Convert.ToString(reader["description"]),
                CategoryID = Convert.ToInt32(reader["category_id"]),
                CategoryName = Convert.ToString(reader["category_name"]),
                UserID = Convert.ToInt32(reader["user_id"]),
                Public = Convert.ToBoolean(reader["is_public"]),
                Cards = new List<Card>()
            };
            return deck;
        }
        public Card GetCardFromReader(SqlDataReader reader)
        {
            Card card = new Card()
            {
                CardID = Convert.ToInt32(reader["card_id"]),
                TypeID = Convert.ToInt32(reader["type_id"]),
                Difficulty = Convert.ToInt32(reader["difficulty"]),
                Tags = new List<string>(),
                Front = Convert.ToString(reader["front"]),
                Back = Convert.ToString(reader["back"])
            };
            return card;
        }

        public Session GetSessionFromReader(SqlDataReader reader)
        {
            Session session = new Session()
            {
                SessionID = Convert.ToInt32(reader["session_id"]),
                SessionTypeID = Convert.ToInt32(reader["session_type_id"]),
                SessionDescription = Convert.ToString(reader["session_description"]),
                UserID = Convert.ToInt32(reader["user_id"]),
                TimeCreated = Convert.ToString(reader["time_created"]),
                TimeCompleted = Convert.ToString(reader["time_completed"]),
                Score = Convert.ToDouble(reader["score"]),
                ReviewCards = new List<Card>(),
                MasteredCards = new List<Card>(),
                Decks = new List<Deck>()
            };
            return session;

        }

        public Category GetCategoryFromReader(SqlDataReader reader)
        {
            Category category = new Category()
            {
                CategoryID = Convert.ToInt32(reader["category_id"]),
                CategoryName = Convert.ToString(reader["category_name"]),
            };
            return category;
        }

        public Tag GetTagFromReader(SqlDataReader reader)
        {
            Tag tag = new Tag()
            {
                TagID = Convert.ToInt32(reader["tag_id"]),
                TagName = Convert.ToString(reader["tag_name"]),
            };
            return tag;
        }
        
        public CardType GetCardTypeFromReader(SqlDataReader reader)
        {
            CardType type = new CardType()
            {
                TypeID = Convert.ToInt32(reader["type_id"]),
                CardTypeName = Convert.ToString(reader["card_type_name"]),
            };
            return type;
        }
        
        public SessionType GetSessionTypeFromReader(SqlDataReader reader)
        {
            SessionType type = new SessionType()
            {
                SessionTypeID = Convert.ToInt32(reader["session_type_id"]),
                SessionTypeName = Convert.ToString(reader["session_type_name"]),
            };
            return type;
        }
    }
}
