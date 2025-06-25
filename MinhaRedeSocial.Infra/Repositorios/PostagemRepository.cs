using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MinhaRedeSocial.Domain.Contratos.Dto.Postagem;
using MinhaRedeSocial.Domain.Contratos.Paged;
using MinhaRedeSocial.Domain.Contratos.Repositorios;
using MinhaRedeSocial.Domain.Enums.Sorts;
using MinhaRedeSocial.Domain.Models.Postagens;
using MinhaRedeSocial.Infra.Dados;
using System.Linq;

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

    public async Task<Postagem> Cadastrar(Postagem postagem, CancellationToken cancellationToken)
    {
        try
        {
            await _context.Postagens.AddAsync(postagem);
            await _context.SaveChangesAsync(cancellationToken);
            return postagem;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao cadastrar postagem.");
            throw;
        }
    }

    public async Task<IPagedList<Postagem>> Buscar(BuscarPostagensDto request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.SortDirection.Equals(SortDirection.Desc))
            {
                return request.BuscarPostagensSort switch
                {
                    BuscarPostagensSort.Data
                        => new PagedList<Postagem>(
                            await _context.Postagens
                                .AsNoTracking()
                                .Include(x => x.Comentarios)
                                .Include(x => x.Usuario)
                                .ThenInclude(x => x.Amigos)
                                .Where(x => x.UsuarioId == request.Id || request.Amigos!.Contains(x.UsuarioId))
                                .OrderByDescending(x => x.Data)
                                .Skip((request.Page - 1) * request.PageSize)
                                .Take(request.PageSize)
                                .ToListAsync(cancellationToken),
                        request.Page, request.PageSize, await BuscarTotal(request, cancellationToken)),

                    BuscarPostagensSort.Nome
                        => new PagedList<Postagem>(
                            await _context.Postagens
                                .AsNoTracking()
                                .Include(x => x.Comentarios)
                                .Include(x => x.Usuario)
                                .ThenInclude(x => x.Amigos)
                                .Where(x => x.UsuarioId == request.Id || request.Amigos!.Contains(x.UsuarioId))
                                .OrderByDescending(x => x.Usuario.Nome)
                                .Skip((request.Page - 1) * request.PageSize)
                                .Take(request.PageSize)
                                .ToListAsync(cancellationToken),
                        request.Page, request.PageSize, await BuscarTotal(request, cancellationToken)),

                    _ => new PagedList<Postagem>(
                        await _context.Postagens
                            .AsNoTracking()
                            .Include(x => x.Comentarios)
                            .Include(x => x.Usuario)
                            .ThenInclude(x => x.Amigos)
                            .Where(x => x.UsuarioId == request.Id || request.Amigos!.Contains(x.UsuarioId))
                            .OrderByDescending(x => x.Data)
                            .Skip((request.Page - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .ToListAsync(cancellationToken),
                        request.Page, request.PageSize, await BuscarTotal(request, cancellationToken))
                };
            }

            return request.BuscarPostagensSort switch
            {
                BuscarPostagensSort.Data
                    => new PagedList<Postagem>(
                            await _context.Postagens
                                .AsNoTracking()
                                .Include(x => x.Comentarios)
                                .Include(x => x.Usuario)
                                .ThenInclude(x => x.Amigos)
                                .Where(x => x.UsuarioId == request.Id || request.Amigos!.Contains(x.UsuarioId))
                                .OrderBy(x => x.Data)
                                .Skip((request.Page - 1) * request.PageSize)
                                .Take(request.PageSize)
                                .ToListAsync(cancellationToken),
                        request.Page, request.PageSize, await BuscarTotal(request, cancellationToken)),

                BuscarPostagensSort.Nome
                    => new PagedList<Postagem>(
                            await _context.Postagens
                                .AsNoTracking()
                                .Include(x => x.Comentarios)
                                .Include(x => x.Usuario)
                                .ThenInclude(x => x.Amigos)
                                .Where(x => x.UsuarioId == request.Id || request.Amigos!.Contains(x.UsuarioId))
                                .OrderBy(x => x.Usuario.Nome)
                                .Skip((request.Page - 1) * request.PageSize)
                                .Take(request.PageSize)
                                .ToListAsync(cancellationToken),
                        request.Page, request.PageSize, await BuscarTotal(request, cancellationToken)),

                _ => new PagedList<Postagem>(
                    await _context.Postagens
                        .AsNoTracking()
                        .Include(x => x.Comentarios)
                        .Include(x => x.Usuario)
                        .ThenInclude(x => x.Amigos)
                        .Where(x => x.UsuarioId == request.Id || request.Amigos!.Contains(x.UsuarioId))
                        .OrderBy(x => x.Data)
                        .Skip((request.Page - 1) * request.PageSize)
                        .Take(request.PageSize)
                        .ToListAsync(cancellationToken),
                    request.Page, request.PageSize, await BuscarTotal(request, cancellationToken))
            };
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao buscar postagens do usuário de Id {request.Id}.");
            throw;
        }
    }

    private async Task<int> BuscarTotal(BuscarPostagensDto request, CancellationToken cancellationToken)
        => await _context.Postagens
            .AsNoTracking()
            .Where(x => x.UsuarioId == request.Id || request.Amigos!.Contains(x.UsuarioId))
            .CountAsync(cancellationToken);
}