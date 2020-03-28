using System;
using System.Security.Claims;
using Helpers.Contracts;
using Microsoft.AspNetCore.Http;
using Models.DTOs;
using Newtonsoft.Json;

namespace Helpers.Implementation
{
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IEncryptionService encryptionServices;

        public SessionService(IHttpContextAccessor httpContextAccessor, IEncryptionService encryptionServices)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.encryptionServices = encryptionServices;
        }

        public HttpContext HttpContext
        {
            get
            {
                return httpContextAccessor.HttpContext;
            }
            set
            {
                httpContextAccessor.HttpContext = value;
            }
        }

        public int? UserId
        {
            get
            {
                if (HttpContext.User == null)
                {
                    return null;
                }

                Claim claim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (claim == null)
                {
                    return null;
                }

                return int.Parse(claim.Value);
            }
        }

        public string UserName
        {
            get
            {
                if (HttpContext.User == null || HttpContext.User.Identity == null)
                {
                    return null;
                }

                return HttpContext.User.Identity.Name;
            }
        }

        public int? RoleId
        {
            get
            {
                if (HttpContext.User == null)
                {
                    return null;
                }

                Claim claim = HttpContext.User.FindFirst("RoleId");
                if (claim == null)
                {
                    return null;
                }

                return int.Parse(claim.Value);
            }
        }


        public void SetAuthTicket(string username, AuthTicketDTO authTicket)
        {
            HttpContext.Session.SetString(username.ToUpper(), JsonConvert.SerializeObject(authTicket));
        }

        public AuthTicketDTO GetAuthTicket(string username)
        {
            string Auth = HttpContext.Session.GetString(username.ToUpper());
            if (Auth != null)
            {
                return JsonConvert.DeserializeObject<AuthTicketDTO>(Auth);
            }
            else
            {
                return null;
            }
        }

        #region Private Methods

        private T GetClaim<T>(string key, T defaultValue = default(T))
        {
            T result = defaultValue;
            string value = HttpContext.User.HasClaim(x => x.Type == key) ? HttpContext.User.FindFirst(key).Value : null;
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    Type t = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
                    result = (T)Convert.ChangeType(value, t);
                }
                catch
                {
                    result = defaultValue;
                }
            }
            return result;
        }

        #endregion Private Methods
    }
}
