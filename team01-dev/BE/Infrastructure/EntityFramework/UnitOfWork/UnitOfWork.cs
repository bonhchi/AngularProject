using Infrastructure.EntityFramework.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Extensions;

namespace Infrastructure.EntityFramework
{
    public class UnitOfWork : IUnitOfWorkAsync
    {
        #region Private Fields

        private IDataContextAsync _dbContext;
        private bool _disposed;
        private IDbContextTransaction _transaction;

        #endregion Private Fields

        #region Constuctor/Dispose
        protected IRepositoryProvider RepositoryProvider { get; set; }

        public UnitOfWork(IDataContextAsync dataContext, IRepositoryProvider repositoryProvider)
        {
            RepositoryProvider = repositoryProvider;
            RepositoryProvider.DataContext = _dbContext = dataContext;
            RepositoryProvider.UnitOfWork = this;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // free other managed objects that implement
                // IDisposable only

                try
                {
                    if (_dbContext != null)
                    {
                        var conn = _dbContext.Database.GetDbConnection();
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                }
                catch (ObjectDisposedException)
                {
                    // do nothing, the objectContext has already been disposed
                }

                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    _dbContext = null;
                }
            }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
        }

        #endregion Constuctor/Dispose

        public int SaveChanges()
        {
            if (_dbContext.IsNotNullOrEmpty())
            {
                var conn = _dbContext.Database.GetDbConnection();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                
                return _dbContext.SaveChanges();    
            }
            return -1;
        }

        public Task<int> SaveChangesAsync()
        {
            if (_dbContext.IsNotNullOrEmpty())
            {
                var conn = _dbContext.Database.GetDbConnection();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                
                return  _dbContext.SaveChangesAsync();    
            }
            return null;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            if (_dbContext.IsNotNullOrEmpty())
            {
                var conn = _dbContext.Database.GetDbConnection();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                
                
                return  _dbContext.SaveChangesAsync(cancellationToken);
            }

            return null;
        }


        #region Unit of Work Transactions
        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            if (_dbContext != null)
            {
                var conn = _dbContext.Database.GetDbConnection();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                _transaction = _dbContext.Database.BeginTransaction(isolationLevel);
            }
        }

        public bool Commit()
        {
            if(_transaction != null)
            {
                _transaction.Commit();
            }            
            return true;
        }

        public void Rollback()
        {
            if (_transaction != null)
                _transaction.Rollback();
        }

        #endregion
        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IObjectState
        {
            return RepositoryAsync<TEntity>();
        }

        public IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, IObjectState
        {
            return RepositoryProvider.GetRepositoryForEntityType<TEntity>();
        }
    }
}
