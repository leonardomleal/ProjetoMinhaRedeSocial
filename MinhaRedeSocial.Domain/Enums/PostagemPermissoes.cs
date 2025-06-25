using System.Text.Json.Serialization;

namespace MinhaRedeSocial.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PostagemPermissoes
{
    Publico = 1,
    Privado
}