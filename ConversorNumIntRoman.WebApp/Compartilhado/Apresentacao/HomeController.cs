using Microsoft.AspNetCore.Mvc;

namespace ConversorNumerosInteirosRomanos.WebApp.Compartilhado.Apresentacao;

public class HomeController : Controller
{
    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }
}