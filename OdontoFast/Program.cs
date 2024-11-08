using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore; // Importa o namespace para Entity Framework Core
using OdontoFast.Data; // Importa o contexto do banco de dados
using OdontoFast.Middleware; // Importa o middleware personalizado
using OdontoFast.Repository.Implementations; // Importa as implementações de repositório
using OdontoFast.Repository.Interfaces; // Importa as interfaces de repositório
using OdontoFast.Services; // Importa os serviços

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner.
builder.Services.AddControllersWithViews(); // Adiciona suporte para controladores MVC
builder.Services.AddEndpointsApiExplorer(); // Habilita a exploração de endpoints

// Configuração da conexão com o banco de dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// Configuração do AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Injeção de repositório
builder.Services.AddScoped<OdontoFast.Repository.Interfaces.IDentistaRepository, OdontoFast.Repository.Implementations.DentistaRepository>();

// Injeção de serviços
builder.Services.AddScoped<IDentistaService, DentistaService>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de expiração da sessão
});

var app = builder.Build();

app.UseSession(); // Ativa o uso da sessão
app.UseMiddleware<ExceptionHandlingMiddleware>(); // Middleware para tratamento de exceções
app.UseStaticFiles(); // Adicione esta linha para servir arquivos estáticos de wwwroot
app.UseHttpsRedirection(); // Redireciona HTTP para HTTPS
app.UseAuthorization(); // Habilita a autorização
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}"
); // Mapear rotas para controladores

app.Run(); // Inicia a aplicação
