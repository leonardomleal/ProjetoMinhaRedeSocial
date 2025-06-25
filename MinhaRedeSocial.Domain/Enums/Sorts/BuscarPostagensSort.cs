using System.Text.Json.Serialization;

namespace MinhaRedeSocial.Domain.Enums.Sorts;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BuscarPostagensSort : byte
{
    Data = 1,
    Nome
}