using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MinhaRedeSocial.Domain.Contratos.Paged;
using MinhaRedeSocial.Domain.Contratos.Repositorios;
using MinhaRedeSocial.Domain.Enums.Sorts;
using MinhaRedeSocial.Domain.Models.Usuarios;
using MinhaRedeSocial.Infra.Dados;

namespace MinhaRedeSocial.Infra.Repositorios;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly DadosContext _context;
    private readonly ILogger<UsuarioRepository> _logger;

    public UsuarioRepository(DadosContext dadosContext, ILogger<UsuarioRepository> logger)
    {
        _context = dadosContext;
        _logger = logger;
    }

    public async Task<List<Usuario>> BuscarTodos()
    {
        try
        {
            return await _context.Usuarios
                .AsNoTracking()
                .ToListAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao buscar todos os usuários.");
            throw;
        }
    }

    public async Task<Usuario?> Buscar(Guid id)
    {
        try
        {
            return await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao buscar o usuário de ID {id}.");
            throw;
        }
    }

    public async Task<Usuario> Adicionar(Usuario usuario)
    {
        try
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao adicionar o usuário.");
            throw;
        }
    }

    public async Task<Usuario?> Atualizar(Usuario usuario)
    {
        try
        {
            var alteracao = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == usuario.Id);
            if (alteracao is not null)
            {
                alteracao.Nome = usuario.Nome;
                alteracao.Email = usuario.Email;
                alteracao.Apelido = usuario.Apelido;
                alteracao.DataNascimento = usuario.DataNascimento;
                alteracao.Cep = usuario.Cep;
                alteracao.Senha = usuario.Senha;
                alteracao.Foto = alteracao.Foto;
                await _context.SaveChangesAsync();
            }

            return alteracao;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao atualizar o usuário de ID {usuario.Id}.");
            throw;
        }
    }

    public async Task<bool> Deletar(Guid id)
    {
        try
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            if (usuario is not null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao deletar o usuário de ID {id}.");
            throw;
        }
    }

    public async Task<Usuario?> Buscar(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            return await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao buscar o usuário.");
            throw;
        }
    }

    public async Task<Usuario> Cadastrar(Usuario usuario, CancellationToken cancellationToken)
    {
        try
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync(cancellationToken);
            return usuario;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao cadastrar o usuário.");
            throw;
        }
    }

    public async Task<List<Usuario>> Pesquisar(string nomeEmail, CancellationToken cancellationToken)
    {
        try
        {
            return await _context.Usuarios
                .AsNoTracking()
                .Where(x => x.Nome.Contains(nomeEmail) || x.Email.Contains(nomeEmail))
                .ToListAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao buscar o usuários.");
            throw;
        }
    }

    public async Task<IPagedList<Usuario>> PesquisarPaginado(string nomeEmail, PesquisarUsuariosSort orderBy, int pageNumber, int pageSize, SortDirection sort, CancellationToken cancellationToken)
    {
        try
        {
            if (sort.Equals(SortDirection.Desc))
                return new PagedList<Usuario>(
                    await _context.Usuarios
                        .OrderByDescending(x => x.ToString() == Enum.GetName(orderBy))
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync(cancellationToken),
                    pageNumber, pageSize, await _context.Usuarios.CountAsync());

            return new PagedList<Usuario>(
                await _context.Usuarios
                    .OrderBy(x => x.ToString() == Enum.GetName(orderBy))
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken),
                pageNumber, pageSize, await _context.Usuarios.CountAsync());
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao buscar o usuários págiando.");
            throw;
        }
    }
}