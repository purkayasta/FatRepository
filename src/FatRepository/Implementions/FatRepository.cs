using FatRepository.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace FatRepository.Implementions
{
    internal sealed class FatRepository<TEntity, TDbContext> : IFatRepository<TEntity, TDbContext> where TEntity : class where TDbContext : DbContext
    {
        private readonly DbSet<TEntity> _dbSet;

        public FatRepository(TDbContext dbContext) => _dbSet = dbContext.Set<TEntity>();

        public DbSet<TEntity> GetEntity() => _dbSet;


        #region Insert and Modify Methods

        public void InsertOne(TEntity entity) => _dbSet.Add(entity);
        public async Task InsertOneAsync(TEntity entity, CancellationToken cancellationToken = default) => await _dbSet.AddAsync(entity, cancellationToken);

        public void Insert(IEnumerable<TEntity> entities) => _dbSet.AddRange(entities);
        public Task InsertAsync(IEnumerable<TEntity> entites, CancellationToken cancellationToken = default) => _dbSet.AddRangeAsync(entites, cancellationToken);

        public void ModifyOne(TEntity entity) => _dbSet.Update(entity);
        public void Modify(IEnumerable<TEntity> entities) => _dbSet.UpdateRange(entities);



        #endregion

        #region Simple FindOne Queries

        public TEntity? FindOne(bool isTracking = true)
        {
            if (isTracking) return _dbSet.FirstOrDefault();
            return _dbSet.AsNoTracking().FirstOrDefault();
        }
        public Task<TEntity?> FindOneAsync(bool isTracking = true, CancellationToken cancellationToken = default)
        {
            if (isTracking) return _dbSet.FirstOrDefaultAsync(cancellationToken);
            return _dbSet.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        }

        public TEntity? FindOne(Expression<Func<TEntity, bool>> whereQuery, bool isTracking = true)
        {
            if (isTracking) return _dbSet.Where(whereQuery).FirstOrDefault();
            return _dbSet.Where(whereQuery).AsNoTracking().FirstOrDefault();
        }
        public Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> whereQuery, bool isTracking = true, CancellationToken cancellationToken = default)
        {
            if (isTracking) _dbSet.Where(whereQuery).FirstOrDefaultAsync(cancellationToken);
            return _dbSet.Where(whereQuery).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        }

        #endregion

        #region FindOne Queries With Include

        public TEntity? FindOne(Expression<Func<TEntity, bool>> whereQuery, bool isTracking = true, string[]? includableMembers = default)
        {
            IQueryable<TEntity> queryable = _dbSet.Where(whereQuery);

            if (includableMembers is not null && includableMembers.Length > 0)
            {
                foreach (string member in includableMembers) queryable.Include(member);
            }

            if (isTracking) return queryable.FirstOrDefault();
            return queryable.AsNoTracking().FirstOrDefault();
        }
        public Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> whereQuery, bool isTracking = true, string[]? includableMembers = default, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = _dbSet.Where(whereQuery);

            if (includableMembers is not null && includableMembers.Length > 0)
            {
                foreach (string member in includableMembers) queryable.Include(member);
            }

            if (isTracking) return queryable.FirstOrDefaultAsync(cancellationToken);
            return queryable.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        }

        #endregion

        #region Simple Find Queries

        public List<TEntity> All(bool isTracking = true)
        {
            if (isTracking) return _dbSet.ToList();
            return _dbSet.AsNoTracking().ToList();
        }
        public Task<List<TEntity>> AllAsync(bool isTracking = true, CancellationToken cancellationToken = default)
        {
            if (isTracking) return _dbSet.ToListAsync(cancellationToken);
            return _dbSet.AsNoTracking().ToListAsync(cancellationToken);
        }

        public List<TEntity> Find(Expression<Func<TEntity, bool>> whereQuery, bool isTracking = true)
        {
            if (isTracking) return _dbSet.Where(whereQuery).ToList();
            return _dbSet.Where(whereQuery).AsNoTracking().ToList();
        }
        public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> whereQuery, bool isTracking = true, CancellationToken cancellationToken = default)
        {
            if (isTracking) return _dbSet.Where(whereQuery).ToListAsync(cancellationToken);
            return _dbSet.Where(whereQuery).AsNoTracking().ToListAsync(cancellationToken);
        }

        #endregion

        #region Find Queries with Include

        public List<TEntity> Find(Expression<Func<TEntity, bool>> whereQuery, bool isTracking = true, string[]? includableMembers = default)
        {
            IQueryable<TEntity> queryable = _dbSet.Where(whereQuery);

            if (includableMembers is not null && includableMembers.Length > 0)
            {
                foreach (string member in includableMembers) queryable.Include(member);
            }

            if (isTracking) return queryable.ToList();
            return queryable.AsNoTracking().ToList();
        }

        public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> whereQuery, bool isTracking = true, string[]? includableMembers = default, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = _dbSet.Where(whereQuery);

            if (includableMembers is not null && includableMembers.Length > 0)
            {
                foreach (string member in includableMembers) queryable.Include(member);
            }

            if (isTracking) return queryable.ToListAsync(cancellationToken);
            return queryable.AsNoTracking().ToListAsync(cancellationToken);
        }

        #endregion

        #region Find Queries Selector

        public List<TResult> Find<TResult>(Expression<Func<TEntity, bool>> whereQuery, Expression<Func<TEntity, TResult>> selectQuery, bool isTracking = true)
        {
            if (isTracking) return _dbSet.Where(whereQuery).Select(selectQuery).ToList();
            return _dbSet.Where(whereQuery).AsNoTracking().Select(selectQuery).ToList();
        }

        public Task<List<TResult>> FindAsync<TResult>(Expression<Func<TEntity, bool>> whereQuery, Expression<Func<TEntity, TResult>> selectQuery, bool isTracking = true, CancellationToken cancellationToken = default)
        {
            if (isTracking) return _dbSet.Where(whereQuery).Select(selectQuery).ToListAsync(cancellationToken);
            return _dbSet.Where(whereQuery).AsNoTracking().Select(selectQuery).ToListAsync(cancellationToken);
        }

        #endregion

        #region Find queries Pagination

        public List<TEntity> Find(Expression<Func<TEntity, bool>> whereQuery, int? skip, int? take, bool isTracking = true)
        {
            IQueryable<TEntity> queryable = _dbSet.Where(whereQuery).AsQueryable();

            bool isSkipCountExist = skip is not null && skip.HasValue;
            bool isTakeCountExist = take is not null && take.HasValue;
            bool isSkipableAndTakeableExist = isSkipCountExist && isTakeCountExist;

            if (isSkipableAndTakeableExist) queryable.Skip(skip!.Value * take!.Value);
            else
            {
                if (isSkipCountExist) queryable.Skip(skip!.Value);
                if (isTakeCountExist) queryable.Take(take!.Value);
            }

            if (isTracking) return queryable.ToList();
            return queryable.ToList();
        }

        public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> query, int? skip, int? take, bool isTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = _dbSet.Where(query).AsQueryable();

            bool isSkipCountExist = skip is not null && skip.HasValue;
            bool isTakeCountExist = take is not null && take.HasValue;
            bool isSkipableAndTakeableExist = isSkipCountExist && isTakeCountExist;

            if (isSkipableAndTakeableExist) queryable.Skip(skip!.Value * take!.Value);
            else
            {
                if (isSkipCountExist) queryable.Skip(skip!.Value);
                if (isTakeCountExist) queryable.Take(take!.Value);
            }

            if (isTracking) return queryable.ToListAsync(cancellationToken);
            return queryable.AsNoTracking().ToListAsync(cancellationToken);
        }

        #endregion

        #region All Option (select, pagination, include)

        public List<TResult> Find<TResult>(Expression<Func<TEntity, bool>>? whereQuery, Expression<Func<TEntity, TResult>> selectQuery, int? skip, int? take, bool isTracking = true, string[]? includableMembers = default)
        {
            IQueryable<TEntity> queryable = _dbSet.AsQueryable();

            if (whereQuery is not null) queryable.Where(whereQuery);

            if (includableMembers is not null && includableMembers.Length > 0)
            {
                foreach (string member in includableMembers) queryable.Include(member);
            }

            bool isSkipCountExist = skip is not null && skip.HasValue;
            bool isTakeCountExist = take is not null && take.HasValue;
            bool isSkipableAndTakeableExist = isSkipCountExist && isTakeCountExist;

            if (isSkipableAndTakeableExist) queryable.Skip(skip!.Value * take!.Value);
            else
            {
                if (isSkipCountExist) queryable.Skip(skip!.Value);
                if (isTakeCountExist) queryable.Take(take!.Value);
            }

            if (isTracking) return queryable.Select(selectQuery).ToList();
            return queryable.AsNoTracking().Select(selectQuery).ToList();
        }

        public Task<List<TResult>> FindAsync<TResult>(Expression<Func<TEntity, bool>>? whereQuery, Expression<Func<TEntity, TResult>> selectQuery, int? skip, int? take, bool isTracking = true, string[]? includableMembers = default, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = _dbSet.AsQueryable();

            if (whereQuery is not null) queryable.Where(whereQuery);

            if (includableMembers is not null && includableMembers.Length > 0)
            {
                foreach (string member in includableMembers) queryable.Include(member);
            }

            bool isSkipCountExist = skip is not null && skip.HasValue;
            bool isTakeCountExist = take is not null && take.HasValue;
            bool isSkipableAndTakeableExist = isSkipCountExist && isTakeCountExist;

            if (isSkipableAndTakeableExist) queryable.Skip(skip!.Value * take!.Value);
            else
            {
                if (isSkipCountExist) queryable.Skip(skip!.Value);
                if (isTakeCountExist) queryable.Take(take!.Value);
            }

            if (isTracking) return queryable.Select(selectQuery).ToListAsync(cancellationToken);
            return queryable.AsNoTracking().Select(selectQuery).ToListAsync(cancellationToken);
        }

        public List<TEntity> Find(Expression<Func<TEntity, bool>>? whereQuery, int? skip, int? take, bool isTracking = true, string[]? includableMembers = default)
        {
            IQueryable<TEntity> queryable = _dbSet.AsQueryable();

            if (whereQuery is not null) queryable.Where(whereQuery);


            if (includableMembers is not null && includableMembers.Length > 0)
            {
                foreach (string member in includableMembers) queryable.Include(member);
            }

            bool isSkipCountExist = skip is not null && skip.HasValue;
            bool isTakeCountExist = take is not null && take.HasValue;
            bool isSkipableAndTakeableExist = isSkipCountExist && isTakeCountExist;

            if (isSkipableAndTakeableExist) queryable.Skip(skip!.Value * take!.Value);
            else
            {
                if (isSkipCountExist) queryable.Skip(skip!.Value);
                if (isTakeCountExist) queryable.Take(take!.Value);
            }

            if (isTracking) return queryable.ToList();
            return queryable.AsNoTracking().ToList();
        }

        public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>>? whereQuery, int? skip, int? take, bool isTracking = true, string[]? includableMembers = default, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = _dbSet.AsQueryable();

            if (whereQuery is not null) queryable.Where(whereQuery);


            if (includableMembers is not null && includableMembers.Length > 0)
            {
                foreach (string member in includableMembers) queryable.Include(member);
            }

            bool isSkipCountExist = skip is not null && skip.HasValue;
            bool isTakeCountExist = take is not null && take.HasValue;
            bool isSkipableAndTakeableExist = isSkipCountExist && isTakeCountExist;

            if (isSkipableAndTakeableExist) queryable.Skip(skip!.Value * take!.Value);
            else
            {
                if (isSkipCountExist) queryable.Skip(skip!.Value);
                if (isTakeCountExist) queryable.Take(take!.Value);
            }

            if (isTracking) return queryable.ToListAsync(cancellationToken);
            return queryable.AsNoTracking().ToListAsync(cancellationToken);
        }

        #endregion
    }
}
