using MinhaRedeSocial.Aplicacao.Contratos.Request;
using MinhaRedeSocial.Domain.Models.Postagens;

namespace MinhaRedeSocial.Aplicacao.Extensions.Mapping.Request;

public static class CadastrarPostagemRequestExtensionsMap
{
    public static Postagem MapToPostagem(this CadastrarPostagemRequest request, Guid usuarioId)
        => new(request.Texto, request.Permissao, usuarioId);
}