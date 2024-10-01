using OMS.Core.Models.Account;
using System.Collections.Generic;
using OMS.Helpers;
using System;
using System.Data.SqlClient;
using OMS.DataAccess.Repositories.AppRepositories;

namespace OMS.SqlData.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly string _connectionString;
        //Constructor
        public AccountRepository()
        {
            _connectionString = DbHelper.Connection;
        }

        //Public Data Access Methods Implementation
        public IEnumerable<IAccount> GetAll()
        {
            var accounts = new List<IAccount>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Account", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        accounts.Add(new Account
                        {
                            AccountID = (int)reader["AccountID"],
                            AccountName = (string)reader["AccountName"],
                            AccountNumber = (string)reader["AccountNumber"],
                            CreatedDate = (DateTime)reader["CreatedDate"]
                        });
                    }
                }
            }
            return accounts;
        }
        public IAccount GetById(int id)
        {
            IAccount account = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Account WHERE AccountID = @AccountID", connection);
                command.Parameters.AddWithValue("@AccountID", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        account = new Account
                        {
                            AccountID = (int)reader["AccountID"],
                            AccountName = (string)reader["AccountName"],
                            AccountNumber = (string)reader["AccountNumber"],
                            CreatedDate = (DateTime)reader["CreatedDate"]
                        };
                    }
                }
            }
            return account;
        }
       
    }
}
