namespace PortalVagas.Models;

public interface IPessoaRepositorio
{
    Task<List<Pessoa>> ObterTodosAsync();
    Task SalvarTodosAsync(List<Pessoa> pessoas);
}
