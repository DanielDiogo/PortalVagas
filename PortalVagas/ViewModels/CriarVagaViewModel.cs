using System.ComponentModel.DataAnnotations;

namespace PortalVagas.ViewModels;

public class CriarVagaViewModel
{
    [Required(ErrorMessage = "O campo Título é obrigatório.")]
    [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
    public string Titulo { get; init; } = string.Empty;

    [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
    public string Descricao { get; init; } = string.Empty;
}