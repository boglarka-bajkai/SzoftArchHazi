using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using SzoftArchHazi.Common.Services;
using SzoftArchHazi.Common.Services.Contracts;
using SzoftArchHazi.Frontend;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44339") }); 
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
