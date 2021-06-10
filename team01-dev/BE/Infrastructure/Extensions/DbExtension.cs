using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Extensions
{
    public static class DbExtension
    {
        public static IQueryable<TEntity> DynamicIncludeProperty<TEntity>(this DbSet<TEntity> dbSet, params string[] properties) where TEntity : class
        {
            var query = dbSet.AsQueryable();

            var queryIncluded = properties.Aggregate(query, (currentQuery, property) => currentQuery.Include(property));

            return queryIncluded;
        }
    }
}
