using MinhaRedeSocial.Domain.Enums;
using MinhaRedeSocial.Domain.Models.Usuarios;

namespace MinhaRedeSocial.Domain.Models.Postagens;

public class Postagem
{
    public Guid Id { get; set; }
    public DateTime Data {  get; set; }
    public string Texto { get; set; }
    public int Curtidas { get; set; } = 0;
    public PostagemPermissoes Permissao {  get; set; }
    public Guid UsuarioId { get; set; }

    public virtual Usuario Usuario { get; set; }
    public virtual List<Comentario> Comentarios { get; set; }
}