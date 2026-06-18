using AutoMapper;
using ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Dominio;

namespace ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Aplicacao;

public class ServicoConversor
{
    private readonly IMapper _mapper;
    private readonly IRepositorioConversor _repositorio;

    public ServicoConversor(IMapper mapper, IRepositorioConversor repositorio)
    {
        _mapper = mapper;
        _repositorio = repositorio;
    }

    public DetalhesConversaoDto ConverterParaRomano(ConverterParaRomanoDto dto)
    {
        var detalhes = _mapper.Map<DetalhesConversaoDto>(dto);
        _repositorio.Salvar(detalhes);
        return detalhes;
    }

    public DetalhesConversaoDto ConverterDeRomano(ConverterDeRomanoDto dto)
    {
        var detalhes = _mapper.Map<DetalhesConversaoDto>(dto);
        _repositorio.Salvar(detalhes);
        return detalhes;
    }
}