using MinhaRedeSocial.Domain.Models.Postagens;

namespace MinhaRedeSocial.Domain.Contratos.Repositorios;

public interface IComentarioRepository
{
    Task<Comentario?> Cadastrar(Comentario comentario, CancellationToken cancellationToken);
    Task<Comentario?> Buscar(Guid id, CancellationToken cancellationToken);
}