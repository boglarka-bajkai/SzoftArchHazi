using System.ComponentModel.DataAnnotations.Schema;
using SzoftArchHazi.Api;

public class OnDuty {

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int ProjectId { get; set; }
    public Employee Employee { get; set; } = null!;
    public Project Project { get; set; } = null!;
}