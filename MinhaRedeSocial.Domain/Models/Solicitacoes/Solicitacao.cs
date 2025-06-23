namespace MinhaRedeSocial.Domain.Models.Solicitacoes;

public class Solicitacao
{
    public Guid Id { get; set; }
    public Solicitante Solicitante { get; set; }
    public Guid Solicitado { get; set; }
}