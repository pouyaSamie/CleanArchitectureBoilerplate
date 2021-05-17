using Microsoft.EntityFrameworkCore;
using Infrastructure.Identity;

namespace Persistence
{
    public class UserIdentityDbContextFactory : DesignTimeIdentityDbContextFactoryBase<UserIdentityContext>
    {
        protected override UserIdentityContext CreateNewInstance(DbContextOptions<UserIdentityContext> options)
        {
            return new UserIdentityContext(options);
        }
    }
}
