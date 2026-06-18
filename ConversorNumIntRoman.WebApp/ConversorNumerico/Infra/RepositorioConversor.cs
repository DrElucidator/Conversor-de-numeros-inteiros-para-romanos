using System.Text.Json;
using ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Dominio;

namespace ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Infra;

public class RepositorioConversor : IRepositorioConversor
{
    private const string CaminhoArquivo = "conversoes.json";

    private List<ConversorNumIntRoman> Carregar()
    {
        if (!File.Exists(CaminhoArquivo))
            return new List<ConversorNumIntRoman>();

        var json = File.ReadAllText(CaminhoArquivo);
        return JsonSerializer.Deserialize<List<ConversorNumIntRoman>>(json) ?? new List<ConversorNumIntRoman>();
    }

    private void Salvar(List<ConversorNumIntRoman> lista)
    {
        var json = JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(CaminhoArquivo, json);
    }

    public void Cadastrar(ConversorNumIntRoman entidade)
    {
        var lista = Carregar();
        lista.Add(entidade);
        Salvar(lista);
    }

    public List<ConversorNumIntRoman> SelecionarTodos() => Carregar();

    public ConversorNumIntRoman? SelecionarPorId(Guid id) => Carregar().FirstOrDefault(c => c.Id == id);

    public bool Excluir(Guid id)
    {
        var lista = Carregar();
        var entidade = lista.FirstOrDefault(c => c.Id == id);

        if (entidade == null)
            return false;

        lista.Remove(entidade);
        Salvar(lista);
        return true;
    }
}