using Microsoft.AspNetCore.Mvc;
using MinhaRedeSocial.Aplicacao.Contratos.Request;
using MinhaRedeSocial.Aplicacao.Contratos.Response;
using MinhaRedeSocial.Aplicacao.Contratos.Services;
using MinhaRedeSocial.Aplicacao.Services.Postagem;
using MinhaRedeSocial.Domain.Enums;
using System.Net;

namespace MinhaRedeSocial.Api.Controllers;

[ApiController]
[Route("api/Postagem")]
public class PostagemController : ControllerBase
{
    private readonly IBuscarPostagensService _buscarPostagensService;
    private readonly IRegistrarCurtidaService _registrarCurtidaService;
    private readonly IRegistrarDescurtidaService _registrarDescurtidaService;
    private readonly ICadastrarComentarioService _cadastrarComentarioService;
    private readonly IAlterarPermissaoService _alterarPermissaoService; 
    private readonly ILogger<PostagemController> _logger;

    public PostagemController(
        IBuscarPostagensService buscarPostagensService,
        IRegistrarCurtidaService registrarCurtidaService,
        IRegistrarDescurtidaService registrarDescurtidaService,
        ICadastrarComentarioService cadastrarComentarioService,
        IAlterarPermissaoService alterarPermissaoService,
        ILogger<PostagemController> logger)
    {
        _buscarPostagensService = buscarPostagensService;
        _registrarCurtidaService = registrarCurtidaService;
        _registrarDescurtidaService = registrarDescurtidaService;
        _cadastrarComentarioService = cadastrarComentarioService;
        _alterarPermissaoService = alterarPermissaoService;
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
            return Problem(ex.StackTrace, statusCode: (int)HttpStatusCode.InternalServerError, title: ex.Message);
        }
    }

    [HttpPatch("{id}/curtir")]
    public async Task<IActionResult> RegistrarCurtida([FromRoute] Guid id, CancellationToken cancelationToken)
    {
        var serviceResult = new BuscarPostagensResponse();

        try
        {
            _logger.LogInformation($"Solicitação do endpoint [{nameof(RegistrarCurtida)}].", id);
            serviceResult = await _registrarCurtidaService.Executar(id, cancelationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao registrar curtida para a postagem de Id {id}.");
            return Problem(ex.StackTrace, statusCode: (int)HttpStatusCode.InternalServerError, title: ex.Message);
        }

        return Ok(serviceResult);
    }

    [HttpPatch("{id}/descurtir")]
    public async Task<IActionResult> RegistrarDescurtida([FromRoute] Guid id, CancellationToken cancelationToken)
    {
        var serviceResult = new BuscarPostagensResponse();

        try
        {
            _logger.LogInformation($"Solicitação do endpoint [{nameof(RegistrarDescurtida)}].", id);
            serviceResult = await _registrarDescurtidaService.Executar(id, cancelationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao registrar descurtida para a postagem de Id {id}.");
            return Problem(ex.StackTrace, statusCode: (int)HttpStatusCode.InternalServerError, title: ex.Message);
        }

        return Ok(serviceResult);
    }

    [HttpPost("{id}/comentar")]
    public async Task<IActionResult> CadastrarComentario([FromRoute] Guid id, [FromBody] CadastrarComentarioRequest request, CancellationToken cancelationToken)
    {
        var serviceResult = new CadastrarComentarioResponse();

        try
        {
            _logger.LogInformation($"Solicitação do endpoint [{nameof(CadastrarComentario)}].", id);
            serviceResult = await _cadastrarComentarioService.Executar(id, request, cancelationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao cadastrar comentario para a postagem de Id {id}.");
            return Problem(ex.StackTrace, statusCode: (int)HttpStatusCode.InternalServerError, title: ex.Message);
        }

        return Ok(serviceResult);
    }

    [HttpPatch("{id}/Permissao/{permissao}")]
    public async Task<IActionResult> AlterarPermissao([FromRoute] Guid id, PostagemPermissoes permissao, CancellationToken cancelationToken)
    {
        var serviceResult = new BuscarPostagensResponse();

        try
        {
            _logger.LogInformation($"Solicitação do endpoint [{nameof(AlterarPermissao)}].", id);
            serviceResult = await _alterarPermissaoService.Executar(id, permissao, cancelationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao alterar permissão para a postagem de Id {id}.");
            return Problem(ex.StackTrace, statusCode: (int)HttpStatusCode.InternalServerError, title: ex.Message);
        }

        return Ok(serviceResult);
    }
}