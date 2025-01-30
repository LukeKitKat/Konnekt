using Konnect.Service.BaseServices;
using Konnect.Service.ServerNavigator;
using Konnect.Service.UserManager;
using Konnekt.Client.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddServerSideBlazor();
builder.Services.AddRazorPages();

builder.Services.AddScoped<ServerNavigator, ServerNavigator>();
builder.Services.AddScoped<CryptoManager, CryptoManager>();
builder.Services.AddScoped<UserManager, UserManager>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapRazorPages();

app.Run();


