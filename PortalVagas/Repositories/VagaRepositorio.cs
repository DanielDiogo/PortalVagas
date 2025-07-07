using System.Text.Json;
using PortalVagas.Models;

namespace PortalVagas.Repositories;

public class VagaRepositorio : IVagaRepositorio
{
    private const string CaminhoArquivo = "Data/vagas.txt";

    public async Task<List<Vaga>> ObterTodosAsync()
    {
        if (!File.Exists(CaminhoArquivo))
        {
            return [];
        }

        var json = await File.ReadAllTextAsync(CaminhoArquivo);

        if (string.IsNullOrEmpty(json))
        {
            return [];
        }

        return JsonSerializer.Deserialize<List<Vaga>>(json) ?? [];
    }

    public async Task SalvarTodosAsync(List<Vaga> vagas)
    {
        var json = JsonSerializer.Serialize(vagas, new JsonSerializerOptions { WriteIndented = true });

        await File.WriteAllTextAsync(CaminhoArquivo, json);
    }
}