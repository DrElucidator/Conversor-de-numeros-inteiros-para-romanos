using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Aplicacao;

namespace ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Apresentacao;

public class ConversorController(ServicoConversor servicoConversor, IMapper mapeador) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<DetalhesConversaoDto> dtos = servicoConversor.SelecionarTodos();
        List<DetalhesConversaoViewModel> listarVms = mapeador.Map<List<DetalhesConversaoViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult ConverterParaRomano()
    {
        ConverterParaRomanoViewModel vm = new ConverterParaRomanoViewModel(0);
        return View(vm);
    }

    [HttpPost]
    public ActionResult ConverterParaRomano(ConverterParaRomanoViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        ConverterParaRomanoDto dto = mapeador.Map<ConverterParaRomanoDto>(vm);
        Result<DetalhesConversaoDto> resultado = servicoConversor.ConverterParaRomano(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado.Errors.First().Metadata["Campo"]?.ToString() ?? string.Empty,
                                     resultado.Errors.First().Message);
            return View(vm);
        }

        DetalhesConversaoViewModel detalhesVm = mapeador.Map<DetalhesConversaoViewModel>(resultado.Value);
        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult ConverterDeRomano()
    {
        ConverterDeRomanoViewModel vm = new ConverterDeRomanoViewModel(string.Empty);
        return View(vm);
    }

    [HttpPost]
    public ActionResult ConverterDeRomano(ConverterDeRomanoViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        ConverterDeRomanoDto dto = mapeador.Map<ConverterDeRomanoDto>(vm);
        Result<DetalhesConversaoDto> resultado = servicoConversor.ConverterDeRomano(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado.Errors.First().Metadata["Campo"]?.ToString() ?? string.Empty,
                                     resultado.Errors.First().Message);
            return View(vm);
        }

        DetalhesConversaoViewModel detalhesVm = mapeador.Map<DetalhesConversaoViewModel>(resultado.Value);
        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<DetalhesConversaoDto> resultado = servicoConversor.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData["Erro"] = resultado.Errors.First().Message;
            return RedirectToAction(nameof(Listar));
        }

        DetalhesConversaoViewModel excluirVm = mapeador.Map<DetalhesConversaoViewModel>(resultado.Value);
        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(DetalhesConversaoViewModel vm)
    {
        Result resultado = servicoConversor.Excluir(vm.Id);

        if (resultado.IsFailed)
            TempData["Erro"] = resultado.Errors.First().Message;

        return RedirectToAction(nameof(Listar));
    }
}