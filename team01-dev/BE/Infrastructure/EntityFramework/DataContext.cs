using Infrastructure.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.EntityFramework
{
    public class DataContext : DbContext, IDataContextAsync
    {
        #region Private Fields

        private readonly Guid _instanceId;
        #endregion Private Fields

        public Guid InstanceId { get { return _instanceId; } }

        DatabaseFacade IDataContextAsync.Database { get { return this.Database; } }

        public DataContext(DbContextOptions options) : base(options)
        {
            _instanceId = Guid.NewGuid();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            SyncObjectsStatePreCommit();
            var changesAsync = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            SyncObjectsStatePostCommit();
            return changesAsync;
        }

        public override int SaveChanges()
        {
            SyncObjectsStatePreCommit();
            var changes = base.SaveChanges();
            SyncObjectsStatePostCommit();
            return changes;
        }

        public int ExecSQLCommand(string sqlCommand, params object[] parameters)
        {
            return base.Database.ExecuteSqlRaw(sqlCommand, parameters);
        }

        public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState
        {
            Entry(entity).State = StateHelper.ConvertState(entity.ObjectState);
        }

        private void SyncObjectsStatePreCommit()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries())
            {
                dbEntityEntry.State = StateHelper.ConvertState(((IObjectState)dbEntityEntry.Entity).ObjectState);
            }
        }

        public void SyncObjectsStatePostCommit()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries())
            {
                ((IObjectState)dbEntityEntry.Entity).ObjectState = StateHelper.ConvertState(dbEntityEntry.State);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
            base.OnModelCreating(modelBuilder);
        }
    }
}