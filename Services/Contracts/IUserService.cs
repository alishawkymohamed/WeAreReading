﻿using System.Collections.Generic;
using Models.DbModels;
using Models.DTOs;

namespace Services.Contracts
{
    public interface IUserService : IService<User>
    {
        string GetSerialNumber(int userId);
        bool ValidateUserPassword(string username, string password);
        User FindUser(int userId);
        void UpdateUserLastActivityDate(int userId);
        User GetCurrentUser();
        IEnumerable<object> Delete(IEnumerable<object> ids);
        int GetCurrentUserId();
        void RegisterUser(RegisterUserDTO registerUSerDTO);
        (bool Succeeded, string Error) ChangePassword(User user, string currentPassword, string newPassword);
        AuthTicketDTO GetAuthDTO(string userName);
        string GetUserName(int userId);
        UserDTO GetById(int userId);
        UserDTO GetByUserName(string username);
        bool IsUserNameExisted(string username);
        bool IsPhoneExisted(string phone);
        bool IsEmailExisted(string email);
    }
}
