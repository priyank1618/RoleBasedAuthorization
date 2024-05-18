using RoleBasedAuthorization.DataContext;
using RoleBasedAuthorization.Interface;
using RoleBasedAuthorization.Modals;

namespace RoleBasedAuthorization.Repository
{
    public class EmployeeRepo : IEmployee
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepo(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public Employee AddEmployee(Employee employee)
        {
          var employees = _context.Employees.Add(employee);
           _context.SaveChanges();
           return employees.Entity;
        }

        public List<Employee> GetEmployees()
        {
            var employees = _context.Employees.ToList();
            return employees;
        }
    }
}
