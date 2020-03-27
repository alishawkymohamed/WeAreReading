using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Services.Contracts;

namespace Services.Implementation
{
    public class TokenValidatorService : ITokenValidatorService
    {
        private readonly IUserService userService;
        private readonly ITokenStoreService tokenStoreService;

        public TokenValidatorService(IUserService userService, ITokenStoreService tokenStoreService)
        {
            this.userService = userService;
            this.tokenStoreService = tokenStoreService;
        }

        public void Validate(TokenValidatedContext context)
        {
            ClaimsPrincipal userPrincipal = context.Principal;

            ClaimsIdentity claimsIdentity = context.Principal.Identity as ClaimsIdentity;
            if (claimsIdentity?.Claims == null || !claimsIdentity.Claims.Any())
            {
                context.Fail("This is not our issued token. It has no claims.");
                return;
            }

            Claim serialNumberClaim = claimsIdentity.FindFirst(ClaimTypes.SerialNumber);
            if (serialNumberClaim == null)
            {
                context.Fail("This is not our issued token. It has no serial.");
                return;
            }

            string userIdString = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (!int.TryParse(userIdString, out int userId))
            {
                context.Fail("This is not our issued token. It has no user-id.");
                return;
            }

            Models.DbModels.User user = userService.FindUser(userId);
            if (user == null || user.SerialNumber != serialNumberClaim.Value || user.IsDeleted)
            {
                // user has changed his/her password/roles/stat/IsActive
                context.Fail("This token is expired. Please login again.");
            }

            JwtSecurityToken accessToken = context.SecurityToken as JwtSecurityToken;
            if (accessToken == null || string.IsNullOrWhiteSpace(accessToken.RawData) ||
                !tokenStoreService.IsValidToken(accessToken.RawData, userId))
            {
                context.Fail("This token is not in our database.");
                return;
            }

            userService.UpdateUserLastActivityDate(userId);
        }
    }
}
