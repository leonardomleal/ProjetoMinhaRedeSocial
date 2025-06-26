using Microsoft.AspNetCore.Mvc;
using MinhaRedeSocial.Aplicacao.Contratos.Request;
using MinhaRedeSocial.Aplicacao.Contratos.Response;
using MinhaRedeSocial.Aplicacao.Contratos.Services;
using MinhaRedeSocial.Aplicacao.Validators;
using System.Net;

namespace MinhaRedeSocial.Api.Controllers;

[ApiController]
[Route("api/Usuario")]
public class UsuarioController : ControllerBase
{
    private readonly IBuscarUsuarioService _pesquisarUsuarioService;
    private readonly IPesquisarUsuariosService _pesquisarUsuariosService;
    private readonly IPesquisarUsuariosPaginadoService _pesquisarUsuariosPaginadoService;
    private readonly ICadastrarUsuarioService _cadastrarUsuarioService;
    private readonly IBuscarSolicitacoesPorUsuarioService _buscarSolicitacoesPorUsuarioService;
    private readonly ICadastrarPostagemService _cadastrarPostagemService;
    private readonly IPesquisarAmigosService _pesquisarAmigosService;
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(
        IBuscarUsuarioService pesquisarUsuarioService,
        IPesquisarUsuariosService pesquisarUsuariosService,
        IPesquisarUsuariosPaginadoService pesquisarUsuariosPaginadoService,
        ICadastrarUsuarioService cadastrarUsuarioService,
        IBuscarSolicitacoesPorUsuarioService buscarSolicitacoesPorUsuarioService,
        ICadastrarPostagemService cadastrarPostagemService,
        IPesquisarAmigosService pesquisarAmigosService,
        ILogger<UsuarioController> logger)
    {
        _pesquisarUsuarioService = pesquisarUsuarioService;
        _pesquisarUsuariosService = pesquisarUsuariosService;
        _pesquisarUsuariosPaginadoService = pesquisarUsuariosPaginadoService;
        _cadastrarUsuarioService = cadastrarUsuarioService;
        _buscarSolicitacoesPorUsuarioService = buscarSolicitacoesPorUsuarioService;
        _cadastrarPostagemService = cadastrarPostagemService;
        _pesquisarAmigosService = pesquisarAmigosService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CadastrarUsuario([FromBody] CadastrarUsuarioRequest request, CancellationToken cancelationToken)
    {
        var serviceResult = new CadastrarUsuarioResponse();

        try
        {
            _logger.LogInformation($"Solicitação do endpoint [{nameof(CadastrarUsuario)}].", request);

            var resultValidation = await new CadastrarUsuarioRequestValidator().ValidateAsync(request, cancelationToken);
            if (!resultValidation.IsValid)
                return BadRequest($"{resultValidation.Errors.First().PropertyName} - {resultValidation.Errors.First().ErrorMessage}");

            var emailJaExiste = await _pesquisarUsuariosService.Executar(request.Email, cancelationToken);
            if (emailJaExiste.Count >= 1)
                return BadRequest("E-mail já cadastrado! Por favor, utiliza outro.");

            serviceResult = await _cadastrarUsuarioService.Executar(request, cancelationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao cadastrar usuário {request}.");
            return Problem(ex.StackTrace, statusCode: (int)HttpStatusCode.InternalServerError, title: ex.Message);
        }

        return Ok(serviceResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarUsuario([FromRoute] Guid id, CancellationToken cancelationToken)
    {
        var serviceResult = new BuscarUsuarioResponse();

        try
        {
            _logger.LogInformation($"Solicitação do endpoint [{nameof(BuscarUsuario)}].", id);
            serviceResult = await _pesquisarUsuarioService.Executar(id, cancelationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao buscar usuário de Id {id}.");
            return Problem(ex.StackTrace, statusCode: (int)HttpStatusCode.InternalServerError, title: ex.Message);
        }

        return Ok(serviceResult);
    }

    [HttpPost("pesquisar")]
    public async Task<IActionResult> PesquisarUsuarios([FromBody] PesquisarUsuariosRequest request, CancellationToken cancelationToken)
    {
        try
        {
            _logger.LogInformation($"Solicitação do endpoint [{nameof(PesquisarUsuarios)}].", request);
            var serviceResult = await _pesquisarUsuariosPaginadoService.Executar(request, cancelationToken);
            return Ok(serviceResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao pesquisar usuários. (Filtro: {request})");
            return Problem(ex.StackTrace, statusCode: (int)HttpStatusCode.InternalServerError, title: ex.Message);
        }
    }

    [HttpGet("{id}/solicitacoes")]
    public async Task<IActionResult> BuscarSolicitacoes([FromRoute] Guid id, CancellationToken cancelationToken)
    {
        var serviceResult = new List<BuscarSolicitacaoResponse>();

        try
        {
            _logger.LogInformation($"Solicitação do endpoint [{nameof(BuscarSolicitacoes)}].", id);
            serviceResult = await _buscarSolicitacoesPorUsuarioService.Executar(id, cancelationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao buscar solicitações de amizade para o usuário de Id {id}.");
            return Problem(ex.StackTrace, statusCode: (int)HttpStatusCode.InternalServerError, title: ex.Message);
        }

        return Ok(serviceResult);
    }

    [HttpPost("{id}/Postar")]
    public async Task<IActionResult> CadastrarPostagem([FromRoute] Guid id, [FromBody] CadastrarPostagemRequest request, CancellationToken cancelationToken)
    {
        var serviceResult = new CadastrarPostagemResponse();

        try
        {
            _logger.LogInformation($"Solicitação do endpoint [{nameof(CadastrarPostagem)}].", request);

            var resultValidation = await new CadastrarPostagemRequestValidator().ValidateAsync(request, cancelationToken);
            if (!resultValidation.IsValid)
                return BadRequest($"{resultValidation.Errors.First().PropertyName} - {resultValidation.Errors.First().ErrorMessage}");

            serviceResult = await _cadastrarPostagemService.Executar(id, request, cancelationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao cadastrar postagem para o usuário de Id {id}. ({request})");
            return Problem(ex.StackTrace, statusCode: (int)HttpStatusCode.InternalServerError, title: ex.Message);
        }

        return Ok(serviceResult);
    }

    [HttpPost("{id}/amigos")]
    public async Task<IActionResult> PesquisarAmigos([FromRoute] Guid id,[FromBody] PesquisarAmigosRequest request, CancellationToken cancelationToken)
    {
        try
        {
            _logger.LogInformation($"Solicitação do endpoint [{nameof(PesquisarAmigos)}].", request);
            var serviceResult = await _pesquisarAmigosService.Executar(id, request, cancelationToken);
            return Ok(serviceResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao pesquisar amigo do usuário {id}. (Filtro: {request})");
            return Problem(ex.StackTrace, statusCode: (int)HttpStatusCode.InternalServerError, title: ex.Message);
        }
    }
}