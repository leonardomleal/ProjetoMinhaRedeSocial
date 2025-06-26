using MinhaRedeSocial.Aplicacao.Contratos.Response;

namespace MinhaRedeSocial.Aplicacao.Contratos.Services;

public interface IRegistrarDescurtidaService
{
    Task<BuscarPostagensResponse> Executar(Guid id, CancellationToken cancellationToken);
}