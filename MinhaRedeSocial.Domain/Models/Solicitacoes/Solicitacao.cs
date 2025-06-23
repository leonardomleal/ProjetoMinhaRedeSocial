using MinhaRedeSocial.Domain.Models.Usuarios;

namespace MinhaRedeSocial.Domain.Models.Solicitacoes;

public class Solicitacao
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid SolicitadoId { get; set; }
    public string? Mensagem { get; set; }

    public virtual Usuario Usuario { get; set; } 
    public virtual Solicitante Solicitante { get; set; }
}