using MinhaRedeSocial.Domain.Models.Usuarios;

namespace MinhaRedeSocial.Domain.Contratos.Repositorios;

public interface IUsuarioRepository
{
    Task<List<Usuario>> BuscarTodos();
    Task<Usuario?> Buscar(Guid id);
    Task<Usuario> Adicionar(Usuario usuario);
    Task<Usuario?> Atualizar(Usuario usuario);
    Task<bool> Deletar(Guid id);
}