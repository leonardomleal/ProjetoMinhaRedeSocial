using Microsoft.AspNetCore.Mvc;
using MinhaRedeSocial.Aplicacao.Contratos.Request;
using MinhaRedeSocial.Aplicacao.Contratos.Response;
using MinhaRedeSocial.Aplicacao.Contratos.Services;
using MinhaRedeSocial.Aplicacao.Validators;
using System.Net;

namespace MinhaRedeSocial.Api.Controllers;

[ApiVersion("1")]
[ApiController, Route("api/v{version:apiVersion}/Usuario"), Produces("application/json")]
public class UsuarioController : ControllerBase
{
    private readonly IBuscarUsuarioService _buscarUsuarioService;
    private readonly ICadastrarUsuarioService _cadastrarUsuarioService;
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(
        IBuscarUsuarioService buscarUsuarioService,
        ICadastrarUsuarioService cadastrarUsuarioService,
        ILogger<UsuarioController> logger)
    {
        _buscarUsuarioService = buscarUsuarioService;
        _cadastrarUsuarioService = cadastrarUsuarioService;
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

            var emailJaExiste = _buscarUsuarioService.Executar(request.Email, cancelationToken);
            if (emailJaExiste is not null)
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
}