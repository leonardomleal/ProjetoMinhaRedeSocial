using MinhaRedeSocial.Domain.Contratos.Dto.Postagem;
using MinhaRedeSocial.Domain.Contratos.Paged;
using MinhaRedeSocial.Domain.Enums;
using MinhaRedeSocial.Domain.Models.Postagens;

namespace MinhaRedeSocial.Domain.Contratos.Repositorios;

public interface IPostagemRepository
{
    Task<Postagem> Cadastrar(Postagem postagem, CancellationToken cancellationToken);
    Task<Postagem?> Buscar(Guid id, CancellationToken cancellationToken);
    Task<IPagedList<Postagem>> BuscarFeed(BuscarPostagensDto request, CancellationToken cancellationToken);
    Task<Postagem?> Curtir(Guid id, CancellationToken cancellation);
    Task<Postagem?> Descurtir(Guid id, CancellationToken cancellation);
    Task<Postagem?> AlterarPermissao(Guid id, PostagemPermissoes permissao, CancellationToken cancellation);
}