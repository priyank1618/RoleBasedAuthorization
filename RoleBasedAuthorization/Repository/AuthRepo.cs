using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using RoleBasedAuthorization.DataContext;
using RoleBasedAuthorization.Interface;
using RoleBasedAuthorization.Modals;
using RoleBasedAuthorization.ViewModal;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace RoleBasedAuthorization.Repository
{
    public class AuthRepo : IAuth
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepo(ApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            _context = applicationDbContext;
            _configuration = configuration;
        }

        public Role AddRoles(Role roles)
        {
            var role = _context.Roles.Add(roles);
            _context.SaveChanges();
            return role.Entity;
        }

        public User AddUser(User user)
        {
            var users = _context.Users.Add(user);
            _context.SaveChanges();
            return users.Entity;
        }


        //how assign the roles
        //roles can be multiple for the single user

        public bool AssignRole(AssignRoles assignRoles)
        {

            try
            {
                //make an obj of the user role that will added to the db
                //make one list and then added to the db

                var list = new List<UserRole>();

                //now check wheather the user with that user id present or not
                var user = _context.Users.FirstOrDefault(s => s.UserId == assignRoles.UserID);

                if (user == null)
                {
                    throw new Exception("user is not valid");
                }

                foreach (var role in assignRoles.RoleID)
                {
                    UserRole userRole = new UserRole();
                    userRole.RoleId = role;
                    userRole.UserId = assignRoles.UserID;
                    list.Add(userRole);

                }

                _context.AddRange(list);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
               
                return false;
            }

        }

        public string Login(LoginRequest loginRequest)
        {
            //first check the credentials are not null

            if (loginRequest.Email != null && loginRequest.Password != null)
            {
                //check user is present or not
                var user = _context.Users.FirstOrDefault(s => s.UserEmail == loginRequest.Email && s.Password == loginRequest.Password);

                if (user != null)
                {
                    //now it time to make jwt
                    //lets make some claims first that would be claims

                    var claims = new List<Claim> {

                        new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
                        new Claim("Id",user.UserId.ToString()),
                        new Claim("Email",user.UserEmail)
                    };

                    //now its time to add the roles in the jwt token
                    //the assigned roles to the user will be in the userroles tables

                    //we have user that have userid and also userrole has the user id
                    // so take the list of the userid

                    var userwithsameuserid = _context.UserRoles.Where(s => s.UserId == user.UserId).ToList();

                    //get the roles id particular with attached with the userid
                    var roleidlist = userwithsameuserid.Select(s => s.RoleId).ToList();

                    //now we have the list of the roleids and we also have roleid in the role table
                    //we can get the name of the roles from the role table using the roleidlist


                    //this will give us the list from role table that is match with role table
                    var rolelist = _context.Roles.Where(s => roleidlist.Contains(s.RoleId)).ToList();


                    foreach (var item in rolelist)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item.RoleName));
                    }
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn);
                    return new JwtSecurityTokenHandler().WriteToken(token);
                }
                else
                {
                    throw new Exception("User is not valid");
                }
            }
            else
            {
                throw new Exception("Credentials are not valid");
            }


        }
    }
}
