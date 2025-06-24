using MinhaRedeSocial.Aplicacao.Contratos.Request;
using MinhaRedeSocial.Domain.Models.Usuarios;

namespace MinhaRedeSocial.Aplicacao.Extensions.Mapping.Request;

public static class CadastrarUsuarioRequestExtensionMap
{
    public static Usuario MapToUsuario(this CadastrarUsuarioRequest request)
        => new(request.Nome, request.Email, request.Apelido, request.DataNascimento, request.Cep, request.Senha, request.Foto);
}