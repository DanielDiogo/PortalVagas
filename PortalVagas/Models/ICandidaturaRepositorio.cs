namespace PortalVagas.Models;

public interface ICandidaturaRepositorio
{
    Task<List<Candidatura>> ObterTodosAsync();
    Task SalvarTodosAsync(List<Candidatura> candidaturas);
}