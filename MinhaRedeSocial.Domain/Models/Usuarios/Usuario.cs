using MinhaRedeSocial.Domain.Models.Amigos;

namespace MinhaRedeSocial.Domain.Models.Usuarios;

public class Usuario
{
    public Usuario(string nome, string email, string? apelido, DateTime dataNascimento, string cep, string senha, string? foto)
    {
        Nome = nome;
        Email = email;
        Apelido = apelido;
        DataNascimento = dataNascimento;
        Cep = cep;
        Senha = senha;
        Foto = foto;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; }
    public string Email { get; set; }
    public string? Apelido { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Cep { get; set; }
    public string Senha { get; set; }
    public string? Foto { get; set; }

    public virtual List<Amizade> Amizades { get; set; }
    public virtual Amigo Amigo { get; set; }
}