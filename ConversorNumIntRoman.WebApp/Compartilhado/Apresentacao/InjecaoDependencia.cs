using Microsoft.Extensions.DependencyInjection;

namespace ConversorNumerosInteirosRomanos.WebApp.Compartilhado.Apresentacao;

public static class InjecaoDependencia
{
    public static IServiceCollection AddPresentationConfig(this IServiceCollection services)
    {
        services.AddControllersWithViews().AddRazorOptions(options =>
        {
            options.ViewLocationFormats.Clear();

            options.ViewLocationFormats.Add("/ConversorNumerico/Apresentacao/Views/{0}.cshtml");

            options.ViewLocationFormats.Add("/Compartilhado/Apresentacao/Views/{0}.cshtml");
        });

        return services;
    }
}