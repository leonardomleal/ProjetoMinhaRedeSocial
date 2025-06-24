using MinhaRedeSocial.Domain.Enums.Sorts;

namespace MinhaRedeSocial.Domain.Contratos.Paged;

public abstract class PagedModel
{
    protected PagedModel()
    {
        Page = 1;
        PageSize = 10;
        SortDirection = SortDirection.Asc;
    }

    public int Page { get; set; }

    public int PageSize { get; set; }

    public SortDirection SortDirection { get; set; }
}