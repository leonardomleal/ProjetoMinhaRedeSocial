using Microsoft.Extensions.Logging;
using MinhaRedeSocial.Aplicacao.Contratos.Request;
using MinhaRedeSocial.Aplicacao.Contratos.Response;
using MinhaRedeSocial.Aplicacao.Contratos.Services;
using MinhaRedeSocial.Aplicacao.Extensions.Mapping.Model;
using MinhaRedeSocial.Domain.Contratos.Paged;
using MinhaRedeSocial.Domain.Contratos.Repositorios;

namespace MinhaRedeSocial.Aplicacao.Services.Amigo;

public class PesquisarAmigosService : IPesquisarAmigosService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IAmigoRepository _amigoRepository;
    private readonly ILogger<PesquisarAmigosService> _logger;

    public PesquisarAmigosService(
        IUsuarioRepository usuarioRepository, 
        IAmigoRepository amigoRepository,
        ILogger<PesquisarAmigosService> logger)
    {
        _usuarioRepository = usuarioRepository;
        _amigoRepository = amigoRepository;
        _logger = logger;
    }

    public async Task<IPagedList<PesquisarAmigosResponse>> Executar(Guid id, PesquisarAmigosRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var usuario = await _usuarioRepository.Buscar(id, cancellationToken);
            if (usuario is null)
            {
                _logger.LogError($"Nenhum usuário foi encontrado para o id {id}.", id);
                throw new Exception($"Nenhum usuário foi encontrado para o id {id}.");
            }

            if (usuario.Amigos is null || usuario.Amigos.Count < 1)
            {
                _logger.LogInformation($"Usuário de Id {id} não possui amigos.");
                return new PagedList<PesquisarAmigosResponse>(new List<PesquisarAmigosResponse>(), request.Page, request.PageSize, 0);
            }

            var resultado = await _amigoRepository.Pesquisar(request.NomeEmail, 
                usuario.Amigos.Select(x => x.Id).ToList(), 
                request.PesquisarAmigosSort, request.Page, request.PageSize, request.SortDirection, 
                cancellationToken);

            return new PagedList<PesquisarAmigosResponse>(resultado!.Source.MapToPesquisarAmigosResponse(), resultado.Page, resultado.PageSize, resultado.TotalCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao buscar amigos págiando para o usuário {id}. (Filtro: {request})");
            throw;
        }
    }
}