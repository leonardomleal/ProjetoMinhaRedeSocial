using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MinhaRedeSocial.Domain.Contratos.Repositorios;
using MinhaRedeSocial.Domain.Models.Amigos;
using MinhaRedeSocial.Infra.Dados;

namespace MinhaRedeSocial.Infra.Repositorios;

public class AmizadeRepository : IAmizadeRepository
{
    private readonly DadosContext _context;
    private readonly ILogger<AmizadeRepository> _logger;

    public AmizadeRepository(DadosContext dadosContext, ILogger<AmizadeRepository> logger)
    {
        _context = dadosContext;
        _logger = logger;
    }

    public async Task<List<Amizade>> Buscar(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            return await _context.Amizades
                .AsNoTracking()
                .Join(_context.Amigos,
                    amizade => amizade.AmigoId,
                    amigo => amigo.Id,
                    (amizade, amigo) => new Amizade()
                    {
                        Id = amizade.Id,
                        UsuarioId = amizade.UsuarioId,
                        AmigoId = amizade.Id,
                        Amigo = new Amigo()
                        {
                            Id = amigo.Id,
                            Nome = amigo.Nome,
                            Email = amigo.Email,
                            Apelido = amigo.Apelido,
                            Foto = amigo.Foto,
                            UsuarioId = amigo.UsuarioId
                        }
                    }
                )
                .Where(x => x.UsuarioId == id)
                .ToListAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao buscar amizades do usuário {id}.");
            throw;
        }
    }
}