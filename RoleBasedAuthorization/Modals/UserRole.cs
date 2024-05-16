namespace RoleBasedAuthorization.Modals
{
    public class UserRole
    {

        public int Id { get; set; }


        public int RoleId { get; set; }

        public int UserId { get; set; }

        public Roles Role { get; set; }

        public User User { get; set; }
    }
}
