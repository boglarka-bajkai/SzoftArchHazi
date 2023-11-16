using SzoftArchHazi.Api;

public class EmployeeRepository {

    public static List<Employee> Employees = new List<Employee>();

    private static readonly string[] FirstNames = new[]
    {
        "James", "John", "Sarah", "Nick", "Rebekah", "Elijah", "Stefan"
    };

    private static readonly string[] LastNames = new[]
    {
        "Smith", "Brookes", "Salvatore", "Ferguson", "Friers", "Runners", "Saint"
    };

    private static readonly string[] Passwords = new[]
    {
        "adhsfhdfg", "adfsfhgfj", "werwerw", "123xhfdfg", "dsfh3w4t", "djdjdf564dgd", "dsfdjk456gfdngh34"
    };

    public static void CreateEmployees()
    {
        for (int i = 0; i < FirstNames.Length; ++i)
        {
            Employee employee = new Employee();
            employee.Id = i;
            employee.Email = FirstNames[i] + "." + LastNames[i] + "@gmail.com";
            employee.Name = FirstNames[i] + " " + LastNames[i];
            employee.Password = Passwords[Random.Shared.Next(Passwords.Length)];
            employee.IsAdmin = false;
            Employees.Add(employee);
        }
    }

}