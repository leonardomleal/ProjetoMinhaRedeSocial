using Microsoft.AspNetCore.Mvc;
using MinhaRedeSocial.Aplicacao.Contratos.Response;
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

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPostagens([FromRoute] Guid id, CancellationToken cancelationToken)
    {
        var serviceResult = new List<BuscarPostagensResponse>();

        try
        {
            _logger.LogInformation($"Solicitação do endpoint [{nameof(BuscarPostagens)}].", id);
            serviceResult = await _buscarPostagensService.Executar(id, cancelationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao buscar postagens para o usuário {id}.");
            return Problem($"Ocorreu um erro ao buscar postagens para o usuário {id}. ({ex})", statusCode: (int)HttpStatusCode.InternalServerError);
        }

        return Ok(serviceResult);
    }

}