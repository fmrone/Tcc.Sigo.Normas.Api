using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tcc.Sigo.Normas.Api.Dtos
{
    public class NormaPut
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public byte Area { get; set; }
        public DateTime EmVigorDesde { get; set; }
        public DateTime? EmVigorAte { get; set; }
        public string OrgaoLegal { get; set; }
    }
}
