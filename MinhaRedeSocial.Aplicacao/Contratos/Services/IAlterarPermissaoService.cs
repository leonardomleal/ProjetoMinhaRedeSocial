using MinhaRedeSocial.Aplicacao.Contratos.Response;
using MinhaRedeSocial.Domain.Enums;

namespace MinhaRedeSocial.Aplicacao.Contratos.Services;

public interface IAlterarPermissaoService
{
    Task<BuscarPostagensResponse> Executar(Guid id, PostagemPermissoes permissao, CancellationToken cancellationToken);
}