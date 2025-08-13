using Contas.Core.Entities;
using Contas.Infrastructure.Services.Dtos;

namespace Contas.Infrastructure.Extensions;

public static class SegmentoDoCredorMappingExtensions
{
    public static SegmentoDoCredor ToEntity(this SegmentoDoCredorDto segmentoDoCredorDto)
    {
        ArgumentNullException.ThrowIfNull(segmentoDoCredorDto);

        return new SegmentoDoCredor
        {
            Id = segmentoDoCredorDto.Id,
            Nome = segmentoDoCredorDto.Nome,
            DataDeCriacao = segmentoDoCredorDto.DataDeCriacao,
            DataDeAtualizacao = segmentoDoCredorDto.DataDeAtualizacao
        };
    }

    public static SegmentoDoCredorDto ToDto(this SegmentoDoCredor segmentoDoCredor)
    {
        ArgumentNullException.ThrowIfNull(segmentoDoCredor);

        return new SegmentoDoCredorDto
        {
            Id = segmentoDoCredor.Id,
            Nome = segmentoDoCredor.Nome,
            DataDeCriacao = segmentoDoCredor.DataDeCriacao,
            DataDeAtualizacao = segmentoDoCredor.DataDeAtualizacao
        };
    }

}
