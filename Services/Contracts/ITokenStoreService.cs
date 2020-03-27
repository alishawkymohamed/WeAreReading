using System.Collections.Generic;
using System.Security.Claims;
using Models.DbModels;

namespace Services.Contracts
{
    public interface ITokenStoreService : IService
    {
        void AddUserToken(UserToken userToken);
        void AddUserToken(User user, string refreshToken, string accessToken, string refreshTokenSource);
        bool IsValidToken(string accessToken, int userId);
        void DeleteExpiredTokensAsync();
        UserToken FindToken(string refreshToken);
        void DeleteToken(string refreshToken);
        void DeleteTokensWithSameRefreshTokenSource(string refreshTokenIdHashSource);
        void InvalidateUserTokens(int userId);
        (string accessToken, string refreshToken, IEnumerable<Claim> Claims) CreateJwtTokens(User user, string refreshTokenSource);
        void RevokeUserBearerTokens(string userIdValue, string refreshToken);
    }
}
