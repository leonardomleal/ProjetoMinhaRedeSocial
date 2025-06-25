using MinhaRedeSocial.Aplicacao.Contratos.Request;
using MinhaRedeSocial.Domain.Contratos.Dto.Postagem;

namespace MinhaRedeSocial.Aplicacao.Extensions.Mapping.Request;

public static class BuscarPostagensRequestExtensionMap
{
    public static BuscarPostagensDto MapToBuscarPostagensDto(this BuscarPostagensRequest request, List<Guid>? amigos = null)
        => new()
        {
            Id = request.Id,
            BuscarPostagensSort = request.BuscarPostagensSort,
            Page = request.Page,
            PageSize = request.PageSize,
            SortDirection = request.SortDirection,
            Amigos = amigos
        };
}