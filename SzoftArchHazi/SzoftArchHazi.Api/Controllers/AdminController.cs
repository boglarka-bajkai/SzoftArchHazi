using Microsoft.AspNetCore.Mvc;

namespace SzoftArchHazi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        
        private readonly ILogger<AdminController> _logger;
        
        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet("GetEmployees")]
        public IEnumerable<EmployeeDTO> GetEmployees()
        {
            ControllerUtils.FillRepos();
            List<EmployeeDTO> employees = new List<EmployeeDTO>();
            EmployeeRepository.Employees.ForEach(e => { employees.Add(ControllerUtils.CreateDTOFromEmployee(e)); });
            return employees;
        }

        [HttpGet("GetProjects")]
        public IEnumerable<Project> GetProjects()
        {
            ControllerUtils.FillRepos();
            return ProjectRepository.Projects;
        }

        [HttpPost("AddProject")]
        public void AddProject(Project newProject)
        {
            ControllerUtils.FillRepos();
            ProjectRepository.Projects.Add(newProject);
        }

        [HttpDelete("DeleteProject")]
        public void DeleteProject(int id) {
            ControllerUtils.FillRepos();
            Project ProjectToRemove = new();
            foreach (Project project in ProjectRepository.Projects)
            {
                if (project.Id == id)
                {
                    ProjectToRemove = project;
                }
            }
            ProjectRepository.Projects.Remove(ProjectToRemove);
            List<OnDuty> OnDutiesToRemove = new List<OnDuty>();
            List<int> DutiesToRemoveIds = new List<int>();
            foreach (OnDuty onDuty in OnDutyRepository.Duties)
            {
                if (onDuty.ProjectId == id) { 
                    OnDutiesToRemove.Add(onDuty);
                    DutiesToRemoveIds.Add(onDuty.Id);
                }
            }
            foreach (OnDuty onDuty in OnDutiesToRemove)
            {
                OnDutyRepository.Duties.Remove(onDuty);
            }
            List<OnDutyDate> OnDutyDatesToRemove = new List<OnDutyDate>();
            foreach (OnDutyDate onDutyDate in OnDutyDateRepository.OnDutyDates)
            {
                if (DutiesToRemoveIds.Contains(onDutyDate.Id))
                {
                    OnDutyDatesToRemove.Add(onDutyDate);
                }
            }
            foreach (OnDutyDate onDutyDate in OnDutyDatesToRemove)
            {
                OnDutyDateRepository.OnDutyDates.Remove(onDutyDate);
            }
        }
        
        [HttpGet("GetEmployeesForProject")]
        public IEnumerable<Employee> GetEmployeesForProject(int ProjectId)
        {
            ControllerUtils.FillRepos();
            List<int> indexes = new List<int>();
            for (int i = 0; i < OnDutyRepository.Duties.Count; ++i)
            {
                if (OnDutyRepository.Duties[i].ProjectId == ProjectId)
                {
                    indexes.Add(i);
                }
            }
            List<Employee> employees = new List<Employee>();
            foreach (int id in indexes)
            {
                employees.Add(EmployeeRepository.Employees[id]);
            }
            return employees;
        }

        [HttpGet("GetOnDutyDatesForProject")]
        public IEnumerable<OnDutyDate> GetOnDutyDatesForProject(int ProjectId)
        {
            ControllerUtils.FillRepos();
            List<int> onDuties = new List<int>();
            foreach (OnDuty duty in OnDutyRepository.Duties)
            {
                if (duty.ProjectId.Equals(ProjectId))
                {
                    onDuties.Add(duty.Id);
                }
            }
            List<OnDutyDate> onDutyDates = new List<OnDutyDate>();
            foreach (OnDutyDate dutyDate in OnDutyDateRepository.OnDutyDates)
            {
                if (onDuties.Contains(dutyDate.OnDutyId))
                {
                    onDutyDates.Add(dutyDate);
                }
            }
            return onDutyDates;
        }

        [HttpGet("GetOnDutiesForDate")]
        public IEnumerable<OnDutyDTO> GetOnDutiesForDate(DateTime date)
        {
            ControllerUtils.FillRepos();
            List<int> dutyIds = new List<int>();
            foreach (OnDutyDate dutyDate in OnDutyDateRepository.OnDutyDates)
            {
                if (dutyDate.DutyDay.Equals(date))
                {
                    dutyIds.Add(dutyDate.OnDutyId);
                }
            }
            List<OnDutyDTO> onDuties = new List<OnDutyDTO>();
            foreach (OnDuty onDuty in OnDutyRepository.Duties)
            {
                if (dutyIds.Contains(onDuty.Id))
                {
                    onDuties.Add(ControllerUtils.CreateDTOFromOnDuty(onDuty));
                }
            }
            return onDuties;

        }

    }
}