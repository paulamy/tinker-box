using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class CardTypeSqlDao : ICardTypeDao
    {
        private readonly string connectionString;
        private GetObjectFromReader obj = new GetObjectFromReader();

        public CardTypeSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public List<CardType> GetAllCardTypes()
        {
            List<CardType> returnTypes = new List<CardType>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM card_types", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CardType tempType = obj.GetCardTypeFromReader(reader);
                            returnTypes.Add(tempType);
                        }
                    }
                }

            }
            catch (SqlException e)
            {
                throw e;
            }
            return returnTypes;
        }

        public CardType GetCardTypeByName(string cardTypeName)
        {
            CardType returnType = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM card_types WHERE card_type_name LIKE @type", conn);
                    cmd.Parameters.AddWithValue("@type", cardTypeName);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        returnType = obj.GetCardTypeFromReader(reader);
                    }
                }

            }
            catch (SqlException e)
            {
                throw e;
            }
            return returnType;
        }

        public CardType GetCardTypeByID(int cardTypeID)
        {
            CardType returnType = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM card_types WHERE type_id = @type", conn);
                    cmd.Parameters.AddWithValue("@type", cardTypeID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        returnType = obj.GetCardTypeFromReader(reader);
                    }
                }

            }
            catch (SqlException e)
            {
                throw e;
            }
            return returnType;
        }
    
    }
}
