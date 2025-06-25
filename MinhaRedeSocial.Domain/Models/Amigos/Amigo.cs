using MinhaRedeSocial.Domain.Models.Usuarios;

namespace MinhaRedeSocial.Domain.Models.Amigos;

public class Amigo
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string? Apelido { get; set; }
    public string? Foto { get; set; }
    public Guid UsuarioId { get; set; }

    public List<Usuario> Usuarios { get; set; } = [];
    public Usuario Usuario { get; set; } = null!;
}