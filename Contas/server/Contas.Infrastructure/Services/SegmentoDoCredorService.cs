using Contas.Core.Entities;
using Contas.Core.Interfaces.Repositories;
using Contas.Infrastructure.Extensions;
using Contas.Infrastructure.Services.Dtos;
using Contas.Infrastructure.Services.Interfaces;

namespace Contas.Infrastructure.Services;

public class SegmentoDoCredorService(IUnitOfWork unitOfWork) : ISegmentoDoCredorService
{
    public async Task<IEnumerable<SegmentoDoCredorDto>> GetAllSegimentoDoCredorAsync(CancellationToken cancellationToken)
    {
        var lista = await unitOfWork.Repository<SegmentoDoCredor>().GetAllAsync(cancellationToken);

        return lista.Select(x => x.ToDto());
    }

    public async Task<SegmentoDoCredorDto> GetSegmentoDoCredorByIdAsync(int id, CancellationToken cancellationToken)
    {
        var segmentoDoCredor = await unitOfWork.Repository<SegmentoDoCredor>().GetByIdAsync(id, cancellationToken);

        if (segmentoDoCredor == null)
            return null!;

        return segmentoDoCredor.ToDto();
    }

    public async Task<SegmentoDoCredorDto> CreateSegmentoDoCredorAsync(SegmentoDoCredorDto segmentoDoCredorDto, CancellationToken cancellationToken)
    {
        var segmentoDoCredor = segmentoDoCredorDto.ToEntity();

        await unitOfWork.Repository<SegmentoDoCredor>().AddAsync(segmentoDoCredor, cancellationToken);

        await unitOfWork.SaveAllAsync();

        return segmentoDoCredor.ToDto();
    }

    public async Task<SegmentoDoCredorDto> UpdateSegmentoDoCredorAsync(SegmentoDoCredorDto segmentoDoCredorDto, CancellationToken cancellationToken)
    {
        var segmentoDoCredorExistente = await unitOfWork.Repository<SegmentoDoCredor>().GetByIdAsync(segmentoDoCredorDto.Id, cancellationToken);

        if (segmentoDoCredorExistente == null)
            return null!;

        segmentoDoCredorExistente.Nome = segmentoDoCredorDto.Nome;
        segmentoDoCredorExistente.DataDeAtualizacao = segmentoDoCredorDto.DataDeAtualizacao;

        unitOfWork.Repository<SegmentoDoCredor>().Update(segmentoDoCredorExistente);

        await unitOfWork.SaveAllAsync();

        return segmentoDoCredorExistente.ToDto();
    }    

    public async Task<bool> DeleteSegmentoDoCredorAsync(int id, CancellationToken cancellationToken)
    {
        var segmentoDoCredor = await unitOfWork.Repository<SegmentoDoCredor>().GetByIdAsync(id, cancellationToken);

        if (segmentoDoCredor == null)
            return false;            

        unitOfWork.Repository<SegmentoDoCredor>().Delete(segmentoDoCredor);
        
        return await unitOfWork.SaveAllAsync();
    }

    public async Task<bool> ExistsSegmentoDoCredorAsync(int id, CancellationToken cancellationToken)
    {
        return await unitOfWork.Repository<SegmentoDoCredor>().ExistsAsync(id, cancellationToken);
    }
}
