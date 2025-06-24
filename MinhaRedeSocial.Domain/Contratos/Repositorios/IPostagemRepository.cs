using MinhaRedeSocial.Domain.Models.Postagens;

namespace MinhaRedeSocial.Domain.Contratos.Repositorios;

public interface IPostagemRepository
{
    Task<List<Postagem>> Buscar(Guid id, CancellationToken cancellationToken);
    Task<List<Postagem>> BuscarPostagensAmigos(List<Guid> ids, CancellationToken cancellationToken);
}