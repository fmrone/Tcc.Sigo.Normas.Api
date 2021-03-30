using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace Tcc.Sigo.Normas.Domain.Entities
{
    public class NormaEntity : Notifiable
    {
        public Guid Id { get; private set; }
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
            Id = Guid.NewGuid();
            Codigo = codigo;
            Descricao = descricao;
            Area = area;
            Status = true;
            CadastradoEm = DateTime.Now;
            EmVigorDesde = emVigorDesde;
            EmVigorAte = emVigorAte;
            OrgaoLegal = orgaoLegal;

            AdicionaContrato();

            ValidaTamanhoCamposTexto();

            ValidaDatas();
        }

        /// <summary>
        /// Construtor para alteração de normas
        /// </summary>
        /// <param name="id"></param>
        /// <param name="codigo"></param>
        /// <param name="descricao"></param>
        /// <param name="area"></param>
        /// <param name="emVigorDesde"></param>
        /// <param name="emVigorAte"></param>
        /// <param name="orgaoLegal"></param>
        public NormaEntity(Guid id,
            string codigo,
            string descricao,
            byte area,
            bool status,
            DateTime emVigorDesde,
            DateTime? emVigorAte,
            string orgaoLegal)
        {
            Id = id;
            Codigo = codigo;
            Descricao = descricao;
            Area = area;
            Status = status;
            EmVigorDesde = emVigorDesde;
            EmVigorAte = emVigorAte;
            OrgaoLegal = orgaoLegal;

            AdicionaContrato();

            ValidaTamanhoCamposTexto();

            ValidaDatas();
        }

        private void AdicionaContrato()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(Codigo, nameof(Codigo), "Código não pode ser nulo")
                .IsNotNull(Descricao, nameof(Descricao), "Descrição não pode ser nulo")
                .IsNotNull(OrgaoLegal, nameof(OrgaoLegal), "Órgão legal não pode ser nulo"));
        }

        private void ValidaTamanhoCamposTexto()
        {
            if (Codigo.Length > 6)
                AddNotification(nameof(Codigo), "Tamanho do campo código deve ser menor do que 6");

            if (Descricao.Length > 100)
                AddNotification(nameof(Descricao), "Tamanho do campo descrição deve ser menor do que 100");

            if (OrgaoLegal.Length > 100)
                AddNotification(nameof(OrgaoLegal), "Tamanho do campo órgão legal deve ser menor do que 100");
        }

        private void ValidaDatas()
        {
            if (EmVigorDesde < DateTime.MinValue || EmVigorDesde < new DateTime(1930, 1, 1))
                AddNotification(nameof(EmVigorAte), $"Data de início de vigor inválida {EmVigorDesde:dd/MM/yyyy}");

            if (EmVigorAte.HasValue)
                if (EmVigorAte.Value < DateTime.MinValue || EmVigorAte.Value < new DateTime(1930, 1, 1))
                    AddNotification(nameof(EmVigorAte), $"Data de início de vigor inválida {EmVigorDesde:dd/MM/yyyy}");

            if (EmVigorAte.HasValue)
                if (EmVigorAte.Value < EmVigorDesde)
                    AddNotification(nameof(EmVigorAte), $"A data fim do vigor {EmVigorAte.Value:dd/MM/yyyy} não pode ser inferior à data início de vigor {EmVigorDesde:dd/MM/yyyy}");
        }

    }
}
