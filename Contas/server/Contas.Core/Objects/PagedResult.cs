using Contas.Core.Interfaces;

namespace Contas.Core.Objects;

public class PagedResult<TDto>(int pageIndex, int pageSize, int count, IEnumerable<TDto> itens) where TDto : IDto
{
    public int PageIndex { get; set; } = pageIndex;
    public int PageSize { get; set; } = pageSize;   
    public int Count { get; set; } = count;
    public int TotalPages => (int)Math.Ceiling((double)Count / PageSize);
    public IEnumerable<TDto> Itens { get; set; } = itens;
}
