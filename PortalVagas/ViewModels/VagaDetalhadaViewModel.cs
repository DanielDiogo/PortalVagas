using Microsoft.AspNetCore.Mvc.Rendering;
using PortalVagas.Models;

namespace PortalVagas.ViewModels;

public class VagaDetalhadaViewModel
{
    public Vaga Vaga { get; init; } = new();
    public List<CandidatoViewModel> Candidatos { get; init; } = [];
    public List<SelectListItem> PessoasDisponiveis { get; init; } = [];
}