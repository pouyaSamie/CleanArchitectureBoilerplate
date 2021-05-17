using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Infrastructure.Identity;
using System;
using System.Threading.Tasks;

namespace Api.IntegrationTest.Common
{
    public class FakeUserManager : UserManager<ApplicationUser>
    {
        public FakeUserManager()
            : base(new Mock<IUserStore<ApplicationUser>>().Object,
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<IPasswordHasher<ApplicationUser>>().Object,
                  new IUserValidator<ApplicationUser>[0],
                  new IPasswordValidator<ApplicationUser>[0],
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<IServiceProvider>().Object,
                  new Mock<ILogger<UserManager<ApplicationUser>>>().Object)
        {



        }

        public override Task<ApplicationUser> FindByNameAsync(string userName)
        {
            //This will compile
            var user = new ApplicationUser
            {
                Email = "pouya.samie@gmail.com",
                FirstName = "pouya",
                UserName = "pouya.samie@gmail.com",
                Id = 1
            };
            return Task.FromResult(user);
        }

        public override Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {

            return Task.FromResult(user.UserName == "pouya.samie@gmail.com" && password == "123qwe!@#QWE!");

        }



    }
}
