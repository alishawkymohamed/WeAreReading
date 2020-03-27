using System;
using System.Collections.Generic;
using System.Security.Claims;
using Models.DbModels;
using Services.Contracts;

namespace Services.Implementation
{
    public class TokenStoreService : ITokenStoreService
    {
        public void AddUserToken(UserToken userToken)
        {
            throw new NotImplementedException();
        }

        public void AddUserToken(User user, string refreshToken, string accessToken, string refreshTokenSource)
        {
            throw new NotImplementedException();
        }

        public (string accessToken, string refreshToken, IEnumerable<Claim> Claims) CreateJwtTokens(User user, string refreshTokenSource)
        {
            throw new NotImplementedException();
        }

        public void DeleteExpiredTokensAsync()
        {
            throw new NotImplementedException();
        }

        public void DeleteToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public void DeleteTokensWithSameRefreshTokenSource(string refreshTokenIdHashSource)
        {
            throw new NotImplementedException();
        }

        public UserToken FindToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public void InvalidateUserTokens(int userId)
        {
            throw new NotImplementedException();
        }

        public bool IsValidToken(string accessToken, int userId)
        {
            throw new NotImplementedException();
        }

        public void RevokeUserBearerTokens(string userIdValue, string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
