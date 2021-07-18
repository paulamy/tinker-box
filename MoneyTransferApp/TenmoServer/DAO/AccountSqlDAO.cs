using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class AccountSqlDAO : IAccountDAO
    {
        private readonly string connectionString;

        public AccountSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public PrivateAccount GetAccount(int id)
        {
            PrivateAccount returnAccount = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT account_id, user_id, balance FROM accounts WHERE user_id = @userId", conn);
                    cmd.Parameters.AddWithValue("@userId", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        returnAccount = GetPrivateAccountFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnAccount;
        }

        public List<PublicAccount> GetAccounts()
        {
            List<PublicAccount> returnAccounts = new List<PublicAccount>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT account_id, accounts.user_id, username FROM accounts JOIN users ON users.user_id = accounts.user_id", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            PublicAccount a = GetPublicAccountFromReader(reader);
                            returnAccounts.Add(a);
                        }

                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnAccounts;
        }

        private PrivateAccount GetPrivateAccountFromReader(SqlDataReader reader)
        {
            PrivateAccount a = new PrivateAccount()
            {
                AccountId = Convert.ToInt32(reader["account_id"]),
                UserId = Convert.ToInt32(reader["user_id"]),
                Balance = Convert.ToDecimal(reader["balance"]),
            };

            return a;
        }

        private PublicAccount GetPublicAccountFromReader(SqlDataReader reader)
        {
            PublicAccount a = new PublicAccount()
            {
                AccountId = Convert.ToInt32(reader["account_id"]),
                UserId = Convert.ToInt32(reader["user_id"]),
                Username = Convert.ToString(reader["username"]),
            };

            return a;
        }
    }
}

