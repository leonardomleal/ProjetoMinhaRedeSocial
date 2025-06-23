using MinhaRedeSocial.Domain.Enums;

namespace MinhaRedeSocial.Domain.Models.Postagens;

public class Postagem
{
    public Guid Id { get; set; }
    public Guid Usuario { get; set; }
    public DateTime Data {  get; set; }
    public string Texto { get; set; }
    public List<Guid> Curtidas { get; set; }
    public List<Comentario> Comentarios { get; set; }
    public PostagemPermissoes Permissao {  get; set; }
}