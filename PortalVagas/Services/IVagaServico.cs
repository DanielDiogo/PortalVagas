using PortalVagas.Models;
using PortalVagas.ViewModels;

namespace PortalVagas.Services;

public interface IVagaServico
{
    Task<Vaga?> ObterPorIdAsync(int id);
    Task CriarAsync(CriarVagaViewModel viewModel);
    Task<VagaDetalhadaViewModel?> ObterVagaDetalhadaViewModelAsync(int vagaId);
    Task<CandidatosPorVagaViewModel?> ObterCandidatosPorVagaViewModelAsync(int vagaId);
    Task<List<VagaListaViewModel>> ObterListaDeVagasViewModelAsync(string? termoBusca);
}