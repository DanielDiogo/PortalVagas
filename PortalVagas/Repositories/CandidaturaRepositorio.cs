using System.Text.Json;
using PortalVagas.Models;

namespace PortalVagas.Repositories;

public class CandidaturaRepositorio : ICandidaturaRepositorio
{
    private const string CaminhoArquivo = "Data/candidaturas.txt";

    public async Task<List<Candidatura>> ObterTodosAsync()
    {
        if (!File.Exists(CaminhoArquivo)) return [];
        
        var json = await File.ReadAllTextAsync(CaminhoArquivo);

        if (string.IsNullOrEmpty(json)) return [];
        
        return JsonSerializer.Deserialize<List<Candidatura>>(json) ?? [];
    }

    public async Task SalvarTodosAsync(List<Candidatura> candidaturas)
    {
        var json = JsonSerializer.Serialize(candidaturas, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(CaminhoArquivo, json);
    }
}