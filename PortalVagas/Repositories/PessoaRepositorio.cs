using System.Text.Json;
using PortalVagas.Models;

namespace PortalVagas.Repositories;

public class PessoaRepositorio : IPessoaRepositorio
{
    private const string CaminhoArquivo = "Data/pessoas.txt";

    public async Task<List<Pessoa>> ObterTodosAsync()
    {
        if (!File.Exists(CaminhoArquivo)) return [];
        
        var json = await File.ReadAllTextAsync(CaminhoArquivo);
        
        if (string.IsNullOrEmpty(json)) return [];
        
        return JsonSerializer.Deserialize<List<Pessoa>>(json) ?? [];
    }

    public async Task SalvarTodosAsync(List<Pessoa> pessoas)
    {
        var json = JsonSerializer.Serialize(pessoas, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(CaminhoArquivo, json);
    }
}