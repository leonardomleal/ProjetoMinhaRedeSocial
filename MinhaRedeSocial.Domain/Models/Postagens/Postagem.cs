using MinhaRedeSocial.Domain.Enums;
using MinhaRedeSocial.Domain.Models.Usuarios;

namespace MinhaRedeSocial.Domain.Models.Postagens;

public class Postagem
{
    public Postagem(string texto, PostagemPermissoes permissao, Guid usuarioId)
    {
        Texto = texto;
        Permissao = permissao;
        UsuarioId = usuarioId;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Data { get; set; } = DateTime.Now;
    public string Texto { get; set; }
    public int Curtidas { get; private set; } = 0;
    public PostagemPermissoes Permissao { get; private set; }
    public Guid UsuarioId { get; set; }

    public void Curtir()
        => Curtidas++;

    public void Descurtir()
        => Curtidas = Curtidas > 0 ? --Curtidas : Curtidas = 0;

    public void AlterarPermissao(PostagemPermissoes permissao)
        => Permissao = permissao;

    public List<Comentario> Comentarios { get; set; } = [];
    public Usuario Usuario { get; set; } = null!;
}