using Microsoft.AspNetCore.Mvc;
using SzoftArchHazi.Data;

namespace SzoftArchHazi.Api.Controllers
{
        [ApiController]
        [Route("[controller]")]
    
    public class ProfileController {

        [HttpGet("GetEmployeeProfile")]
        public EmployeeDTO? GetEmployeeProfile(int id)
        {
            ControllerUtils.FillRepos();
            foreach (Employee employee in EmployeeRepository.Employees)
            {
                if (employee.Id == id)
                {

                }
            }
            return null;
        }

        [HttpDelete("DeleteEmployeeProfile")]
        public void DeleteEmployeeProfile(int id)
        {
            ControllerUtils.FillRepos();
            Employee EmployeeToRemove = new();
            foreach (Employee employee in EmployeeRepository.Employees)
            { if (employee.Id == id)
                {
                    EmployeeToRemove = employee;
                }
            }
            EmployeeRepository.Employees.Remove(EmployeeToRemove);
        }

    }
}