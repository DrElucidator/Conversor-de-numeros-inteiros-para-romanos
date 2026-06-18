namespace ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Dominio;

using ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Aplicacao;

public interface IRepositorioConversor
{
    void Salvar(DetalhesConversaoDto detalhes);
}