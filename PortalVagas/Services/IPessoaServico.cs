using PortalVagas.Models;
using PortalVagas.ViewModels;

namespace PortalVagas.Services;

public interface IPessoaServico
{
    Task<List<Pessoa>> ObterTodasAsync();
    Task CriarAsync(CriarPessoaViewModel viewModel);
    Task<PessoasPaginadasViewModel> ObterPessoasPaginadoAsync(int paginaAtual);
}