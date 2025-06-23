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
}