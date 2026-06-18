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
            return Result.Fail("Número deve estar entre 1 e 3999.");

        var romano = ExecutarConversaoParaRomano(dto.Numero);
        var entidade = new ConversorNumIntRoman(dto.Numero, romano);

        repositorioConversor.Cadastrar(entidade);

        return Result.Ok(new DetalhesConversaoDto(entidade.Id, entidade.Numero, entidade.Romano, entidade.DataHora));
    }

    public Result<DetalhesConversaoDto> ConverterDeRomano(ConverterDeRomanoDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Romano))
            return Result.Fail("Número romano inválido.");

        var numero = ExecutarConversaoDeRomano(dto.Romano);
        var entidade = new ConversorNumIntRoman(numero, dto.Romano.ToUpper());

        repositorioConversor.Cadastrar(entidade);

        return Result.Ok(new DetalhesConversaoDto(entidade.Id, entidade.Numero, entidade.Romano, entidade.DataHora));
    }

    // Métodos utilitários internos
    private static string ExecutarConversaoParaRomano(int num)
    {
        var numerais = new List<(int valor, string simbolo)>
        {
            (1000, "M"), (900, "CM"), (500, "D"), (400, "CD"),
            (100, "C"), (90, "XC"), (50, "L"), (40, "XL"),
            (10, "X"), (9, "IX"), (5, "V"), (4, "IV"), (1, "I")
        };

        var result = new System.Text.StringBuilder();
        foreach (var (valor, simbolo) in numerais)
        {
            while (num >= valor)
            {
                result.Append(simbolo);
                num -= valor;
            }
        }
        return result.ToString();
    }

    private static int ExecutarConversaoDeRomano(string romano)
    {
        var mapa = new Dictionary<char, int>
        {
            {'I', 1}, {'V', 5}, {'X', 10}, {'L', 50},
            {'C', 100}, {'D', 500}, {'M', 1000}
        };

        int total = 0;
        var chars = romano.ToUpper().ToCharArray();

        foreach (var (atual, proximo) in chars.Zip(chars.Skip(1).Append('\0')))
        {
            if (proximo != '\0' && mapa[atual] < mapa[proximo])
                total -= mapa[atual];
            else
                total += mapa[atual];
        }

        return total;
    }
}