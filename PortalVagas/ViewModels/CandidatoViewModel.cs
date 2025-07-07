using PortalVagas.Models;

namespace PortalVagas.ViewModels;

public class CandidatoViewModel
{
    public int PessoaId { get; init; }
    public string NomePessoa { get; init; } = string.Empty;
    public StatusCandidatura Status { get; init; }
}