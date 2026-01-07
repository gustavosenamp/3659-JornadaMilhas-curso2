using Bogus;
using JornadaMilhas.Dados;
using JornadaMilhasV1.Gerenciador;
using JornadaMilhasV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Test.Integracao
{
    [Collection(nameof(ContextoCollection))]
    public class OfertaViagemDAORecuperaMaiorDesconto : IDisposable
    {
        private readonly JornadaMilhasContext _context;
        private readonly ContextoFixture fixture;

        public OfertaViagemDAORecuperaMaiorDesconto(ContextoFixture fixture)
        {
            _context = fixture.Context;
            this.fixture = fixture;
        }

        public void Dispose()
        {
            fixture.LimpaDadosDoBanco();
        }

        [Fact]
        public void RetornaOfertaEspecificaQuandoDestinoSaoPauloEDesconto60()
        {
            //arrange
            Rota rota = new Rota("Cuiabá", "São Paulo");
            Periodo periodo = new PeriodoDataBuilder() { DataFinal = new DateTime(2024, 5 , 20) }.Build();
            fixture.CriaDadosFake();

            var ofertaEscolhida = new OfertaViagem(rota, periodo, 80)
            {
                Desconto = 60,
                Ativa = true
            };

            var dao = new OfertaViagemDAL(_context);
            dao.Adicionar(ofertaEscolhida);

            Func<OfertaViagem, bool> filtro = o => o.Rota.Destino.Equals("São Paulo");
            var precoEsperado = 20;

            //act
            var oferta = dao.RecuperaMaiorDesconto(filtro);

            //assert
            Assert.NotNull(oferta);
            Assert.Equal(precoEsperado, oferta.Preco, 0.0001);
        }
    }
}
