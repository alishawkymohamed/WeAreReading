using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Helpers.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models.DbModels;
using Models.DTOs;
using Models.HelperModels;
using Newtonsoft.Json.Linq;
using Services.Contracts;
using WebApi.ViewModels;

namespace WebApi
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserService usersService;
        private readonly IFileService fileService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IOptions<AppSettings> appSettings;
        private readonly ITokenStoreService tokenStoreService;

        public AccountController(
            IUserService usersService,
            IFileService fileService,
            IWebHostEnvironment webHostEnvironment,
            IOptions<AppSettings> appSettings,
            ITokenStoreService tokenStoreService)
        {
            this.usersService = usersService;
            this.fileService = fileService;
            this.webHostEnvironment = webHostEnvironment;
            this.appSettings = appSettings;
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
        [ProducesResponseType(200, Type = typeof(void))]
        [Authorize]
        public IActionResult Logout(string refreshToken)
        {
            ClaimsIdentity claimsIdentity = User.Identity as ClaimsIdentity;
            string userIdValue = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // The Jwt implementation does not support "revoke OAuth token" (logout) by design.
            // Delete the user's tokens from the database (revoke its bearer token)
            tokenStoreService.RevokeUserBearerTokens(userIdValue, refreshToken);

            //_signalRServices.SignOut(User.Identity.Name);
            return Ok();
        }

        [HttpGet("[action]")]
        [Authorize]
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
        public IActionResult Register([FromBody]RegisterUserDTO registerUserDTO)
        {
            if (this.usersService.IsEmailExisted(registerUserDTO.Email))
                ModelState.AddModelError("Email", "This email is already exixted !!");

            if (this.usersService.IsPhoneExisted(registerUserDTO.PhoneNumber))
                ModelState.AddModelError("PhoneNumber", "This phone number is already exixted !!");

            if (this.usersService.IsUserNameExisted(registerUserDTO.Username))
                ModelState.AddModelError("Username", "This username is already exixted !!");

            if (ModelState.IsValid)
            {
                usersService.RegisterUser(registerUserDTO);
                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(200, Type = typeof(string))]
        [AllowAnonymous]
        public async Task<IActionResult> UploadUserImage(UploadImageViewModel uploadImageViewModel)
        {
            if (ModelState.IsValid)
            {
                string path = webHostEnvironment.WebRootPath + appSettings.Value.FileSettings.UserProfileImages;
                string imageId = await fileService.WriteImage(uploadImageViewModel.Photo, path);
                return Ok(new UploadImageResponseDTO
                {
                    ImageId = imageId
                });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(UserDTO))]
        //[Authorize]
        public async Task<IActionResult> GetUserDetails(int userId)
        {
            var username = this.usersService.GetUserName(userId);
            var user = this.usersService.GetByUserName(username);
            return Ok(user);
        }
    }
}
