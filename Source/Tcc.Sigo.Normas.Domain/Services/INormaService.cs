using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tcc.Sigo.Normas.Domain.Entities;
using Tcc.Sigo.Normas.Domain.Models;
using Tcc.Sigo.Normas.Domain.Results;

namespace Tcc.Sigo.Normas.Domain.Services
{
    public interface INormaService
    {
        Task<Result<NormaEntity>> Alterar(NormaEntity normaEntity);
        Task<Result> AtivarInativar(Guid id, bool status);
        Task<Result<NormaEntity>> Inserir(NormaEntity normaEntity);
        Task<IEnumerable<NormaModel>> Obter();
        Task<NormaModel> ObterPor(Guid id);
        Task<IEnumerable<NormaModel>> ObterPor(byte area);
    }
}
