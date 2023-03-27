using FatRepository.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FatRepository.Implementions
{
    internal class FatRepository<TEntity, TDbContext> : IFatRepository<TEntity, TDbContext> where TEntity : class where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public FatRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        #region Insert and Modify Methods

        public void InsertOne(TEntity entity) => _dbSet.Add(entity);
        public void InsertRange(IEnumerable<TEntity> entities) => _dbSet.AddRange(entities);
        public void Modify(TEntity entity) => _dbSet.Update(entity);
        public void ModifyRange(IEnumerable<TEntity> entities) => _dbSet.UpdateRange(entities);


        public async Task InsertOneAsync(TEntity entity) => await _dbSet.AddAsync(entity);
        public Task InsertAsync(IEnumerable<TEntity> entites) => _dbSet.AddRangeAsync(entites);
        public Task ModifyAsync(TEntity entity) => Task.FromResult(_dbSet.Update(entity));

        #endregion

        #region Simple FindOne Queries

        public TEntity? FindOne() => _dbSet.FirstOrDefault();
        public TEntity? FindOne(Expression<Func<TEntity, bool>> query) => _dbSet.Where(query).FirstOrDefault();
        public Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> query) => _dbSet.Where(query).FirstOrDefaultAsync();

        #endregion

        #region FindOne Queries With Include

        public TEntity? FindOne(Expression<Func<TEntity, bool>> query, params string[] includableMembers)
        {
            var findQuery = _dbSet.Where(query);

            if (includableMembers.Length < 1) throw new ArgumentException($"{nameof(includableMembers)} cannot be empty");

            foreach (string member in includableMembers) findQuery.Include(member);

            return findQuery.FirstOrDefault();
        }

        public Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> query, params string[] includableMembers)
        {
            var findQuery = _dbSet.Where(query);

            if (includableMembers.Length < 1) throw new ArgumentException($"{nameof(includableMembers)} cannot be empty");

            foreach (string member in includableMembers) findQuery.Include(member);

            return findQuery.FirstOrDefaultAsync();
        }

        #endregion

        #region Simple Find Queries

        public List<TEntity> Find() => _dbSet.ToList();
        public List<TEntity> Find(Expression<Func<TEntity, bool>> query) => _dbSet.Where(query).ToList();
        public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> query) => _dbSet.Where(query).ToListAsync();
        public Task<List<TEntity>> FindAsync() => _dbSet.ToListAsync();

        #endregion

        #region Find Queries with Include

        public List<TEntity> Find(Expression<Func<TEntity, bool>> query, params string[] includableMembers)
        {
            if (includableMembers.Length < 1) throw new ArgumentException($"{nameof(includableMembers)} is empty");

            IQueryable<TEntity> findQuery = _dbSet.Where(query);

            foreach (string member in includableMembers) findQuery.Include(member);

            return findQuery.ToList();
        }

        public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> query, params string[] includableMembers)
        {
            if (includableMembers.Length < 1) throw new ArgumentException($"{nameof(includableMembers)} is empty");

            IQueryable<TEntity> findQuery = _dbSet.Where(query);

            foreach (string member in includableMembers) findQuery.Include(member);

            return findQuery.ToListAsync();
        }

        #endregion

        #region Find Queries Selector

        public List<TResult> Find<TResult>(Expression<Func<TEntity, bool>> query, Expression<Func<TEntity, TResult>> selector)
            => _dbSet.Where(query).Select(selector).ToList();

        public Task<List<TResult>> FindAsync<TResult>(Expression<Func<TEntity, bool>> query, Expression<Func<TEntity, TResult>> selector)
            => _dbSet.Where(query).Select(selector).ToListAsync();

        #endregion

        #region Find queries Pagination

        public List<TEntity> Find(Expression<Func<TEntity, bool>> query, int skip, int take)
            => _dbSet.Where(query).Skip(skip).Take(take).ToList();

        public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> query, int skip, int take)
            => _dbSet.Where(query).Skip(skip).Take(take).ToListAsync();

        #endregion

        #region All Option (select, pagination, include)

        public List<TResult> Find<TResult>(Expression<Func<TEntity, bool>> query, Expression<Func<TEntity, TResult>> selector, int skip, int take, params string[] includableMembers)
        {
            var findQuery = _dbSet.Where(query).Skip(skip).Take(take);

            if (includableMembers.Length > 0)
            {
                foreach (string member in includableMembers) findQuery.Include(member);
            }

            return findQuery.Select(selector).ToList();
        }

        public Task<List<TResult>> FindAsync<TResult>(Expression<Func<TEntity, bool>> query, Expression<Func<TEntity, TResult>> selector, int skip, int take, params string[] includableMembers)
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
