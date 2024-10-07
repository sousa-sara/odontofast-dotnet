using OdontoFast.DTOs; // Importa os Data Transfer Objects (DTOs)

public interface IDentistaService
{
    // Método para criar um novo dentista
    Task<DentistaDto> CreateDentistaAsync(CreateDentistaDto dto);

    // Método para obter um dentista pelo ID
    Task<DentistaDto> GetDentistaByIdAsync(int id);

    // Método para obter todos os dentistas com paginação
    Task<IEnumerable<DentistaDto>> GetAllDentistasAsync(int pageNumber, int pageSize);

    // Método para atualizar um dentista
    Task<DentistaDto> UpdateDentistaAsync(int id, UpdateDentistaDto dto);

    // Método para realizar o login do dentista
    Task<DentistaDto> LoginAsync(DentistaLoginDto loginDto);

    // Método para deletar um dentista
    Task DeleteDentistaAsync(int id);
}
