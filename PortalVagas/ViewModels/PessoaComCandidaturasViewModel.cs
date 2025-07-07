using PortalVagas.Models;

namespace PortalVagas.ViewModels;

public class PessoaComCandidaturasViewModel
{
    public Pessoa Pessoa { get; init; } = new();
    public List<CandidaturaInfoViewModel> Candidaturas { get; init; } = [];
}