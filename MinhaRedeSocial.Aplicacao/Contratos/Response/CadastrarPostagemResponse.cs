using MinhaRedeSocial.Domain.Enums;

namespace MinhaRedeSocial.Aplicacao.Contratos.Response;

public class CadastrarPostagemResponse
{
    public Guid Id { get; set; }
    public DateTime Data { get; set; }
    public string Texto { get; set; }
    public PostagemPermissoes Permissao { get; set; }
}