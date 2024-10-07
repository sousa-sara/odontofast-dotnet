using Microsoft.EntityFrameworkCore; // Importa o Entity Framework Core para operações de acesso a dados
using OdontoFast.Data; // Importa o contexto de dados da aplicação
using System.Collections.Generic; // Importa o namespace para usar a coleção IEnumerable
using System.Threading.Tasks; // Importa o namespace para suportar operações assíncronas

// Classe genérica que implementa um repositório básico para operações CRUD
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context; // Contexto da aplicação para acesso a dados

    // Construtor que recebe o contexto da aplicação
    public Repository(ApplicationDbContext context)
    {
        _context = context; // Inicializa o contexto
    }

    // Método assíncrono para obter todos os registros da entidade
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        // Retorna todos os registros da entidade como uma lista
        return await _context.Set<T>().ToListAsync();
    }

    // Método assíncrono para obter um registro específico pelo ID
    public async Task<T> GetByIdAsync(int id)
    {
        // Retorna o registro correspondente ao ID fornecido
        return await _context.Set<T>().FindAsync(id);
    }

    // Método assíncrono para adicionar um novo registro
    public async Task<T> AddAsync(T entity)
    {
        // Adiciona a entidade ao contexto
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
        return entity; // Retorna a entidade adicionada
    }

    // Método assíncrono para atualizar um registro existente
    public async Task UpdateAsync(T entity)
    {
        // Define o estado da entidade como Modificado
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
    }

    // Método assíncrono para remover um registro pelo ID
    public async Task DeleteAsync(int id)
    {
        // Busca a entidade pelo ID
        var entity = await GetByIdAsync(id);
        if (entity != null) // Verifica se a entidade foi encontrada
        {
            _context.Set<T>().Remove(entity); // Remove a entidade do contexto
            await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
        }
    }
}
