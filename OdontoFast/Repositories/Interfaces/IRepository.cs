// Definição da interface genérica IRepository
public interface IRepository<T> where T : class
{
    // Método para obter todos os registros do tipo T
    Task<IEnumerable<T>> GetAllAsync();

    // Método para obter um registro específico pelo ID
    Task<T> GetByIdAsync(int id);

    // Método para adicionar um novo registro do tipo T
    Task<T> AddAsync(T entity);

    // Método para atualizar um registro existente do tipo T
    Task UpdateAsync(T entity);

    // Método para excluir um registro do tipo T pelo ID
    Task DeleteAsync(int id);
}
