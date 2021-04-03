using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public async Task Alterar(NormaModel norma)
        {
            if (norma == null)
                throw new ArgumentNullException(nameof(norma));

            var parametros = new DynamicParameters();
            parametros.Add("Id", norma.Id);
            parametros.Add("Codigo", norma.Codigo);
            parametros.Add("Descricao", norma.Descricao);
            parametros.Add("Area", norma.Area);
            parametros.Add("EmVigorDesde", norma.EmVigorDesde);
            parametros.Add("EmVigorAte", norma.EmVigorAte);
            parametros.Add("OrgaoLegal", norma.OrgaoLegal);

            var cmd = @"UPDATE Norma 
                           SET Codigo = @Codigo,
                               Descricao = @Descricao, 
                               Area = @Area,
                               EmVigorDesde = @EmVigorDesde, 
                               EmVigorAte = @EmVigorAte,
                               OrgaoLegal = @OrgaoLegal
                         WHERE Id = @Id";

            await _dbSession.Connection.ExecuteAsync(cmd, parametros, _dbSession.Transaction);
        }

        public async Task AtivarInativar(Guid id, bool status)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var parametros = new DynamicParameters();
            parametros.Add("Id", id);
            parametros.Add("Status", status, DbType.Boolean);
            

            var cmd = @"UPDATE Norma 
                           SET Status = @Status
                         WHERE Id = @Id";

            await _dbSession.Connection.ExecuteAsync(cmd, parametros, _dbSession.Transaction);
        }

        public async Task Incluir(NormaModel norma)
        {
            if (norma == null)
                throw new ArgumentNullException(nameof(norma));

            var parametros = new DynamicParameters();
            parametros.Add("Id", norma.Id);
            parametros.Add("Codigo", norma.Codigo);
            parametros.Add("Descricao", norma.Descricao);
            parametros.Add("Area", norma.Area);
            parametros.Add("Status", norma.Status, DbType.Boolean);
            parametros.Add("CadastradoEm", norma.CadastradoEm);
            parametros.Add("EmVigorDesde", norma.EmVigorDesde);
            parametros.Add("EmVigorAte", norma.EmVigorAte);
            parametros.Add("OrgaoLegal", norma.OrgaoLegal);

            var cmd = @"INSERT INTO Norma (Id, Codigo, Descricao, Area, Status, CadastradoEm, EmVigorDesde, EmVigorAte, OrgaoLegal)
                        VALUES (@Id, @Codigo, @Descricao, @Area, @Status, @CadastradoEm, @EmVigorDesde, @EmVigorAte, @OrgaoLegal)";

            await _dbSession.Connection.ExecuteAsync(cmd, parametros, _dbSession.Transaction);
        }

        public async Task<IEnumerable<NormaModel>> Obter()
        {
            var query = @"SELECT Id, Codigo, Descricao, Area, Status, CadastradoEm, EmVigorDesde, EmVigorAte, OrgaoLegal
                            FROM Norma";

            var normas = await _dbSession.Connection.QueryAsync<NormaModel>(query);

            return normas.ToList();
        }

        public async Task<NormaModel> ObterPor(Guid id)
        {
            var parametros = new DynamicParameters();
            parametros.Add("id", id);

            var query = @"SELECT Id, Codigo, Descricao, Area, Status, CadastradoEm, EmVigorDesde, EmVigorAte, OrgaoLegal
                            FROM Norma 
                           WHERE Id = @id";

            var norma = await _dbSession.Connection.QueryAsync<NormaModel>(query, parametros);

            return norma.SingleOrDefault();
        }

        public async Task<IEnumerable<NormaModel>> ObterPor(byte area)
        {
            var parametros = new DynamicParameters();
            parametros.Add("area", area);

            var query = @"SELECT Id, Codigo, Descricao, Area, Status, CadastradoEm, EmVigorDesde, EmVigorAte, OrgaoLegal
                            FROM Norma 
                           WHERE Area = @area";

            var normas = await _dbSession.Connection.QueryAsync<NormaModel>(query, parametros);

            return normas.ToList();
        }
    }
}
