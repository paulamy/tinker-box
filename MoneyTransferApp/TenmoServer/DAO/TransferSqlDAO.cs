using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;


namespace TenmoServer.DAO
{
    public class TransferSqlDAO : ITransferDAO
    {
        private readonly string connectionString;

        public TransferSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Transfer> GetTransfers(int userId)
        {
            List<Transfer> returnTransfers = new List<Transfer>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT transfer_id, transfers.transfer_type_id, transfers.transfer_status_id, account_from, account_to, amount, " +
                        "transfer_type_desc, transfer_status_desc FROM transfers " +
                        "JOIN accounts on account_from = account_id OR account_to = account_id JOIN users on accounts.user_id = users.user_id " +
                        "JOIN transfer_statuses ON transfers.transfer_status_id = transfer_statuses.transfer_status_id " +
                        "JOIN transfer_types ON transfers.transfer_type_id = transfer_types.transfer_type_id " +
                        "WHERE users.user_id = @userId;", conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Transfer t = GetTransferFromReader(reader);
                            returnTransfers.Add(t);
                        }

                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnTransfers;
        }


        public List<Transfer> GetPendingTransfers(int userId)
        {
            List<Transfer> returnTransfers = new List<Transfer>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT transfer_id, transfers.transfer_type_id, transfers.transfer_status_id, account_from, account_to, amount, " +
                        "transfer_type_desc, transfer_status_desc FROM transfers " +
                        "JOIN accounts on account_from = account_id JOIN users on accounts.user_id = users.user_id " +
                        "JOIN transfer_statuses ON transfers.transfer_status_id = transfer_statuses.transfer_status_id " +
                        "JOIN transfer_types ON transfers.transfer_type_id = transfer_types.transfer_type_id " +
                        "WHERE users.user_id = @userId AND transfers.transfer_status_id = 1;", conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Transfer t = GetTransferFromReader(reader);
                            returnTransfers.Add(t);
                        }

                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnTransfers;
        }

        public Transfer GetTransfer(int userId, int id)
        {
            Transfer returnTransfer = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT transfer_id, transfers.transfer_type_id, transfers.transfer_status_id, account_from, account_to, amount, " +
                        "transfer_type_desc, transfer_status_desc FROM transfers " +
                        "JOIN accounts on account_from = account_id OR account_to = account_id JOIN users on accounts.user_id = users.user_id " +
                        "JOIN transfer_statuses ON transfers.transfer_status_id = transfer_statuses.transfer_status_id " +
                        "JOIN transfer_types ON transfers.transfer_type_id = transfer_types.transfer_type_id " +
                       "WHERE users.user_id = @userId AND transfer_id = @transferId;", conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@transferId", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows && reader.Read())
                    {

                        returnTransfer = GetTransferFromReader(reader);

                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnTransfer;
        }

        public Transfer CreateTransfer(int userId, Transfer transfer)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    if (transfer.TransferStatusId == 2)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO transfers (transfer_type_id, transfer_status_id, account_from, account_to, amount) " +
                          "VALUES (@type, @status, @from, @to, @amount) " +
                          "UPDATE accounts SET balance += @amount WHERE account_id = @to " +
                          "UPDATE accounts SET balance -= @amount WHERE account_id = @from", conn);
                        cmd.Parameters.AddWithValue("@type", transfer.TransferTypeId);
                        cmd.Parameters.AddWithValue("@status", transfer.TransferStatusId);
                        cmd.Parameters.AddWithValue("@from", transfer.AccountFrom);
                        cmd.Parameters.AddWithValue("@to", transfer.AccountTo);
                        cmd.Parameters.AddWithValue("@amount", transfer.Amount);
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("SELECT @@IDENTITY", conn);
                        int newId = Convert.ToInt32(cmd.ExecuteScalar());

                        transfer.TransferId = newId;
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO transfers (transfer_type_id, transfer_status_id, account_from, account_to, amount) " +
                          "VALUES (@type, @status, @from, @to, @amount)", conn);
                        cmd.Parameters.AddWithValue("@type", transfer.TransferTypeId);
                        cmd.Parameters.AddWithValue("@status", transfer.TransferStatusId);
                        cmd.Parameters.AddWithValue("@from", transfer.AccountFrom);
                        cmd.Parameters.AddWithValue("@to", transfer.AccountTo);
                        cmd.Parameters.AddWithValue("@amount", transfer.Amount);
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("SELECT @@IDENTITY", conn);
                        int newId = Convert.ToInt32(cmd.ExecuteScalar());

                        transfer.TransferId = newId;

                    }
                    return transfer;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }


        public Transfer UpdateTransferStatus(int userId, Transfer transfer)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    if (transfer.TransferStatusId == 2)
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE transfers SET transfer_status_id = @status WHERE transfer_id = @id " +
                        "UPDATE accounts SET balance += @amount WHERE account_id = @to " +
                        "UPDATE accounts SET balance -= @amount WHERE account_id = @from", conn);
                        cmd.Parameters.AddWithValue("@id", transfer.TransferId);
                        cmd.Parameters.AddWithValue("@status", transfer.TransferStatusId);
                        cmd.Parameters.AddWithValue("@amount", transfer.Amount);
                        cmd.Parameters.AddWithValue("@from", transfer.AccountFrom);
                        cmd.Parameters.AddWithValue("@to", transfer.AccountTo);

                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE transfers SET transfer_status_id = @status WHERE transfer_id = @id" , conn);
                        cmd.Parameters.AddWithValue("@id", transfer.TransferId);
                        cmd.Parameters.AddWithValue("@status", transfer.TransferStatusId);

                        cmd.ExecuteNonQuery();
                    }

                    return transfer;
                }
            }
            catch (SqlException)
            {
                throw;
            }
            
        }

        

        public Transfer GetTransferFromReader(SqlDataReader reader)
        {
            Transfer t = new Transfer()
            {
                TransferId = Convert.ToInt32(reader["transfer_id"]),
                TransferTypeId = Convert.ToInt32(reader["transfer_type_id"]),
                TransferStatusId = Convert.ToInt32(reader["transfer_status_id"]),
                AccountFrom = Convert.ToInt32(reader["account_from"]),
                AccountTo = Convert.ToInt32(reader["account_to"]),
                Amount = Convert.ToDecimal(reader["amount"]),
                TransferTypeDescription = Convert.ToString(reader["transfer_type_desc"]),
                TransferStatusDescription = Convert.ToString(reader["transfer_status_desc"])
            };
            return t;
        }
       
        
    }
}
