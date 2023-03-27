using FatRepository.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace FatRepository.Implementions
{
    internal class FatUnitOfWork<TDbContext> : IFatUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;

        public FatUnitOfWork(TDbContext dbContext) => _dbContext = dbContext;

        public ChangeTracker ChangeTracker { get => _dbContext.ChangeTracker; }

        public int Commit() => _dbContext.SaveChanges();
        public Task<int> CommitAsync() => _dbContext.SaveChangesAsync();

        public IDbContextTransaction OpenTransaction() => _dbContext.Database.BeginTransaction();
        public void CloseTransaction() => _dbContext.Database.CommitTransaction();
        public void RevertTransaction() => _dbContext.Database.RollbackTransaction();

        public Task<IDbContextTransaction> OpenTransactionAsync() => _dbContext.Database.BeginTransactionAsync();
        public Task CloseTransactionAsync() => _dbContext.Database.CommitTransactionAsync();
        public Task RevertTransactionAsync() => _dbContext.Database.RollbackTransactionAsync();


        public IExecutionStrategy CreateStrategy() => _dbContext.Database.CreateExecutionStrategy();
        public bool DatabaseCreate() => _dbContext.Database.EnsureCreated();
        public bool DatabaseDelete() => _dbContext.Database.EnsureDeleted();

        public Task<bool> DatabaseCreateAsync() => _dbContext.Database.EnsureCreatedAsync();
        public Task<bool> DatabaseDeleteAsync() => _dbContext.Database.EnsureDeletedAsync();
    }
}
