using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RoleBasedAuthorization.Modals
{
    public class UserRole
    {

        public int Id { get; set; }


        public int RoleId { get; set; }

        public int UserId { get; set; }


        [JsonIgnore]
        public Role Role { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
