using AutoMapper;
using PortalVagas.Models;
using PortalVagas.ViewModels;

namespace PortalVagas.Services;

public class PessoaServico(
    IPessoaRepositorio pessoaRepo,
    IVagaRepositorio vagaRepo,
    ICandidaturaRepositorio candidaturaRepo,
    IMapper mapper)
    : IPessoaServico
{
    public async Task<List<Pessoa>> ObterTodasAsync()
    {
        return await pessoaRepo.ObterTodosAsync();
    }

    public async Task CriarAsync(CriarPessoaViewModel viewModel)
    {
        var novaPessoa = mapper.Map<Pessoa>(viewModel);
        var pessoas = await pessoaRepo.ObterTodosAsync();
        novaPessoa.Id = pessoas.Count != 0 ? pessoas.Max(p => p.Id) + 1 : 1;
        pessoas.Add(novaPessoa);
        await pessoaRepo.SalvarTodosAsync(pessoas);
    }

    public async Task<PessoasPaginadasViewModel> ObterPessoasPaginadoAsync(int paginaAtual)
    {
        const int itensPorPagina = 10;
        
        var todasAsPessoasComCandidaturas = await ObterListaCompletaDePessoasComCandidaturas();

        var totalDeItens = todasAsPessoasComCandidaturas.Count;
        var totalPaginas = (int)Math.Ceiling(totalDeItens / (double)itensPorPagina);

        var pessoasDaPagina = todasAsPessoasComCandidaturas
            .Skip((paginaAtual - 1) * itensPorPagina)
            .Take(itensPorPagina)
            .ToList();

        return new PessoasPaginadasViewModel
        {
            Pessoas = pessoasDaPagina,
            PaginaAtual = paginaAtual,
            TotalPaginas = totalPaginas
        };
    }

    private async Task<List<PessoaComCandidaturasViewModel>> ObterListaCompletaDePessoasComCandidaturas()
    {
        var todasPessoas = await pessoaRepo.ObterTodosAsync();
        var todasVagas = await vagaRepo.ObterTodosAsync();
        var todasCandidaturas = await candidaturaRepo.ObterTodosAsync();

        return todasPessoas.Select(pessoa =>
        {
            var candidaturasDaPessoa = todasCandidaturas
                .Where(c => c.IdPessoa == pessoa.Id)
                .Select(c => new CandidaturaInfoViewModel
                {
                    TituloVaga = todasVagas.FirstOrDefault(v => v.Id == c.IdVaga)?.Titulo ?? "Vaga não encontrada",
                    Status = c.Status
                }).ToList();

            return new PessoaComCandidaturasViewModel
            {
                Pessoa = pessoa,
                Candidaturas = candidaturasDaPessoa
            };
        }).ToList();
    }
}