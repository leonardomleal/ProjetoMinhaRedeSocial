using Microsoft.Extensions.Logging;
using MinhaRedeSocial.Aplicacao.Contratos.Request;
using MinhaRedeSocial.Aplicacao.Contratos.Response;
using MinhaRedeSocial.Aplicacao.Contratos.Services;
using MinhaRedeSocial.Aplicacao.Extensions.Mapping.Model;
using MinhaRedeSocial.Aplicacao.Extensions.Mapping.Request;
using MinhaRedeSocial.Domain.Contratos.Repositorios;

namespace MinhaRedeSocial.Aplicacao.Services.Usuario;

public class CadastrarUsuarioService : ICadastrarUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ILogger<CadastrarUsuarioService> _logger;

    public CadastrarUsuarioService(IUsuarioRepository usuarioRepository, ILogger<CadastrarUsuarioService> logger)
    {
        _usuarioRepository = usuarioRepository;
        _logger = logger;
    }

    public async Task<CadastrarUsuarioResponse> Executar(CadastrarUsuarioRequest request, CancellationToken cancellationToken)
    {
        var retorno = new CadastrarUsuarioResponse();

        try
        {
            var usuario = request.MapToUsuario();
            var resultado = await _usuarioRepository.Cadastrar(usuario, cancellationToken);
            retorno = resultado.MapToCadastrarUsuarioResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao buscar usuário {request}.");
            throw;
        }

        return retorno;
    }
}