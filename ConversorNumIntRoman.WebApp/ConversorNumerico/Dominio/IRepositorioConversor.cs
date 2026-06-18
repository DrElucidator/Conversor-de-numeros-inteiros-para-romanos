namespace ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Dominio;

public interface IRepositorioConversor
{
    void Cadastrar(ConversorNumIntRoman entidade);
    List<ConversorNumIntRoman> SelecionarTodos();
    ConversorNumIntRoman? SelecionarPorId(Guid id);
    bool Excluir(Guid id);
}