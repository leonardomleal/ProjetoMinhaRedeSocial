namespace MinhaRedeSocial.Aplicacao.Contratos.Response;

public class CadastrarComentarioResponse
{
    public Guid Id { get; set; }
    public DateTime Data { get; set; }
    public string Nome { get; set; }
    public string? Foto { get; set; }
    public string Texto { get; set; }
}