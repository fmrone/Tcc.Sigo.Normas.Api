using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tcc.Sigo.Normas.Domain.Adapters
{
    public interface IMomAdapter
    {
        /// <summary>
        /// Publicar mensaagem
        /// </summary>
        /// <returns></returns>
        Task Publicar();
    }
}
