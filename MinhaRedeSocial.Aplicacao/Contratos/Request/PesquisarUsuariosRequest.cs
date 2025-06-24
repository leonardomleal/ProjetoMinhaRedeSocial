using MinhaRedeSocial.Domain.Contratos.Paged;

namespace MinhaRedeSocial.Aplicacao.Contratos.Request;

public class PesquisarUsuariosRequest : PagedModel
{
    public string NomeEmail { get; set; }
}