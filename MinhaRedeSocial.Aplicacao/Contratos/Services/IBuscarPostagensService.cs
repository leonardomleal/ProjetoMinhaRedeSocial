using MinhaRedeSocial.Aplicacao.Contratos.Response;

namespace MinhaRedeSocial.Aplicacao.Contratos.Services;

public interface IBuscarPostagensService
{
    Task<List<BuscarPostagensResponse>> Executar(Guid id, CancellationToken cancellationToken);
}