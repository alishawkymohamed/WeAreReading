using System.Collections.Generic;

namespace Models.DTOs
{
    public class AuthTicketDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public string DefaultCulture { get; set; }
        public IEnumerable<UserRoleDTO> UserRoles { get; set; }
    }
}
