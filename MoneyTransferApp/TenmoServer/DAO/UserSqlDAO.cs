using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TenmoServer.Models;
using TenmoServer.Security;
using TenmoServer.Security.Models;

namespace TenmoServer.DAO
{
    public class UserSqlDAO : IUserDAO
    {
        private readonly string connectionString;
        const decimal startingBalance = 1000;

        public UserSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public User GetUser(string username)
        {
            User returnUser = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT user_id, username, password_hash, salt FROM users WHERE username = @username", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        returnUser = GetUserFromReader(reader);
                    }
                    if (username == null || returnUser == null || returnUser.Username != username)
                    {
                        return null;
                    }

                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnUser;
        }
        
        public DisplayUser GetUserById(int id)
        {
            DisplayUser returnUser = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT user_id, username FROM users WHERE user_id = @userId", conn);
                    cmd.Parameters.AddWithValue("@userId", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        returnUser = GetDisplayUserFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnUser;
        }

        public List<DisplayUser> GetUsers()
        {
            List<DisplayUser> returnUsers = new List<DisplayUser>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT user_id, username FROM users", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            DisplayUser u = GetDisplayUserFromReader(reader);
                            returnUsers.Add(u);
                        }

                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnUsers;
        }

        public User AddUser(string username, string password)
        {
            IPasswordHasher passwordHasher = new PasswordHasher();
            PasswordHash hash = passwordHasher.ComputeHash(password);

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO users (username, password_hash, salt) VALUES (@username, @password_hash, @salt)", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password_hash", hash.Password);
                    cmd.Parameters.AddWithValue("@salt", hash.Salt);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SELECT @@IDENTITY", conn);
                    int userId = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd = new SqlCommand("INSERT INTO accounts (user_id, balance) VALUES (@userid, @startBalance)", conn);
                    cmd.Parameters.AddWithValue("@userid", userId);
                    cmd.Parameters.AddWithValue("@startBalance", startingBalance);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return GetUser(username);
        }

        private User GetUserFromReader(SqlDataReader reader)
        {
            User u = new User()
            {
                UserId = Convert.ToInt32(reader["user_id"]),
                Username = Convert.ToString(reader["username"]),
                PasswordHash = Convert.ToString(reader["password_hash"]),
                Salt = Convert.ToString(reader["salt"]),
            };

            return u;
        }
        private DisplayUser GetDisplayUserFromReader(SqlDataReader reader)
        {
            DisplayUser u = new DisplayUser()
            {
                UserId = Convert.ToInt32(reader["user_id"]),
                Username = Convert.ToString(reader["username"])
            };

            return u;
        }
    }
}
