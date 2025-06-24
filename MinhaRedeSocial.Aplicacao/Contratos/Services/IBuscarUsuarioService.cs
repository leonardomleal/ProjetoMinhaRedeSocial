using MinhaRedeSocial.Aplicacao.Contratos.Response;

namespace MinhaRedeSocial.Aplicacao.Contratos.Services;

public interface IBuscarUsuarioService
{
    Task<BuscarUsuarioResponse> Executar(Guid id, CancellationToken cancellationToken);
}