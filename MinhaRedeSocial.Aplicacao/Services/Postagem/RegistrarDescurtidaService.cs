using Microsoft.Extensions.Logging;
using MinhaRedeSocial.Aplicacao.Contratos.Response;
using MinhaRedeSocial.Aplicacao.Contratos.Services;
using MinhaRedeSocial.Aplicacao.Extensions.Mapping.Model;
using MinhaRedeSocial.Domain.Contratos.Repositorios;

namespace MinhaRedeSocial.Aplicacao.Services.Postagem;

public class RegistrarDescurtidaService : IRegistrarDescurtidaService
{
    private readonly IPostagemRepository _postagemRepository;
    private readonly ILogger<RegistrarDescurtidaService> _logger;

    public RegistrarDescurtidaService(IPostagemRepository postagemRepository, ILogger<RegistrarDescurtidaService> logger)
    {
        _postagemRepository = postagemRepository;
        _logger = logger;
    }

    public async Task<BuscarPostagensResponse> Executar(Guid id, CancellationToken cancellationToken)
    {
        var retorno = new BuscarPostagensResponse();

        try
        {
            var postagem = await _postagemRepository.Buscar(id, cancellationToken);
            if (postagem is null)
            {
                _logger.LogInformation($"Nenhuma postagens com o Id {id} foi encontrada.");
                throw new Exception($"Nenhuma postagens com o Id {id} foi encontrada.");
            }

            var postagemDescurtida = await _postagemRepository.Descurtir(id, cancellationToken);
            retorno = postagemDescurtida?.MapToBuscarPostagensResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao registrar descurtida para a postagem {id}.");
            throw;
        }

        return retorno;
    }
}