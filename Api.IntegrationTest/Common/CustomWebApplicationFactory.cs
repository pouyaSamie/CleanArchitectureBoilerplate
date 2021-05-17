using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Application.Common.Interfaces;
using Infrastructure.Identity.Jwt;
using Persistence;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.IntegrationTest.Common
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
               .ConfigureServices(services =>
               {
                   var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));


                   if (descriptor != null)
                       services.Remove(descriptor);



                   // Create a new service provider.
                   var serviceProvider = new ServiceCollection()
                       .AddEntityFrameworkInMemoryDatabase()
                       .BuildServiceProvider();

                   // Add a database context using an in-memory 
                   // database for testing.
                   services.AddDbContext<ApplicationDbContext>(options =>
                  {
                      options.UseInMemoryDatabase("InMemoryDbForTesting");
                      options.UseInternalServiceProvider(serviceProvider);
                  });

                   services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
                   var sp = services.BuildServiceProvider();

                   // Create a scope to obtain a reference to the database
                   using var scope = sp.CreateScope();
                   var scopedServices = scope.ServiceProvider;
                   var context = scopedServices.GetRequiredService<ApplicationDbContext>();
                   var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                   // Ensure the database is created.
                   context.Database.EnsureCreated();

                   try
                   {
                       // Seed the database with test data.
                       Utilities.InitializeDbForTests(context);
                   }
                   catch (Exception ex)
                   {
                       logger.LogError(ex, "An error occurred seeding the " +
                                           $"database with test messages. Error: {ex.Message}");
                   }
               })
               .UseEnvironment("Test");

        }

        public HttpClient GetAnonymousClient()
        {
            return CreateClient();
        }

        public async Task<HttpClient> GetAuthenticatedClientAsync()
        {
            return await GetAuthenticatedClientAsync("pouya.samie@gmail.com", "123qwe!@#QWE!");
        }

        public async Task<HttpClient> GetAuthenticatedClientAsync(string userName, string password)
        {
            var client = CreateClient();

            var token = await GetAccessTokenAsync(client, userName, password);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            return client;
        }

        private async Task<string> GetAccessTokenAsync(HttpClient client, string userName, string password)
        {
            var jwt = new JwtFactory(new FakeUserManager(), Utilities.GetFakeJwtConfig());
            var jwtResult = await jwt.GetTokenAsync(userName, password);
            if (!jwtResult.IsSuccess)
                throw new Exception(jwtResult.Errors[0]);

            return jwtResult.Data.Token;


        }

    }
}
