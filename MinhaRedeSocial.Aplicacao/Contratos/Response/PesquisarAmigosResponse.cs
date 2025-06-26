namespace MinhaRedeSocial.Aplicacao.Contratos.Response;

public class PesquisarAmigosResponse
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string? Apelido { get; set; }
    public string? Foto { get; set; }
}