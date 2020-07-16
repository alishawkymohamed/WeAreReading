using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Helpers.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.DbModels;
using Models.HelperModels;
using Repos.Contracts;
using Services.Contracts;

namespace Services.Implementation
{
    public class TokenStoreService : ITokenStoreService
    {
        private readonly IUserRepo userRepo;
        private readonly IUserTokenRepo userTokenRepo;
        private readonly IEncryptionService encryptionService;
        private readonly IOptions<AppSettings> appSettings;

        public TokenStoreService(
            IUserRepo userRepo,
            IUserTokenRepo userTokenRepo,
            IEncryptionService encryptionService,
            IOptions<AppSettings> appSettings)
        {
            this.userRepo = userRepo;
            this.userTokenRepo = userTokenRepo;
            this.encryptionService = encryptionService;
            this.appSettings = appSettings;
        }


        public void AddUserToken(User user, string refreshToken, string accessToken, string refreshTokenSource)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            UserToken token = new UserToken
            {
                UserId = user.Id,
                // Refresh token handles should be treated as secrets and should be stored hashed
                RefreshTokenIdHash = encryptionService.GetSha256Hash(refreshToken),
                RefreshTokenIdHashSource = string.IsNullOrWhiteSpace(refreshTokenSource) ?
                                           null : encryptionService.GetSha256Hash(refreshTokenSource),
                AccessTokenHash = encryptionService.GetSha256Hash(accessToken),
                RefreshTokenExpiresDateTime = now.AddMinutes(appSettings.Value.BearerTokensSettings.RefreshTokenExpirationMinutes),
                AccessTokenExpiresDateTime = now.AddMinutes(appSettings.Value.BearerTokensSettings.AccessTokenExpirationMinutes)
            };
            AddUserToken(token);
        }

        public (string accessToken, string refreshToken, IEnumerable<Claim> Claims) CreateJwtTokens(string userName, string refreshTokenSource)
        {
            var user = userRepo.Get(x => x.Username.ToUpper() == userName.ToUpper() || x.Email == userName.ToUpper());
            (string AccessToken, IEnumerable<Claim> Claims) = CreateAccessTokenAsync(user);
            string refreshToken = Guid.NewGuid().ToString().Replace("-", "");
            AddUserToken(user, refreshToken, AccessToken, refreshTokenSource);
            return (AccessToken, refreshToken, Claims);
        }

        public void DeleteExpiredTokens()
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            var ExpiredTokens = userTokenRepo.GetAll(x => x.RefreshTokenExpiresDateTime < now);
            if (ExpiredTokens != null && ExpiredTokens.Count() > 0)
                userTokenRepo.Delete(ExpiredTokens);
        }

        public void DeleteToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public void DeleteTokensWithSameRefreshTokenSource(string refreshTokenIdHashSource)
        {
            if (string.IsNullOrWhiteSpace(refreshTokenIdHashSource))
                return;

            IEnumerable<UserToken> ToBeDeletedTokens = userTokenRepo.GetAll(t => t.RefreshTokenIdHash == refreshTokenIdHashSource);
            if (ToBeDeletedTokens != null && ToBeDeletedTokens.Count() > 0)
                userTokenRepo.Delete(ToBeDeletedTokens);
        }

        public UserToken FindToken(string refreshToken)
        {
            string refreshTokenIdHash = encryptionService.GetSha256Hash(refreshToken);
            return userTokenRepo.Get(x => x.RefreshTokenIdHash.ToUpper() == refreshTokenIdHash.ToUpper());
        }

        public void InvalidateUserTokens(int userId)
        {
            var UserTokens = userTokenRepo.GetAll(x => x.UserId == userId);
            if (UserTokens != null && UserTokens.Count() > 0)
                userTokenRepo.Delete(UserTokens);
        }

        public bool IsValidToken(string accessToken, int userId)
        {
            string accessTokenHash = this.encryptionService.GetSha256Hash(accessToken);
            UserToken userToken = this.userTokenRepo.GetAll(x => x.AccessTokenHash == accessTokenHash && x.UserId == userId).FirstOrDefault();
            return userToken == null ? false : userToken.AccessTokenExpiresDateTime >= DateTimeOffset.UtcNow;
        }

        public void RevokeUserBearerTokens(string userIdValue, string refreshToken)
        {
            if (!string.IsNullOrWhiteSpace(userIdValue) && int.TryParse(userIdValue, out int userId))
            {
                if (appSettings.Value.BearerTokensSettings.AllowSignoutAllUserActiveClients)
                {
                    InvalidateUserTokens(userId);
                }
            }

            if (!string.IsNullOrWhiteSpace(refreshToken))
            {
                string refreshTokenIdHashSource = encryptionService.GetSha256Hash(refreshToken);
                DeleteTokensWithSameRefreshTokenSource(refreshTokenIdHashSource);
            }
            DeleteExpiredTokens();
        }


        private (string AccessToken, IEnumerable<Claim> Claims) CreateAccessTokenAsync(User user)
        {
            try
            {
                List<Claim> claims = new List<Claim>();
                // Unique Id for all Jwt tokes
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, appSettings.Value.BearerTokensSettings.Issuer));
                // Issuer
                claims.Add(new Claim(JwtRegisteredClaimNames.Iss, appSettings.Value.BearerTokensSettings.Issuer, ClaimValueTypes.String, appSettings.Value.BearerTokensSettings.Issuer));
                // Issued at
                claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(), ClaimValueTypes.Integer64, appSettings.Value.BearerTokensSettings.Issuer));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String, appSettings.Value.BearerTokensSettings.Issuer));
                claims.Add(new Claim(ClaimTypes.Name, user.Username, ClaimValueTypes.String, appSettings.Value.BearerTokensSettings.Issuer));
                claims.Add(new Claim("DisplayName", user.FullName, ClaimValueTypes.String, appSettings.Value.BearerTokensSettings.Issuer));
                // to invalidate the cookie
                claims.Add(new Claim(ClaimTypes.SerialNumber, user.SerialNumber, ClaimValueTypes.String, appSettings.Value.BearerTokensSettings.Issuer));

                // custom data
                //AuthTicketDTO authData = _userService.GetAuthDTO(user.Username);
                //claims.Add(new Claim(ClaimTypes.UserData, Newtonsoft.Json.JsonConvert.SerializeObject(authData), ClaimValueTypes.String, appSettings.Value.BearerTokensSettings.Issuer));


                // add current LoggedIn OrganizationId
                // add current LoggedIn RoleId

                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Value.BearerTokensSettings.Key));
                SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                DateTime now = DateTime.Now;
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: appSettings.Value.BearerTokensSettings.Issuer,
                    audience: appSettings.Value.BearerTokensSettings.Audience,
                    claims: claims,
                    notBefore: now,
                    expires: now.AddMilliseconds(appSettings.Value.BearerTokensSettings.AccessTokenExpirationMinutes * 60 * 1000),
                    signingCredentials: creds);
                return (new JwtSecurityTokenHandler().WriteToken(token), claims);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void AddUserToken(UserToken userToken)
        {
            if (!appSettings.Value.BearerTokensSettings.AllowMultipleLoginsFromTheSameUser)
            {
                InvalidateUserTokens(userToken.UserId);
            }
            DeleteTokensWithSameRefreshTokenSource(userToken.RefreshTokenIdHashSource);
            userTokenRepo.Insert(userToken);
        }

    }
}
