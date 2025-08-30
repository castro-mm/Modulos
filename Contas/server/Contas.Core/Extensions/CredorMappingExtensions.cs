using Contas.Core.Dtos;
using Contas.Core.Entities;

namespace Contas.Core.Extensions;

public static class CredorMappingExtensions
{
    public static Credor ToEntity(this CredorDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto, nameof(dto));

        return new Credor
        {
            Id = dto.Id,
            SegmentoDoCredorId = dto.SegmentoDoCredorId,
            RazaoSocial = dto.RazaoSocial,
            NomeFantasia = dto.NomeFantasia,
            CNPJ = dto.CNPJ,
            DataDeCriacao = dto.DataDeCriacao,
            DataDeAtualizacao = dto.DataDeAtualizacao,
            SegmentoDoCredor = null! // Assuming SegmentoDoCredor will be set later
        };
    }

    public static CredorDto ToDto(this Credor credor)
    {
        ArgumentNullException.ThrowIfNull(credor, nameof(credor));

        return new CredorDto
        {
            Id = credor.Id,
            SegmentoDoCredorId = credor.SegmentoDoCredorId,
            RazaoSocial = credor.RazaoSocial,
            NomeFantasia = credor.NomeFantasia,
            CNPJ = credor.CNPJ,
            DataDeCriacao = credor.DataDeCriacao,
            DataDeAtualizacao = credor.DataDeAtualizacao,
            SegmentoDoCredor = credor.SegmentoDoCredor?.ToDto()
        };
    }

    public static void FromDto(this Credor credor, CredorDto dto)
    {
        ArgumentNullException.ThrowIfNull(credor, nameof(credor));
        ArgumentNullException.ThrowIfNull(dto, nameof(dto));

        credor.SegmentoDoCredorId = dto.SegmentoDoCredorId;
        credor.RazaoSocial = dto.RazaoSocial;
        credor.NomeFantasia = dto.NomeFantasia;
        credor.CNPJ = dto.CNPJ;
        credor.DataDeAtualizacao = dto.DataDeAtualizacao;
    }

    public static List<CredorDto> ToDtoList(List<Credor> arquivos)
    {
        ArgumentNullException.ThrowIfNull(arquivos);

        // TODO: Continuar a partir daqui, pois o método ToDtoList não está sendo utilizado em lugar nenhum.
        // será preciso implementar este metodo nas chamadas

        return [.. arquivos.Select(ToDto)];
    }}
