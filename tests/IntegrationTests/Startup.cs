using Microsoft.Extensions.DependencyInjection;
using Application;
using Infrastructure;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
/*
builder.Services.AddDbContext<DoveDbContext>(options =>
{
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    options.UseSqlite(builder.Configuration.GetConnectionString("DatabaseConnection"),
        b => b.MigrationsAssembly(typeof(DoveDbContext).Assembly.FullName));
});
*/

builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseHsts();


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");;

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    //options.RoutePrefix = string.Empty;
});

app.Run();