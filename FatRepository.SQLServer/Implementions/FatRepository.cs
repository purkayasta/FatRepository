using FatRepository.SQLServer.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FatRepository.SQLServer.Implementions
{
    internal class FatRepository<T> : IFatRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public FatRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        #region Insert and Modify Methods

        public void InsertOne(T entity) => _dbSet.Add(entity);
        public void InsertRange(IEnumerable<T> entities) => _dbSet.AddRange(entities);
        public void Modify(T entity) => _dbSet.Update(entity);
        public void ModifyRange(IEnumerable<T> entities) => _dbSet.UpdateRange(entities);


        public async Task InsertOneAsync(T entities) => await _dbSet.AddAsync(entities);
        public Task InsertAsync(IEnumerable<T> entites) => _dbSet.AddRangeAsync(entites);
        public Task ModifyAsync(T entity) => Task.FromResult(_dbSet.Update(entity));

        #endregion

        #region Simple FindOne Queries

        public T? FindOne(Expression<Func<T, bool>> query) => _dbSet.Where(query).FirstOrDefault();
        public Task<T?> FindOneAsync(Expression<Func<T, bool>> query) => _dbSet.Where(query).FirstOrDefaultAsync();

        #endregion

        #region FindOne Queries With Include

        public T? FindOne(Expression<Func<T, bool>> query, params string[] includableMembers)
        {
            var findQuery = _dbSet.Where(query);

            if (includableMembers.Length < 1) throw new ArgumentException($"{nameof(includableMembers)} cannot be empty");

            foreach (string member in includableMembers) findQuery.Include(member);

            return findQuery.FirstOrDefault();
        }

        public Task<T?> FindOneAsync(Expression<Func<T, bool>> query, params string[] includableMembers)
        {
            var findQuery = _dbSet.Where(query);

            if (includableMembers.Length < 1) throw new ArgumentException($"{nameof(includableMembers)} cannot be empty");

            foreach (string member in includableMembers) findQuery.Include(member);

            return findQuery.FirstOrDefaultAsync();
        }

        #endregion

        #region Simple Find Queries

        public List<T> Find(Expression<Func<T, bool>> query) => _dbSet.Where(query).ToList();
        public Task<List<T>> FindAsync(Expression<Func<T, bool>> query) => _dbSet.Where(query).ToListAsync();

        #endregion

        #region Find Queries with Include

        public List<T> Find(Expression<Func<T, bool>> query, params string[] includableMembers)
        {
            if (includableMembers.Length < 1) throw new ArgumentException($"{nameof(includableMembers)} is empty");

            IQueryable<T> findQuery = _dbSet.Where(query);

            foreach (string member in includableMembers) findQuery.Include(member);

            return findQuery.ToList();
        }

        public Task<List<T>> FindAsync(Expression<Func<T, bool>> query, params string[] includableMembers)
        {
            if (includableMembers.Length < 1) throw new ArgumentException($"{nameof(includableMembers)} is empty");

            IQueryable<T> findQuery = _dbSet.Where(query);

            foreach (string member in includableMembers) findQuery.Include(member);

            return findQuery.ToListAsync();
        }

        #endregion

        #region Find Queries Selector

        public List<TResult> Find<TResult>(Expression<Func<T, bool>> query, Expression<Func<T, TResult>> selector)
            => _dbSet.Where(query).Select(selector).ToList();

        public Task<List<TResult>> FindAsync<TResult>(Expression<Func<T, bool>> query, Expression<Func<T, TResult>> selector)
            => _dbSet.Where(query).Select(selector).ToListAsync();

        #endregion

        #region Find queries Pagination

        public List<T> Find(Expression<Func<T, bool>> query, int skip, int take)
            => _dbSet.Where(query).Skip(skip).Take(take).ToList();

        public Task<List<T>> FindAsync(Expression<Func<T, bool>> query, int skip, int take)
            => _dbSet.Where(query).Skip(skip).Take(take).ToListAsync();

        #endregion

        #region All Option (select, pagination, include)

        public List<TResult> Find<TResult>(Expression<Func<T, bool>> query, Expression<Func<T, TResult>> selector, int skip, int take, params string[] includableMembers)
        {
            var findQuery = _dbSet.Where(query).Skip(skip).Take(take);

            if (includableMembers.Length > 0)
            {
                foreach (string member in includableMembers) findQuery.Include(member);
            }

            return findQuery.Select(selector).ToList();
        }

        public Task<List<TResult>> FindAsync<TResult>(Expression<Func<T, bool>> query, Expression<Func<T, TResult>> selector, int skip, int take, params string[] includableMembers)
        {
            var findQuery = _dbSet.Where(query).Skip(skip).Take(take);

            if (includableMembers.Length > 0)
            {
                foreach (string member in includableMembers) findQuery.Include(member);
            }

            return findQuery.Select(selector).ToListAsync();
        }

        #endregion
    }
}
