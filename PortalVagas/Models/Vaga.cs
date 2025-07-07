namespace PortalVagas.Models;

public class Vaga
{
    public int Id { get; set; }
    public string Titulo { get; init; } = string.Empty;
    public string Descricao { get; } = string.Empty;
}