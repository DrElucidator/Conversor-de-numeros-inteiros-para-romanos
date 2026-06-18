using System.Text;

namespace ConversorNumerosInteirosRomanos.WebApp.Dominio;

public static class ConversorNumIntRoman
{
    private static readonly Dictionary<char, int> NumRomano = new()
        {
            {'I', 1}, {'V', 5}, {'X', 10}, {'L', 50},
            {'C', 100}, {'D', 500}, {'M', 1000}
        };

    public static string ParaRomano(int num)
    {
        if (num < 1 || num > 3999)
            throw new ArgumentOutOfRangeException(nameof(num), "Valor deve estar entre 1 e 3999.");

        var numerais = new List<(int valor, string simbolo)>
            {
                (1000, "M"), (900, "CM"), (500, "D"), (400, "CD"),
                (100, "C"), (90, "XC"), (50, "L"), (40, "XL"),
                (10, "X"), (9, "IX"), (5, "V"), (4, "IV"), (1, "I")
            };

        var result = new StringBuilder();
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

    public static int DeRomano(string romano)
    {
        int total = 0;
        var chars = romano.ToUpper().ToCharArray();

        foreach (var (atual, proximo) in chars.Zip(chars.Skip(1).Append('\0')))
        {
            if (proximo != '\0' && NumRomano[atual] < NumRomano[proximo])
                total -= NumRomano[atual];
            else
                total += NumRomano[atual];
        }

        return total;
    }
}