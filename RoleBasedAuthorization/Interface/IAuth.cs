using RoleBasedAuthorization.Modals;
using RoleBasedAuthorization.ViewModal;

namespace RoleBasedAuthorization.Interface
{
    public interface IAuth
    {

        public User AddUser(User user);

        public Role AddRoles(Role roles);

        //pass viewmodel in it 
        public bool AssignRole(AssignRoles assignRoles);


        //this login will return the jwt token 
        public string Login(LoginRequest loginRequest);
    
    }
}
