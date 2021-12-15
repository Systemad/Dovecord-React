using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using WebUI.Databases;
using WebUI.Seeders;
using WebUI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    // use default version when version is not specified
    config.AssumeDefaultVersionWhenUnspecified = true;
    // Advertise the API versions supported for the particular endpoint
    config.ReportApiVersions = true;
});

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});
builder.Services.AddAppAuthentication(builder.Configuration);
builder.Services.AddCorsService();
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment.IsProduction());
builder.Services.AddApplication();

builder.Services.AddOpenApiDocument(configure =>
{
    configure.DocumentName = "v1";
    configure.Version = "v1";
    configure.Title = "Dovecord API";
    configure.Description = "Backend API for Dovecord";
});
var app = builder.Build();

app.UseOpenApi();
app.UseSwaggerUi3();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
/*
if (app.Environment.IsDevelopment())
{
    using var context = app.Services.GetService<DoveDbContext>();
    context.Database.EnsureCreated();
    
    ChannelSeeder.SeedSampleChannels(app.Services.GetService<DoveDbContext>());
}
*/
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");;
app.Run();
