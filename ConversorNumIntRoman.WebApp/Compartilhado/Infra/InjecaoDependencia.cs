using Microsoft.Extensions.DependencyInjection;
using ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Dominio;
using ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Infra;

namespace ConversorNumerosInteirosRomanos.WebApp.Compartilhado.Infra;

public static class InjecaoDependencia
{
    public static IServiceCollection AddInfraRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepositorioConversor, RepositorioConversor>();

        return services;
    }
}