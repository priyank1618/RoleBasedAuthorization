namespace RoleBasedAuthorization.Modals
{
    public class User
    {

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public string Password { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }

    }
}
