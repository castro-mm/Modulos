using Contas.Infrastructure.Services.Dtos;

namespace Contas.Infrastructure.Services.Interfaces;

public interface ISegmentoDoCredorService 
{
    public Task<IEnumerable<SegmentoDoCredorDto>> GetAllSegimentoDoCredorAsync(CancellationToken cancellationToken);
    public Task<SegmentoDoCredorDto> GetSegmentoDoCredorByIdAsync(int id, CancellationToken cancellationToken);
    public Task<SegmentoDoCredorDto> CreateSegmentoDoCredorAsync(SegmentoDoCredorDto segmentoDoCredorDto, CancellationToken cancellationToken);
    public Task<SegmentoDoCredorDto> UpdateSegmentoDoCredorAsync(SegmentoDoCredorDto segmentoDoCredorDto, CancellationToken cancellationToken);
    public Task<bool> DeleteSegmentoDoCredorAsync(int id, CancellationToken cancellationToken);
    public Task<bool> ExistsSegmentoDoCredorAsync(int id, CancellationToken cancellationToken);
}
