using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class CategorySqlDao : ICategoryDao
    {
        private readonly string connectionString;
        private GetObjectFromReader obj = new GetObjectFromReader();

        public CategorySqlDao(string dbConnectionstring)
        {
            connectionString = dbConnectionstring;
        }

        public List<Category> GetCategories()
        {
            List<Category> returnCategories = new List<Category>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM category_types", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Category tempCategory = obj.GetCategoryFromReader(reader);
                            returnCategories.Add(tempCategory);
                        }
                    }
                }

            }
            catch (SqlException e)
            {
                throw e;
            }
            return returnCategories;
        }

        public Category GetCategoryByID(int categoryID)
        {
            Category returnCategory = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM category_types WHERE category_id = @category", conn);
                    cmd.Parameters.AddWithValue("@category", categoryID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        returnCategory = obj.GetCategoryFromReader(reader);
                    }
                }

            }
            catch (SqlException e)
            {
                throw e;
            }
            return returnCategory;
        }

        public Category GetCategoryByName(string name)
        {
            Category returnCategory = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM category_types WHERE category_name = @category", conn);
                    cmd.Parameters.AddWithValue("@category", name);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        returnCategory = obj.GetCategoryFromReader(reader);
                    }
                }

            }
            catch (SqlException e)
            {
                throw e;
            }
            return returnCategory;
        }

    }
}
