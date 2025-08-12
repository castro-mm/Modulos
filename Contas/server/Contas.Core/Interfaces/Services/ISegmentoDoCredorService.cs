using Contas.Core.Entities;

namespace Contas.Core.Interfaces.Services;

public interface ISegmentoDoCredorService
{
    public Task<IEnumerable<SegmentoDoCredor>> GetAllSegimentoDoCredorAsync();
    public Task<SegmentoDoCredor> GetSegmentoDoCredorByIdAsync(int id);
    public Task<bool> AddSegmentoDoCredorAsync(SegmentoDoCredor segmentoDoCredor);
    public Task<SegmentoDoCredor> UpdateSegmentoDoCredorAsync(SegmentoDoCredor segmentoDoCredor);
    public Task<bool> DeleteSegmentoDoCredorAsync(int id);
    public Task<bool> ExistsSegmentoDoCredorAsync(int id);
}
