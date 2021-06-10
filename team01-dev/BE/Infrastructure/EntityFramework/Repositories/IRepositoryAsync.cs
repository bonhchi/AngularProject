using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.EntityFramework
{
    public interface IRepositoryAsync<TEntity> : IRepository<TEntity> where TEntity : class, IObjectState
    {
        void InsertAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> FindAsync(params object[] keyValues);

        Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);

        Task<bool> DeleteAsync(params object[] keyValues);

        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);

        Task<int> ExecuteSqlRawAsync(string sqlCommand, params object[] keyValues);

        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null,
            int? take = null, int? skip = null,
            Expression<Func<TEntity, object>> orderExpression = null,
            params string[] propertiesIncluded);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params string[] propertiesIncluded);
    }
}
