using Microsoft.Extensions.Logging;
using MinhaRedeSocial.Aplicacao.Contratos.Response;
using MinhaRedeSocial.Aplicacao.Contratos.Services;
using MinhaRedeSocial.Aplicacao.Extensions.Mapping.Model;
using MinhaRedeSocial.Domain.Contratos.Repositorios;
using MinhaRedeSocial.Domain.Enums;

namespace MinhaRedeSocial.Aplicacao.Services.Postagem;

public class AlterarPermissaoService : IAlterarPermissaoService
{
    private readonly IPostagemRepository _postagemRepository;
    private readonly ILogger<AlterarPermissaoService> _logger;

    public AlterarPermissaoService(IPostagemRepository postagemRepository, ILogger<AlterarPermissaoService> logger)
    {
        _postagemRepository = postagemRepository;
        _logger = logger;
    }

    public async Task<BuscarPostagensResponse> Executar(Guid id, PostagemPermissoes permissao, CancellationToken cancellationToken)
    {
        var retorno = new BuscarPostagensResponse();

        try
        {
            var postagem = await _postagemRepository.Buscar(id, cancellationToken);
            if (postagem is null)
            {
                _logger.LogInformation($"Nenhuma postagens com o Id {id} foi encontrada.");
                throw new Exception($"Nenhuma postagens com o Id {id} foi encontrada.");
            }

            var postagemAlterada = await _postagemRepository.AlterarPermissao(id, permissao, cancellationToken);
            retorno = postagemAlterada?.MapToBuscarPostagensResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao alterar permissão para a postagem {id}.");
            throw;
        }

        return retorno;
    }
}