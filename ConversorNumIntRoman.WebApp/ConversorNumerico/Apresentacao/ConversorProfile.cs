using AutoMapper;
using ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Aplicacao;

namespace ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Apresentacao;

public class ConversorProfile : Profile
{
    public ConversorProfile()
    {
        CreateMap<ConverterParaRomanoViewModel, ConverterParaRomanoDto>();
        CreateMap<ConverterDeRomanoViewModel, ConverterDeRomanoDto>();
        CreateMap<DetalhesConversaoDto, DetalhesConversaoViewModel>();
    }
}