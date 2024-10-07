using Microsoft.EntityFrameworkCore; // Importa o namespace para Entity Framework Core
using OdontoFast.Data; // Importa o contexto do banco de dados
using OdontoFast.Middleware; // Importa o middleware personalizado
using OdontoFast.Repository.Implementations; // Importa as implementações de repositório
using OdontoFast.Repository.Interfaces; // Importa as interfaces de repositório
using OdontoFast.Services; // Importa os serviços

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner.
builder.Services.AddControllers(); // Adiciona suporte para controladores MVC
builder.Services.AddEndpointsApiExplorer(); // Habilita a exploração de endpoints
builder.Services.AddSwaggerGen(); // Adiciona Swagger para documentação da API

// Configuração da conexão com o banco de dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// Configuração do AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Injeção de repositório
builder.Services.AddScoped<OdontoFast.Repository.Interfaces.IDentistaRepository, OdontoFast.Repository.Implementations.DentistaRepository>();

// Injeção de serviços
builder.Services.AddScoped<IDentistaService, DentistaService>();

var app = builder.Build();

// Configuração do pipeline de requisições HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Habilita o Swagger em ambiente de desenvolvimento
    app.UseSwaggerUI(); // Habilita a interface do Swagger UI
}

// Middleware para tratamento de exceções
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection(); // Redireciona HTTP para HTTPS
app.UseAuthorization(); // Habilita a autorização
app.MapControllers(); // Mapear rotas para controladores

app.Run(); // Inicia a aplicação
