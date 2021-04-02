using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tcc.Sigo.Normas.Domain.Adapters;
using Tcc.Sigo.Normas.Domain.Models;

namespace Tcc.Sigo.Normas.MomAdapter
{
    public class RabbitMQMomAdapter : IMomAdapter
    {
        public Task Publicar(NormaModel normaModel)
        {
            throw new NotImplementedException();
        }
    }
}
