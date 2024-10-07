using System; // Importa o namespace base para usar classes de exceção

namespace OdontoFast.Exceptions
{
    // Classe que representa uma exceção específica para erros de negócios
    public class BusinessException : Exception
    {
        // Construtor que aceita uma mensagem de erro e a passa para a classe base Exception
        public BusinessException(string message) : base(message)
        {
        }
    }
}
