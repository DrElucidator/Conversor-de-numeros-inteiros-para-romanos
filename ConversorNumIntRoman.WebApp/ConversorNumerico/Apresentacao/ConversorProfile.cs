using AutoMapper;
using ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Dominio;
using ConversorNumerosInteirosRomanos.WebApp.Dominio;

namespace ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Aplicacao;

public class ConversorProfile : Profile
{
    public ConversorProfile()
    {
        CreateMap<ConverterParaRomanoDto, DetalhesConversaoDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Romano, opt => opt.MapFrom(src => ConversorNumIntRoman.ParaRomano(src.Numero)))
            .ForMember(dest => dest.DataHora, opt => opt.MapFrom(_ => DateTime.Now));

        CreateMap<ConverterDeRomanoDto, DetalhesConversaoDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => ConversorNumIntRoman.DeRomano(src.Romano)))
            .ForMember(dest => dest.DataHora, opt => opt.MapFrom(_ => DateTime.Now));
    }
}