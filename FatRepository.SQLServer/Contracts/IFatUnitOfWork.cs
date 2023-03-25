using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace FatRepository.SQLServer.Contracts
{
    public interface IFatUnitOfWork
    {
        ChangeTracker ChangeTracker { get; }

        void CloseTransaction();
        Task CloseTransactionAsync();
        int Commit();
        Task<int> CommitAsync();
        IExecutionStrategy CreateStrategy();
        bool DatabaseCreate();
        Task<bool> DatabaseCreateAsync();
        bool DatabaseDelete();
        Task<bool> DatabaseDeleteAsync();
        IDbContextTransaction OpenTransaction();
        Task<IDbContextTransaction> OpenTransactionAsync();
        void RevertTransaction();
        Task RevertTransactionAsync();
    }
}