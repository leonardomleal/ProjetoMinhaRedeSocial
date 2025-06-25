using Microsoft.Extensions.Logging;
using MinhaRedeSocial.Aplicacao.Contratos.Request;
using MinhaRedeSocial.Aplicacao.Contratos.Response;
using MinhaRedeSocial.Aplicacao.Contratos.Services;
using MinhaRedeSocial.Aplicacao.Extensions.Mapping.Model;
using MinhaRedeSocial.Aplicacao.Extensions.Mapping.Request;
using MinhaRedeSocial.Domain.Contratos.Paged;
using MinhaRedeSocial.Domain.Contratos.Repositorios;

namespace MinhaRedeSocial.Aplicacao.Services.Postagem;

public class BuscarPostagensService : IBuscarPostagensService
{
    private readonly IPostagemRepository _postagemRepository;
    private readonly IAmizadeRepository _amizadeRepository;
    private readonly ILogger<BuscarPostagensService> _logger;

    public BuscarPostagensService(
        IPostagemRepository usuarioRepository,
        IAmizadeRepository amizadeRepository,
        ILogger<BuscarPostagensService> logger)
    {
        _postagemRepository = usuarioRepository;
        _amizadeRepository = amizadeRepository;
        _logger = logger;
    }

    public async Task<IPagedList<BuscarPostagensResponse>> Executar(BuscarPostagensRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var amizades = await _amizadeRepository.Buscar(request.Id, cancellationToken);
            if (amizades is null || amizades.Count < 1)
            {
                _logger.LogInformation($"Nenhuma amizade foi encontrada para o usuário {request.Id}.");

                var postagens = await _postagemRepository.Buscar(request.MapToBuscarPostagensDto(), cancellationToken);
                if (postagens is null || postagens.Source.Count < 1)
                    _logger.LogInformation($"Nenhuma postagem foi encontrada para o usuário {request.Id}.");

                return new PagedList<BuscarPostagensResponse>(
                    postagens!.Source.MapToBuscarPostagensResponse(), 
                    postagens.Page, postagens.PageSize, postagens.TotalCount);
            }
            else
            {
                var postagens = await _postagemRepository.BuscarComAmigos(request.MapToBuscarPostagensDto(
                    amizades.Select(x => x.Amigo.UsuarioId).ToList()), cancellationToken);
                if (postagens is null || postagens.Source.Count < 1)
                    _logger.LogInformation($"Nenhuma postagem foi encontrada para o usuário {request.Id}.");

                return new PagedList<BuscarPostagensResponse>(
                    postagens!.Source.MapToBuscarPostagensResponse(), 
                    postagens.Page, postagens.PageSize, postagens.TotalCount);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao buscar postagens para o usuário {request.Id}.");
            throw;
        }
    }
}