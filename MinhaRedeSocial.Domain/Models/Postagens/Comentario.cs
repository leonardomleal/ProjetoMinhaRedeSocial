namespace MinhaRedeSocial.Domain.Models.Postagens;

public class Comentario
{
    public Guid Id { get; set; }
    public Guid Usuario { get; set; }
    public string Texto { get; set; }
}