﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Tcc.Sigo.Normas.Domain.Models
{
    public class NormaModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public byte Area { get; set; }
        public bool Status { get; set; }
        public DateTime CadastradoEm { get; set; }
        public DateTime EmVigorDesde { get; set; }
        public DateTime? EmVigorAte { get; set; }
        public string OrgaoLegal { get; set; }
    }
}
