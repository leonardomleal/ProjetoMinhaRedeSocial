using MinhaRedeSocial.Domain.Models.Usuarios;

namespace MinhaRedeSocial.Domain.Models.Solicitacoes;

public class Solicitante
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string? Apelido { get; set; }
    public string? Foto { get; set; }
    public Guid UsuarioId { get; set; }


    public List<Solicitacao> Solicitacoes { get; } = [];
    public Usuario Usuario { get; } = null!;
}