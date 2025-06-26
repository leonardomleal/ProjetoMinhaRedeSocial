using MinhaRedeSocial.Aplicacao.Contratos.Response;
using MinhaRedeSocial.Domain.Models.Postagens;

namespace MinhaRedeSocial.Aplicacao.Extensions.Mapping.Model;

public static class ComentarioExtensionMap
{
    public static CadastrarComentarioResponse MapToCadastrarComentarioResponse(this Comentario comentario)
        => new()
        {
            Id = comentario.Id,
            Data = comentario.Data,
            Nome = comentario.Usuario.Nome,
            Foto = comentario.Usuario.Foto,
            Texto = comentario.Texto
        };
}