using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class TagSqlDao : ITagDao
    {
        private readonly string connectionString;
        private GetObjectFromReader obj = new GetObjectFromReader();

        public TagSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Tag> GetAllTags()
        {
            List<Tag> returnTags = new List<Tag>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM tag_types", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Tag tempTag = obj.GetTagFromReader(reader);
                            returnTags.Add(tempTag);
                        }
                    }
                }

            }
            catch (SqlException e)
            {
                throw e;
            }
            return returnTags;
        }

        public Tag GetTagByName(string tagName)
        {
            Tag returnTag = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM tag_types WHERE tag_name LIKE @tag", conn);
                    cmd.Parameters.AddWithValue("@tag", tagName);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        returnTag = obj.GetTagFromReader(reader);
                    }
                }

            }
            catch (SqlException e)
            {
                throw e;
            }
            return returnTag;
        }

        public Tag GetTagByID(int tagID)
        {
            Tag returnTag = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM tag_types WHERE tag_id = @tag", conn);
                    cmd.Parameters.AddWithValue("@tag", tagID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        returnTag = obj.GetTagFromReader(reader);
                    }
                }

            }
            catch (SqlException e)
            {
                throw e;
            }
            return returnTag;
        }
    }
}
