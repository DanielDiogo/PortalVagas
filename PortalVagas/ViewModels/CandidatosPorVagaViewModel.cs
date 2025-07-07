using PortalVagas.Models;

namespace PortalVagas.ViewModels;

public class CandidatosPorVagaViewModel
{
    public Vaga Vaga { get; init; } = new();
    public List<CandidatoViewModel> Candidatos { get; init; } = [];
}