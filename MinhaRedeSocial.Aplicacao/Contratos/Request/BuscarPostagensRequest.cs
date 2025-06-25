using MinhaRedeSocial.Domain.Contratos.Paged;
using MinhaRedeSocial.Domain.Enums.Sorts;

namespace MinhaRedeSocial.Aplicacao.Contratos.Request;

public class BuscarPostagensRequest : PagedModel
{
    public BuscarPostagensRequest()
    {
        BuscarPostagensSort = BuscarPostagensSort.Data;
    }

    public Guid Id { get; set; }
    public BuscarPostagensSort BuscarPostagensSort { get; set; }
}