using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MinhaRedeSocial.Domain.Contratos.Paged;
using MinhaRedeSocial.Domain.Contratos.Repositorios;
using MinhaRedeSocial.Domain.Enums.Sorts;
using MinhaRedeSocial.Domain.Models.Amigos;
using MinhaRedeSocial.Infra.Dados;

namespace MinhaRedeSocial.Infra.Repositorios;

public class AmigoRepository : IAmigoRepository
{
    private readonly DadosContext _context;
    private readonly ILogger<AmigoRepository> _logger;

    public AmigoRepository(DadosContext dadosContext, ILogger<AmigoRepository> logger)
    {
        _context = dadosContext;
        _logger = logger;
    }

    public async Task<IPagedList<Amigo>> Pesquisar(string nomeEmail, List<Guid> amigos, PesquisarAmigosSort orderBy, int pageNumber, int pageSize, SortDirection sort, CancellationToken cancellationToken)
    {
        try
        {
            if (sort.Equals(SortDirection.Desc))
            {
                return orderBy switch
                {
                    PesquisarAmigosSort.Id => new PagedList<Amigo>(
                        await _context.Amigos
                            .AsNoTracking()
                            .Where(x => amigos.Contains(x.Id) 
                                && (string.IsNullOrEmpty(nomeEmail) || x.Nome.Contains(nomeEmail) || x.Email.Contains(nomeEmail)))
                            .OrderByDescending(x => x.Id)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync(cancellationToken),
                        pageNumber, pageSize, await PesquisarTotal(nomeEmail, amigos, cancellationToken)),

                    PesquisarAmigosSort.Nome => new PagedList<Amigo>(
                        await _context.Amigos
                            .AsNoTracking()
                            .Where(x => amigos.Contains(x.Id)
                                && (string.IsNullOrEmpty(nomeEmail) || x.Nome.Contains(nomeEmail) || x.Email.Contains(nomeEmail)))
                            .OrderByDescending(x => x.Nome)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync(cancellationToken),
                        pageNumber, pageSize, await PesquisarTotal(nomeEmail, amigos, cancellationToken)),

                    PesquisarAmigosSort.Email => new PagedList<Amigo>(
                        await _context.Amigos
                            .AsNoTracking()
                            .Where(x => amigos.Contains(x.Id)
                                && (string.IsNullOrEmpty(nomeEmail) || x.Nome.Contains(nomeEmail) || x.Email.Contains(nomeEmail)))
                            .OrderByDescending(x => x.Email)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync(cancellationToken),
                        pageNumber, pageSize, await PesquisarTotal(nomeEmail, amigos, cancellationToken)),

                    _ => new PagedList<Amigo>(
                        await _context.Amigos
                            .AsNoTracking()
                            .Where(x => amigos.Contains(x.Id)
                                && (string.IsNullOrEmpty(nomeEmail) || x.Nome.Contains(nomeEmail) || x.Email.Contains(nomeEmail)))
                            .OrderByDescending(x => x.Nome)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync(cancellationToken),
                        pageNumber, pageSize, await PesquisarTotal(nomeEmail, amigos, cancellationToken)),
                };
            }

            return orderBy switch
            {
                PesquisarAmigosSort.Id => new PagedList<Amigo>(
                    await _context.Amigos
                        .AsNoTracking()
                        .Where(x => amigos.Contains(x.Id)
                            && (string.IsNullOrEmpty(nomeEmail) || x.Nome.Contains(nomeEmail) || x.Email.Contains(nomeEmail)))
                        .OrderBy(x => x.Id)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync(cancellationToken),
                    pageNumber, pageSize, await PesquisarTotal(nomeEmail, amigos, cancellationToken)),

                PesquisarAmigosSort.Nome => new PagedList<Amigo>(
                    await _context.Amigos
                        .AsNoTracking()
                        .Where(x => amigos.Contains(x.Id)
                            && (string.IsNullOrEmpty(nomeEmail) || x.Nome.Contains(nomeEmail) || x.Email.Contains(nomeEmail)))
                        .OrderBy(x => x.Nome)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync(cancellationToken),
                    pageNumber, pageSize, await PesquisarTotal(nomeEmail, amigos, cancellationToken)),

                PesquisarAmigosSort.Email => new PagedList<Amigo>(
                    await _context.Amigos
                        .AsNoTracking()
                        .Where(x => amigos.Contains(x.Id)
                            && (string.IsNullOrEmpty(nomeEmail) || x.Nome.Contains(nomeEmail) || x.Email.Contains(nomeEmail)))
                        .OrderBy(x => x.Email)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync(cancellationToken),
                    pageNumber, pageSize, await PesquisarTotal(nomeEmail, amigos, cancellationToken)),

                _ => new PagedList<Amigo>(
                    await _context.Amigos
                        .AsNoTracking()
                        .Where(x => amigos.Contains(x.Id)
                            && (string.IsNullOrEmpty(nomeEmail) || x.Nome.Contains(nomeEmail) || x.Email.Contains(nomeEmail)))
                        .OrderBy(x => x.Nome)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync(cancellationToken),
                    pageNumber, pageSize, await PesquisarTotal(nomeEmail, amigos, cancellationToken)),
            };
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao buscar o amigos págiando.");
            throw;
        }
    }

    private async Task<int> PesquisarTotal(string nomeEmail, List<Guid> amigos, CancellationToken cancellationToken)
        => await _context.Usuarios
            .AsNoTracking()
            .Where(x => amigos.Contains(x.Id)
                && (string.IsNullOrEmpty(nomeEmail) || x.Nome.Contains(nomeEmail) || x.Email.Contains(nomeEmail)))
            .CountAsync(cancellationToken);
}