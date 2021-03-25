using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Tcc.Sigo.Normas.Repository
{

    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DbSession _dbSession;

        public UnitOfWork(DbSession dbSession)
        {
            _dbSession = dbSession ??
                throw new ArgumentNullException(nameof(dbSession));
        }

        public void BeginTransaction()
        {
            _dbSession.Transaction = _dbSession.Connection.BeginTransaction();
        }

        public void Commit()
        {
            _dbSession.Transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _dbSession.Transaction.Rollback();
            Dispose();
        }

        public void Dispose() => _dbSession.Transaction?.Dispose();
    }
    
}
