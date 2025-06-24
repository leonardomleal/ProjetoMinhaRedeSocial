using Microsoft.Extensions.Logging;
using MinhaRedeSocial.Aplicacao.Contratos.Response;
using MinhaRedeSocial.Aplicacao.Contratos.Services;
using MinhaRedeSocial.Aplicacao.Extensions.Mapping.Model;
using MinhaRedeSocial.Domain.Contratos.Repositorios;

namespace MinhaRedeSocial.Aplicacao.Services.Postagem;

public class BuscarPostagensService : IBuscarPostagensService
{
    private readonly IPostagemRepository _postagemRepository;
    private readonly IAmizadeRepository _amizadeRepository;
    private readonly ILogger<BuscarPostagensService> _logger;

    public BuscarPostagensService(IPostagemRepository usuarioRepository, ILogger<BuscarPostagensService> logger)
    {
        _postagemRepository = usuarioRepository;
        _logger = logger;
    }

    public async Task<List<BuscarPostagensResponse>> Executar(Guid id, CancellationToken cancellationToken)
    {
        var retorno = new List<BuscarPostagensResponse>();

        try
        {
            var postagens = await _postagemRepository.Buscar(id, cancellationToken);
            if (postagens.Count < 1)
                _logger.LogInformation($"Nenhuma postagem foi encontrada para o usuário {id}.");

            var amizades = await _amizadeRepository.Buscar(id, cancellationToken);
            if (amizades.Count > 0)
            {
                var postagensAmigos = await _postagemRepository.BuscarPostagensAmigos(amizades.Select(x => x.Id).ToList(), cancellationToken);
                if (postagensAmigos.Count < 1)
                    _logger.LogInformation($"Nenhuma postagem de amizades foi encontrada para o usuário {id}.");
                else
                    postagens.AddRange(postagensAmigos);
            }

            retorno = postagens?.MapToBuscarPostagensResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao buscar postagens para o usuário {id}.");
            throw;
        }

        return retorno;
    }
}