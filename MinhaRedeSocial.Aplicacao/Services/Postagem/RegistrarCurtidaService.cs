using Microsoft.Extensions.Logging;
using MinhaRedeSocial.Aplicacao.Contratos.Response;
using MinhaRedeSocial.Aplicacao.Contratos.Services;
using MinhaRedeSocial.Aplicacao.Extensions.Mapping.Model;
using MinhaRedeSocial.Domain.Contratos.Repositorios;

namespace MinhaRedeSocial.Aplicacao.Services.Postagem;

public class RegistrarCurtidaService : IRegistrarCurtidaService
{
    private readonly IPostagemRepository _postagemRepository;
    private readonly ILogger<RegistrarCurtidaService> _logger;

    public RegistrarCurtidaService(IPostagemRepository postagemRepository, ILogger<RegistrarCurtidaService> logger)
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

            var postagemCurtida = await _postagemRepository.Curtir(id, cancellationToken);
            retorno = postagemCurtida?.MapToBuscarPostagensResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao registrar curtida para a postagem {id}.");
            throw;
        }

        return retorno;
    }
}