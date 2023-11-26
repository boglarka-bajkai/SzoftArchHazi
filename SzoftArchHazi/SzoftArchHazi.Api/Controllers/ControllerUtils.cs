namespace SzoftArchHazi.Api.Controllers;

using System.Data.Common;
using SzoftArchHazi.Data;

public class ControllerUtils {
    public static void FillRepos()
    {
        DbProviderFactories.RegisterFactory("System.Data.SqlClient", System.Data.SqlClient.SqlClientFactory.Instance);
        SzoftArchContext context = new SzoftArchContext();
        EmployeeRepository.Context = context;

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