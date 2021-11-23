using System;
using System.IO;
using System.Threading.Tasks;
using Infrastructure.Persistance;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace IntegrationTests;

[SetUpFixture]
public class Testing
{
    private static IConfigurationRoot _configuration = null!;
    private static IServiceScopeFactory _scopeFactory = null!;
        private readonly ServiceProvider _provider;
        private readonly string DbName = Guid.NewGuid() + ".db";
        

        [OneTimeSetUp]
        public void Setup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();
            
            _configuration = builder.Build();
            
            //var startup = new Startup(_configuration);

            var services = new ServiceCollection();
            
            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "Development" &&
                w.ApplicationName == "CleanArchitecture.WebUI"));

            services.AddLogging();
            
            //startup.ConfigureServices(services);
            
            _scopeFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();
            
            EnsureDatabase();
        }
        
        private static void EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<DoveDbContext>();

            context.Database.Migrate();
        }
        
        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            return await mediator.Send(request);
        }

        public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<DoveDbContext>();

            return await context.FindAsync<TEntity>(keyValues);
        }
        
        public static async Task AddAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<DoveDbContext>();

            context.Add(entity);

            await context.SaveChangesAsync();
        }
        
        public static async Task<int> CountAsync<TEntity>() where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<DoveDbContext>();

            return await context.Set<TEntity>().CountAsync();
        }
}