using System.Text;
using PortalVagas.Models;
using PortalVagas.ViewModels;

namespace PortalVagas.Services;

public class CandidaturaServico(
    ICandidaturaRepositorio candidaturaRepo,
    IPessoaRepositorio pessoaRepo) : ICandidaturaServico
{
    public async Task CandidatarAsync(int vagaId, int pessoaId)
    {
        var candidaturas = await candidaturaRepo.ObterTodosAsync();
        if (candidaturas.Any(c => c.IdVaga == vagaId && c.IdPessoa == pessoaId)) return;

        candidaturas.Add(new Candidatura
        {
            IdVaga = vagaId,
            IdPessoa = pessoaId,
            Status = StatusCandidatura.EmAnalise
        });
        await candidaturaRepo.SalvarTodosAsync(candidaturas);
    }

    public async Task<List<CandidatoViewModel>> ObterCandidatosViewModelPorVagaAsync(int vagaId)
    {
        var candidaturas = await candidaturaRepo.ObterTodosAsync();
        var pessoas = await pessoaRepo.ObterTodosAsync();

        return (from c in candidaturas
            join p in pessoas on c.IdPessoa equals p.Id
            where c.IdVaga == vagaId
            select new CandidatoViewModel
            {
                PessoaId = p.Id,
                NomePessoa = p.Nome,
                Status = c.Status
            }).ToList();
    }

    public async Task AtualizarStatusAsync(int vagaId, int pessoaId, StatusCandidatura novoStatus)
    {
        var candidaturas = await candidaturaRepo.ObterTodosAsync();
        var alvo = candidaturas.FirstOrDefault(c => c.IdVaga == vagaId && c.IdPessoa == pessoaId);
        if (alvo != null)
        {
            alvo.Status = novoStatus;
            await candidaturaRepo.SalvarTodosAsync(candidaturas);
        }
    }

    public async Task<List<CandidatoViewModel>> ObterAprovadosPorVagaAsync(int vagaId)
    {
        var todasCandidaturas = await candidaturaRepo.ObterTodosAsync();
        var todasPessoas = await pessoaRepo.ObterTodosAsync();

        return (from cand in todasCandidaturas
            join pess in todasPessoas on cand.IdPessoa equals pess.Id
            where cand.IdVaga == vagaId && cand.Status == StatusCandidatura.Aprovado
            select new CandidatoViewModel
            {
                PessoaId = pess.Id,
                NomePessoa = pess.Nome,
                Status = cand.Status
            }).ToList();
    }

    public async Task<string> GerarCsvAprovadosAsync(int vagaId)
    {
        var aprovados = await ObterAprovadosPorVagaAsync(vagaId);

        var builder = new StringBuilder();

        builder.AppendLine("IdPessoa,Nome,Status");

        foreach (var candidato in aprovados)
        {
            builder.AppendLine($"{candidato.PessoaId},{candidato.NomePessoa},{candidato.Status}");
        }

        return builder.ToString();
    }
}