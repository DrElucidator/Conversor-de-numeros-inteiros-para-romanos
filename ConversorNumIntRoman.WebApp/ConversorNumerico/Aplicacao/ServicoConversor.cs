using FluentResults;
using ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Dominio;

namespace ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Aplicacao;

public class ServicoConversor
{
    private readonly IRepositorioConversor repositorioConversor;

    public ServicoConversor(IRepositorioConversor repositorioConversor)
    {
        this.repositorioConversor = repositorioConversor;
    }

    public Result<DetalhesConversaoDto> ConverterParaRomano(ConverterParaRomanoDto dto)
    {
        if (dto.Numero < 1 || dto.Numero > 3999)
            return Falha(nameof(dto.Numero), "Número deve estar entre 1 e 3999.");

        string romano = ExecutarConversaoParaRomano(dto.Numero);

        ConversorNumIntRoman entidade = new ConversorNumIntRoman(dto.Numero, romano);

        repositorioConversor.Cadastrar(entidade);

        return Result.Ok(new DetalhesConversaoDto(entidade.Id, entidade.Numero, entidade.Romano, entidade.DataHora));
    }

    public Result<DetalhesConversaoDto> ConverterDeRomano(ConverterDeRomanoDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Romano))
            return Falha(nameof(dto.Romano), "Número romano inválido.");

        int numero = ExecutarConversaoDeRomano(dto.Romano);

        ConversorNumIntRoman entidade = new ConversorNumIntRoman(numero, dto.Romano.ToUpper());

        repositorioConversor.Cadastrar(entidade);

        return Result.Ok(new DetalhesConversaoDto(entidade.Id, entidade.Numero, entidade.Romano, entidade.DataHora));
    }

    public List<DetalhesConversaoDto> SelecionarTodos()
    {
        return repositorioConversor
            .SelecionarTodos()
            .Select(c => new DetalhesConversaoDto(c.Id, c.Numero, c.Romano, c.DataHora))
            .ToList();
    }

    public Result<DetalhesConversaoDto> SelecionarPorId(Guid id)
    {
        ConversorNumIntRoman? entidade = repositorioConversor.SelecionarPorId(id);

        if (entidade == null)
            return Result.Fail("Conversão não encontrada.");

        return Result.Ok(new DetalhesConversaoDto(entidade.Id, entidade.Numero, entidade.Romano, entidade.DataHora));
    }

    public Result Excluir(Guid id)
    {
        ConversorNumIntRoman? entidade = repositorioConversor.SelecionarPorId(id);

        if (entidade == null)
            return Result.Fail("Conversão não encontrada.");

        repositorioConversor.Excluir(id);

        return Result.Ok();
    }

    private static string ExecutarConversaoParaRomano(int numero)
    {
        List<(int valor, string simbolo)> numerais = new List<(int, string)>
        {
            (1000, "M"), (900, "CM"), (500, "D"), (400, "CD"),
            (100, "C"), (90, "XC"), (50, "L"), (40, "XL"),
            (10, "X"), (9, "IX"), (5, "V"), (4, "IV"), (1, "I")
        };

        System.Text.StringBuilder resultado = new System.Text.StringBuilder();
        foreach ((int valor, string simbolo) in numerais)
        {
            while (numero >= valor)
            {
                resultado.Append(simbolo);
                numero -= valor;
            }
        }
        return resultado.ToString();
    }

    private static int ExecutarConversaoDeRomano(string romano)
    {
        Dictionary<char, int> mapa = new Dictionary<char, int>
        {
            {'I', 1}, {'V', 5}, {'X', 10}, {'L', 50},
            {'C', 100}, {'D', 500}, {'M', 1000}
        };

        int total = 0;
        char[] caracteres = romano.ToUpper().ToCharArray();

        for (int i = 0; i < caracteres.Length; i++)
        {
            int valorAtual = mapa[caracteres[i]];
            int valorProximo = (i + 1 < caracteres.Length) ? mapa[caracteres[i + 1]] : 0;

            if (valorAtual < valorProximo)
                total -= valorAtual;
            else
                total += valorAtual;
        }

        return total;
    }

    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }
}