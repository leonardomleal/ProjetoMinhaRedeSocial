using MinhaRedeSocial.Aplicacao.Contratos.Response;
using MinhaRedeSocial.Domain.Models.Usuarios;

namespace MinhaRedeSocial.Aplicacao.Extensions.Mapping.Model;

public static class UsuarioExtensionMap
{
    public static BuscarUsuarioResponse MapToBuscarUsuarioResponse(this Usuario usuario) 
        => new()
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Apelido = usuario.Apelido,
            DataNascimento = usuario.DataNascimento,
            Cep = usuario.Cep,
            Senha = usuario.Senha,
            Foto = usuario.Foto
        }; 

    public static CadastrarUsuarioResponse MapToCadastrarUsuarioResponse(this Usuario usuario)
        => new()
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Apelido = usuario.Apelido,
            DataNascimento = usuario.DataNascimento,
            Cep = usuario.Cep,
            Senha = usuario.Senha,
            Foto = usuario.Foto
        };

    public static List<BuscarUsuarioResponse> MapToBuscarUsuariosResponse(this List<Usuario> usuario)
    {
        var listaUsuarios = new List<BuscarUsuarioResponse>();

        usuario.ForEach(x => listaUsuarios.Add(new()
        {
            Id = x.Id,
            Nome = x.Nome,
            Email = x.Email,
            Apelido = x.Apelido,
            DataNascimento = x.DataNascimento,
            Cep = x.Cep,
            Senha = x.Senha,
            Foto = x.Foto
        }));

        return listaUsuarios;
    }
}