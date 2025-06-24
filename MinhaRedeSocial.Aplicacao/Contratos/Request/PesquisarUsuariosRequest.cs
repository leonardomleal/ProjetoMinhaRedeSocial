using MinhaRedeSocial.Domain.Contratos.Paged;
using MinhaRedeSocial.Domain.Enums.Sorts;

namespace MinhaRedeSocial.Aplicacao.Contratos.Request;

public class PesquisarUsuariosRequest : PagedModel
{
    public PesquisarUsuariosRequest()
    {
        PesquisarUsuariosSort = PesquisarUsuariosSort.Nome;
    }

    public string NomeEmail { get; set; }
    public PesquisarUsuariosSort PesquisarUsuariosSort { get; set; }
}