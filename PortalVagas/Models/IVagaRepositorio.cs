namespace PortalVagas.Models;

public interface IVagaRepositorio
{
    Task<List<Vaga>> ObterTodosAsync();
    Task SalvarTodosAsync(List<Vaga> vagas);
}