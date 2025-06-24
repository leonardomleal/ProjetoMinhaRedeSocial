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
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(
        IBuscarUsuarioService pesquisarUsuarioService,
        IPesquisarUsuariosService pesquisarUsuariosService,
        IPesquisarUsuariosPaginadoService pesquisarUsuariosPaginadoService,
        ICadastrarUsuarioService cadastrarUsuarioService,
        IBuscarSolicitacoesPorUsuarioService buscarSolicitacoesPorUsuarioService,
        ILogger<UsuarioController> logger)
    {
        _pesquisarUsuarioService = pesquisarUsuarioService;
        _pesquisarUsuariosService = pesquisarUsuariosService;
        _pesquisarUsuariosPaginadoService = pesquisarUsuariosPaginadoService;
        _cadastrarUsuarioService = cadastrarUsuarioService;
        _buscarSolicitacoesPorUsuarioService = buscarSolicitacoesPorUsuarioService;
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
            return Problem($"Ocorreu um erro ao cadastrar usuário {ex}.", statusCode: (int)HttpStatusCode.InternalServerError);
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
            return Problem($"Ocorreu um erro ao buscar usuário {ex}.", statusCode: (int)HttpStatusCode.InternalServerError);
        }

        return Ok(serviceResult);
    }

    [HttpGet("pesquisar")]
    public async Task<IActionResult> PesquisarUsuarios([FromQuery] PesquisarUsuariosRequest request, CancellationToken cancelationToken)
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
            return Problem($"Ocorreu um erro ao pesquisar usuários {ex}.", statusCode: (int)HttpStatusCode.InternalServerError);
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
            return Problem($"Ocorreu um erro ao buscar solicitações de amizade {ex}.", statusCode: (int)HttpStatusCode.InternalServerError);
        }

        return Ok(serviceResult);
    }
}