namespace SzoftArchHazi.Api.Controllers;

public class ControllerUtils {
    public static void FillRepos()
    {
        if (EmployeeRepository.Employees.Count == 0)
        {
            EmployeeRepository.CreateEmployees();
        }
        if (ProjectRepository.Projects.Count == 0)
        {
            ProjectRepository.CreateProjects();
        }
        if (OnDutyRepository.Duties.Count == 0)
        {
            OnDutyRepository.CreateDuties();
        }
        if (OnDutyDateRepository.OnDutyDates.Count == 0)
        {
            OnDutyDateRepository.CreateOnDutyDays();
        }
    }

    public static EmployeeDTO CreateDTOFromEmployee(Employee employee)
    {
        EmployeeDTO employeeDTO = new EmployeeDTO();
        List<OnDutyDTO> onDutyDTOs = new List<OnDutyDTO>();
        foreach (OnDuty duty in employee.Duties)
        {
            onDutyDTOs.Add(CreateDTOFromOnDuty(duty));
        };
        employeeDTO.Email = employee.Email;
        employeeDTO.Duties = onDutyDTOs;
        employeeDTO.Name = employee.Name;
        return employeeDTO;
    }

    public static OnDutyDTO CreateDTOFromOnDuty(OnDuty duty)
    {
        OnDutyDTO onDutyDTO = new OnDutyDTO();
        onDutyDTO.Id = duty.Id;
        onDutyDTO.ProjectId = duty.ProjectId;
        onDutyDTO.EmployeeId = duty.EmployeeId;
        return onDutyDTO;
    }
}