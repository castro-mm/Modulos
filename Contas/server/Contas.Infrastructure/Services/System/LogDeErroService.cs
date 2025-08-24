using Contas.Core.Dtos.System;
using Contas.Core.Entities.System;
using Contas.Core.Interfaces.Repositories;
using Contas.Infrastructure.Services.Base;
using Contas.Infrastructure.Services.Interfaces.System;

namespace Contas.Infrastructure.Services.System;

public class LogDeErroService(IUnitOfWork unitOfWork) : Service<LogDeErroDto, LogDeErro>(unitOfWork), ILogDeErroService
{
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    // Avaliar o uso do Specification Pattern para consultas complexas
    public async Task<LogDeErroDto> GetLogByTraceIdAsync(string traceId, CancellationToken cancellationToken)
    {
        var logDeErroList = await unitOfWork.Repository<LogDeErro>().GetAllAsync(cancellationToken);

        if (logDeErroList == null || !logDeErroList.Any())
            return null!;

        var logDeErro = logDeErroList
            .Where(x => x.TraceId.ToString() == traceId)
            .FirstOrDefault();

        if (logDeErro == null)
            return null!;

        return logDeErro.ConvertToDto();
    }
}


