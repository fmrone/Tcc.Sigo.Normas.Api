using System;
using System.Data;
using System.Data.SqlClient;

namespace Tcc.Sigo.Normas.Repository
{
    public sealed class DbSession : IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public DbSession()
        {
            Connection = new SqlConnection("");
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
