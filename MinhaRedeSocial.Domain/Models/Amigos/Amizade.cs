using MinhaRedeSocial.Domain.Models.Usuarios;

namespace MinhaRedeSocial.Domain.Models.Amigos;

public class Amizade
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public List<Amigo> Amizades { get; set; }

    public virtual Usuario Usuario { get; set; } 
}