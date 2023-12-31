using Microsoft.AspNetCore.Mvc;
using SzoftArchHazi.Data;

namespace SzoftArchHazi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController
    {
        [HttpPost("RegisterEmployee")]
        public void RegisterEmployee(Employee employee)
        {
            ControllerUtils.FillRepos();
            EmployeeRepository.Employees.Add(employee);
        }
    }
}