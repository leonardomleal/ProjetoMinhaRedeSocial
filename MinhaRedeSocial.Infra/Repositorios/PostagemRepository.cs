using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using MinhaRedeSocial.Domain.Contratos.Repositorios;
using MinhaRedeSocial.Domain.Models.Postagens;
using MinhaRedeSocial.Domain.Models.Usuarios;
using MinhaRedeSocial.Infra.Dados;

namespace MinhaRedeSocial.Infra.Repositorios;

public class PostagemRepository : IPostagemRepository
{
    private readonly DadosContext _context;
    private readonly ILogger<PostagemRepository> _logger;

    public PostagemRepository(DadosContext dadosContext, ILogger<PostagemRepository> logger)
    {
        _context = dadosContext;
        _logger = logger;
    }

    public async Task<List<Postagem>> Buscar(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var teste = await _context.Postagens
                .AsNoTracking()
                .Where(x => x.UsuarioId == id)
                .Join(_context.Usuarios,
                    postagem => postagem.UsuarioId,
                    usuario => usuario.Id,
                    (postagem, usuario) => new Postagem()
                    {
                        Id = postagem.Id,
                        Data = postagem.Data,
                        Texto = postagem.Texto,
                        Curtidas = postagem.Curtidas,
                        Permissao = postagem.Permissao,
                        Usuario = new Usuario(
                            usuario.Nome,
                            usuario.Email,
                            usuario.Apelido,
                            usuario.DataNascimento,
                            usuario.Cep,
                            usuario.Senha,
                            usuario.Foto)
                    })
                .GroupJoin(_context.Comentarios,
                    postagem => postagem.Id,
                    comentario => comentario.PostagemId,
                    (postagem, comentario) => new 
                    { 
                        postagem,
                        comentario
                    })
                .SelectMany(x => x.comentario.DefaultIfEmpty(),
                    (x, comentario) => new
                    {
                        Postagem = x.postagem,
                        Comentario = comentario
                    })
                .ToListAsync(cancellationToken);


            return new List<Postagem>();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao buscar postagens do usuário de Id {id}.");
            throw;
        }
    }

    public async Task<List<Postagem>> BuscarPostagensAmigos(List<Guid> ids, CancellationToken cancellationToken)
    {
        try
        {
            return await _context.Postagens
                .AsNoTracking()
                .Join(_context.Usuarios,
                    postagem => postagem.UsuarioId,
                    usuario => usuario.Id,
                    (postagem, usuario) => new Postagem()
                    {
                        Id = postagem.Id,
                        Data = postagem.Data,
                        Texto = postagem.Texto,
                        Curtidas = postagem.Curtidas,
                        Permissao = postagem.Permissao,
                        Usuario = new Usuario(
                            usuario.Nome,
                            usuario.Email,
                            usuario.Apelido,
                            usuario.DataNascimento,
                            usuario.Cep,
                            usuario.Senha,
                            usuario.Foto)
                    })
                .Join(_context.Comentarios,
                    postagem => postagem.Id,
                    comentario => comentario.PostagemId,
                    (postagem, comentario) => new Postagem()
                    {
                        Id = postagem.Id,
                        Data = postagem.Data,
                        Texto = postagem.Texto,
                        Curtidas = postagem.Curtidas,
                        Permissao = postagem.Permissao,
                        Comentarios = new List<Comentario>()
                        {
                            new()
                            {
                                Id = comentario.Id,
                                Texto = comentario.Texto
                            }
                        }
                    })
                .Where(x => ids.Contains(x.UsuarioId))
                .ToListAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao buscar postagens de amigos.");
            throw;
        }
    }
}