using MinhaRedeSocial.Aplicacao.Contratos.Request;
using MinhaRedeSocial.Aplicacao.Contratos.Response;
using MinhaRedeSocial.Domain.Contratos.Paged;

namespace MinhaRedeSocial.Aplicacao.Contratos.Services;

public interface IPesquisarAmigosService
{
    Task<IPagedList<PesquisarAmigosResponse>> Executar(Guid id, PesquisarAmigosRequest request, CancellationToken cancellationToken);
}