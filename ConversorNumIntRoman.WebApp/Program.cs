using ConversorNumerosInteirosRomanos.WebApp.Compartilhado.Aplicacao;
using ConversorNumerosInteirosRomanos.WebApp.Compartilhado.Apresentacao;
using ConversorNumerosInteirosRomanos.WebApp.Compartilhado.Infra;

var builder = WebApplication.CreateBuilder(args);

// Injeções de dependências
builder.Services.AddInfraRepositories();
builder.Services.AddApplicationServices();
builder.Services.AddPresentationConfig();

var app = builder.Build();

// Middlewares
app.UseStaticFiles();
app.UseRouting();
app.MapDefaultControllerRoute();

app.Run();