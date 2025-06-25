using MinhaRedeSocial.Domain.Contratos.Paged;
using MinhaRedeSocial.Domain.Enums.Sorts;

namespace MinhaRedeSocial.Domain.Contratos.Dto.Postagem;

public class BuscarPostagensDto : PagedModel
{
    public BuscarPostagensDto()
    {
        BuscarPostagensSort = BuscarPostagensSort.Data;
    }

    public Guid Id { get; set; }
    public List<Guid> Amigos { get; set; } = [];
    public BuscarPostagensSort BuscarPostagensSort { get; set; }
}