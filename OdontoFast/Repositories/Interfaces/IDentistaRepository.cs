using Microsoft.EntityFrameworkCore;
using OdontoFast.Data;
using OdontoFast.Models;

namespace OdontoFast.Repository.Interfaces
{
    public interface IDentistaRepository : IRepository<Dentista>
    {
        Task<Dentista> GetByCroAsync(string cro);
        Task DeleteAsync(int id);
        Task<IEnumerable<Dentista>> GetAllAsync(); // Método para retornar todos os dentistas
    }

    public class DentistaRepository : Repository<Dentista>, IDentistaRepository
    {
        public DentistaRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Dentista> GetByCroAsync(string cro)
        {
            return await _context.Dentistas.FirstOrDefaultAsync(d => d.Cro == cro);
        }

        public async Task DeleteAsync(int id)
        {
            var dentista = await _context.Dentistas.FindAsync(id);
            if (dentista != null)
            {
                _context.Dentistas.Remove(dentista);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Dentista>> GetAllAsync()
        {
            return await _context.Dentistas.ToListAsync(); // Retorna todos os dentistas
        }
    }
}
