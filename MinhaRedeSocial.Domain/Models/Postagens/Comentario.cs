using MinhaRedeSocial.Domain.Models.Usuarios;

namespace MinhaRedeSocial.Domain.Models.Postagens;

public class Comentario
{
    public Guid Id { get; set; }
    public DateTime Data {  get; set; }
    public string Texto { get; set; }
    public Guid PostagemId { get; set; }
    public Guid UsuarioId { get; set; }

    public Usuario Usuario { get; } = null!;
    public Postagem Postagem { get; } = null!;
}