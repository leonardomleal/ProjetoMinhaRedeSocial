namespace MinhaRedeSocial.Domain.Models.Solicitacoes;

public class Solicitante
{
    public Guid Usuario { get; set; }
    public string Nome { get; set; }
    public string Apelido { get; set; }
    public string Foto { get; set; }
}