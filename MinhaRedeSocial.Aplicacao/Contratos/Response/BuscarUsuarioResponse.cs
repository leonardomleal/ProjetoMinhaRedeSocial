namespace MinhaRedeSocial.Aplicacao.Contratos.Response;

public class BuscarUsuarioResponse
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string? Apelido { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Cep { get; set; }
    public string Senha { get; set; }
    public string? Foto { get; set; }
}