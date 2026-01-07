using JornadaMilhas.Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace JornadaMilhas.Test.Integracao
{
    [Collection(nameof(ContextoCollection))]
    public class OfertaViagemDAORecuperarPorId
    {
        private readonly JornadaMilhasContext _context;

        public OfertaViagemDAORecuperarPorId(ITestOutputHelper output, ContextoFixture fixture)
        {
            _context = fixture.Context;
            output.WriteLine(_context.GetHashCode().ToString());
        }

        [Fact]
        public void RetornaNuloQuandoIdInexistente()
        {
            // Arrange
            var ofertaViagemDAL = new OfertaViagemDAL(_context);
            int idInexistente = -1;

            // Act
            var resultado = ofertaViagemDAL.RecuperarPorId(idInexistente);

            // Assert
            Assert.Null(resultado);
        }
    }
}
