using System.ComponentModel.DataAnnotations;

namespace RoleBasedAuthorization.Modals
{
    public class Roles
    {
        [Key]
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public string RoleDescription { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
