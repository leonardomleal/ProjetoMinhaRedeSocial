using MinhaRedeSocial.Aplicacao.Contratos.Request;
using MinhaRedeSocial.Aplicacao.Contratos.Response;
using MinhaRedeSocial.Domain.Contratos.Paged;

namespace MinhaRedeSocial.Aplicacao.Contratos.Services;

public interface IPesquisarUsuariosPaginadoService
{
    Task<IPagedList<BuscarUsuarioResponse>> Executar(PesquisarUsuariosRequest request, CancellationToken cancellationToken);
}