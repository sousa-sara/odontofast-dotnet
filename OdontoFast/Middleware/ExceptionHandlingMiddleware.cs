using System.Net; // Importa para utilizar tipos de StatusCode
using System.Text.Json; // Importa para serializar objetos em JSON

namespace OdontoFast.Middleware
{
    // Middleware responsável por capturar exceções não tratadas durante o processamento das requisições
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next; // Delegate para o próximo middleware na pipeline

        // Construtor que recebe o próximo middleware
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next; // Armazena a referência para o próximo middleware
        }

        // Método que invoca o próximo middleware e captura exceções
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext); // Chama o próximo middleware
            }
            catch (Exception ex) // Captura exceções
            {
                await HandleExceptionAsync(httpContext, ex); // Trata a exceção
            }
        }

        // Método responsável por construir a resposta de erro em caso de exceção
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json"; // Define o tipo de conteúdo da resposta
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // Define o status como 500

            // Cria um objeto com detalhes do erro
            var errorDetails = new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error. Please try again later." // Mensagem padrão de erro
            };

            // Serializa os detalhes do erro e escreve na resposta
            return context.Response.WriteAsync(errorDetails.ToString());
        }
    }

    // Classe para armazenar os detalhes do erro a serem retornados na resposta
    public class ErrorDetails
    {
        public int StatusCode { get; set; } // Código de status HTTP
        public string Message { get; set; } // Mensagem descritiva do erro

        // Método para serializar a instância atual em JSON
        public override string ToString()
        {
            return JsonSerializer.Serialize(this); // Converte o objeto em uma string JSON
        }
    }
}
