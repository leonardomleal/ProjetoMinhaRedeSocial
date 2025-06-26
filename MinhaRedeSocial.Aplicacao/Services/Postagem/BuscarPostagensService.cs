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
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ILogger<BuscarPostagensService> _logger;

    public BuscarPostagensService(
        IPostagemRepository postagemRepository,
        IUsuarioRepository usuarioRepository,
        ILogger<BuscarPostagensService> logger)
    {
        _postagemRepository = postagemRepository;
        _usuarioRepository = usuarioRepository;
        _logger = logger;
    }

    public async Task<IPagedList<BuscarPostagensResponse>> Executar(BuscarPostagensRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var usuario = await _usuarioRepository.Buscar(request.Id, cancellationToken);
            if (usuario is null)
            {
                _logger.LogError($"Usuário de Id {request.Id} não encontrado.");
                throw new Exception($"Usuário de Id {request.Id} não encontrado.");
            }

            var amigos = usuario!.Amigos.Select(x => x.UsuarioId).ToList();
            if (amigos is null || amigos.Count < 1)
                _logger.LogInformation($"Nenhum amigo foi encontrado para o usuário de Id {request.Id}.");

            var postagens = await _postagemRepository.BuscarFeed(request.MapToBuscarPostagensDto(amigos), cancellationToken);
            if (postagens is null || postagens.Source.Count < 1)
                _logger.LogInformation($"Nenhuma postagem foi encontrada para o usuário {request.Id}.");

            return new PagedList<BuscarPostagensResponse>(
                postagens!.Source.MapToBuscarPostagensResponse(), 
                postagens.Page, postagens.PageSize, postagens.TotalCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao buscar postagens para o usuário {request.Id}.");
            throw;
        }
    }
}