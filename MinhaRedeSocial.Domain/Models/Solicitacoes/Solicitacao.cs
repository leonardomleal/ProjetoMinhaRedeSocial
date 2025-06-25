using MinhaRedeSocial.Domain.Models.Usuarios;

namespace MinhaRedeSocial.Domain.Models.Solicitacoes;

public class Solicitacao
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public string? Mensagem { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid SolicitanteId { get; set; }


    public Usuario Usuario { get; set; } = null!;
    public Solicitante Solicitante { get; set; } = null!;
}