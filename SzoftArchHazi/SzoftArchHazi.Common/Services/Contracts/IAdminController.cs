using SzoftArchHazi.Common.Models;

namespace SzoftArchHazi.Common.Services.Contracts
{
	public interface IAdminService
	{
		Task<IEnumerable<Project>> GetProjects();
	}
}
