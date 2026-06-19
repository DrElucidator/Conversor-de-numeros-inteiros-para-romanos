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

        string json = File.ReadAllText(CaminhoArquivo);
        List<ConversorNumIntRoman>? lista = JsonSerializer.Deserialize<List<ConversorNumIntRoman>>(json);
        return lista ?? new List<ConversorNumIntRoman>();
    }

    private void Salvar(List<ConversorNumIntRoman> lista)
    {
        string json = JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(CaminhoArquivo, json);
    }

    public void Cadastrar(ConversorNumIntRoman entidade)
    {
        List<ConversorNumIntRoman> lista = Carregar();
        lista.Add(entidade);
        Salvar(lista);
    }

    public List<ConversorNumIntRoman> SelecionarTodos() => Carregar();

    public ConversorNumIntRoman? SelecionarPorId(Guid id)
    {
        List<ConversorNumIntRoman> lista = Carregar();
        ConversorNumIntRoman? entidade = lista.FirstOrDefault(c => c.Id == id);
        return entidade;
    }

    public bool Excluir(Guid id)
    {
        List<ConversorNumIntRoman> lista = Carregar();
        ConversorNumIntRoman? entidade = lista.FirstOrDefault(c => c.Id == id);

        if (entidade == null)
            return false;

        lista.Remove(entidade);
        Salvar(lista);
        return true;
    }
}