namespace ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Aplicacao;

public record ConverterParaRomanoDto(int Numero);
public record ConverterDeRomanoDto(string Romano);
public record DetalhesConversaoDto(
    Guid Id,
    int Numero,
    string Romano,
    DateTime DataHora
    );