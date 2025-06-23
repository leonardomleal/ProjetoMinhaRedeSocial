using Microsoft.Extensions.Logging;
using MinhaRedeSocial.Aplicacao.Contratos.Response;
using MinhaRedeSocial.Aplicacao.Contratos.Services;
using MinhaRedeSocial.Aplicacao.Extensions.Mapping.Model;
using MinhaRedeSocial.Domain.Contratos.Repositorios;

namespace MinhaRedeSocial.Aplicacao.Services.Usuario;

public class PesquisarUsuariosService : IPesquisarUsuariosService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ILogger<PesquisarUsuariosService> _logger;

    public PesquisarUsuariosService(IUsuarioRepository usuarioRepository, ILogger<PesquisarUsuariosService> logger)
    {
        _usuarioRepository = usuarioRepository;
        _logger = logger;
    }

    public async Task<List<BuscarUsuarioResponse>> Executar(string nomeEmail, CancellationToken cancellationToken)
    {
        var retorno = new List<BuscarUsuarioResponse>();

		try
		{
            var resultado = await _usuarioRepository.Pesquisar(nomeEmail, cancellationToken);

            if (resultado.Count < 1)
                _logger.LogInformation("Nenhum usuário foi encontrado.");

            retorno = resultado?.MapToBuscarUsuariosResponse();
        }
		catch (Exception ex)
		{
            _logger.LogError(ex, $"Ocorreu um erro ao buscar usuários. (Filtro: {nomeEmail})");
            throw;
		}

        return retorno;
    }
}