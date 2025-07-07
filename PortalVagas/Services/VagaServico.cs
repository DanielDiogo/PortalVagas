using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortalVagas.Models;
using PortalVagas.ViewModels;

namespace PortalVagas.Services;

public class VagaServico(
    IVagaRepositorio vagaRepositorio,
    ICandidaturaRepositorio candidaturaRepositorio,
    IPessoaServico pessoaServico,
    ICandidaturaServico candidaturaServico,
    IMapper mapper)
    : IVagaServico
{
    public async Task<Vaga?> ObterPorIdAsync(int id)
    {
        var vagas = await vagaRepositorio.ObterTodosAsync();
        return vagas.FirstOrDefault(v => v.Id == id);
    }

    public async Task CriarAsync(CriarVagaViewModel viewModel)
    {
        var novaVaga = mapper.Map<Vaga>(viewModel);
        var vagas = await vagaRepositorio.ObterTodosAsync();
        novaVaga.Id = vagas.Count != 0 ? vagas.Max(v => v.Id) + 1 : 1;
        vagas.Add(novaVaga);
        await vagaRepositorio.SalvarTodosAsync(vagas);
    }

    public async Task<List<VagaListaViewModel>> ObterListaDeVagasViewModelAsync(string? termoBusca)
    {
        var todasVagas = await vagaRepositorio.ObterTodosAsync();
        var todasCandidaturas = await candidaturaRepositorio.ObterTodosAsync();

        if (!string.IsNullOrWhiteSpace(termoBusca))
        {
            todasVagas = todasVagas.Where(v =>
                v.Titulo.Contains(termoBusca, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }

        return todasVagas.Select(vaga => new VagaListaViewModel
        {
            Id = vaga.Id,
            Titulo = vaga.Titulo,
            NumeroDeCandidatos = todasCandidaturas.Count(c => c.IdVaga == vaga.Id)
        }).ToList();
    }

    public async Task<VagaDetalhadaViewModel?> ObterVagaDetalhadaViewModelAsync(int vagaId)
    {
        var vaga = await ObterPorIdAsync(vagaId);
        if (vaga == null) return null;

        var candidatos = await candidaturaServico.ObterCandidatosViewModelPorVagaAsync(vagaId);
        var todasPessoas = await pessoaServico.ObterTodasAsync();

        var pessoasDisponiveis = todasPessoas
            .Where(p => candidatos.All(c => c.PessoaId != p.Id))
            .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Nome })
            .ToList();

        return new VagaDetalhadaViewModel
        {
            Vaga = vaga,
            Candidatos = candidatos,
            PessoasDisponiveis = pessoasDisponiveis
        };
    }

    public async Task<CandidatosPorVagaViewModel?> ObterCandidatosPorVagaViewModelAsync(int vagaId)
    {
        var vaga = await ObterPorIdAsync(vagaId);
        if (vaga == null)
        {
            return null;
        }

        var candidatos = await candidaturaServico.ObterCandidatosViewModelPorVagaAsync(vagaId);

        return new CandidatosPorVagaViewModel
        {
            Vaga = vaga,
            Candidatos = candidatos
        };
    }
}