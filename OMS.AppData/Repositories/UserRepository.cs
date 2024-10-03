using OMS.DataAccess.Repositories.AppRepositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using OMS.Helpers;
using OMS.Core.Models.User;
using OMS.SqlData.Model;
using System.Linq;
using AutoMapper;
using OMS.Core.Models.Account;

namespace OMS.SqlData.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        IMapper Mapper;

        //Constructor
        public UserRepository(IMapper mapper)
        {
            Mapper = mapper;
            _connectionString = DbHelper.Connection;
        }

        //Public Data Access Methods Implementation
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
        public IEnumerable<IUser> GetAll()
        {
            throw new NotImplementedException();
        }
        public IUser GetById(int id)
        {
            IUser user = null;
            using(var context = new OMSContext() )
            {
                var entity =  context.Users.Where(u=> u.UserID == id).FirstOrDefault();
                if(entity == null)
                {
                    return null;
                }
                user = Mapper.Map<IUser>(entity);
                return user;
            }
        }
        public bool UpdateUser(IUser user)
        {
            bool isUpdated = false;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Users SET Email = @email, " +
                    "Password = @Password", connection);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                connection.Open();
                command.ExecuteNonQuery();
                isUpdated = true;
            }
            return isUpdated;
        }
    }
}
