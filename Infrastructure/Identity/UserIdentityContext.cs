using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace Infrastructure.Identity
{
    public class UserIdentityContext : IdentityDbContext<ApplicationUser,ApplicationRole,long>
    {
        public UserIdentityContext(DbContextOptions<UserIdentityContext> options)
                                   : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
