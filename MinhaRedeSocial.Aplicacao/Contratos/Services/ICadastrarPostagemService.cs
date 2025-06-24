using MinhaRedeSocial.Aplicacao.Contratos.Request;
using MinhaRedeSocial.Aplicacao.Contratos.Response;

namespace MinhaRedeSocial.Aplicacao.Contratos.Services;

public interface ICadastrarPostagemService
{
    Task<CadastrarPostagemResponse> Executar(Guid usuarioId, CadastrarPostagemRequest request, CancellationToken cancellationToken);
}