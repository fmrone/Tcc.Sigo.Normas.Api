using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Tcc.Sigo.Normas.Domain.Models;
using Tcc.Sigo.Normas.Domain.Repositories;

namespace Tcc.Sigo.Normas.Repository.Repositories
{
    public class NormaRepository : INormaWriteOnlyRepository, INormaReadOnlyRepository
    {
        private readonly DbSession _dbSession;

        static NormaRepository() => SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);

        public NormaRepository(DbSession dbSession)
        {
            _dbSession = dbSession ?? 
                throw new ArgumentNullException(nameof(dbSession));
        }

        public Task Alterar(NormaModel norma)
        {
            throw new NotImplementedException();
        }

        public Task AtivarInativar(Guid id, bool status)
        {
            throw new NotImplementedException();
        }

        public Task Incluir(NormaModel norma)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NormaModel>> Obter()
        {
            throw new NotImplementedException();
        }

        public Task<NormaModel> ObterPor(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NormaModel>> ObterPor(byte area)
        {
            throw new NotImplementedException();
        }
    }
}
