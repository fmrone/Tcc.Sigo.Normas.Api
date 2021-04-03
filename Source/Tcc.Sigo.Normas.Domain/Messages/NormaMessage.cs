using System;
using System.Collections.Generic;
using System.Text;
using Tcc.Sigo.Normas.Domain.Models;

namespace Tcc.Sigo.Normas.Domain.Messages
{
    public class NormaMessage : NormaModel
    {
        public byte Operacao { get; set; }
    }
}
