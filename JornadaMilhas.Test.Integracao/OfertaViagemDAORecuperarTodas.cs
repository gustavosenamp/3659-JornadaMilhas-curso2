using JornadaMilhas.Dados;
using JornadaMilhasV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace JornadaMilhas.Test.Integracao
{
    [Collection(nameof(ContextoCollection))]
    public class OfertaViagemDAORecuperarTodas
    {
        private readonly JornadaMilhasContext _context;

        public OfertaViagemDAORecuperarTodas(ITestOutputHelper output, ContextoFixture context)
        {
            _context = context.Context;
            output.WriteLine(_context.GetHashCode().ToString());
        }

        [Fact]
        public void RetornaListaDeOfertas()
        {
            // Arrange
            var ofertaViagemDAL = new OfertaViagemDAL(_context);

            // Act
            var resultado = ofertaViagemDAL.RecuperarTodas();

            // Assert
            Assert.NotNull(resultado);
            Assert.IsType<List<OfertaViagem>>(resultado);
        }
    }
}
