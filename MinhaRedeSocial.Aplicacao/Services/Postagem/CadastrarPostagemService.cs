using Microsoft.Extensions.Logging;
using MinhaRedeSocial.Aplicacao.Contratos.Request;
using MinhaRedeSocial.Aplicacao.Contratos.Response;
using MinhaRedeSocial.Aplicacao.Contratos.Services;
using MinhaRedeSocial.Aplicacao.Extensions.Mapping.Model;
using MinhaRedeSocial.Aplicacao.Extensions.Mapping.Request;
using MinhaRedeSocial.Domain.Contratos.Repositorios;

namespace MinhaRedeSocial.Aplicacao.Services.Postagem;

public class CadastrarPostagemService : ICadastrarPostagemService
{
    private readonly IPostagemRepository _postagemRepository;
    private readonly ILogger<CadastrarPostagemService> _logger;

    public CadastrarPostagemService(IPostagemRepository postagemRepository, ILogger<CadastrarPostagemService> logger)
    {
        _postagemRepository = postagemRepository;
    }

    public async Task<CadastrarPostagemResponse> Executar(Guid usuarioId, CadastrarPostagemRequest request, CancellationToken cancellationToken)
    {
        var retorno = new CadastrarPostagemResponse();

        try
        {
            var postagem = request.MapToPostagem(usuarioId);
            var resultado = await _postagemRepository.Cadastrar(postagem, cancellationToken);
            retorno = resultado.MapToCadastrarPostagemResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao cadastrar postagem. ({request}).");
            throw;
        }

        return retorno;
    }
}