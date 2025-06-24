using Microsoft.Extensions.Logging;
using MinhaRedeSocial.Aplicacao.Contratos.Request;
using MinhaRedeSocial.Aplicacao.Contratos.Response;
using MinhaRedeSocial.Aplicacao.Contratos.Services;
using MinhaRedeSocial.Aplicacao.Extensions.Mapping.Model;
using MinhaRedeSocial.Domain.Contratos.Paged;
using MinhaRedeSocial.Domain.Contratos.Repositorios;

namespace MinhaRedeSocial.Aplicacao.Services.Usuario;

public class PesquisarUsuariosPaginadoService : IPesquisarUsuariosPaginadoService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ILogger<PesquisarUsuariosPaginadoService> _logger;

    public PesquisarUsuariosPaginadoService(IUsuarioRepository usuarioRepository, ILogger<PesquisarUsuariosPaginadoService> logger)
    {
        _usuarioRepository = usuarioRepository;
        _logger = logger;
    }

    public async Task<IPagedList<BuscarUsuarioResponse>> Executar(PesquisarUsuariosRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var resultado = await _usuarioRepository.PesquisarPaginado(request.NomeEmail, request.Page, request.PageSize, request.SortDirection, cancellationToken);

            if (resultado is null || resultado.Source.Count < 1)
                _logger.LogInformation("Nenhum usuário foi encontrado.");

            return new PagedList<BuscarUsuarioResponse>(resultado!.Source.MapToBuscarUsuariosResponse(), resultado.Page, resultado.PageSize, resultado.TotalCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao buscar usuários págiando. (Filtro: {request})");
            throw;
        }
    }
}