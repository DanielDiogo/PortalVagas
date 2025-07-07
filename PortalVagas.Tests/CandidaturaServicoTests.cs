using Moq;
using PortalVagas.Models;
using PortalVagas.Services;

namespace PortalVagas.Tests;

public class CandidaturaServicoTests
{
    [Fact]
    public async Task CandidatarAsync_QuandoCandidaturaJaExiste_NaoDeveSalvarNovamente()
    {
        const int vagaIdExistente = 1;
        const int pessoaIdExistente = 1;

        var candidaturasFalsas = new List<Candidatura>
        {
            new() { IdVaga = vagaIdExistente, IdPessoa = pessoaIdExistente }
        };

        var mockCandidaturaRepo = new Mock<ICandidaturaRepositorio>();
        mockCandidaturaRepo.Setup(repo => repo.ObterTodosAsync()).ReturnsAsync(candidaturasFalsas);

        var mockPessoaRepo = new Mock<IPessoaRepositorio>();

        var mockVagaRepo = new Mock<IVagaRepositorio>();

        var servico = new CandidaturaServico(
            mockCandidaturaRepo.Object,
            mockPessoaRepo.Object
        );

        await servico.CandidatarAsync(vagaIdExistente, pessoaIdExistente);

        mockCandidaturaRepo.Verify(
            repo => repo.SalvarTodosAsync(It.IsAny<List<Candidatura>>()),
            Times.Never()
        );
    }
}