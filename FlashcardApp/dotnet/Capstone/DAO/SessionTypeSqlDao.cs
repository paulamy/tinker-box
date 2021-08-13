using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class SessionTypeSqlDao : ISessionTypeDao
    {
        private readonly string connectionString;
        private GetObjectFromReader obj = new GetObjectFromReader();

        public SessionTypeSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public List<SessionType> GetAllSessionTypes()
        {
            List<SessionType> returnTypes = new List<SessionType>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM session_types", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            SessionType tempType = obj.GetSessionTypeFromReader(reader);
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

        public SessionType GetSessionTypeByName(string sessionTypeName)
        {
            SessionType returnType = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM session_types WHERE session_type_name LIKE @type", conn);
                    cmd.Parameters.AddWithValue("@type", sessionTypeName);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        returnType = obj.GetSessionTypeFromReader(reader);
                    }
                }

            }
            catch (SqlException e)
            {
                throw e;
            }
            return returnType;
        }

        public SessionType GetSessionTypeByID(int sessionTypeID)
        {
            SessionType returnType = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM session_types WHERE session_type_id = @type", conn);
                    cmd.Parameters.AddWithValue("@type", sessionTypeID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        returnType = obj.GetSessionTypeFromReader(reader);
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
