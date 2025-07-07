using PortalVagas.Models;

namespace PortalVagas.ViewModels;

public class CandidaturaInfoViewModel
{
    public string TituloVaga { get; init; } = string.Empty;
    public StatusCandidatura Status { get; init; }
}