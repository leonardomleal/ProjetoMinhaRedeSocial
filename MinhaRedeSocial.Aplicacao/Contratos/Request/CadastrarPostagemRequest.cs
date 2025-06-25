using MinhaRedeSocial.Domain.Enums;

namespace MinhaRedeSocial.Aplicacao.Contratos.Request;

public class CadastrarPostagemRequest
{
    public string Texto { get; set; }
    public PostagemPermissoes Permissao { get; set; }
}