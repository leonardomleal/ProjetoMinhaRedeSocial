using MinhaRedeSocial.Aplicacao.Contratos.Response;
using MinhaRedeSocial.Domain.Models.Amigos;

namespace MinhaRedeSocial.Aplicacao.Extensions.Mapping.Model;

public static class AmigoExtensionMap
{
    public static List<PesquisarAmigosResponse> MapToPesquisarAmigosResponse(this List<Amigo> amigos)
    {
        var listaAmigos = new List<PesquisarAmigosResponse>();

        amigos.ForEach(x => listaAmigos.Add(new()
        {
            Id = x.Id,
            Nome = x.Nome,
            Email = x.Email,
            Apelido = x.Apelido,
            Foto = x.Foto
        }));

        return listaAmigos;
    }
}