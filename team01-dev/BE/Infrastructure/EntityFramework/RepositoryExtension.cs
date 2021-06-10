using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.EntityFramework
{
    public static class RepositoryExtension
    {
        public static async Task<string> GetKeyObjectUnique<TEntity>(this IRepositoryAsync<TEntity> repository, string prefix, Expression<Func<TEntity, bool>> condition = null)
            where TEntity : class, IObjectState, IEntityCreateInfo
        {
            int current = 0;
            try
            {
                var items = repository.Queryable();
                if (condition != null) items = items.Where(condition);
                current = await items.CountAsync(x => x.CreatedDate.HasValue && x.CreatedDate.Value.Year == DateTime.Now.Year);
            }
            catch
            {
                current = 0;
            }

            current++;
            return $"{prefix}-{DateTime.Now:ddMMyy}-{current:00000}";
        }



    }
}
