namespace PortalVagas.Models;

public class Candidatura
{
    public int IdVaga { get; set; }
    public int IdPessoa { get; set; }
    public StatusCandidatura Status { get; set; }
}

public enum StatusCandidatura
{
    EmAnalise,
    Aprovado,
    Reprovado
}