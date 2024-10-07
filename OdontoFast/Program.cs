using Microsoft.EntityFrameworkCore; // Importa o namespace para Entity Framework Core
using OdontoFast.Data; // Importa o contexto do banco de dados
using OdontoFast.Middleware; // Importa o middleware personalizado
using OdontoFast.Repository.Implementations; // Importa as implementa��es de reposit�rio
using OdontoFast.Repository.Interfaces; // Importa as interfaces de reposit�rio
using OdontoFast.Services; // Importa os servi�os

var builder = WebApplication.CreateBuilder(args);

// Adiciona servi�os ao cont�iner.
builder.Services.AddControllers(); // Adiciona suporte para controladores MVC
builder.Services.AddEndpointsApiExplorer(); // Habilita a explora��o de endpoints
builder.Services.AddSwaggerGen(); // Adiciona Swagger para documenta��o da API

// Configura��o da conex�o com o banco de dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// Configura��o do AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Inje��o de reposit�rio
builder.Services.AddScoped<OdontoFast.Repository.Interfaces.IDentistaRepository, OdontoFast.Repository.Implementations.DentistaRepository>();

// Inje��o de servi�os
builder.Services.AddScoped<IDentistaService, DentistaService>();

var app = builder.Build();

// Configura��o do pipeline de requisi��es HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Habilita o Swagger em ambiente de desenvolvimento
    app.UseSwaggerUI(); // Habilita a interface do Swagger UI
}

// Middleware para tratamento de exce��es
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection(); // Redireciona HTTP para HTTPS
app.UseAuthorization(); // Habilita a autoriza��o
app.MapControllers(); // Mapear rotas para controladores

app.Run(); // Inicia a aplica��o
