namespace ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Dominio;

public class ConversorNumIntRoman
{
    public Guid Id { get; set; }
    public int Numero { get; set; }
    public string Romano { get; set; }
    public DateTime DataHora { get; set; }

    public ConversorNumIntRoman(int numero, string romano)
    {
        Id = Guid.NewGuid();
        Numero = numero;
        Romano = romano;
        DataHora = DateTime.Now;
    }
}