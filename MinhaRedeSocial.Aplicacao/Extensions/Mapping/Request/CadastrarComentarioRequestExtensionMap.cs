using MinhaRedeSocial.Aplicacao.Contratos.Request;
using MinhaRedeSocial.Domain.Models.Postagens;

namespace MinhaRedeSocial.Aplicacao.Extensions.Mapping.Request;

public static class CadastrarComentarioRequestExtensionMap
{
    public static Comentario MapToComentario(this CadastrarComentarioRequest request, Guid postagemId, Guid usuarioId)
        => new(request.Texto, postagemId, usuarioId);
}