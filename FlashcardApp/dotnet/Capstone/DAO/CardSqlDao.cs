using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class CardSqlDao : ICardDao
    {
        private readonly string connectionString;
        private GetObjectFromReader obj = new GetObjectFromReader();

        public CardSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Card> GetPublicCards()
        {
            List<Card> returnCards = new List<Card>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM cards JOIN decks_cards ON decks_cards.card_id = cards.card_id " +
                        "JOIN decks ON decks.deck_id = decks_cards.deck_id WHERE is_public = 1; ", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Card tempCard = obj.GetCardFromReader(reader);
                            returnCards.Add(tempCard);
                        }
                    }
                    reader.Close();

                    foreach (Card card in returnCards)
                    {
                        cmd = new SqlCommand("SELECT tag_types.tag_id, tag_name FROM tag_types JOIN cards_tags ON cards_tags.tag_id = tag_types.tag_id " +
                        "WHERE card_id = @card", conn);
                        cmd.Parameters.AddWithValue("@card", card.CardID);
                        reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Tag tempTag = obj.GetTagFromReader(reader);
                                card.Tags.Add(tempTag.TagName);
                            }
                        }
                        reader.Close();
                    }
                }

            }
            catch (SqlException e)
            {
                throw e;
            }
            return returnCards;
        }

        public List<Card> GetPublicCardsByTag(string tag)
        {
            List<Card> returnCards = new List<Card>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT cards.card_id, difficulty, front, back, type_id, tag_types.tag_id, tag_types.tag_name, decks.deck_id, " +
                        "name, description, category_id, is_public, user_id FROM cards JOIN cards_tags ON cards.card_id = cards_tags.card_id " +
                        "JOIN tag_types ON cards_tags.tag_id = tag_types.tag_id " +
                        "JOIN decks_cards ON decks_cards.card_id = cards.card_id JOIN decks ON decks.deck_id = decks_cards.deck_id " +
                        "WHERE is_public = 1 AND tag_name LIKE @tag", conn);
                    cmd.Parameters.AddWithValue("@tag", tag);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            Card tempCard = obj.GetCardFromReader(reader);
                            returnCards.Add(tempCard);
                        }
                    }
                    reader.Close();

                    foreach(Card card in returnCards)
                    {
                        cmd = new SqlCommand("SELECT tag_types.tag_id, tag_name FROM tag_types JOIN cards_tags ON cards_tags.tag_id = tag_types.tag_id " +
                        "WHERE card_id = @card", conn);
                        cmd.Parameters.AddWithValue("@card", card.CardID);
                        reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Tag tempTag = obj.GetTagFromReader(reader);
                                card.Tags.Add(tempTag.TagName);
                            }
                        }
                        reader.Close();
                    }
                    
                }

            }
            catch(SqlException e)
            {
                throw e;
            }
            return returnCards;
        }

        public Card GetCardByID(int userID, int cardID)
        {
            Card returnCard = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT cards.card_id, difficulty, front, back, type_id " +
                        "FROM cards JOIN decks_cards ON decks_cards.card_id = cards.card_id " +
                        "JOIN decks ON decks.deck_id = decks_cards.deck_id WHERE user_id = @user AND cards.card_id = @card; ", conn);
                    cmd.Parameters.AddWithValue("@card", cardID);
                    cmd.Parameters.AddWithValue("@user", userID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        returnCard = obj.GetCardFromReader(reader);
                    }
                    reader.Close();

                    if (returnCard != null)
                    {
                        cmd = new SqlCommand("SELECT tag_types.tag_id, tag_name FROM tag_types JOIN cards_tags ON cards_tags.tag_id = tag_types.tag_id " +
                           "WHERE card_id = @card", conn);
                        cmd.Parameters.AddWithValue("@card", returnCard.CardID);
                        reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Tag tempTag = obj.GetTagFromReader(reader);
                                returnCard.Tags.Add(tempTag.TagName);
                            }
                        }
                    }
                   

                }

            }
            catch (SqlException e)
            {
                throw e;
            }
            return returnCard;
        }

        public Card AddCard(int userID, int deckID, Card newCard)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO cards(difficulty, front, back, type_id)" +
                        "VALUES(@diff, @front, @back, @type)", conn);
                    cmd.Parameters.AddWithValue("@type", newCard.TypeID);
                    cmd.Parameters.AddWithValue("@diff", newCard.Difficulty);
                    cmd.Parameters.AddWithValue("@front", newCard.Front);
                    cmd.Parameters.AddWithValue("@back", newCard.Back);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SELECT @@IDENTITY", conn);
                    int newId = Convert.ToInt32(cmd.ExecuteScalar());

                    newCard.CardID = newId;

                    cmd = new SqlCommand("IF (SELECT user_id FROM decks WHERE deck_id = @deck) = @user BEGIN " +
                        "INSERT INTO decks_cards (card_id, deck_id) VALUES (@card, @deck) END", conn);
                    cmd.Parameters.AddWithValue("@card", newCard.CardID);
                    cmd.Parameters.AddWithValue("@deck", deckID);
                    cmd.Parameters.AddWithValue("@user", userID);
                    cmd.ExecuteNonQuery();

                    foreach (string tag in newCard.Tags)
                    {
                        cmd = new SqlCommand("IF (SELECT COUNT(tag_name) FROM tag_types WHERE tag_name = @tag) = 0 BEGIN " +
                            "INSERT INTO tag_types(tag_name) VALUES(@tag) END", conn);
                        cmd.Parameters.AddWithValue("@tag", tag);
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("INSERT INTO cards_tags(card_id, tag_id) VALUES(@card, (SELECT tag_id FROM tag_types " +
                            "WHERE tag_name = @tag))", conn);
                        cmd.Parameters.AddWithValue("@card", newCard.CardID);
                        cmd.Parameters.AddWithValue("@tag", tag);
                        cmd.ExecuteNonQuery();
                    
                    }

                    
                    return newCard;

                }
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public Card ModifyCard(int userID, int cardID, Card cardToEdit)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("IF (SELECT user_id FROM decks JOIN decks_cards ON decks.deck_id = decks_cards.deck_id WHERE card_id = @card) = @user " +
                        "BEGIN UPDATE cards SET difficulty=@diff, front=@front, back=@back, type_id=@type WHERE card_id = @card END;", conn);
                    cmd.Parameters.AddWithValue("@user", userID);
                    cmd.Parameters.AddWithValue("@card", cardID);
                    cmd.Parameters.AddWithValue("@type", cardToEdit.TypeID);
                    cmd.Parameters.AddWithValue("@diff", cardToEdit.Difficulty);
                    cmd.Parameters.AddWithValue("@front", cardToEdit.Front);
                    cmd.Parameters.AddWithValue("@back", cardToEdit.Back);
                    cmd.ExecuteNonQuery();

                    foreach (string tag in cardToEdit.Tags)
                    {
                        cmd = new SqlCommand("IF (SELECT COUNT(tag_name) FROM tag_types WHERE tag_name = @tag) = 0 BEGIN " +
                            "INSERT INTO tag_types(tag_name) VALUES(@tag) END", conn);
                        cmd.Parameters.AddWithValue("@tag", tag);
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("IF NOT EXISTS (SELECT card_id, tag_id FROM cards_tags WHERE tag_id = (SELECT tag_id FROM tag_types " +
                            "WHERE tag_name = @tag) AND card_id=@card) " +
                            "INSERT INTO cards_tags(card_id, tag_id) VALUES(@card, (SELECT tag_id FROM tag_types " +
                            "WHERE tag_name = @tag))", conn);
                        cmd.Parameters.AddWithValue("@card", cardToEdit.CardID);
                        cmd.Parameters.AddWithValue("@tag", tag);
                        cmd.ExecuteNonQuery();

                    }
                    cmd = new SqlCommand("SELECT tag_name, cards_tags.tag_id FROM tag_types " +
                        "JOIN cards_tags ON cards_tags.tag_id = tag_types.tag_id " +
                        "WHERE card_id = @card", conn);
                        cmd.Parameters.AddWithValue("@card", cardToEdit.CardID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Tag> tags = new List<Tag>();
                    if (reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            Tag tag = obj.GetTagFromReader(reader);
                            tags.Add(tag);
                        }
                        
                    }
                    reader.Close();
                    foreach(Tag tag in tags)
                    {
                        if (!cardToEdit.Tags.Contains(tag.TagName))
                        {
                            cmd = new SqlCommand("DELETE FROM cards_tags WHERE tag_id = @tag AND card_id = @card", conn);
                            cmd.Parameters.AddWithValue("@tag", tag.TagID);
                            cmd.Parameters.AddWithValue("@card",cardToEdit.CardID);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    return cardToEdit;

                }

            }
            catch(SqlException e)
            {
                throw e;
            }
        }

        public bool DeleteCard(int userID, int cardID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("IF (SELECT decks.user_id FROM decks JOIN decks_cards ON decks.deck_id = decks_cards.deck_id WHERE card_id = @card) = @user " +
                        "BEGIN UPDATE cards SET type_id = 1 where card_id = @card  " +
                        "UPDATE decks_cards SET deck_id = 1 WHERE card_id = @card END", conn);
                    cmd.Parameters.AddWithValue("@user", userID);
                    cmd.Parameters.AddWithValue("@card", cardID);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch(SqlException e)
            {
                throw e;
            }
        }
    }
}
