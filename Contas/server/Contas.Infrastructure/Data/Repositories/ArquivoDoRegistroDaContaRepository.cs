using Contas.Core.Entities;
using Contas.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Contas.Infrastructure.Data.Repositories;

public class ArquivoDoRegistroDaContaRepository : Repository<ArquivoDoRegistroDaConta>, IArquivoDoRegistroDaContaRepository
{
    private readonly ContasContext _context;

    public ArquivoDoRegistroDaContaRepository(ContasContext context) : base(context)
    {
        this._context = context;
    }

    public async Task<List<ArquivoDoRegistroDaConta>> FindByRegistroDaContaIdAsync(int registroDaContaId, CancellationToken cancellationToken)
    {
        return await _context.ArquivosDoRegistroDaConta
            .Include(a => a.Arquivo)
            .Where(a => a.RegistroDaContaId == registroDaContaId).ToListAsync(cancellationToken);    
    }
}
