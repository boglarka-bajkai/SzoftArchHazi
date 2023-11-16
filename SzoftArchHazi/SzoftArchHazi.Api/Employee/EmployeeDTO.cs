namespace SzoftArchHazi.Api;

public class EmployeeDTO {

    public string Email { get; set; }

    public string Name { get; set; }

    public List<OnDutyDTO> Duties { get; set; } = new();

}