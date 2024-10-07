using Microsoft.EntityFrameworkCore; // Importa o Entity Framework Core para acesso a dados
using OdontoFast.Data; // Importa o contexto de dados da aplicação
using OdontoFast.Exceptions; // Importa as exceções personalizadas
using OdontoFast.Models; // Importa os modelos que representam as entidades
using OdontoFast.Repository.Interfaces; // Importa as interfaces do repositório
using System.Threading.Tasks; // Importa o namespace para suportar operações assíncronas

namespace OdontoFast.Repository.Implementations
{
    // Classe que implementa as operações de repositório para a entidade Dentista
    public class DentistaRepository : Repository<Dentista>, IDentistaRepository
    {
        // Construtor que recebe o contexto da aplicação e passa para a classe base Repository
        public DentistaRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Método assíncrono para buscar um dentista pelo número do CRO (Conselho Regional de Odontologia)
        public async Task<Dentista> GetByCroAsync(string cro)
        {
            // Retorna o primeiro dentista que corresponde ao CRO fornecido, ou null se não encontrar
            return await _context.Dentistas.FirstOrDefaultAsync(d => d.Cro == cro);
        }

        // Implementação do método DeleteAsync que remove um dentista do banco de dados
        public async Task DeleteAsync(int id)
        {
            // Busca o dentista pelo ID fornecido
            var dentista = await _context.Dentistas.FindAsync(id);
            if (dentista != null) // Verifica se o dentista foi encontrado
            {
                _context.Dentistas.Remove(dentista); // Remove o dentista do contexto
                await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
            }
            else
            {
                // Lança exceção se o dentista não existir no banco de dados
                throw new NotFoundException("Dentista não encontrado.");
            }
        }
    }
}
