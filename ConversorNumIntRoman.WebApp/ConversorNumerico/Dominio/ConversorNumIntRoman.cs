namespace ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Dominio;

public class ConversorNumIntRoman
{
    public Guid Id { get; private set; }
    public int Numero { get; private set; }
    public string Romano { get; private set; }
    public DateTime DataHora { get; private set; }

    public ConversorNumIntRoman(int numero, string romano)
    {
        Id = Guid.NewGuid();
        Numero = numero;
        Romano = romano;
        DataHora = DateTime.Now;
    }
}