namespace ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Dominio;

public interface IRepositorioConversor
{
    void Cadastrar(ConversorNumIntRoman entidade);
    List<ConversorNumIntRoman> SelecionarTodos();
    ConversorNumIntRoman? SelecionarPorId(Guid id);
    bool Editar(Guid id, ConversorNumIntRoman entidade);
    bool Excluir(Guid id);
}