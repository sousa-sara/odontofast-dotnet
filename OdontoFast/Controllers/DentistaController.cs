using Microsoft.AspNetCore.Mvc;
using OdontoFast.DTOs;
using OdontoFast.Exceptions;

public class DentistaController : Controller
{
    private readonly IDentistaService _dentistaService;

    public DentistaController(IDentistaService dentistaService)
    {
        _dentistaService = dentistaService;
    }

    // Método para listar todos os dentistas (GETALL)
    [HttpGet]
    public async Task<IActionResult> ListaDentistas([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var dentistas = await _dentistaService.GetAllDentistasAsync(pageNumber, pageSize);

        int? dentistaId = HttpContext.Session.GetInt32("DentistaId");
        if (dentistaId.HasValue)
        {
            return View(dentistas); // Retorna a View com a listagem
        }
        else
        {
            return RedirectToAction("Index", "Login");
        }
    }


    // Método para mostrar o perfil do dentista
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        int? dentistaId = HttpContext.Session.GetInt32("DentistaId");

        if (dentistaId.HasValue)
        {
            var dentista = await _dentistaService.GetDentistaByIdAsync(dentistaId.Value);
            return View(dentista);
        }
        else
        {
            return RedirectToAction("Index", "Login");
        }
    }


    // Método para mostrar o perfil do dentista
    [HttpGet]
    public async Task<IActionResult> Perfil()
    {
        int? dentistaId = HttpContext.Session.GetInt32("DentistaId");

        if (dentistaId.HasValue)
        {
            var dentista = await _dentistaService.GetDentistaByIdAsync(dentistaId.Value);
            return View(dentista);
        }
        else
        {
            return RedirectToAction("Index", "Login");
        }
    }


    // Método para fazer login de um dentista (POST)
    [HttpPost]
    public async Task<IActionResult> Login(DentistaLoginDto loginDto)
    {
        try
        {
            var dentista = await _dentistaService.LoginAsync(loginDto);
            HttpContext.Session.SetInt32("DentistaId", dentista.IdDentista); // Armazena o ID do dentista na sessão
            return RedirectToAction("Index", "Dentista"); // Redireciona para a página principal ou outra ação
        }
        catch (BusinessException ex)
        {
            // Define a mensagem de erro
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("Index", "Login"); // Retorna para a página de login em caso de erro
        }
    }


    // Método para criar um novo dentista
    [HttpPost("create-dentista")]
    public async Task<IActionResult> Create(CreateDentistaDto dto)
    {
        try
        {
            // Chama o serviço para criar um dentista com os dados do DTO
            var createdDentista = await _dentistaService.CreateDentistaAsync(dto);

            TempData["msgRegistro"] = "Acesse o portal de administração utilizando seu CRO e Senha.";
            return RedirectToAction("Index", "Dentista"); // Redireciona para a página principal ou outra ação
        }
        catch (BusinessException ex)
        {
            // Define a mensagem de erro
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("Index", "Signup"); // Retorna para a página de login em caso de erro
        }
    }

    // Método para atualizar as informações do dentista
    [HttpPost]
    public async Task<IActionResult> Update(UpdateDentistaDto dto)
    {
        int dentistaId = (int)HttpContext.Session.GetInt32("DentistaId");
        try
        {
            TempData["okMsg"] = "Seus dados foram atualizados!";
            var updatedDentista = await _dentistaService.UpdateDentistaAsync(dentistaId, dto);
            return RedirectToAction("Perfil"); // Redireciona para o perfil atualizado
        }
        catch (NotFoundException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View("DentistaPerfil", dto); // Retorna à View com a mensagem de erro
        }
        catch (BusinessException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View("DentistaPerfil", dto); // Retorna à View com a mensagem de erro
        }
    }

    // Método para excluir a conta do dentista
    [HttpPost]
    public async Task<IActionResult> Delete()
    {
        int dentistaId = (int)HttpContext.Session.GetInt32("DentistaId");
        try
        {
            await _dentistaService.DeleteDentistaAsync(dentistaId);
            HttpContext.Session.Clear(); // Limpa a sessão
            return RedirectToAction("Index", "Login"); // Redireciona para a página inicial
        }
        catch (NotFoundException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return RedirectToAction("Index", "Perfil");
        }
    }


    public IActionResult Logout()
    {
        HttpContext.Session.Clear(); 
        return RedirectToAction("Index", "Login"); 
    }

}
