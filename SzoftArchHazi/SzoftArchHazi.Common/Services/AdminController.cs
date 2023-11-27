using System.Net.Http.Json;
using SzoftArchHazi.Common.Models;
using SzoftArchHazi.Common.Services.Contracts;

namespace SzoftArchHazi.Common.Services
{
	public class AdminService : IAdminService
	{
		private readonly HttpClient httpClient;

		public AdminService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}
		public async Task<IEnumerable<Project>> GetProjects()
		{
			try
			{
				var projects = await httpClient.GetFromJsonAsync<IEnumerable<Project>>("Admin/GetProjects");
				return projects;
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
