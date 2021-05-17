using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
     
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        public Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade Database { get; }
        public IDbContextTransaction BeginTransaction();
        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        public IDbContextTransaction CurrentTransaction { get; }
        public void CommitTransaction();
        public void RollbackTransaction();
        public void Update(object entity);
    }
}
