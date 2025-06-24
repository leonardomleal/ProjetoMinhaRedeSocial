using System.Text.Json.Serialization;

namespace MinhaRedeSocial.Domain.Enums.Sorts;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SortDirection : byte
{
    Asc = 1,
    Desc
}