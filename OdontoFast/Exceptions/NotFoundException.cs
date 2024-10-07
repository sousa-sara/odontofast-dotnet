using System; // Importa o namespace base para usar classes de exceção

namespace OdontoFast.Exceptions
{
    // Classe que representa uma exceção específica para situações em que um recurso não é encontrado
    public class NotFoundException : Exception
    {
        // Construtor que aceita uma mensagem de erro e a passa para a classe base Exception
        public NotFoundException(string message) : base(message)
        {
        }

        // Construtor que aceita uma mensagem de erro e uma exceção interna
        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
