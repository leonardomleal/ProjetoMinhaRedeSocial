using MinhaRedeSocial.Aplicacao.Contratos.Response;

namespace MinhaRedeSocial.Aplicacao.Contratos.Services;

public interface IBuscarUsuarioService
{
    Task<BuscarUsuarioResponse> Executar(string request, CancellationToken cancellationToken);
}