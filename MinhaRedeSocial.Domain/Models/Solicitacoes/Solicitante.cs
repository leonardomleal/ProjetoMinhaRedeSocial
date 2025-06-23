using MinhaRedeSocial.Domain.Models.Usuarios;

namespace MinhaRedeSocial.Domain.Models.Solicitacoes;

public class Solicitante
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string? Apelido { get; set; }
    public string? Foto { get; set; }
    public Guid UsuarioId { get; set; }

    public virtual Solicitacao Solicitacao { get; set; }
    public virtual Usuario Usuario { get; set; } 
}