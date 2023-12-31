using System.Data.Entity;
using SzoftArchHazi.Data;

public class OnDutyRepository {

    public static List<OnDuty> Duties = new List<OnDuty>();

    private static readonly int[] EmployeeIds = new[]
    {
        0, 1, 2, 2, 2, 3, 4, 5, 6
    };

    private static readonly int[] ProjectIds = new[]
    {
        0, 1, 0, 1, 2, 1, 2, 2, 2
    };

    public static void CreateDuties()
    {
        for (int i = 0; i < ProjectIds.Length; i++)
        {
            OnDuty duty = new OnDuty();
            duty.Id = i;
            duty.ProjectId = ProjectIds[i];
            duty.EmployeeId = EmployeeIds[i];
            Duties.Add(duty);
            EmployeeRepository.Employees[EmployeeIds[i]].Duties.Add(duty);
            ProjectRepository.Projects[ProjectIds[i]].Duties.Add(duty);
        }
    }

    public static void CreateDutiesInDb(SzoftArchContext db)
    {
        for (int i = 0; i < ProjectIds.Length; i++)
        {
            OnDuty duty = new OnDuty();
            duty.ProjectId = ProjectIds[i];
            duty.EmployeeId = EmployeeIds[i];
            db.OnDuties.Add(duty);
            var employee = db.Employees.SingleOrDefault(x => x.Id == EmployeeIds[i]);
            var project = db.Projects.SingleOrDefault(x => x.Id == ProjectIds[i]);
            employee.Duties.Add(duty);
            project.Duties.Add(duty);
            db.SaveChanges();
        }
    }

}