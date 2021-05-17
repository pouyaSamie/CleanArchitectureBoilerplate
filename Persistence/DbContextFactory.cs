using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DbContextFactory : DesignTimeApplicationDbContextFactoryBase<ApplicationDbContext>
    {


        protected override ApplicationDbContext CreateNewInstance(DbContextOptions<ApplicationDbContext> options)
        {
            return new ApplicationDbContext(options);
        }
    }
}
