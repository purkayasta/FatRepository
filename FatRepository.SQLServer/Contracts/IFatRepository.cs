using System.Linq.Expressions;

namespace FatRepository.SQLServer.Contracts
{
    public interface IFatRepository<T> where T : class
    {
        List<T> Find(Expression<Func<T, bool>> query);
        List<T> Find(Expression<Func<T, bool>> query, int skip, int take);
        List<T> Find(Expression<Func<T, bool>> query, params string[] includableMembers);
        List<TResult> Find<TResult>(Expression<Func<T, bool>> query, Expression<Func<T, TResult>> selector);
        List<TResult> Find<TResult>(Expression<Func<T, bool>> query, Expression<Func<T, TResult>> selector, int skip, int take, params string[] includableMembers);
        Task<List<T>> FindAsync(Expression<Func<T, bool>> query);
        Task<List<T>> FindAsync(Expression<Func<T, bool>> query, int skip, int take);
        Task<List<T>> FindAsync(Expression<Func<T, bool>> query, params string[] includableMembers);
        Task<List<TResult>> FindAsync<TResult>(Expression<Func<T, bool>> query, Expression<Func<T, TResult>> selector);
        Task<List<TResult>> FindAsync<TResult>(Expression<Func<T, bool>> query, Expression<Func<T, TResult>> selector, int skip, int take, params string[] includableMembers);
        T? FindOne(Expression<Func<T, bool>> query);
        T? FindOne(Expression<Func<T, bool>> query, params string[] includableMembers);
        Task<T?> FindOneAsync(Expression<Func<T, bool>> query);
        Task<T?> FindOneAsync(Expression<Func<T, bool>> query, params string[] includableMembers);
        Task InsertAsync(IEnumerable<T> entites);
        void InsertOne(T entity);
        Task InsertOneAsync(T entities);
        void InsertRange(IEnumerable<T> entities);
        void Modify(T entity);
        Task ModifyAsync(T entity);
        void ModifyRange(IEnumerable<T> entities);
    }
}