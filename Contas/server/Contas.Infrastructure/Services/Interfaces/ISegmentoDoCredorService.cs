using Contas.Infrastructure.Services.Dtos;

namespace Contas.Infrastructure.Services.Interfaces;

public interface ISegmentoDoCredorService 
{
    public Task<IEnumerable<SegmentoDoCredorDto>> GetAllSegimentoDoCredorAsync();
    public Task<SegmentoDoCredorDto> GetSegmentoDoCredorByIdAsync(int id);
    public Task<SegmentoDoCredorDto> CreateSegmentoDoCredorAsync(SegmentoDoCredorDto segmentoDoCredorDto);
    public Task<SegmentoDoCredorDto> UpdateSegmentoDoCredorAsync(SegmentoDoCredorDto segmentoDoCredorDto);
    public Task<bool> DeleteSegmentoDoCredorAsync(int id);
    public Task<bool> ExistsSegmentoDoCredorAsync(int id);
}
