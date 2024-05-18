using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoleBasedAuthorization.Interface;
using RoleBasedAuthorization.Modals;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoleBasedAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "coder,Hr")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employee;


        public EmployeeController(IEmployee employee)
        {
            _employee = employee;
        }


        // GET: api/<EmployeeController>
        [HttpGet]
        public List<Employee> GetEmployee()
        {
           var list = _employee.GetEmployees();
            return list;
        }

       

        // POST api/<EmployeeController>
        [HttpPost]
        public Employee Addemployee([FromBody] Employee employee)
        {
            var employees = _employee.AddEmployee(employee);
            return employees;
        }

       
    }
}
