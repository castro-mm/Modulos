using Contas.Core.Dtos;
using Contas.Core.Entities;

namespace Contas.Core.Extensions;

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

    public static void FromDto(this SegmentoDoCredor segmentoDoCredor, SegmentoDoCredorDto segmentoDoCredorDto)
    {
        ArgumentNullException.ThrowIfNull(segmentoDoCredorDto);
        ArgumentNullException.ThrowIfNull(segmentoDoCredor);

        segmentoDoCredor.Id = segmentoDoCredorDto.Id;
        segmentoDoCredor.Nome = segmentoDoCredorDto.Nome;
        segmentoDoCredor.DataDeAtualizacao = segmentoDoCredorDto.DataDeAtualizacao;
    }}
