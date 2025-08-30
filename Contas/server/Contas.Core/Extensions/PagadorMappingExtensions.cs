using Contas.Core.Dtos;
using Contas.Core.Entities;

namespace Contas.Core.Extensions;

public static class PagadorMappingExtensions
{
    public static Pagador ToEntity(this PagadorDto pagadorDto)
    {
        ArgumentNullException.ThrowIfNull(pagadorDto, nameof(pagadorDto));

        return new Pagador
        {
            Id = pagadorDto.Id,
            Nome = pagadorDto.Nome,
            Email = pagadorDto.Email,
            CPF = pagadorDto.CPF,
            DataDeCriacao = pagadorDto.DataDeCriacao,
            DataDeAtualizacao = pagadorDto.DataDeAtualizacao
        };
    }
    public static PagadorDto ToDto(this Pagador pagador)
    {
        ArgumentNullException.ThrowIfNull(pagador, nameof(pagador));

        return new PagadorDto
        {
            Id = pagador.Id,
            Nome = pagador.Nome,
            Email = pagador.Email,
            CPF = pagador.CPF,
            DataDeCriacao = pagador.DataDeCriacao,
            DataDeAtualizacao = pagador.DataDeAtualizacao,
        };
    }    

    public static void FromDto(this Pagador pagador, PagadorDto pagadorDto)
    {
        ArgumentNullException.ThrowIfNull(pagador, nameof(pagador));
        ArgumentNullException.ThrowIfNull(pagadorDto, nameof(pagadorDto));

        pagador.Nome = pagadorDto.Nome;
        pagador.Email = pagadorDto.Email;
        pagador.CPF = pagadorDto.CPF;
        pagador.DataDeAtualizacao = DateTime.UtcNow; // Assuming you want to update the timestamp
    }
}
