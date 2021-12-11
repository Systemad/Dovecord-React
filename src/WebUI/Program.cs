using WebUI.Databases;
using WebUI.Seeders;
using WebUI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});
builder.Services.AddCorsService();
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment.IsProduction());
builder.Services.AddApplication();

//builder.Services.AddSwaggerDocument();
builder.Services.AddOpenApiDocument(configure =>
{
    configure.Title = "Dovecord API";
});
//builder.Services.AddSwaggerExtension();

var app = builder.Build();

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
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowAll");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");;

app.UseOpenApi();
app.UseSwaggerUi3();
app.Run();
