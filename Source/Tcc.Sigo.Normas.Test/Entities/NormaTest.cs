using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tcc.Sigo.Normas.Domain.Entities;
using Xunit;

namespace Tcc.Sigo.Normas.Test.Entities
{
    public class NormaTest
    {
        [Theory]
        [InlineData("156456546546S", "Norma 1", 0, "")]
        [InlineData(null, "Norma 1", 0, "")]
        [InlineData("1", null, 0, "")]
        [InlineData(null, null, 0, "")]
        public void InserirNormaIsInvalid(string codigo,
            string descricao,
            byte area,
            string orgaoLegal)
        {

            DateTime emVigorDesde = DateTime.Now;
            DateTime? emVigorAte = null;

            var normaEntity = new NormaEntity(codigo,
                descricao,
                area,
                emVigorDesde,
                emVigorAte,
                orgaoLegal);

            //O teste foi construído para verificar se o cadastro da norma é inválido
            //Para evidenciar a quebra do mesmo, foi alterado o assert, considerando que a norma inválida é válida
            //O teste falha e o git Actions não enviará o commit para produção
            Assert.True(normaEntity.Valid);
            Assert.True(normaEntity.Notifications.Any());
        }
    }
}
