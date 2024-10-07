using Microsoft.AspNetCore.Mvc; // Importa o namespace para trabalhar com ASP.NET Core MVC
using OdontoFast.DTOs; // Importa os DTOs utilizados na aplicação
using OdontoFast.Exceptions; // Importa as exceções personalizadas

// Define a rota base para o controlador de dentistas
[Route("api/[controller]")]
[ApiController] // Indica que esta classe é um controlador API
public class DentistaController : ControllerBase
{
    // Serviço para gerenciar as operações relacionadas aos dentistas
    private readonly IDentistaService _dentistaService;

    // Construtor que recebe a instância do serviço de dentistas
    public DentistaController(IDentistaService dentistaService)
    {
        _dentistaService = dentistaService; // Inicializa o serviço
    }

    // Método para obter todos os dentistas, com suporte à paginação
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DentistaDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        // Chama o serviço para obter a lista de dentistas com a paginação especificada
        var dentistas = await _dentistaService.GetAllDentistasAsync(pageNumber, pageSize);
        return Ok(dentistas); // Retorna a lista de dentistas com status 200 OK
    }

    // Método para obter um dentista específico pelo ID
    [HttpGet("{id}")]
    public async Task<ActionResult<DentistaDto>> GetById(int id)
    {
        try
        {
            // Chama o serviço para obter um dentista pelo ID
            var dentista = await _dentistaService.GetDentistaByIdAsync(id);
            return Ok(dentista); // Retorna o dentista encontrado com status 200 OK
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message); // Retorna 404 Not Found se o dentista não for encontrado
        }
    }

    // Método para criar um novo dentista
    [HttpPost]
    public async Task<ActionResult<DentistaDto>> Create([FromBody] CreateDentistaDto dto)
    {
        try
        {
            // Chama o serviço para criar um dentista com os dados do DTO
            var createdDentista = await _dentistaService.CreateDentistaAsync(dto);
            // Retorna 201 Created com a localização do novo recurso
            return CreatedAtAction(nameof(GetById), new { id = createdDentista.IdDentista }, createdDentista);
        }
        catch (BusinessException ex)
        {
            return BadRequest(ex.Message); // Retorna 400 Bad Request se houver uma exceção de negócio
        }
    }

    // Método para fazer login de um dentista
    [HttpPost("login")]
    public async Task<ActionResult<DentistaDto>> Login([FromBody] DentistaLoginDto loginDto)
    {
        try
        {
            // Chama o serviço para autenticar o dentista
            var dentista = await _dentistaService.LoginAsync(loginDto);
            return Ok(dentista); // Retorna o dentista autenticado com status 200 OK
        }
        catch (BusinessException ex)
        {
            return BadRequest(ex.Message); // Retorna 400 Bad Request se houver uma exceção de negócio
        }
    }

    // Método para atualizar as informações de um dentista
    [HttpPut("{id}")]
    public async Task<ActionResult<DentistaDto>> Update(int id, [FromBody] UpdateDentistaDto dto)
    {
        try
        {
            // Chama o serviço para atualizar o dentista com os dados do DTO
            var updatedDentista = await _dentistaService.UpdateDentistaAsync(id, dto);
            return Ok(updatedDentista); // Retorna o dentista atualizado com status 200 OK
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message); // Retorna 404 Not Found se o dentista não for encontrado
        }
        catch (BusinessException ex)
        {
            return BadRequest(ex.Message); // Retorna 400 Bad Request se houver uma exceção de negócio
        }
    }

    // Método para excluir um dentista
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            // Chama o serviço para excluir o dentista pelo ID
            await _dentistaService.DeleteDentistaAsync(id);
            return NoContent(); // Retorna 204 No Content para indicar que a exclusão foi bem-sucedida
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message); // Retorna 404 Not Found se o dentista não for encontrado
        }
    }
}
