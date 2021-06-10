using Infrastructure.EntityFramework.Factories;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.EntityFramework
{
    public class BaseRepository<TEntity> : IRepositoryAsync<TEntity> where TEntity : class, IObjectState
    {
        #region Private Fields

        private readonly IDataContextAsync _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IUnitOfWorkAsync _unitOfWork;
        public DbSet<TEntity> DbSet { get { return _dbSet; } }
        public DbContext DbContext { get { return _context as DbContext; } }

        #endregion Private Fields

        public BaseRepository(IDataContextAsync context, IUnitOfWorkAsync unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;

            // Temporarily for FakeDbContext, Unit Test and Fakes
            var dbContext = context as DbContext;
            if (dbContext != null)
            {
                _dbSet = dbContext.Set<TEntity>();
            }
        }

        public virtual TEntity Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public virtual IQueryable<TEntity> ExcuteQuery(string query, params object[] parameters)
        {
            return _dbSet.FromSqlRaw(query, parameters).AsQueryable();
        }

        public virtual void InsertAsync(TEntity entity)
        {
            // LINH: In case of Addsync no need to get/update state
            //entity.ObjectState = ObjectState.Added;
            _dbSet.AddAsync(entity);
            //_context.SyncObjectState(entity);
        }
        public virtual void Insert(TEntity entity)
        {
            entity.ObjectState = ObjectState.Added;
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Insert(entity);
            }
        }

        public virtual void Update(TEntity entity, List<Expression<Func<TEntity, object>>> updateProperties = null,
            List<Expression<Func<TEntity, object>>> excludeProperties = null)
        {
       
            entity.ObjectState = ObjectState.Modified;
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Update(entity);
            }
        }
        public virtual void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            Delete(entity);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public virtual void Delete(TEntity entity)
        {
            entity.ObjectState = ObjectState.Deleted;
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
        }

        public IQueryFluent<TEntity> Query()
        {
            return new QueryFluent<TEntity>(this);
        }

        public virtual IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject)
        {
            return new QueryFluent<TEntity>(this, queryObject);
        }

        public virtual IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query)
        {
            return new QueryFluent<TEntity>(this, query);
        }

        public IQueryable<TEntity> Queryable()
        {
            return _dbSet;
        }

        public IRepository<T> GetRepository<T>() where T : class, IObjectState
        {
            return _unitOfWork.Repository<T>();
        }

        public virtual async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await _dbSet.FindAsync(keyValues);
        }

        public virtual async Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return await _dbSet.FindAsync(cancellationToken, keyValues).ConfigureAwait(false);
        }

        public virtual async Task<bool> DeleteAsync(params object[] keyValues)
        {
            return await DeleteAsync(CancellationToken.None, keyValues).ConfigureAwait(false);
        }

        public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            var entity = await FindAsync(cancellationToken, keyValues).ConfigureAwait(false);

            if (entity == null)
            {
                return false;
            }

            entity.ObjectState = ObjectState.Deleted;
            _dbSet.Attach(entity);

            return true;
        }

        public int ExecSQLCommand(string sqlCommand, params object[] paramertes)
        {
            return _context.ExecSQLCommand(sqlCommand, paramertes);
        }
        public async Task<int> ExecuteSqlRawAsync(string sqlCommand, params object[] paramertes)
        {
            return await _context.Database.ExecuteSqlRawAsync(sqlCommand, paramertes).ConfigureAwait(false);
        }

        internal IQueryable<TEntity> Select(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           List<Expression<Func<TEntity, object>>> includes = null,
           int? page = null,
           int? pageSize = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            return query;
        }

        internal async Task<IEnumerable<TEntity>> SelectAsync(
            Expression<Func<TEntity, bool>> query = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            return await Task.Run(() => Select(query, orderBy, includes, page, pageSize).AsEnumerable()).ConfigureAwait(false);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            entity.ObjectState = ObjectState.Modified;
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null,
            int? take = null, int? skip = null,
            Expression<Func<TEntity, object>> orderExpression = null,
            params string[] propertiesIncluded)
        {
            var query = DbSet.DynamicIncludeProperty(propertiesIncluded).AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderExpression != null)
            {
                query = query.OrderByDescending(orderExpression);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            var entities = await query.ToListAsync();

            return entities;
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = DbSet.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var totalItems = await query.CountAsync();

            return totalItems;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params string[] propertiesIncluded)
        {
            var query = DbSet.DynamicIncludeProperty(propertiesIncluded);

            var entity = await query.FirstOrDefaultAsync(predicate);

            return entity;
        }
        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate = null,
     Expression<Func<TEntity, object>> orderExpression = null,
     params string[] propertiesIncluded)
        {
            var query = DbSet.DynamicIncludeProperty(propertiesIncluded).AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderExpression != null)
            {
                query = query.OrderByDescending(orderExpression);
            }

            return query.ToList();
        }

        public PaginatedList<TEntity> GetPaginatedList(Expression<Func<TEntity, bool>> predicate = null,
         int take = 50, int skip = 0,
         Expression<Func<TEntity, object>> orderExpression = null,
         params string[] propertiesIncluded)
        {
            var query = DbSet.DynamicIncludeProperty(propertiesIncluded).AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderExpression != null)
            {
                query = query.OrderByDescending(orderExpression);
            }

            var result = new PaginatedList<TEntity>(query, skip, take);
            return result;
        }
    }
}
