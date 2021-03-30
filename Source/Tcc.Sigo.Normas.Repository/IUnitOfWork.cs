using System;
using System.Collections.Generic;
using System.Text;

namespace Tcc.Sigo.Normas.Repository
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
