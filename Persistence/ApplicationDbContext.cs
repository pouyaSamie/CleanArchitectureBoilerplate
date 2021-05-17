using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Application.Common.Interfaces;
using Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {

        private readonly ICurrentUserService _currentUserService;


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {

        }


        public ApplicationDbContext(
         DbContextOptions<ApplicationDbContext> options,
         ICurrentUserService currentUserService)
         : base(options)
        {
            this.Database.EnsureCreated();
            _currentUserService = currentUserService;

        }

           public override DatabaseFacade Database => base.Database;

        #region Overwritten methods
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreateBy = _currentUserService.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastUpdateBy = _currentUserService.UserId;
                        entry.Entity.LastUpdateDate = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        public IDbContextTransaction BeginTransaction()
        {
            return this.Database.BeginTransaction();
        }
        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return this.Database.BeginTransactionAsync();
        }
        public IDbContextTransaction CurrentTransaction
        {
            get
            {
                return this.Database.CurrentTransaction;
            }
        }
        public void CommitTransaction()
        {
            this.Database.CommitTransaction();
        }
        public void RollbackTransaction()
        {
            this.RollbackTransaction();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        void IApplicationDbContext.Update(object entity)
        {
            this.Update(entity);
        }

        #endregion


    }
}
