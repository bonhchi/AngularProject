using System;

namespace Infrastructure.EntityFramework
{
    public interface IDataContext : IDisposable
    {
        int SaveChanges();
        void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState;
        void SyncObjectsStatePostCommit();
        int ExecSQLCommand(string sqlCommand, params object[] parameters);
    }
}
