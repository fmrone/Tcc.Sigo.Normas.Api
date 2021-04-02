using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tcc.Sigo.Normas.Domain.Entities;
using Tcc.Sigo.Normas.Domain.Models;

namespace Tcc.Sigo.Normas.Domain.Services
{
    public interface INormaService
    {
        Task<NormaModel> ObterPor(Guid id);
        Task Inserir(NormaEntity normaEntity);
    }
}
