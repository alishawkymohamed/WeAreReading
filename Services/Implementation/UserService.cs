using System;
using System.Collections.Generic;
using Models.DbModels;
using Models.DTOs;
using Services.Contracts;

namespace Services.Implementation
{
    public class UserService : IUserService
    {
        public (bool Succeeded, string Error) ChangePassword(User user, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> Delete(IEnumerable<object> Ids)
        {
            throw new NotImplementedException();
        }

        public User FindUser(int userId)
        {
            throw new NotImplementedException();
        }

        public User FindUserPassword(string username, string password)
        {
            throw new NotImplementedException();
        }

        public AuthTicketDTO GetAuthDTO(string userName)
        {
            throw new NotImplementedException();
        }

        public UserDTO GetByUserName(string Username)
        {
            throw new NotImplementedException();
        }

        public User GetCurrentUser()
        {
            throw new NotImplementedException();
        }

        public int GetCurrentUserId()
        {
            throw new NotImplementedException();
        }

        public string GetSerialNumber(int userId)
        {
            throw new NotImplementedException();
        }

        public string GetUserName(int? userId)
        {
            throw new NotImplementedException();
        }

        public bool RegisterUser(RegisterUserDTO registerUSerDTO)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserLastActivityDate(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
