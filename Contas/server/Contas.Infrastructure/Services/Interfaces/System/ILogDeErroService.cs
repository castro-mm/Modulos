using Contas.Core.Dtos.System;
using Contas.Core.Entities.System;

namespace Contas.Infrastructure.Services.Interfaces.System;

public interface ILogDeErroService : IService<LogDeErroDto, LogDeErro>
{
    // Define any additional methods specific to SegmentoDoCredor if needed
    // For example, you might want to add methods for specific queries or operations
    Task<LogDeErroDto> GetLogByTraceIdAsync(string traceId, CancellationToken cancellationToken);
}

