using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tcc.Sigo.Normas.Domain.Models;

namespace Tcc.Sigo.Normas.Domain.Repositories
{
    public interface INormaWriteOnlyRepository
    {
        Task Incluir(NormaModel norma);
        Task Alterar(NormaModel norma);
        Task AtivarInativar(Guid id, bool status);
    }
}
