using MinhaRedeSocial.Domain.Contratos.Paged;
using MinhaRedeSocial.Domain.Enums.Sorts;
using MinhaRedeSocial.Domain.Models.Amigos;

namespace MinhaRedeSocial.Domain.Contratos.Repositorios;

public interface IAmigoRepository
{
    Task<IPagedList<Amigo>> Pesquisar(string nomeEmail, List<Guid> amigos, PesquisarAmigosSort orderBy, int pageNumber, int pageSize, SortDirection sort, CancellationToken cancellationToken);
}