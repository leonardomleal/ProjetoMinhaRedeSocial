using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MinhaRedeSocial.Domain.Contratos.Repositorios;
using MinhaRedeSocial.Domain.Models.Postagens;
using MinhaRedeSocial.Infra.Dados;

namespace MinhaRedeSocial.Infra.Repositorios;

public class ComentarioRepository : IComentarioRepository
{
    private readonly DadosContext _context;
    private readonly ILogger<ComentarioRepository> _logger;

    public ComentarioRepository(DadosContext dadosContext, ILogger<ComentarioRepository> logger)
    {
        _context = dadosContext;
        _logger = logger;
    }

    public async Task<Comentario?> Cadastrar(Comentario comentario, CancellationToken cancellationToken)
    {
        try
        {
            await _context.Comentarios.AddAsync(comentario);
            await _context.SaveChangesAsync(cancellationToken);
            return await Buscar(comentario.Id, cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao cadastrar o comentário.");
            throw;
        }
    }

    public async Task<Comentario?> Buscar(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            return await _context.Comentarios
                .AsNoTracking()
                .Include(x => x.Postagem)
                .Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao buscar comentário de id {id}.");
            throw;
        }
    }
}