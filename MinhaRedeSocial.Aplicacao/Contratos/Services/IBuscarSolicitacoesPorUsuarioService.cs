using MinhaRedeSocial.Aplicacao.Contratos.Response;

namespace MinhaRedeSocial.Aplicacao.Contratos.Services;

public interface IBuscarSolicitacoesPorUsuarioService
{
    Task<List<BuscarSolicitacaoResponse>> Executar(Guid id, CancellationToken cancellationToken);
}