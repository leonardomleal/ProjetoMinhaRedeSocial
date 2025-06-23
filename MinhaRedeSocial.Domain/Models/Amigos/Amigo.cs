namespace MinhaRedeSocial.Domain.Models.Amigos;

public class Amigo
{
    public Guid Usuario { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Apelido { get; set; }
    public string Foto { get; set; }
}