using MinhaRedeSocial.Aplicacao.Contratos.Response;

namespace MinhaRedeSocial.Aplicacao.Contratos.Services;

public interface IPesquisarUsuariosService
{
    Task<List<BuscarUsuarioResponse>> Executar(string nomeEmail, CancellationToken cancellationToken);
}