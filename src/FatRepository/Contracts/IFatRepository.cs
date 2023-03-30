using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FatRepository.Contracts
{
    /// <summary>
    /// All the method that interacts with dbset class to apply the update in the database.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IFatRepository<TEntity, TDbContext> where TEntity : class where TDbContext : DbContext
    {
        /// <summary>
        /// Get the Direct DbSet for raw power.
        /// </summary>
        /// <returns></returns>
        DbSet<TEntity> GetEntity();

        /// <summary>
        /// This will return everything.
        /// </summary>
        /// <returns></returns>
        List<TEntity> All(bool isTracking = true);

        /// <summary>
        /// This method will return everything inside of a task wrapper so that it can be awaited.
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> AllAsync(bool isTracking = true);

        /// <summary>
        /// This method will return filtered data.
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <returns></returns>
        List<TEntity> Find(Expression<Func<TEntity, bool>> whereQuery, bool isTracking = true);

        /// <summary>
        /// This method will return filtered data asynchronously
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <returns></returns>
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> whereQuery, bool isTracking = true);

        /// <summary>
        /// This method will return filtered data with some includable foreign property.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="includableMembers"></param>
        /// <returns></returns>
        List<TEntity> Find(Expression<Func<TEntity, bool>> whereQuery, bool isTracking = true, params string[] includableMembers);

        /// <summary>
        /// This method will return filtered data with some includable foreign property asynchronously.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="includableMembers"></param>
        /// <returns></returns>
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> whereQuery, bool isTracking = true, params string[] includableMembers);

        /// <summary>
        /// This method will return with some selective properties.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="selectQuery"></param>
        /// <returns></returns>
        List<TResult> Find<TResult>(Expression<Func<TEntity, bool>> whereQuery, Expression<Func<TEntity, TResult>> selectQuery, bool isTracking = true);

        /// <summary>
        /// This method will return with some selective properties asynchronously.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="selectQuery"></param>
        /// <returns></returns>
        Task<List<TResult>> FindAsync<TResult>(Expression<Func<TEntity, bool>> whereQuery, Expression<Func<TEntity, TResult>> selectQuery, bool isTracking = true);

        /// <summary>
        /// This method will return limited data as per condition.
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        List<TEntity> Find(Expression<Func<TEntity, bool>> whereQuery, int? skip, int? take, bool isTracking = true);

        /// <summary>
        /// This method will return limited data as per condition asynchronously.
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> whereQuery, int? skip, int? take, bool isTracking = true);

        /// <summary>
        /// This will return filtered limited data with the foregin properties.
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="includableMembers"></param>
        /// <returns></returns>
        List<TEntity> Find(Expression<Func<TEntity, bool>>? whereQuery, int? skip, int? take, bool isTracking = true, params string[]? includableMembers);

        /// <summary>
        /// This will return filtered limited data with the foreign properties asynchronously.
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="includableMembers"></param>
        /// <returns></returns>
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>>? whereQuery, int? skip, int? take, bool isTracking = true, params string[]? includableMembers);

        /// <summary>
        /// This will return limited filtered data with the foreign properties 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="whereQuery"></param>
        /// <param name="selectQuery"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="includableMembers"></param>
        /// <returns></returns>
        List<TResult> Find<TResult>(Expression<Func<TEntity, bool>>? whereQuery, Expression<Func<TEntity, TResult>> selectQuery, int? skip, int? take, bool isTracking = true, params string[]? includableMembers);

        /// <summary>
        /// This will return limited filtered data with the foreign properties asynchronously.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="whereQuery"></param>
        /// <param name="selectQuery"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="includableMembers"></param>
        /// <returns></returns>
        Task<List<TResult>> FindAsync<TResult>(Expression<Func<TEntity, bool>>? whereQuery, Expression<Func<TEntity, TResult>> selectQuery, int? skip, int? take, bool isTracking = true, params string[]? includableMembers);

        /// <summary>
        /// Find the first document.
        /// </summary>
        /// <returns></returns>
        TEntity? FindOne(bool isTracking = true);

        /// <summary>
        /// Find the first document asynchronously.
        /// </summary>
        /// <returns></returns>
        Task<TEntity?> FindOneAsync(bool isTracking = true);

        /// <summary>
        /// Find the first document with filter query
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <returns></returns>
        TEntity? FindOne(Expression<Func<TEntity, bool>> whereQuery, bool isTracking = true);

        /// <summary>
        /// Find the first document with filter query asynchronously.
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <returns></returns>
        Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> whereQuery, bool isTracking = true);

        /// <summary>
        /// Find the first document with includable members.
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="includableMembers"></param>
        /// <returns></returns>
        TEntity? FindOne(Expression<Func<TEntity, bool>> whereQuery, bool isTracking = true, params string[] includableMembers);

        /// <summary>
        /// Find the first document with includable members.
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="includableMembers"></param>
        /// <returns></returns>
        Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> whereQuery, bool isTracking = true, params string[] includableMembers);

        /// <summary>
        /// Insert into db
        /// </summary>
        /// <param name="entites"></param>
        /// <returns></returns>
        Task InsertAsync(IEnumerable<TEntity> entites);

        /// <summary>
        /// Insert into db.
        /// </summary>
        /// <param name="entities"></param>
        void Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Insert one document in db.
        /// </summary>
        /// <param name="entity"></param>
        void InsertOne(TEntity entity);

        /// <summary>
        /// Insert one document in db.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task InsertOneAsync(TEntity entity);

        /// <summary>
        /// Update one document.
        /// </summary>
        /// <param name="entity"></param>
        void ModifyOne(TEntity entity);

        /// <summary>
        /// Update one document.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task ModifyOneAsync(TEntity entity);

        /// <summary>
        /// Update document in db.
        /// </summary>
        /// <param name="entities"></param>
        void Modify(IEnumerable<TEntity> entities);
    }
}