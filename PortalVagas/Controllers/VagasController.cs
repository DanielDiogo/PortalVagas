using Microsoft.AspNetCore.Mvc;
using PortalVagas.Models;
using PortalVagas.Services;
using PortalVagas.ViewModels;

namespace PortalVagas.Controllers;

public class VagasController(
    IVagaServico vagaServico,
    ICandidaturaServico candidaturaServico) : Controller
{
    public async Task<IActionResult> Detalhes(int id)
    {
        var viewModel = await vagaServico.ObterVagaDetalhadaViewModelAsync(id);
        if (viewModel == null) return NotFound();
        return View(viewModel);
    }

    public IActionResult Criar()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Criar(CriarVagaViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        await vagaServico.CriarAsync(viewModel);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Candidatar(int vagaId, int pessoaSelecionadaId)
    {
        if (pessoaSelecionadaId > 0)
            await candidaturaServico.CandidatarAsync(vagaId, pessoaSelecionadaId);
        return RedirectToAction(nameof(Detalhes), new { id = vagaId });
    }

    [HttpPost]
    public async Task<IActionResult> Aprovar(int vagaId, int pessoaId)
    {
        await candidaturaServico.AtualizarStatusAsync(vagaId, pessoaId, StatusCandidatura.Aprovado);
        return RedirectToAction(nameof(Detalhes), new { id = vagaId });
    }

    [HttpPost]
    public async Task<IActionResult> Reprovar(int vagaId, int pessoaId)
    {
        await candidaturaServico.AtualizarStatusAsync(vagaId, pessoaId, StatusCandidatura.Reprovado);
        return RedirectToAction(nameof(Detalhes), new { id = vagaId });
    }

    public async Task<IActionResult> Index(string? termoBusca)
    {
        var vagasViewModel = await vagaServico.ObterListaDeVagasViewModelAsync(termoBusca);
        ViewData["FiltroAtual"] = termoBusca;
        return View(vagasViewModel);
    }
}