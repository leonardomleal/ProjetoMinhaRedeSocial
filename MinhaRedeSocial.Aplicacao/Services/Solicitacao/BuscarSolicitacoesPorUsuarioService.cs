using Microsoft.Extensions.Logging;
using MinhaRedeSocial.Aplicacao.Contratos.Response;
using MinhaRedeSocial.Aplicacao.Contratos.Services;
using MinhaRedeSocial.Aplicacao.Extensions.Mapping.Model;
using MinhaRedeSocial.Domain.Contratos.Repositorios;

namespace MinhaRedeSocial.Aplicacao.Services.Solicitacao;

public class BuscarSolicitacoesPorUsuarioService : IBuscarSolicitacoesPorUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ISolicitacaoRepository _solicitacaoRepository;
    private readonly ILogger<BuscarSolicitacoesPorUsuarioService> _logger;

    public BuscarSolicitacoesPorUsuarioService(
        IUsuarioRepository usuarioRepository, 
        ISolicitacaoRepository solicitacaoRepository, 
        ILogger<BuscarSolicitacoesPorUsuarioService> logger)
    {
        _usuarioRepository = usuarioRepository;
        _solicitacaoRepository = solicitacaoRepository;
        _logger = logger;
    }

    public async Task<List<BuscarSolicitacaoResponse>> Executar(Guid id, CancellationToken cancellationToken)
    {
        var retorno = new List<BuscarSolicitacaoResponse>();

        try
        {
            _logger.LogInformation($"Solicitação de service BuscarSolicitacoesPorUsuarioService.[{nameof(Executar)}].", id);

            var usuario = await _usuarioRepository.Buscar(id, cancellationToken);
            if (usuario is null)
            {
                _logger.LogError($"Nenhum usuário foi encontrado para o id {id}.", id);
                throw new Exception($"Nenhum usuário foi encontrado para o id {id}.");
            }
            
            var solicitacoes = await _solicitacaoRepository.BuscarPorUsuario(id, cancellationToken);
            if (solicitacoes is null || solicitacoes.Count < 1)
                _logger.LogInformation($"Nenhum solicitação para o usuário de Id {id}.");

            retorno = solicitacoes?.MapToBuscarSolicitacaoResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao buscar solicitações de amizade para o usuário {id}.");
            throw;
        }

        return retorno;
    }
}