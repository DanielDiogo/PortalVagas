namespace PortalVagas.ViewModels;

public class PessoasPaginadasViewModel
{
    public List<PessoaComCandidaturasViewModel> Pessoas { get; init; } = [];
    public int PaginaAtual { get; init; }
    public int TotalPaginas { get; init; }
}