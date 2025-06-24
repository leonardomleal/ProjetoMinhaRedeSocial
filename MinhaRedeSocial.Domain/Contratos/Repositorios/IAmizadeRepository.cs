using MinhaRedeSocial.Domain.Models.Amigos;

namespace MinhaRedeSocial.Domain.Contratos.Repositorios;

public interface IAmizadeRepository
{
    Task<List<Amizade>> Buscar(Guid id, CancellationToken cancellationToken);
}