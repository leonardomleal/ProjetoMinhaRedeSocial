using MinhaRedeSocial.Domain.Models.Usuarios;

namespace MinhaRedeSocial.Domain.Models.Postagens;

public class Comentario
{
    public Comentario(string texto, Guid postagemId, Guid usuarioId)
    {
        Texto = texto;
        PostagemId = postagemId;
        UsuarioId = usuarioId;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Data {  get; set; } = DateTime.Now;
    public string Texto { get; set; }
    public Guid PostagemId { get; set; }
    public Guid UsuarioId { get; set; }

    public Usuario Usuario { get; set; } = null!;
    public Postagem Postagem { get; set; } = null!;
}