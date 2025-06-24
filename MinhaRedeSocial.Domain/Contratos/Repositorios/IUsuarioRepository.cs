using MinhaRedeSocial.Domain.Contratos.Paged;
using MinhaRedeSocial.Domain.Enums;
using MinhaRedeSocial.Domain.Models.Usuarios;

namespace MinhaRedeSocial.Domain.Contratos.Repositorios;

public interface IUsuarioRepository
{
    Task<List<Usuario>> BuscarTodos();
    Task<Usuario?> Buscar(Guid id);
    Task<Usuario> Adicionar(Usuario usuario);
    Task<Usuario?> Atualizar(Usuario usuario);
    Task<bool> Deletar(Guid id);
    Task<Usuario?> Buscar(Guid id, CancellationToken cancellationToken);
    Task<Usuario> Cadastrar(Usuario usuario, CancellationToken cancellationToken);
    Task<List<Usuario>> Pesquisar(string nomeEmail, CancellationToken cancellationToken);
    Task<IPagedList<Usuario>> PesquisarPaginado(string nomeEmail, int pageNumber, int pageSize, SortDirection sort, CancellationToken cancellationToken);
}