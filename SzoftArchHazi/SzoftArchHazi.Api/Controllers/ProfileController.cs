using Microsoft.AspNetCore.Mvc;

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

    }
}