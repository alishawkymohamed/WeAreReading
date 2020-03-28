using System.Collections.Generic;
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
            if (ModelState.IsValid)
            {
                if (!usersService.ValidateUserPassword(loginUser.Username, loginUser.Password))
                    return Unauthorized("Invalid Credentials");

                (string accessToken, string refreshToken, IEnumerable<Claim> claims) = tokenStoreService.CreateJwtTokens(loginUser.Username, refreshTokenSource: null);
                return Ok(new AccessTokenDTO { access_token = accessToken, refresh_token = refreshToken });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(200, Type = typeof(AccessTokenDTO))]
        public IActionResult RefreshToken([FromQuery]string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                return BadRequest("refreshToken is not set.");

            UserToken token = tokenStoreService.FindToken(refreshToken);
            if (token == null)
                return Unauthorized();

            (string accessToken, string newRefreshToken, IEnumerable<Claim> claims) = tokenStoreService.CreateJwtTokens(token.User.Username, refreshToken);

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
        [ProducesResponseType(200, Type = typeof(void))]
        [AllowAnonymous]
        public IActionResult Register([FromBody]RegisterUserDTO registerUSerDTO)
        {
            if (ModelState.IsValid)
            {
                usersService.RegisterUser(registerUSerDTO);
                return Ok();
            }

            return BadRequest(ModelState);
        }
    }
}
