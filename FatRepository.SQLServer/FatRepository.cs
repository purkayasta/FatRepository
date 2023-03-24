using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Linq.Expressions;

/* TODO: Add Select Pattern
 */

namespace FatRepository.SQLServer
{
    internal class FatRepository<T> where T : class
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

        #region Simple Find Methods

        public T? FindOne(Expression<Func<T, bool>> query) => _dbSet.Where(query).FirstOrDefault();
        public Task<T?> FindOneAsync(Expression<Func<T, bool>> query) => _dbSet.Where(query).FirstOrDefaultAsync();

        #endregion

        #region Find Queries With Include

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

    }
}
