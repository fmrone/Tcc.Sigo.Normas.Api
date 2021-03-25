using System;
using System.Collections.Generic;
using System.Text;

namespace Tcc.Sigo.Normas.Domain.Entities
{
    public class NormaEntity
    {
        public string Codigo { get; private set; }
        public string Descricao { get; private set; }
        public byte Area { get; private set; }
        public bool Status { get; private set; }
        public DateTime CadastradoEm { get; private set; }
        public DateTime EmVigorDesde { get; private set; }
        public DateTime? EmVigorAte { get; private set; }
        public string OrgaoLegal { get; private set; }

        /// <summary>
        /// Construtor para inclusão de normas
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="descricao"></param>
        /// <param name="area"></param>
        /// <param name="emVigorDesde"></param>
        /// <param name="emVigorAte"></param>
        /// <param name="orgaoLegal"></param>
        public NormaEntity(string codigo, 
            string descricao, 
            byte area, 
            DateTime emVigorDesde, 
            DateTime? emVigorAte, 
            string orgaoLegal)
        {
            Codigo = codigo;
            Descricao = descricao;
            Area = area;
            Status = true;
            CadastradoEm = DateTime.Now;
            EmVigorDesde = emVigorDesde;
            EmVigorAte = emVigorAte;
            OrgaoLegal = orgaoLegal;
        }

    }
}
