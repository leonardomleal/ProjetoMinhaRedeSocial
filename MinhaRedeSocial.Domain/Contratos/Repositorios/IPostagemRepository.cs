using MinhaRedeSocial.Domain.Models.Postagens;

namespace MinhaRedeSocial.Domain.Contratos.Repositorios;

public interface IPostagemRepository
{
    Task<Postagem> Cadastrar(Postagem postagem, CancellationToken cancellationToken);
}