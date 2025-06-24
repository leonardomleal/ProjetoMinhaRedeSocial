using MinhaRedeSocial.Aplicacao.Contratos.Request;
using MinhaRedeSocial.Aplicacao.Contratos.Response;

namespace MinhaRedeSocial.Aplicacao.Contratos.Services;

public interface ICadastrarUsuarioService
{
    Task<CadastrarUsuarioResponse> Executar(CadastrarUsuarioRequest request, CancellationToken cancellationToken);
}