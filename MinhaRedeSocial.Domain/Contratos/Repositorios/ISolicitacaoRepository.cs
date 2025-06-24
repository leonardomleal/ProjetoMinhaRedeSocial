using MinhaRedeSocial.Domain.Models.Solicitacoes;

namespace MinhaRedeSocial.Domain.Contratos.Repositorios;

public interface ISolicitacaoRepository
{
    Task<List<Solicitacao>> BuscarPorUsuario(Guid id, CancellationToken cancellationToken);
}