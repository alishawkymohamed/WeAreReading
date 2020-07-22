using System;
using System.Collections.Generic;
using System.Linq;
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
            AuthTicketDTO AuthTicket = sessionService.GetAuthTicket(userName);
            if (AuthTicket != null)
                return AuthTicket;

            User AuthUser = userRepo.GetAll(x => x.Username.ToUpper() == userName.ToUpper()).FirstOrDefault();
            if (AuthUser != null)
            {
                AuthTicketDTO Result = new AuthTicketDTO()
                {
                    Email = AuthUser.Email,
                    FullName = AuthUser.FullName,
                    UserName = AuthUser.Username,
                    UserId = AuthUser.Id,
                    RoleId = AuthUser.UserRoles.FirstOrDefault()?.RoleId,
                    RoleName = AuthUser.UserRoles.FirstOrDefault()?.Role.Name,
                };

                //Using Sessions Cache to Save AuthTicket
                sessionService.SetAuthTicket(Result.UserName, Result);
                return Result;
            }
            return null;
        }

        public UserDTO GetByUserName(string username)
        {
            try
            {
                var user = this.userRepo.Get(x => x.Username == username);
                var userDto = this.mapper.Map<UserDTO>(user);
                return userDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        public string GetUserName(int userId)
        {
            return this.FindUser(userId).Username;
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
