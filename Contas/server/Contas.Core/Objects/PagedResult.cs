using Contas.Core.Interfaces;

namespace Contas.Core.Objects;

public class PagedResult<TDto> where TDto : IDto
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; } 
    public int Count { get; set; } 
    public int TotalPages => (int)Math.Ceiling((double)Count / PageSize);
    public IEnumerable<TDto> Items { get; set; } 

    public PagedResult(int pageIndex, int pageSize, int count, IEnumerable<TDto> items)
    {        
        PageIndex = pageIndex;
        PageSize = pageSize;
        Count = count;
        Items = items;
    }
}