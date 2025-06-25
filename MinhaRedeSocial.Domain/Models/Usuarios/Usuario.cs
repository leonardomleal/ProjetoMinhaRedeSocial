using MinhaRedeSocial.Domain.Models.Amigos;
using MinhaRedeSocial.Domain.Models.Postagens;
using MinhaRedeSocial.Domain.Models.Solicitacoes;

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

    public List<Amigo> Amigos { get; } = [];
    public Amigo Amigo { get; } = null!;
    public virtual List<Solicitacao> Solicitacaos { get; set; }
    public virtual List<Solicitante> Solicitantes { get; set; }
    public virtual List<Postagem> Postagens {  get; set; }
    public virtual List<Comentario> Comentarios { get; set; }
}