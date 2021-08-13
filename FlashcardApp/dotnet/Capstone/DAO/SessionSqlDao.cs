using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class SessionSqlDao : ISessionDao
    {
        private readonly string connectionString;
        private GetObjectFromReader obj = new GetObjectFromReader();

        public SessionSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Session> GetAllSessions(int userID)
        {
            List<Session> returnSessions = new List<Session>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open(); 

                    SqlCommand cmd = new SqlCommand("SELECT * FROM sessions WHERE user_id = @user", conn);
                    cmd.Parameters.AddWithValue("@user", userID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows) 
                    {
                        while(reader.Read())
                        {
                            Session tempSession = obj.GetSessionFromReader(reader);
                            returnSessions.Add(tempSession);
                        }
                    }
                    reader.Close();

                    cmd = new SqlCommand("SELECT decks.deck_id, name, description, decks.category_id, category_name, is_public, decks.user_id, session_id FROM decks " +
                        "JOIN sessions_decks ON sessions_decks.deck_id = decks.deck_id " +
                        "JOIN category_types ON decks.category_id = category_types.category_id", conn);
                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            foreach (Session session in returnSessions)
                            {
                                if (session.SessionID == Convert.ToInt32(reader["session_id"]))
                                {
                                    Deck tempDeck = obj.GetDeckFromReader(reader);
                                    session.Decks.Add(tempDeck);
                                }
                            }
                        }
                    }
                    reader.Close();

                    cmd = new SqlCommand("SELECT session_id, deck_id, cards.card_id, status, difficulty, front, back, type_id FROM cards " +
                        "JOIN decks_cards ON cards.card_id = decks_cards.card_id " +
                        "JOIN sessions_cards ON cards.card_id = sessions_cards.card_id", conn);
                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            foreach (Session session in returnSessions)
                            {
                                foreach (Deck deck in session.Decks)
                                {
                                    if (deck.DeckID == Convert.ToInt32(reader["deck_id"]) && session.SessionID == Convert.ToInt32(reader["session_id"]))
                                    {
                                        Card tempCard = obj.GetCardFromReader(reader);
                                        deck.Cards.Add(tempCard);
                                        if (Convert.ToInt32(reader["status"]) == 0 && !session.ReviewCards.Contains(tempCard))
                                        {
                                            session.ReviewCards.Add(tempCard);
                                        }
                                        else if (Convert.ToInt32(reader["status"]) == 1 && !session.MasteredCards.Contains(tempCard))
                                        {
                                            session.MasteredCards.Add(tempCard);
                                        }
                                    }
                                }
                            }

                        }
                    }
                    reader.Close();

                    foreach (Session session in returnSessions)
                    {
                        foreach (Deck deck in session.Decks)
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
            }
            catch(SqlException e)
            {
                throw e;
            }

            return returnSessions;
        }

        public Session GetSessionByID(int userID, int sessionID)
        {
            Session returnSession = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM sessions WHERE user_id = @user AND session_id = @session", conn);
                    cmd.Parameters.AddWithValue("@user", userID);
                    cmd.Parameters.AddWithValue("@session", sessionID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        returnSession = obj.GetSessionFromReader(reader);
                    }
                    reader.Close();

                    cmd = new SqlCommand("SELECT decks.deck_id, name, description, decks.category_id, category_name, is_public, decks.user_id, session_id FROM decks " +
                        "JOIN sessions_decks ON sessions_decks.deck_id = decks.deck_id " +
                        "JOIN category_types ON decks.category_id = category_types.category_id", conn);
                    cmd.Parameters.AddWithValue("@user", userID);
                    reader = cmd.ExecuteReader();

                    if (returnSession != null && reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (returnSession.SessionID == Convert.ToInt32(reader["session_id"]))
                            {
                                Deck tempDeck = obj.GetDeckFromReader(reader);
                                returnSession.Decks.Add(tempDeck);
                            }
                        }
                    }
                    reader.Close();

                    if(returnSession != null)
                    {
                        cmd = new SqlCommand("SELECT session_id, deck_id, cards.card_id, status, difficulty, front, back, type_id FROM cards " +
                        "JOIN decks_cards ON cards.card_id = decks_cards.card_id " +
                        "JOIN sessions_cards ON cards.card_id = sessions_cards.card_id", conn);
                        reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                foreach (Deck deck in returnSession.Decks)
                                {
                                    if (deck.DeckID == Convert.ToInt32(reader["deck_id"]) && returnSession.SessionID == Convert.ToInt32(reader["session_id"]))
                                    {
                                        Card tempCard = obj.GetCardFromReader(reader);
                                        deck.Cards.Add(tempCard);
                                        if (Convert.ToInt32(reader["status"]) == 0 && !returnSession.ReviewCards.Contains(tempCard))
                                        {
                                            returnSession.ReviewCards.Add(tempCard);
                                        }
                                        else if (Convert.ToInt32(reader["status"]) == 1 && !returnSession.MasteredCards.Contains(tempCard))
                                        {
                                            returnSession.MasteredCards.Add(tempCard);
                                        }
                                    }
                                }
                            }
                        }
                        reader.Close();
                        foreach (Deck deck in returnSession.Decks)
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
            }
            catch (SqlException e)
            {
                throw e;
            }

            return returnSession;
        }

        public Session AddSession(int userID, Session newSession)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO sessions(session_type_id, session_description, user_id, time_created, score, time_completed) " +
                        "VALUES(@type, @desc, @user, @start, @score, @end)", conn);
                    cmd.Parameters.AddWithValue("@user", userID);
                    cmd.Parameters.AddWithValue("@desc", newSession.SessionDescription);
                    cmd.Parameters.AddWithValue("@type", newSession.SessionTypeID);
                    cmd.Parameters.AddWithValue("@start", newSession.SessionTypeID);
                    cmd.Parameters.AddWithValue("@end", newSession.TimeCompleted);
                    cmd.Parameters.AddWithValue("@score", newSession.Score);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SELECT @@IDENTITY", conn);
                    int newId = Convert.ToInt32(cmd.ExecuteScalar());

                    newSession.SessionID = newId;

                    //foreach(Deck deck in newSession.Decks)
                    //{
                    //    cmd = new SqlCommand("INSERT INTO sessions_decks(session_id, deck_id) VALUES(@session, @deck)", conn);
                    //    cmd.Parameters.AddWithValue("@session", newSession.SessionID);
                    //    cmd.Parameters.AddWithValue("@deck", deck.DeckID);
                    //    cmd.ExecuteNonQuery();
                    //}

                    //foreach(Card card in newSession.ReviewCards)
                    //{
                    //    cmd = new SqlCommand("INSERT INTO sessions_cards(session_id, card_id, status) VALUES(@session, @card, 0)", conn);
                    //    cmd.Parameters.AddWithValue("@session", newSession.SessionID);
                    //    cmd.Parameters.AddWithValue("@card", card.CardID);
                    //    cmd.ExecuteNonQuery();
                    //}

                    //foreach (Card card in newSession.MasteredCards)
                    //{
                    //    cmd = new SqlCommand("INSERT INTO sessions_cards(session_id, card_id, status) VALUES(@session, @card, 1)", conn);
                    //    cmd.Parameters.AddWithValue("@session", newSession.SessionID);
                    //    cmd.Parameters.AddWithValue("@card", card.CardID);
                    //    cmd.ExecuteNonQuery();
                    //}

                    return newSession;
                }
                    
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

    }
}
