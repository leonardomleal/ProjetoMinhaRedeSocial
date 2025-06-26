using MinhaRedeSocial.Aplicacao.Contratos.Response;

namespace MinhaRedeSocial.Aplicacao.Contratos.Services;

public interface IRegistrarCurtidaService
{
    Task<BuscarPostagensResponse> Executar(Guid id, CancellationToken cancellationToken);
}