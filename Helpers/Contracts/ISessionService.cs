using Microsoft.AspNetCore.Http;
using Models.DTOs;

namespace Helpers.Contracts
{
    public interface ISessionService
    {
        HttpContext HttpContext { get; set; }
        int? UserId { get; }
        string UserName { get; }
        int? RoleId { get; }
        //string MachineName { get; }
        //string MachineIP { get; }
        //string Browser { get; }
        //string Url { get; }
        void SetAuthTicket(string username, AuthTicketDTO authTicket);
        AuthTicketDTO GetAuthTicket(string username);
    }
}
