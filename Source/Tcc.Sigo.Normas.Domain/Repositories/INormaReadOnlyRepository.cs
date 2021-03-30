using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tcc.Sigo.Normas.Domain.Models;

namespace Tcc.Sigo.Normas.Domain.Repositories
{
    public interface INormaReadOnlyRepository
    {
        Task<NormaModel> ObterPor(Guid id);
        Task<IEnumerable<NormaModel>> ObterPor(byte area);
        Task<IEnumerable<NormaModel>> Obter();
    }
}
