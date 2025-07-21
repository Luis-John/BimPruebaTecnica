using BIM.PruebaTecnica.AppMVC.Models;
using BIM.PruebaTecnica.AppMVC.Services;
using BIM.PruebaTecnica.AppMVC.Services.Localidad;
using BIM.PruebaTecnica.AppMVC.Services.Usuarios;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<UsuariosServices>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5158/");
});

builder.Services.AddHttpClient<LocalidadServices>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5158/");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuarios}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();
