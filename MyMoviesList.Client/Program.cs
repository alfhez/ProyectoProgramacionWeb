using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyMoviesList.Client;
using MyMoviesList.Client.Auth;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7025/") });
//

builder.Services.AddScoped(sp => new HttpClient
{BaseAddress = new Uri("https://mymovieslist20231203181715.azurewebsites.net")});
//builder.Services.AddScoped<ILoginService, ProveedorAutenticacion>();


builder.Services.AddScoped<ProveedorAutenticacion>();

builder.Services.AddScoped<AuthenticationStateProvider, ProveedorAutenticacion>(proveedor =>
proveedor.GetRequiredService<ProveedorAutenticacion>());


builder.Services.AddScoped<ILoginService, ProveedorAutenticacion>(proveedor =>
proveedor.GetRequiredService<ProveedorAutenticacion>());

await builder.Build().RunAsync();
