using MinhaRedeSocial.Aplicacao.Contratos.Response;
using MinhaRedeSocial.Domain.Models.Solicitacoes;

namespace MinhaRedeSocial.Aplicacao.Extensions.Mapping.Model;

public static class SolicitacaoExtensionMap
{
    public static List<BuscarSolicitacaoResponse> MapToBuscarSolicitacaoResponse(this List<Solicitacao> solicitacaos)
    {
        var listaSolicitacoes = new List<BuscarSolicitacaoResponse>();

        solicitacaos.ForEach(x => listaSolicitacoes.Add(new()
        {
            Nome = x.Solicitante.Nome,
            Apelido = x.Solicitante.Apelido,
            Foto = x.Solicitante.Foto,
            Mensagem = x.Mensagem
        }));

        return listaSolicitacoes;
    }
}