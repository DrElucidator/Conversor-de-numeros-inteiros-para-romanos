using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Aplicacao;

namespace ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Apresentacao;

public class ConversorController(ServicoConversor servicoConversor, IMapper mapeador) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ParaRomano(ConverterParaRomanoViewModel vm)
    {
        if (!ModelState.IsValid)
            return View("Index", vm);

        var dto = mapeador.Map<ConverterParaRomanoDto>(vm);
        Result<DetalhesConversaoDto> resultado = servicoConversor.ConverterParaRomano(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(string.Empty, resultado.Errors.First().Message);
            return View("Index", vm);
        }

        var detalhesVm = mapeador.Map<DetalhesConversaoViewModel>(resultado.Value);
        return View("Index", detalhesVm);
    }

    [HttpPost]
    public IActionResult DeRomano(ConverterDeRomanoViewModel vm)
    {
        if (!ModelState.IsValid)
            return View("Index", vm);

        var dto = mapeador.Map<ConverterDeRomanoDto>(vm);
        Result<DetalhesConversaoDto> resultado = servicoConversor.ConverterDeRomano(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(string.Empty, resultado.Errors.First().Message);
            return View("Index", vm);
        }

        var detalhesVm = mapeador.Map<DetalhesConversaoViewModel>(resultado.Value);
        return View("Index", detalhesVm);
    }
}