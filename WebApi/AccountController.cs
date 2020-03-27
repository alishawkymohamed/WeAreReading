using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DbModels;
using Models.DTOs;
using Newtonsoft.Json.Linq;
using Services.Contracts;

namespace WebApi
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserService usersService;
        private readonly ITokenStoreService tokenStoreService;

        public AccountController(IUserService usersService,
            ITokenStoreService tokenStoreService)
        {
            this.usersService = usersService;
            this.tokenStoreService = tokenStoreService;
        }


        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(200, Type = typeof(AccessTokenDTO))]
        public IActionResult Login([FromBody] UserLoginDTO loginUser)
        {
            if (loginUser == null)
            {
                return BadRequest("User is not set.");
            }

            Models.DbModels.User user = usersService.FindUserPassword(loginUser.Username, loginUser.Password);
            if (user == null)
            {
                return Unauthorized("InvalidCredentials");
            }

            (string accessToken, string refreshToken, System.Collections.Generic.IEnumerable<Claim> claims) = tokenStoreService.CreateJwtTokens(user, refreshTokenSource: null);
            return Ok(new AccessTokenDTO { access_token = accessToken, refresh_token = refreshToken });
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(200, Type = typeof(AccessTokenDTO))]
        public IActionResult RefreshToken([FromBody]JToken jsonBody)
        {
            string refreshToken = jsonBody.Value<string>("refreshToken");
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return BadRequest("refreshToken is not set.");
            }

            UserToken token = tokenStoreService.FindToken(refreshToken);
            if (token == null)
            {
                return Unauthorized();
            }

            (string accessToken, string newRefreshToken, System.Collections.Generic.IEnumerable<Claim> claims) = tokenStoreService.CreateJwtTokens(token.User, refreshToken);

            return Ok(new AccessTokenDTO { access_token = accessToken, refresh_token = newRefreshToken });
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public bool Logout(string refreshToken)
        {
            ClaimsIdentity claimsIdentity = User.Identity as ClaimsIdentity;
            string userIdValue = claimsIdentity.FindFirst(ClaimTypes.UserData)?.Value;

            // The Jwt implementation does not support "revoke OAuth token" (logout) by design.
            // Delete the user's tokens from the database (revoke its bearer token)
            tokenStoreService.RevokeUserBearerTokens(userIdValue, refreshToken);

            //_signalRServices.SignOut(User.Identity.Name);
            return true;
        }

        [HttpGet("[action]")]
        public bool IsAuthenticated()
        {
            return User.Identity.IsAuthenticated;
        }

        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(AuthTicketDTO))]
        [Authorize]
        public IActionResult GetUserAuthTicket()
        {
            ClaimsIdentity claimsIdentity = User.Identity as ClaimsIdentity;
            string Username = claimsIdentity.Name;
            AuthTicketDTO AuthTicket = usersService.GetAuthDTO(Username);
            return Ok(AuthTicket != null ? AuthTicket : null);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [AllowAnonymous]
        public IActionResult Register([FromBody]RegisterUserDTO registerUSerDTO)
        {
            if (ModelState.IsValid && registerUSerDTO.Password == registerUSerDTO.ConfirmPassword)
            {
                if (usersService.RegisterUser(registerUSerDTO))
                {
                    return Ok(true);
                }
            }

            return BadRequest(ModelState);
        }
    }
}
