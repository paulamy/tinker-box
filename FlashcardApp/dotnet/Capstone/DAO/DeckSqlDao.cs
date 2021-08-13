using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class DeckSqlDao : IDeckDao
    {
        private readonly string connectionString;
        private GetObjectFromReader obj = new GetObjectFromReader();

        public DeckSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
            
        }

        public List<Deck> GetPublicDecks()
        {
            List<Deck> returnDecks = new List<Deck>();

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT decks.deck_id, name, description, decks.category_id, category_name, is_public, user_id FROM decks " +
                        "JOIN category_types ON decks.category_id = category_types.category_id WHERE is_public = 1", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if(reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Deck tempDeck = obj.GetDeckFromReader(reader);
                            returnDecks.Add(tempDeck);
                        }
                    }
                    reader.Close();

                    cmd = new SqlCommand("SELECT deck_id, cards.card_id, difficulty, front, back, type_id FROM cards " +
                        "JOIN decks_cards ON cards.card_id = decks_cards.card_id", conn);
                    reader = cmd.ExecuteReader();

                    if (returnDecks != null && reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            foreach (Deck deck in returnDecks)
                            {
                                if (deck.DeckID == Convert.ToInt32(reader["deck_id"]))
                                {
                                    Card tempCard = obj.GetCardFromReader(reader);
                                    deck.Cards.Add(tempCard);
                                }
                            }

                        }
                    }
                    reader.Close();

                    foreach (Deck deck in returnDecks)
                    {
                        foreach (Card card in deck.Cards)
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
                
            }
            catch(SqlException e)
            {
                throw e;
            }
            return returnDecks;
        }

        public List<Deck> GetUserDecks(int userID)
        {
            List<Deck> returnDecks = new List<Deck>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT deck_id, name, description, decks.category_id, category_name, is_public, user_id FROM decks " +
                        "JOIN category_types ON decks.category_id = category_types.category_id WHERE user_id = @user", conn);
                    cmd.Parameters.AddWithValue("@user", userID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Deck tempDeck = obj.GetDeckFromReader(reader);
                            returnDecks.Add(tempDeck);
                        }
                    }
                    reader.Close();

                    cmd = new SqlCommand("SELECT deck_id, cards.card_id, difficulty, front, back, type_id FROM cards " +
                        "JOIN decks_cards ON cards.card_id = decks_cards.card_id", conn);
                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            foreach (Deck deck in returnDecks)
                            {
                                if (deck.DeckID == Convert.ToInt32(reader["deck_id"]))
                                {
                                    Card tempCard = obj.GetCardFromReader(reader);
                                    deck.Cards.Add(tempCard);
                                }
                            }

                        }
                    }

                    reader.Close();

                    foreach (Deck deck in returnDecks)
                    {
                        foreach (Card card in deck.Cards)
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
            }
            catch (SqlException e)
            {
                throw e;
            }
            return returnDecks;
        }
        public Deck GetDeckByName(string name)
        {
            Deck returnDeck = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT deck_id, name, description, decks.category_id, category_name, is_public, user_id FROM decks " +
                        "JOIN category_types ON decks.category_id = category_types.category_id WHERE name LIKE @name AND user_id = 2", conn);
                    cmd.Parameters.AddWithValue("@name", name);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        returnDeck = obj.GetDeckFromReader(reader);
                    }
                    reader.Close();

                    if(returnDeck != null)
                    {
                        cmd = new SqlCommand("SELECT deck_id, cards.card_id, difficulty, front, back, type_id FROM cards " +
                        "JOIN decks_cards ON cards.card_id = decks_cards.card_id " +
                        "WHERE deck_id = @deck", conn);
                        cmd.Parameters.AddWithValue("@deck", returnDeck.DeckID);
                        reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Card tempCard = obj.GetCardFromReader(reader);
                                returnDeck.Cards.Add(tempCard);
                            }
                        }
                        reader.Close();

                        foreach (Card card in returnDeck.Cards)
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
            }
            catch (SqlException e)
            {
                
                throw e;
            }
            return returnDeck;
        }


        public Deck GetDeckByID(int userID, int deckID)
        {
            Deck returnDeck = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT deck_id, name, description, decks.category_id, category_name, is_public, user_id FROM decks " +
                        "JOIN category_types ON decks.category_id = category_types.category_id WHERE user_id = @user AND deck_id = @deck", conn);
                    cmd.Parameters.AddWithValue("@user", userID);
                    cmd.Parameters.AddWithValue("@deck", deckID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        returnDeck = obj.GetDeckFromReader(reader);
                    }
                    reader.Close();

                    if(returnDeck != null)
                    {
                        cmd = new SqlCommand("SELECT deck_id, cards.card_id, difficulty, front, back, type_id FROM cards " +
                        "JOIN decks_cards ON cards.card_id = decks_cards.card_id " +
                        "WHERE deck_id = @deck", conn);
                        cmd.Parameters.AddWithValue("@deck", deckID);
                        reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Card tempCard = obj.GetCardFromReader(reader);
                                returnDeck.Cards.Add(tempCard);
                            }
                        }
                        reader.Close();

                        foreach (Card card in returnDeck.Cards)
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
            }
            catch (SqlException e)
            {
                throw e;
            }
            return returnDeck;
        }

        public Deck AddDeck(int userID, Deck newDeck)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO decks(name,description,category_id,is_public, user_id) " +
                        "VALUES(@name,@desc, @category, @public, @user);", conn);
                    cmd.Parameters.AddWithValue("@user", userID);
                    cmd.Parameters.AddWithValue("@name", newDeck.Name);
                    cmd.Parameters.AddWithValue("@desc", newDeck.Description);
                    cmd.Parameters.AddWithValue("@category", newDeck.CategoryID);
                    cmd.Parameters.AddWithValue("@public", newDeck.Public);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SELECT @@IDENTITY", conn);
                    int newId = Convert.ToInt32(cmd.ExecuteScalar());

                    newDeck.DeckID = newId;
                    return newDeck;
                }
            }
            catch(SqlException e)
            {
                throw e;
            }
        }

        public Deck ModifyDeck(int userID, int deckID, Deck deckToEdit)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE decks SET name=@name, description=@desc, category_id=@category, is_public=@public, user_id=@user " +
                        "WHERE deck_id = @deck", conn);
                    cmd.Parameters.AddWithValue("@user", userID);
                    cmd.Parameters.AddWithValue("@deck", deckID);
                    cmd.Parameters.AddWithValue("@name", deckToEdit.Name);
                    cmd.Parameters.AddWithValue("@desc", deckToEdit.Description);
                    cmd.Parameters.AddWithValue("@category", deckToEdit.CategoryID);
                    cmd.Parameters.AddWithValue("@public", deckToEdit.Public);
                    cmd.ExecuteNonQuery();

                    return deckToEdit;
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public bool DeleteDeck(int userID, int deckID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE decks SET is_public = 0, user_id = 2 " +
                        "WHERE user_id = @user AND deck_ID = @deck", conn);
                    cmd.Parameters.AddWithValue("@user", userID);
                    cmd.Parameters.AddWithValue("@deck", deckID);

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
