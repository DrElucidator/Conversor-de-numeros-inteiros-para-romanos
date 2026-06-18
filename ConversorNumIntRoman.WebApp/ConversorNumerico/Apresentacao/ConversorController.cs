using Microsoft.AspNetCore.Mvc;
using ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Aplicacao;

namespace ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Apresentacao;

public class ConversorController : Controller
{
    private readonly ServicoConversor _servico;

    public ConversorController(ServicoConversor servico)
    {
        _servico = servico;
    }

    public IActionResult Index() => View();

    [HttpPost]
    public IActionResult ParaRomano(int numero)
    {
        var dto = new ConverterParaRomanoDto(numero);
        var resultado = _servico.ConverterParaRomano(dto);
        return View("Index", resultado);
    }

    [HttpPost]
    public IActionResult DeRomano(string romano)
    {
        var dto = new ConverterDeRomanoDto(romano);
        var resultado = _servico.ConverterDeRomano(dto);
        return View("Index", resultado);
    }
}