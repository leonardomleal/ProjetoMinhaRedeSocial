using MinhaRedeSocial.Aplicacao.Contratos.Response;
using MinhaRedeSocial.Domain.Models.Postagens;

namespace MinhaRedeSocial.Aplicacao.Extensions.Mapping.Model;

public static class PostagemExtensionMap
{
    public static CadastrarPostagemResponse MapToCadastrarPostagemResponse(this Postagem postagem)
        => new()
        {
            Id = postagem.Id,
            Data = postagem.Data,
            Texto = postagem.Texto,
            Permissao = postagem.Permissao
        };

    public static List<BuscarPostagensResponse> MapToBuscarPostagensResponse(this List<Postagem> postagem)
        {
        var postagens = new List<BuscarPostagensResponse>();

        postagem.ForEach(x => postagens.Add(new()
        {
            Id = x.Id,
            Data = x.Data,
            Nome = x.Usuario.Nome,
            Foto = x.Usuario.Foto,
            Texto = x.Texto,
            Curtidas = x.Curtidas,
            Comentarios = x.Comentarios.Count
        }));

        return postagens;
    }

    public static BuscarPostagensResponse MapToBuscarPostagensResponse(this Postagem postagem)
        => new()
        {
            Id = postagem.Id,
            Data = postagem.Data,
            Nome = postagem.Usuario.Nome,
            Foto = postagem.Usuario.Foto,
            Texto = postagem.Texto,
            Curtidas = postagem.Curtidas,
            Comentarios = postagem.Comentarios.Count
        };
}