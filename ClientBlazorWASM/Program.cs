using Blazored.LocalStorage;
using ClientBlazorWASM;
using ClientBlazorWASM.Services;
using ClientBlazorWASM.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// D.I interfaces
builder.Services.AddScoped<IPostsService, PostsService>();
builder.Services.AddScoped<IServiceAuthentication, ServiceAuthentication>();

// local storage do navegador
builder.Services.AddBlazoredLocalStorage();

// para autenticação e autorização
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<AuthStateProvider>());

await builder.Build().RunAsync();
