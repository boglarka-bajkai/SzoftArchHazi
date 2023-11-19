using SzoftArchHazi.Api;

public class ProjectRepository {

    public static List<Project> Projects = new List<Project>();

    private static readonly string[] Names = new[]
    {
        "SzoftArch", "Diploma", "Workplace", "OwnProject", "RandomProject", "RandomProject2"
    };

    private static readonly string[] Description = new[]
    {
        "desc1", "desc2", "desc3", "desc4", "desc5", "desc6"
    };

    private static readonly string[] StartDate = new[]
    {
        "9/1/2023 08:00:0 AM", "3/6/2023 08:00:0 AM", "9/1/2022 08:00:0 AM", "6/1/2023 08:00:0 AM", "11/2/2021 08:00:0 AM", "4/1/2022 08:00:0 AM" 
    };

    private static readonly string[] EndDate = new[]
    {
        "12/1/2023 08:00:0 AM", "12/6/2023 08:00:0 AM", "6/3/2024 08:00:0 AM", "12/12/2023 08:00:0 AM", "12/1/2023 08:00:0 AM", "12/2/2023 08:00:0 AM"
    };

    public static void CreateProjects()
    {
        for (int i = 0; i < Names.Length; i++)
        {
            Project project = new Project();
            project.Id = i;
            project.Name = Names[i];
            project.Description = Description[i];
            project.StartDate = DateTime.Parse(ProjectRepository.StartDate[i]);
            project.EndDate = DateTime.Parse(ProjectRepository.EndDate[i]);
            Projects.Add(project);
        }
    }
}