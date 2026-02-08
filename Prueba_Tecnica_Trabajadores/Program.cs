using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica_Trabajadores.Models;
using Prueba_Tecnica_Trabajadores.Repositories;
using Prueba_Tecnica_Trabajadores.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TrabajadoresPruebaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL")));

builder.Services.AddScoped<ITrabajadorRepository, TrabajadorRepository>();
builder.Services.AddScoped<ITrabajadorService, TrabajadorService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Trabajador}/{action=Index}/{id?}");

app.Run();