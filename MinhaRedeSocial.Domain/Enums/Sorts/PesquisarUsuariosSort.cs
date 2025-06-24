using System.Text.Json.Serialization;

namespace MinhaRedeSocial.Domain.Enums.Sorts;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PesquisarUsuariosSort : byte
{
    Id = 1,
    Nome,
    Email,
    DataNascimento
}