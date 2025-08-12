using Contas.Core.Entities;
using Contas.Core.Interfaces.Repositories;
using Contas.Core.Interfaces.Services;

namespace Contas.Infrastructure.Services;

public class SegmentoDoCredorService(IUnitOfWork unitOfWork) : ISegmentoDoCredorService
{
    public async Task<IEnumerable<SegmentoDoCredor>> GetAllSegimentoDoCredorAsync()
    {
        return await unitOfWork.Repository<SegmentoDoCredor>().GetAllAsync();
    }

    public async Task<SegmentoDoCredor> GetSegmentoDoCredorByIdAsync(int id)
    {
        var segmentoDoCredor = await unitOfWork.Repository<SegmentoDoCredor>().GetByIdAsync(id);

        return await Task.FromResult(segmentoDoCredor ?? null!);
    }

    public async Task<bool> AddSegmentoDoCredorAsync(SegmentoDoCredor segmentoDoCredor)
    {
        await unitOfWork.Repository<SegmentoDoCredor>().AddAsync(segmentoDoCredor);

        return await unitOfWork.SaveAllAsync();
    }

    public async Task<SegmentoDoCredor> UpdateSegmentoDoCredorAsync(SegmentoDoCredor segmentoDoCredor)
    {
        var segmentoDoCredorExistente = await unitOfWork.Repository<SegmentoDoCredor>().GetByIdAsync(segmentoDoCredor.Id);

        if (segmentoDoCredorExistente == null)
            return null!;

        segmentoDoCredorExistente.Nome = segmentoDoCredor.Nome;
        segmentoDoCredorExistente.DataDeAtualizacao = segmentoDoCredor.DataDeAtualizacao;

        unitOfWork.Repository<SegmentoDoCredor>().Update(segmentoDoCredorExistente);

        await unitOfWork.SaveAllAsync();

        return segmentoDoCredorExistente;
    }    

    public async Task<bool> DeleteSegmentoDoCredorAsync(int id)
    {
        var segmentoDoCredor = await unitOfWork.Repository<SegmentoDoCredor>().GetByIdAsync(id);

        if (segmentoDoCredor == null)
            return false;            

        unitOfWork.Repository<SegmentoDoCredor>().Delete(segmentoDoCredor);
        
        return await unitOfWork.SaveAllAsync();
    }

    public async Task<bool> ExistsSegmentoDoCredorAsync(int id)
    {
        return await unitOfWork.Repository<SegmentoDoCredor>().ExistsAsync(id);
    }
}
