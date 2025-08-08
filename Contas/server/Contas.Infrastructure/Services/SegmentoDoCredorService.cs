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

        if (segmentoDoCredor == null)
            throw new ArgumentNullException(nameof(segmentoDoCredor), "O segmento do credor não foi encontrado.");

        return segmentoDoCredor;
    }

    public async Task<bool> AddSegmentoDoCredorAsync(SegmentoDoCredor segmentoDoCredor)
    {
        if (segmentoDoCredor == null)
            throw new ArgumentNullException(nameof(segmentoDoCredor), "O segmento do credor não foi informado.");

        await unitOfWork.Repository<SegmentoDoCredor>().AddAsync(segmentoDoCredor);

        return await unitOfWork.SaveAllAsync();
    }

    public async Task<bool> UpdateSegmentoDoCredorAsync(SegmentoDoCredor segmentoDoCredor)
    {
        unitOfWork.Repository<SegmentoDoCredor>().Update(segmentoDoCredor);

        return await unitOfWork.SaveAllAsync();
    }

    public async Task<bool> DeleteSegmentoDoCredorAsync(int id)
    {
        var segmentoDoCredor = await unitOfWork.Repository<SegmentoDoCredor>().GetByIdAsync(id);

        if (segmentoDoCredor == null)
            throw new ArgumentNullException(nameof(segmentoDoCredor), "O segmento do credor não foi encontrado.");

        unitOfWork.Repository<SegmentoDoCredor>().Delete(segmentoDoCredor);
        
        return await unitOfWork.SaveAllAsync();
    }

    public async Task<bool> ExistsSegmentoDoCredorAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("O ID do segmento do credor deve ser maior que zero.", nameof(id));

        return await unitOfWork.Repository<SegmentoDoCredor>().ExistsAsync(id);
    }
}
