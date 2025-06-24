namespace MinhaRedeSocial.Domain.Contratos.Paged;

public static class EmptyEnumerable<T>
{
    private static readonly T[] array = System.Array.Empty<T>();

    public static readonly IEnumerable<T> Enumerable = Array;

    public static readonly ICollection<T> Collection = Array;

    public static T[] Array => array;
}