namespace MinhaRedeSocial.Domain.Contratos.Paged;

public interface IPagedList<T>
{
    int TotalCount { get; }
    int PageCount { get; }
    int Page { get; }
    int PageSize { get; }
    List<T> Source { get; }
}