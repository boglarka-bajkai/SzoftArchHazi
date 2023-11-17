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

        // GET requests

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

        // POST requests

        [HttpPost("AddProject")]
        public void AddProject(Project newProject)
        {
            ControllerUtils.FillRepos();
            ProjectRepository.Projects.Add(newProject);
        }

        // PUT requests

        [HttpPut("UpdateEmployee")]
        public void UpdateEmployee(int Id, string? Name, string? Email, string? Password)
        {
            ControllerUtils.FillRepos();
            foreach (Employee employee in EmployeeRepository.Employees)
            {
                if (employee.Id == Id)
                {
                    if (Name != null)
                    {
                        employee.Name = Name;
                    }
                    if (Email != null)
                    {
                        employee.Email = Email;
                    }
                    if (Password != null)
                    {
                        employee.Password = Password;
                    }
                }
            }
        }

        [HttpPut("UpdateProject")]
        public void UpdateProject(int Id, string? Name, string? Description, DateTime? StartDate, DateTime? EndDate)
        {
            ControllerUtils.FillRepos();
            foreach (Project project in ProjectRepository.Projects)
            {
                if (project.Id == Id)
                {
                    if (Name != null)
                    {
                        project.Name = Name;
                    }
                    if (Description != null)
                    {
                        project.Description = Description;
                    }
                    if (StartDate != null)
                    {
                        project.StartDate = StartDate.Value;
                    }
                    if (EndDate != null)
                    {
                        project.EndDate = EndDate.Value;
                    }
                }
            }
        }

        [HttpPut("FixOnDutyDate")]
        public void FixOnDutyDate(int OnDutyId, DateTime OnDutyDate)
        {
            ControllerUtils.FillRepos();
            foreach (OnDutyDate onDutyDate in OnDutyDateRepository.OnDutyDates)
            {
                if (onDutyDate.OnDutyId == OnDutyId && onDutyDate.DutyDay == OnDutyDate)
                {
                    onDutyDate.IsFixed = true;
                }
            }
        }

        // DELETE requests

        [HttpDelete("DeleteProject")]
        public void DeleteProject(int id)
        {
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
                if (onDuty.ProjectId == id)
                {
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

        [HttpDelete("RemoveEmployeeFromProject")]
        public void RemoveEmployeeFromProject(int EmployeeId, int ProjectId)
        {
            ControllerUtils.FillRepos();
            List<OnDuty> OnDutiesToRemove = new List<OnDuty>();
            List<int> OnDutiesToRemoveIds = new List<int>();
            foreach (OnDuty onDuty in OnDutyRepository.Duties)
            {
                if (onDuty.EmployeeId == EmployeeId && onDuty.ProjectId == ProjectId)
                {
                    OnDutiesToRemove.Add(onDuty);
                    OnDutiesToRemoveIds.Add(onDuty.Id);
                }
            }
            foreach (OnDuty onDuty in OnDutiesToRemove)
            {
                OnDutyRepository.Duties.Remove(onDuty);
            }
            List<OnDutyDate> OnDutyDatesToRemove = new List<OnDutyDate>();
            foreach (OnDutyDate onDutyDate in OnDutyDateRepository.OnDutyDates)
            {
                if (OnDutiesToRemoveIds.Contains(onDutyDate.OnDutyId))
                {
                    OnDutyDatesToRemove.Add(onDutyDate);
                }
            }
            foreach (OnDutyDate onDutyDate in OnDutyDatesToRemove)
            {
                OnDutyDateRepository.OnDutyDates.Remove(onDutyDate);
            }
        }

        [HttpDelete("RemoveOnDutyForProject")]
        public void RemoveOnDutyForProject(int OnDutyId, DateTime OnDutyDay)
        {
            ControllerUtils.FillRepos();
            OnDutyDate OnDutyDateToRemove = new();
            foreach (OnDutyDate onDutyDate in OnDutyDateRepository.OnDutyDates)
            {
                if (onDutyDate.DutyDay == OnDutyDay && onDutyDate.OnDutyId == OnDutyId)
                {
                    OnDutyDateToRemove = onDutyDate;
                }
            }
            OnDutyDateRepository.OnDutyDates.Remove(OnDutyDateToRemove);
        }
    }
}