using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MinhaRedeSocial.Domain.Contratos.Repositorios;
using MinhaRedeSocial.Domain.Models.Solicitacoes;
using MinhaRedeSocial.Infra.Dados;

namespace MinhaRedeSocial.Infra.Repositorios;

public class SolicitacaoRepository : ISolicitacaoRepository
{
    private readonly DadosContext _context;
    private readonly ILogger<UsuarioRepository> _logger;

    public SolicitacaoRepository(DadosContext dadosContext, ILogger<UsuarioRepository> logger)
    {
        _context = dadosContext;
        _logger = logger;
    }

    public async Task<List<Solicitacao>> BuscarPorUsuario(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            return await _context.Solicitacoes
                .AsNoTracking()
                .Include(x => x.Solicitante)
                .Where(x => x.UsuarioId == id)
                .ToListAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao buscar solicitações de amizade para o usuário de Id {id}.");
            throw;
        }
    }
}