namespace Contas.Core.Interfaces;

public interface ISpecificationParams
{
    // Paginação
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
}
