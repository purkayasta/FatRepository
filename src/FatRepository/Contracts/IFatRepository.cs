using FatRepository.Implementions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FatRepository.Contracts
{
    public interface IFatRepository<TEntity, TDbContext> where TEntity : class where TDbContext : DbContext
    {
        List<TEntity> Find(Expression<Func<TEntity, bool>> query);
        List<TEntity> Find(Expression<Func<TEntity, bool>> query, int skip, int take);
        List<TEntity> Find(Expression<Func<TEntity, bool>> query, params string[] includableMembers);
        List<TResult> Find<TResult>(Expression<Func<TEntity, bool>> query, Expression<Func<TEntity, TResult>> selector);
        List<TResult> Find<TResult>(Expression<Func<TEntity, bool>> query, Expression<Func<TEntity, TResult>> selector, int skip, int take, params string[] includableMembers);
        List<TEntity> Find();
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> query);
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> query, int skip, int take);
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> query, params string[] includableMembers);
        Task<List<TResult>> FindAsync<TResult>(Expression<Func<TEntity, bool>> query, Expression<Func<TEntity, TResult>> selector);
        Task<List<TResult>> FindAsync<TResult>(Expression<Func<TEntity, bool>> query, Expression<Func<TEntity, TResult>> selector, int skip, int take, params string[] includableMembers);
        Task<List<TEntity>> FindAsync();
        TEntity? FindOne(Expression<Func<TEntity, bool>> query);
        TEntity? FindOne(Expression<Func<TEntity, bool>> query, params string[] includableMembers);
        TEntity? FindOne();
        Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> query);
        Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> query, params string[] includableMembers);
        Task InsertAsync(IEnumerable<TEntity> entites);
        void InsertOne(TEntity entity);
        Task InsertOneAsync(TEntity entities);
        void InsertRange(IEnumerable<TEntity> entities);
        void Modify(TEntity entity);
        Task ModifyAsync(TEntity entity);
        void ModifyRange(IEnumerable<TEntity> entities);
    }
}