using Microsoft.AspNetCore.Components;
using SzoftArchHazi.Common.Models;
using SzoftArchHazi.Common.Services.Contracts;

namespace SzoftArchHazi.Frontend.Pages
{
    public class AdminPageBase : ComponentBase
    {
		  [Inject]
		  public IAdminService AdminService { get; set; }
		  public IEnumerable<Project>? Projects { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Projects = await AdminService.GetProjects();
        }
    }
}
