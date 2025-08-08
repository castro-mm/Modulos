using Contas.Core.Entities.Base;

namespace Contas.Core.Entities;

public class Credor : Entity
{
    public int SegmentoDoCredorId { get; set; }
    public required string RazaoSocial { get; set; }
    public required string NomeFantasia { get; set; }
    public long CNPJ { get; set; }

    public ICollection<RegistroDaConta>? RegistrosDaConta { get; set; }   
    public required SegmentoDoCredor SegmentoDoCredor { get; set; } 
}
