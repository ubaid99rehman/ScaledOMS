using OMS.Core.Core.Models.User;
using OMS.DataAccess.Repositories.AppRepositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.Helpers;
using OMS.Core.Enums;
using OMS.Core.Models;

namespace OMS.SqlData.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository()
        {
            _connectionString = DbHelper.Connection;
        }

        public bool AuthenticateUser(string username, string password, out string message, out int isDisabled, out int userID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SP_AuthenticateUser", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                var isAuthenticatedParam = new SqlParameter("@IsAuthenticated", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(isAuthenticatedParam);

                var isDisabledParam = new SqlParameter("@IsDisabled", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };

                command.Parameters.Add(isDisabledParam);

                var _userID = new SqlParameter("@UserId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                command.Parameters.Add(_userID);

                connection.Open();
                command.ExecuteNonQuery();

                bool isAuthenticated = (bool)isAuthenticatedParam.Value;
                bool userDisabled = (bool)isDisabledParam.Value;
                userID = (int)_userID.Value;

                if (userDisabled)
                {
                    isDisabled = 1;
                    message = ("User account is disabled due to multiple incorrect password attempts.");
                    return false;
                }
                else
                {
                    isDisabled = 0;
                }

                if (isAuthenticated)
                {
                    message = ("User Validated!");
                    return true;
                }
                else
                {
                    message = ("Invalid username or password.");
                    return false;
                }
            }
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            User user = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Users WHERE UserID = @OrderID", connection);
                command.Parameters.AddWithValue("@OrderID", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new User
                        {
                            UserID = (int)reader["UserID"],
                            UserName = reader["UserName"].ToString(),
                            Email = reader["Email"].ToString()
                        };
                    }
                }
            }
            return user;
        }
    }
}
