using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MinhaRedeSocial.Domain.Contratos.Repositorios;
using MinhaRedeSocial.Domain.Models.Postagens;
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
}