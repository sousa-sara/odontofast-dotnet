using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore; // Importa o namespace para Entity Framework Core
using OdontoFast.Data; // Importa o contexto do banco de dados
using OdontoFast.Middleware; // Importa o middleware personalizado
using OdontoFast.Repository.Implementations; // Importa as implementa��es de reposit�rio
using OdontoFast.Repository.Interfaces; // Importa as interfaces de reposit�rio
using OdontoFast.Services; // Importa os servi�os

var builder = WebApplication.CreateBuilder(args);

// Adiciona servi�os ao cont�iner.
builder.Services.AddControllersWithViews(); // Adiciona suporte para controladores MVC
builder.Services.AddEndpointsApiExplorer(); // Habilita a explora��o de endpoints

// Configura��o da conex�o com o banco de dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// Configura��o do AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Inje��o de reposit�rio
builder.Services.AddScoped<OdontoFast.Repository.Interfaces.IDentistaRepository, OdontoFast.Repository.Implementations.DentistaRepository>();

// Inje��o de servi�os
builder.Services.AddScoped<IDentistaService, DentistaService>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de expira��o da sess�o
});

var app = builder.Build();

app.UseSession(); // Ativa o uso da sess�o
app.UseMiddleware<ExceptionHandlingMiddleware>(); // Middleware para tratamento de exce��es
app.UseStaticFiles(); // Adicione esta linha para servir arquivos est�ticos de wwwroot
app.UseHttpsRedirection(); // Redireciona HTTP para HTTPS
app.UseAuthorization(); // Habilita a autoriza��o
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}"
); // Mapear rotas para controladores

app.Run(); // Inicia a aplica��o
