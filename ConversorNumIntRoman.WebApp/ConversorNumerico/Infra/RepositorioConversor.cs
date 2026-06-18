using System.Text.Json;
using ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Aplicacao;
using ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Dominio;

namespace ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Infra;

public class RepositorioConversor : IRepositorioConversor
{
    private const string CaminhoArquivo = "conversoes.json";

    public void Salvar(DetalhesConversaoDto detalhes)
    {
        List<DetalhesConversaoDto> lista = new();

        if (File.Exists(CaminhoArquivo))
        {
            var json = File.ReadAllText(CaminhoArquivo);
            lista = JsonSerializer.Deserialize<List<DetalhesConversaoDto>>(json) ?? new();
        }

        lista.Add(detalhes);

        var novoJson = JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(CaminhoArquivo, novoJson);
    }
}