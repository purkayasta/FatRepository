using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace FatRepository.Contracts
{
    /// <summary>
    /// Access control for DbContext instance.
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IFatUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        /// <summary>
        ///     Provides access to information and operations for entity instances this context is tracking.
        /// </summary>
        /// <remarks>
        ///     See <see href="https://aka.ms/efcore-docs-change-tracking">EF Core change tracking</see> for more information.
        /// </remarks>
        ChangeTracker ChangeTracker { get; }


        /// <summary>
        ///     <para>
        ///         Saves all changes made in this context to the database.
        ///     </para>
        ///     <para>
        ///         This method will automatically call <see cref="ChangeTracker.DetectChanges" /> to discover any
        ///         changes to entity instances before saving to the underlying database. This can be disabled via
        ///         <see cref="ChangeTracker.AutoDetectChangesEnabled" />.
        ///     </para>
        ///     <para>
        ///         Entity Framework Core does not support multiple parallel operations being run on the same DbContext instance. This
        ///         includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
        ///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
        ///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more information.
        ///     </para>
        /// </summary>
        /// <remarks>
        ///     See <see href="https://aka.ms/efcore-docs-saving-data">Saving data in EF Core</see> for more information.
        /// </remarks>
        /// <returns>
        ///     The number of state entries written to the database.
        /// </returns>
        /// <exception cref="DbUpdateException">
        ///     An error is encountered while saving to the database.
        /// </exception>
        /// <exception cref="DbUpdateConcurrencyException">
        ///     A concurrency violation is encountered while saving to the database.
        ///     A concurrency violation occurs when an unexpected number of rows are affected during save.
        ///     This is usually because the data in the database has been modified since it was loaded into memory.
        /// </exception>
        int Commit();

        /// <summary>
        ///     <para>
        ///         Saves all changes made in this context to the database.
        ///     </para>
        ///     <para>
        ///         This method will automatically call <see cref="ChangeTracker.DetectChanges" /> to discover any
        ///         changes to entity instances before saving to the underlying database. This can be disabled via
        ///         <see cref="ChangeTracker.AutoDetectChangesEnabled" />.
        ///     </para>
        ///     <para>
        ///         Entity Framework Core does not support multiple parallel operations being run on the same DbContext instance. This
        ///         includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
        ///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
        ///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more information.
        ///     </para>
        /// </summary>
        /// <remarks>
        ///     See <see href="https://aka.ms/efcore-docs-saving-data">Saving data in EF Core</see> for more information.
        /// </remarks>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>
        ///     A task that represents the asynchronous save operation. The task result contains the
        ///     number of state entries written to the database.
        /// </returns>
        /// <exception cref="DbUpdateException">
        ///     An error is encountered while saving to the database.
        /// </exception>
        /// <exception cref="DbUpdateConcurrencyException">
        ///     A concurrency violation is encountered while saving to the database.
        ///     A concurrency violation occurs when an unexpected number of rows are affected during save.
        ///     This is usually because the data in the database has been modified since it was loaded into memory.
        /// </exception>
        /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
        Task<int> CommitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        ///     Creates an instance of the configured <see cref="IExecutionStrategy" />.
        /// </summary>
        /// <remarks>
        ///     See <see href="https://aka.ms/efcore-docs-connection-resiliency">Connection resiliency and database retries</see>
        ///     for more information.
        /// </remarks>
        /// <returns>An <see cref="IExecutionStrategy" /> instance.</returns>
        IExecutionStrategy CreateStrategy();

        /// <summary>
        ///     <para>
        ///         Ensures that the database for the context exists.
        ///     </para>
        ///     <list type="bullet">
        ///         <item>
        ///             <description>
        ///                 If the database exists and has any tables, then no action is taken. Nothing is done to ensure
        ///                 the database schema is compatible with the Entity Framework model.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 If the database exists but does not have any tables, then the Entity Framework model is used to
        ///                 create the database schema.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 If the database does not exist, then the database is created and the Entity Framework model is used to
        ///                 create the database schema.
        ///             </description>
        ///         </item>
        ///     </list>
        ///     <para>
        ///         It is common to use <see cref="EnsureCreated" /> immediately following <see cref="EnsureDeleted" /> when
        ///         testing or prototyping using Entity Framework. This ensures that the database is in a clean state before each
        ///         execution of the test/prototype. Note, however, that data in the database is not preserved.
        ///     </para>
        ///     <para>
        ///         Note that this API does **not** use migrations to create the database. In addition, the database that is
        ///         created cannot be later updated using migrations. If you are targeting a relational database and using migrations,
        ///         then you can use <see cref="M:Microsoft.EntityFrameworkCore.RelationalDatabaseFacadeExtensions.Migrate" />
        ///         to ensure the database is created using migrations and that all migrations have been applied.
        ///     </para>
        /// </summary>
        /// <remarks>
        ///     See <see href="https://aka.ms/efcore-manage-schemas">Managing database schemas with EF Core</see>
        ///     and <see href="https://aka.ms/efcore-ensure-created">Database creation APIs</see> for more information.
        /// </remarks>
        /// <returns><see langword="true" /> if the database is created, <see langword="false" /> if it already existed.</returns>
        bool DatabaseCreate();

        /// <summary>
        ///     <para>
        ///         Ensures that the database for the context exists.
        ///     </para>
        ///     <list type="bullet">
        ///         <item>
        ///             <description>
        ///                 If the database exists and has any tables, then no action is taken. Nothing is done to ensure
        ///                 the database schema is compatible with the Entity Framework model.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 If the database exists but does not have any tables, then the Entity Framework model is used to
        ///                 create the database schema.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 If the database does not exist, then the database is created and the Entity Framework model is used to
        ///                 create the database schema.
        ///             </description>
        ///         </item>
        ///     </list>
        ///     <para>
        ///         It is common to use <see cref="EnsureCreatedAsync" /> immediately following <see cref="EnsureDeletedAsync" /> when
        ///         testing or prototyping using Entity Framework. This ensures that the database is in a clean state before each
        ///         execution of the test/prototype. Note, however, that data in the database is not preserved.
        ///     </para>
        ///     <para>
        ///         Note that this API does **not** use migrations to create the database. In addition, the database that is
        ///         created cannot be later updated using migrations. If you are targeting a relational database and using migrations,
        ///         then you can use <see cref="M:Microsoft.EntityFrameworkCore.RelationalDatabaseFacadeExtensions.MigrateAsync" />
        ///         to ensure the database is created using migrations and that all migrations have been applied.
        ///     </para>
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Entity Framework Core does not support multiple parallel operations being run on the same DbContext instance. This
        ///         includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
        ///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
        ///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see>
        ///         for more information.
        ///     </para>
        ///     <para>
        ///         See <see href="https://aka.ms/efcore-manage-schemas">Managing database schemas with EF Core</see>
        ///         and <see href="https://aka.ms/efcore-ensure-created">Database creation APIs</see> for more information.
        ///     </para>
        /// </remarks>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>
        ///     A task that represents the asynchronous save operation. The task result contains <see langword="true" /> if the database is created,
        ///     <see langword="false" /> if it already existed.
        /// </returns>
        /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
        Task<bool> DatabaseCreateAsync(CancellationToken cancellationToken = default);

        /// <summary>
        ///     <para>
        ///         Ensures that the database for the context does not exist. If it does not exist, no action is taken. If it does
        ///         exist then the database is deleted.
        ///     </para>
        ///     <para>
        ///         Warning: The entire database is deleted, and no effort is made to remove just the database objects that are used by
        ///         the model for this context.
        ///     </para>
        ///     <para>
        ///         It is common to use <see cref="EnsureCreated" /> immediately following <see cref="EnsureDeleted" /> when
        ///         testing or prototyping using Entity Framework. This ensures that the database is in a clean state before each
        ///         execution of the test/prototype. Note, however, that data in the database is not preserved.
        ///     </para>
        /// </summary>
        /// <remarks>
        ///     See <see href="https://aka.ms/efcore-manage-schemas">Managing database schemas with EF Core</see>
        ///     and <see href="https://aka.ms/efcore-ensure-created">Database creation APIs</see> for more information.
        /// </remarks>
        /// <returns><see langword="true" /> if the database is deleted, <see langword="false" /> if it did not exist.</returns>
        bool DatabaseDelete();
        Task<bool> DatabaseDeleteAsync(CancellationToken cancellationToken = default);

        // <summary>
        ///     Starts a new transaction.
        /// </summary>
        /// <remarks>
        ///     See <see href="https://aka.ms/efcore-docs-transactions">Transactions in EF Core</see> for more information.
        /// </remarks>
        /// <returns>
        ///     A <see cref="IDbContextTransaction" /> that represents the started transaction.
        /// </returns>
        IDbContextTransaction OpenTransaction();

        /// <summary>
        ///     Asynchronously starts a new transaction.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Entity Framework Core does not support multiple parallel operations being run on the same DbContext instance. This
        ///         includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
        ///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
        ///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see>
        ///         for more information.
        ///     </para>
        ///     <para>
        ///         See <see href="https://aka.ms/efcore-docs-transactions">Transactions in EF Core</see> for more information.
        ///     </para>
        /// </remarks>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>
        ///     A task that represents the asynchronous transaction initialization. The task result contains a <see cref="IDbContextTransaction" />
        ///     that represents the started transaction.
        /// </returns>
        /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
        Task<IDbContextTransaction> OpenTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        ///     Discards the outstanding operations in the current transaction.
        /// </summary>
        /// <remarks>
        ///     See <see href="https://aka.ms/efcore-docs-transactions">Transactions in EF Core</see> for more information.
        /// </remarks>
        void RevertTransaction();

        /// <summary>
        ///     Discards the outstanding operations in the current transaction.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         See <see href="https://aka.ms/efcore-docs-transactions">Transactions in EF Core</see> for more information.
        ///     </para>
        ///     <para>
        ///         Entity Framework Core does not support multiple parallel operations being run on the same DbContext instance. This
        ///         includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
        ///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
        ///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see>
        ///         for more information.
        ///     </para>
        /// </remarks>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
        Task RevertTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        ///     Applies the outstanding operations in the current transaction to the database.
        /// </summary>
        void CloseTransaction();

        /// <summary>
        ///     Applies the outstanding operations in the current transaction to the database.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Entity Framework Core does not support multiple parallel operations being run on the same DbContext instance. This
        ///         includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
        ///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
        ///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see>
        ///         for more information.
        ///     </para>
        ///     <para>
        ///         See <see href="https://aka.ms/efcore-docs-transactions">Transactions in EF Core</see> for more information.
        ///     </para>
        /// </remarks>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
        Task CloseTransactionAsync(CancellationToken cancellationToken = default);

    }
}