using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tcc.Sigo.Normas.Domain.Messages;

namespace Tcc.Sigo.Normas.Domain.Adapters
{
    public interface IAclAdapter
    {
        Task<bool> Post(NormaMessage normaMessage);
    }
}
