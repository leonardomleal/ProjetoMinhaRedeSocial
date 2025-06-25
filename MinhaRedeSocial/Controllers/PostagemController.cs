using Microsoft.AspNetCore.Mvc;
using MinhaRedeSocial.Aplicacao.Contratos.Request;
using MinhaRedeSocial.Aplicacao.Contratos.Services;
using System.Net;

namespace MinhaRedeSocial.Api.Controllers;

[ApiController]
[Route("api/Postagem")]
public class PostagemController : ControllerBase
{
    private readonly IBuscarPostagensService _buscarPostagensService;
    private readonly ILogger<PostagemController> _logger;

    public PostagemController(IBuscarPostagensService buscarPostagensService, ILogger<PostagemController> logger)
    {
        _buscarPostagensService = buscarPostagensService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> BuscarPostagens([FromQuery] BuscarPostagensRequest request, CancellationToken cancelationToken)
    {
        try
        {
            _logger.LogInformation($"Solicitação do endpoint [{nameof(BuscarPostagens)}].", request);
            var serviceResult = await _buscarPostagensService.Executar(request, cancelationToken);
            return Ok(serviceResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao buscar postagens para o usuário {request.Id}.");
            return Problem($"Ocorreu um erro ao buscar postagens para o usuário {request.Id}. ({ex})", statusCode: (int)HttpStatusCode.InternalServerError);
        }
    }
}