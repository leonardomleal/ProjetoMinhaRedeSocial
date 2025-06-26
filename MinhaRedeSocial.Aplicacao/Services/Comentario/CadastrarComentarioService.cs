using Microsoft.Extensions.Logging;
using MinhaRedeSocial.Aplicacao.Contratos.Request;
using MinhaRedeSocial.Aplicacao.Contratos.Response;
using MinhaRedeSocial.Aplicacao.Contratos.Services;
using MinhaRedeSocial.Aplicacao.Extensions.Mapping.Model;
using MinhaRedeSocial.Aplicacao.Extensions.Mapping.Request;
using MinhaRedeSocial.Domain.Contratos.Repositorios;

namespace MinhaRedeSocial.Aplicacao.Services.Comentario;

public class CadastrarComentarioService : ICadastrarComentarioService
{
    private readonly IPostagemRepository _postagemRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IComentarioRepository _comentarioRepository;
    private readonly ILogger<CadastrarComentarioService> _logger;

    public CadastrarComentarioService(
        IPostagemRepository postagemRepository, 
        IUsuarioRepository usuarioRepository,
        IComentarioRepository comentarioRepository,
        ILogger<CadastrarComentarioService> logger)
    {
        _postagemRepository = postagemRepository;
        _usuarioRepository = usuarioRepository;
        _comentarioRepository = comentarioRepository;
        _logger = logger;
    }

    public async Task<CadastrarComentarioResponse> Executar(Guid postagemId, CadastrarComentarioRequest request, CancellationToken cancellationToken)
    {
        var retorno = new CadastrarComentarioResponse();

        try
        {
            var posatagem = await _postagemRepository.Buscar(postagemId, cancellationToken);
            if (posatagem is null)
            {
                _logger.LogInformation($"Nenhuma postagem com o Id {postagemId} foi encontrada.");
                throw new Exception($"Nenhuma postagem com o Id {postagemId} foi encontrada.");
            }

            var ususario = await _usuarioRepository.Buscar(request.UsuarioId, cancellationToken);
            if (ususario is null)
            {
                _logger.LogInformation($"Nenhum usuário com o Id {request.UsuarioId} foi encontrado.");
                throw new Exception($"Nenhum usuário com o Id {request.UsuarioId} foi encontrado.");
            }

            var comentario = request.MapToComentario(posatagem.Id, ususario.Id);
            var resultado = await _comentarioRepository.Cadastrar(comentario, cancellationToken);
            retorno = resultado!.MapToCadastrarComentarioResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao cadastrar comentário para a postagem {postagemId}. ({request}).");
            throw;
        }

        return retorno;
    }
}