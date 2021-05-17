using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Application.Common.Interfaces;
using Infrastructure.File;
using Infrastructure.Identity;
using Infrastructure.Identity.Interfaces;
using Infrastructure.Identity.Jwt;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddScoped<IUserManager, UserManagerService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddDbContext<UserIdentityContext>(options =>
                                                        options.UseSqlServer(configuration.GetConnectionString("UserIdentityConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<UserIdentityContext>()
                .AddDefaultTokenProviders();


            //services.AddDefaultIdentity<ApplicationUser>()
            //        .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddTransient<ITokenModel, TokenModel>();
            services.AddTransient<IJwtFactory, JwtFactory>();
            services.AddTransient(typeof(IFileManager), typeof(FileManager));

            if (environment.IsDevelopment())
            {
                var scopeFactory = services
                    .BuildServiceProvider()
                    .GetRequiredService<IServiceScopeFactory>();
                using (var scope = scopeFactory.CreateScope())
                {
                    var provider = scope.ServiceProvider;
                    SeedDB.InitializeAsync(provider).Wait();
                }

                //SeedDB.Initialize(configuration.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);


            }

            //services.AddAuthentication()
            // .AddIdentityServerJwt();
            var jwtTokenConfig = new JwtTokenConfig();
            configuration.Bind("jwtSetting", jwtTokenConfig);
            services.AddSingleton(jwtTokenConfig);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.SaveToken = true;
               options.RequireHttpsMetadata = false;
               options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
               {
                   ValidateIssuer = jwtTokenConfig.ValidateIssuer,
                   ValidateAudience = jwtTokenConfig.ValidateAudience,
                   ValidAudience = jwtTokenConfig.ValidAudience,
                   ValidIssuer = jwtTokenConfig.ValidIssuer,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenConfig.Secret)),
                   ValidateLifetime = jwtTokenConfig.ValidateLifetime,
               };
           });

            return services;

        }
    }
}
