namespace MinhaRedeSocial.Aplicacao.Contratos.Request;

public class CadastrarComentarioRequest
{
    public string Texto { get; set; }
    public Guid UsuarioId { get; set; }
}