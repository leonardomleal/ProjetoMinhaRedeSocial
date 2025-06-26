using MinhaRedeSocial.Aplicacao.Contratos.Request;
using MinhaRedeSocial.Aplicacao.Contratos.Response;

namespace MinhaRedeSocial.Aplicacao.Contratos.Services;

public interface ICadastrarComentarioService
{
    Task<CadastrarComentarioResponse> Executar(Guid postagemId, CadastrarComentarioRequest request, CancellationToken cancellationToken);
}