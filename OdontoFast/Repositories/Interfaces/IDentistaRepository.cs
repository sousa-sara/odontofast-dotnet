using Microsoft.EntityFrameworkCore; // Importa o Entity Framework Core para operações de acesso a dados
using OdontoFast.Data; // Importa o contexto da aplicação
using OdontoFast.DTOs; // Importa os Data Transfer Objects (DTOs)
using OdontoFast.Models; // Importa os modelos (entidades)

namespace OdontoFast.Repository.Interfaces
{
    // Interface que define os métodos específicos para o repositório de Dentistas
    public interface IDentistaRepository : IRepository<Dentista>
    {
        // Método para obter um dentista pelo seu número CRO (Cadastro Regional de Odontologia)
        Task<Dentista> GetByCroAsync(string cro);

        // Método para excluir um dentista pelo ID
        Task DeleteAsync(int id);
    }

    // Implementação da interface IDentistaRepository
    public class DentistaRepository : Repository<Dentista>, IDentistaRepository
    {
        // Construtor que inicializa a classe base Repository com o contexto da aplicação
        public DentistaRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Método que busca um dentista pelo seu número CRO
        public async Task<Dentista> GetByCroAsync(string cro)
        {
            // Retorna o primeiro dentista que corresponde ao número CRO ou null se não encontrado
            return await _context.Dentistas.FirstOrDefaultAsync(d => d.Cro == cro);
        }
    }
}
