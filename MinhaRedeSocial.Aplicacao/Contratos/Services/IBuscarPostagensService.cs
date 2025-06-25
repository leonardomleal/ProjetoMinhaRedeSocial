using MinhaRedeSocial.Aplicacao.Contratos.Request;
using MinhaRedeSocial.Aplicacao.Contratos.Response;
using MinhaRedeSocial.Domain.Contratos.Paged;

namespace MinhaRedeSocial.Aplicacao.Contratos.Services;

public interface IBuscarPostagensService
{
    Task<IPagedList<BuscarPostagensResponse>> Executar(BuscarPostagensRequest request, CancellationToken cancellationToken);
}