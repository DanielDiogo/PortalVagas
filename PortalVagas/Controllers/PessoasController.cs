using Microsoft.AspNetCore.Mvc;
using PortalVagas.Services;
using PortalVagas.ViewModels;

namespace PortalVagas.Controllers;

public class PessoasController(IPessoaServico pessoaServico) : Controller
{
    public async Task<IActionResult> Index(int pagina = 1)
    {
        if (pagina < 1)
        {
            pagina = 1;
        }

        var viewModel = await pessoaServico.ObterPessoasPaginadoAsync(pagina);

        return View(viewModel);
    }

    public IActionResult Criar()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Criar(CriarPessoaViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        await pessoaServico.CriarAsync(viewModel);
        return RedirectToAction(nameof(Index));
    }
}