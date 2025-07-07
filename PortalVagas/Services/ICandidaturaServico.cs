using PortalVagas.Models;
using PortalVagas.ViewModels;

namespace PortalVagas.Services;

public interface ICandidaturaServico
{
    Task CandidatarAsync(int vagaId, int pessoaId);
    Task<List<CandidatoViewModel>> ObterCandidatosViewModelPorVagaAsync(int vagaId);
    Task AtualizarStatusAsync(int vagaId, int pessoaId, StatusCandidatura novoStatus);
    Task<List<CandidatoViewModel>> ObterAprovadosPorVagaAsync(int vagaId);
    Task<string> GerarCsvAprovadosAsync(int vagaId);
}