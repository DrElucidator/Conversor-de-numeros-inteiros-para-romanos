using Microsoft.Extensions.DependencyInjection;
using ConversorNumerosInteirosRomanos.WebApp.ConversorNumerico.Aplicacao;

namespace ConversorNumerosInteirosRomanos.WebApp.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ServicoConversor>();

        services.AddAutoMapper(config =>
        {
            config.AddMaps(typeof(Program));
        });

        return services;
    }
}