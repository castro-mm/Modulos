using Contas.Core.Interfaces;

namespace Contas.Core.Specifications.Base;

public class SpecificationParams : ISpecificationParams
{
    private const int MaxPageSize = 50;
    private int _pageSize = 50;
    public int PageSize
    {
        get => _pageSize; 
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    public int PageIndex { get; set; } = 1;
}