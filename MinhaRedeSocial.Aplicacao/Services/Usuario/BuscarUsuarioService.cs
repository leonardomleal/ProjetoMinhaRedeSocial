using Microsoft.Extensions.Logging;
using MinhaRedeSocial.Aplicacao.Contratos.Response;
using MinhaRedeSocial.Aplicacao.Contratos.Services;
using MinhaRedeSocial.Aplicacao.Extensions.Mapping.Model;
using MinhaRedeSocial.Domain.Contratos.Repositorios;

namespace MinhaRedeSocial.Aplicacao.Services.Usuario;

public class BuscarUsuarioService : IBuscarUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ILogger<BuscarUsuarioService> _logger;

    public BuscarUsuarioService(IUsuarioRepository usuarioRepository, ILogger<BuscarUsuarioService> logger)
    {
        _usuarioRepository = usuarioRepository;
        _logger = logger;
    }

    public async Task<BuscarUsuarioResponse> Executar(Guid id, CancellationToken cancellationToken)
    {
        var retorno = new BuscarUsuarioResponse();

        try
        {
            var resultado = await _usuarioRepository.Buscar(id, cancellationToken);

            if (resultado is null)
                _logger.LogInformation("Nenhum usuário foi encontrado.");

            retorno = resultado?.MapToBuscarUsuarioResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao buscar usuário {id}.");
            throw;
        }

        return retorno;
    }
}