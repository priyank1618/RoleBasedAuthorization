using RoleBasedAuthorization.Modals;

namespace RoleBasedAuthorization.Interface
{
    public interface IEmployee
    {

        public List<Employee> GetEmployees();

        public Employee AddEmployee(Employee employee);
    }
}
