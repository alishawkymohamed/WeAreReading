﻿using System;
using System.Collections.Generic;
using AutoMapper;
using Helpers.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using Models.DbModels;
using Models.DTOs;
using Models.HelperModels;
using Repos.Contracts;
using Services.Contracts;

namespace Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepo userRepo;
        private readonly IUserRoleRepo userRoleRepo;
        private readonly IMapper mapper;
        private readonly IOptions<AppSettings> appSettings;
        private readonly IEncryptionService encryptionService;
        private readonly ISessionService sessionService;

        public UserService(
            IUserRepo userRepo,
            IUserRoleRepo userRoleRepo,
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            IEncryptionService encryptionService,
            IHttpContextAccessor contextAccessor,
            ISessionService sessionService)
        {
            this.userRepo = userRepo;
            this.userRoleRepo = userRoleRepo;
            this.mapper = mapper;
            this.appSettings = appSettings;
            this.encryptionService = encryptionService;
            this.sessionService = sessionService;
        }
        public (bool Succeeded, string Error) ChangePassword(User user, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> Delete(IEnumerable<object> ids)
        {
            throw new NotImplementedException();
        }

        public User FindUser(int userId)
        {
            return this.userRepo.Get(x => x.Id == userId);
        }

        public bool ValidateUserPassword(string username, string password)
        {
            string passwordHash = encryptionService.EncryptString(password, appSettings.Value.EncryptionSettings.SecretPassword, appSettings.Value.EncryptionSettings.Salt);
            var user = this.userRepo.Get(x => x.Username.ToUpper() == username.ToUpper() || x.Email.ToUpper() == username.ToUpper());
            return user != null && (user.Password.ToUpper() == passwordHash.ToUpper());
        }

        public AuthTicketDTO GetAuthDTO(string userName)
        {
            throw new NotImplementedException();
        }

        public UserDTO GetByUserName(string username)
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

        public void RegisterUser(RegisterUserDTO registerUserDTO)
        {
            var user = this.mapper.Map<User>(registerUserDTO);
            this.userRepo.Insert(user);

            this.userRoleRepo.Insert(new UserRole
            {
                RoleId = registerUserDTO.RoleId,
                UserId = user.Id
            });
        }

        public void UpdateUserLastActivityDate(int userId)
        {
            User user = FindUser(userId);
            if (user.LastLoggedIn != null)
            {
                TimeSpan updateLastActivityDate = TimeSpan.FromMinutes(20);
                DateTime currentUtc = DateTime.Now;
                TimeSpan timeElapsed = currentUtc.Subtract(user.LastLoggedIn.Value);
                if (timeElapsed < updateLastActivityDate)
                {
                    return;
                }
            }
            user.LastLoggedIn = DateTime.Now;
            //_userRepository.Update(user);
        }

        public bool IsUserNameExisted(string username)
        {
            return this.userRepo.GetAll(x => x.Username == username).Any();
        }

        public bool IsPhoneExisted(string phone)
        {
            return this.userRepo.GetAll(x => x.PhoneNumber == phone).Any();
        }

        public bool IsEmailExisted(string email)
        {
            return this.userRepo.GetAll(x => x.Email == email).Any();
        }
    }
}
