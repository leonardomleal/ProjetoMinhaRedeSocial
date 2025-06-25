using MinhaRedeSocial.Domain.Contratos.Dto.Postagem;
using MinhaRedeSocial.Domain.Contratos.Paged;
using MinhaRedeSocial.Domain.Models.Postagens;

namespace MinhaRedeSocial.Domain.Contratos.Repositorios;

public interface IPostagemRepository
{
    Task<Postagem> Cadastrar(Postagem postagem, CancellationToken cancellationToken);
    Task<IPagedList<Postagem>> Buscar(BuscarPostagensDto request, CancellationToken cancellationToken);
}