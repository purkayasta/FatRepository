using FatRepository.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace FatRepository.Implementations
{
	internal class FatDatabase<TDbContext> : IFatDatabase<TDbContext> where TDbContext : DbContext
	{
		private readonly TDbContext _dbContext;

		public FatDatabase(TDbContext dbContext) => _dbContext = dbContext;

		public ChangeTracker ChangeTracker { get => _dbContext.ChangeTracker; }

		public int Commit() => _dbContext.SaveChanges();
		public Task<int> CommitAsync(CancellationToken cancellationToken = default) => _dbContext.SaveChangesAsync(cancellationToken);


		public IDbContextTransaction OpenTransaction() => _dbContext.Database.BeginTransaction();
		public Task<IDbContextTransaction> OpenTransactionAsync(CancellationToken cancellationToken = default) => _dbContext.Database.BeginTransactionAsync(cancellationToken);


		public void CloseTransaction() => _dbContext.Database.CommitTransaction();
		public Task CloseTransactionAsync(CancellationToken cancellationToken = default) => _dbContext.Database.CommitTransactionAsync(cancellationToken);


		public void RevertTransaction() => _dbContext.Database.RollbackTransaction();
		public Task RevertTransactionAsync(CancellationToken cancellationToken = default) => _dbContext.Database.RollbackTransactionAsync(cancellationToken);


		public IExecutionStrategy CreateStrategy() => _dbContext.Database.CreateExecutionStrategy();


		public bool DatabaseCreate() => _dbContext.Database.EnsureCreated();
		public Task<bool> DatabaseCreateAsync(CancellationToken cancellationToken = default) => _dbContext.Database.EnsureCreatedAsync(cancellationToken);

		public bool DatabaseDelete() => _dbContext.Database.EnsureDeleted();
		public Task<bool> DatabaseDeleteAsync(CancellationToken cancellationToken = default) => _dbContext.Database.EnsureDeletedAsync(cancellationToken);
	}
}
