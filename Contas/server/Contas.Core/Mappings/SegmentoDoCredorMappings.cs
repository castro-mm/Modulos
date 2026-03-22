using Contas.Core.Dtos;
using Contas.Core.Entities;

namespace Contas.Core.Mappings;

public static class SegmentoDoCredorMappings
{
    public static SegmentoDoCredor ToEntity(this SegmentoDoCredorDto segmentoDoCredorDto)
    {
        ArgumentNullException.ThrowIfNull(segmentoDoCredorDto);

        return new SegmentoDoCredor
        {
            Id = segmentoDoCredorDto.Id,
            Nome = segmentoDoCredorDto.Nome,
            DataDeCriacao = segmentoDoCredorDto.DataDeCriacao,
            DataDeAtualizacao = segmentoDoCredorDto.DataDeAtualizacao,
            UserId = segmentoDoCredorDto.UserId,
            User = null!, // Assuming User will be set later
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
            DataDeAtualizacao = segmentoDoCredor.DataDeAtualizacao,
            UserId = segmentoDoCredor.UserId
        };
    }

    public static void FromDto(this SegmentoDoCredor segmentoDoCredor, SegmentoDoCredorDto segmentoDoCredorDto)
    {
        ArgumentNullException.ThrowIfNull(segmentoDoCredorDto);
        ArgumentNullException.ThrowIfNull(segmentoDoCredor);

        segmentoDoCredor.Nome = segmentoDoCredorDto.Nome;
        segmentoDoCredor.DataDeAtualizacao = segmentoDoCredorDto.DataDeAtualizacao;
        segmentoDoCredor.UserId = segmentoDoCredorDto.UserId;
    }
}
