using System.ComponentModel.DataAnnotations;

namespace ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Apresentacao;

public record ConverterParaRomanoViewModel(
    [Required(ErrorMessage = "Informe um número inteiro.")]
    [Range(1, 3999, ErrorMessage = "O número deve estar entre 1 e 3999.")]
    int Numero
);

public record ConverterDeRomanoViewModel(
    [Required(ErrorMessage = "Informe um número romano.")]
    [RegularExpression(@"^[IVXLCDMivxlcdm]+$", ErrorMessage = "Número romano inválido.")]
    string Romano
);

public record DetalhesConversaoViewModel(
    Guid Id,
    int Numero,
    string Romano,
    DateTime DataHora
);