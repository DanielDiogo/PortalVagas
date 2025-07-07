using System.Text;
using Microsoft.AspNetCore.Mvc;
using PortalVagas.Services;
using PortalVagas.ViewModels;

namespace PortalVagas.Controllers;

public class CandidaturasController(
    IVagaServico vagaServico,
    ICandidaturaServico candidaturaServico) : Controller
{
    public async Task<IActionResult> PorVaga(int id)
    {
        var viewModel = await vagaServico.ObterCandidatosPorVagaViewModelAsync(id);

        if (viewModel == null)
        {
            return NotFound();
        }

        return View(viewModel);
    }

    public async Task<IActionResult> Aprovados(int id)
    {
        var vaga = await vagaServico.ObterPorIdAsync(id);
        if (vaga == null)
        {
            return NotFound();
        }

        var candidatosAprovados = await candidaturaServico.ObterAprovadosPorVagaAsync(id);

        var viewModel = new CandidatosPorVagaViewModel
        {
            Vaga = vaga,
            Candidatos = candidatosAprovados
        };

        return View(viewModel);
    }

    public async Task<IActionResult> ExportarAprovadosCsv(int id)
    {
        var csvString = await candidaturaServico.GerarCsvAprovadosAsync(id);

        var fileBytes = Encoding.UTF8.GetBytes(csvString);

        var fileName = $"aprovados_vaga_{id}.csv";

        return File(fileBytes, "text/csv", fileName);
    }
}