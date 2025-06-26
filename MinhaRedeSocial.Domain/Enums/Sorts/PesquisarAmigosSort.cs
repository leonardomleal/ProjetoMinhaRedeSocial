using System.Text.Json.Serialization;

namespace MinhaRedeSocial.Domain.Enums.Sorts;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PesquisarAmigosSort : byte
{
    Id = 1,
    Nome,
    Email
}