using Microsoft.Extensions.DependencyInjection;
using Application.Common.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class SeedDB
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<UserIdentityContext>();
            var userManager = serviceProvider.GetRequiredService<IUserManager>();
            try
            {

                // var x = context.Database.EnsureDeleted();
                var y = context.Database.EnsureCreated();
            }
            catch (Exception)
            {

                throw;
            }


            if (!context.Users.Any())
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Email = "pouya.samie@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "pouya.samie@gmail.com"
                };
                var result = await userManager.CreateUserAsync("pouya.samie@gmail.com", "123qwe!@#QWE");
                //return  userManager2.CreateAsync(user, "123qwe!@#QWE");
            }

        }
    }
}
