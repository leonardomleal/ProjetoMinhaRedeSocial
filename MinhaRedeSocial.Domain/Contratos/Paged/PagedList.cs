namespace MinhaRedeSocial.Domain.Contratos.Paged;

public sealed class PagedList<T> : IPagedList<T>
{
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int PageCount => GetPageCount();
    public List<T> Source { get; }

    public PagedList(IEnumerable<T> source, int page, int pageSize, int totalCount)

    {
        TotalCount = totalCount;
        Page = page < 1 ? 1 : page;
        PageSize = pageSize;

        Source = new List<T>(pageSize);
        Source.AddRange(source ?? EmptyEnumerable<T>.Enumerable);
    }

    private int GetPageCount()
    {
        if (PageSize == 0)
            return 0;

        var remainder = TotalCount % PageSize;
        return TotalCount / PageSize + (remainder == 0 ? 0 : 1);
    }
}