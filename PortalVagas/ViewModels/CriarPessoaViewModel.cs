using System.ComponentModel.DataAnnotations;

namespace PortalVagas.ViewModels;

public class CriarPessoaViewModel
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    public string Nome { get; init; } = string.Empty;
}