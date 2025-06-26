using MinhaRedeSocial.Domain.Contratos.Paged;
using MinhaRedeSocial.Domain.Enums.Sorts;

namespace MinhaRedeSocial.Aplicacao.Contratos.Request;

public class PesquisarAmigosRequest : PagedModel
{
    public PesquisarAmigosRequest()
    {
        PesquisarAmigosSort = PesquisarAmigosSort.Nome;
    }

    public string NomeEmail { get; set; }
    public PesquisarAmigosSort PesquisarAmigosSort { get; set; }
}