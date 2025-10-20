using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;
using Contas.Core.Entities.System;
using Contas.Core.Helpers;

namespace Contas.Infrastructure.Interceptors;

public class AuditSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly ILogger<AuditSaveChangesInterceptor> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuditSaveChangesInterceptor(ILogger<AuditSaveChangesInterceptor> logger, IHttpContextAccessor httpContextAccessor)
    {
        this._logger = logger;
        this._httpContextAccessor = httpContextAccessor;
    }

    override public async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default
    )
    {
        var context = eventData.Context;
        if (context == null)
        {
            _logger.LogWarning("DbContext is null in AuditSaveChangesInterceptor.");
            return result;
        }

        _logger.LogInformation("Saving changes to the database.");

        CriarTrilhaDeAuditoria(context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void CriarTrilhaDeAuditoria(DbContext context)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            _logger.LogWarning("HttpContext is null in CriarTrilhaDeAuditoria.");
            return;
        }

        _logger.LogInformation("Criando a trilha de auditoria para as mudanças no Contexto.");

        // Obtém as entradas que foram adicionadas, modificadas ou excluídas.
        var entries = context.ChangeTracker
            .Entries()
            .Where(e => e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted);

        var trilhaDeAuditoriaList = new List<TrilhaDeAuditoria>();
        foreach (var entry in entries)
        {
            var trilhaDeAuditoria = new TrilhaDeAuditoria
            {
                Entidade = entry.Entity.GetType().Name.Replace("Proxy", string.Empty),
                Metodo = httpContext.Request.Method,
                Caminho = httpContext.Request.Path,
                Operacao = entry.State.ToString().ToUpper(),
                ValoresAntigos =
                    entry.State is EntityState.Modified or EntityState.Deleted
                    ? JsonSerializer.Serialize(entry.OriginalValues.Properties.ToDictionary(p => p.Name, p => entry.OriginalValues[p]))
                    : null,
                ValoresNovos =
                    entry.State is EntityState.Added or EntityState.Modified
                    ? JsonSerializer.Serialize(entry.CurrentValues.Properties.ToDictionary(p => p.Name, p => entry.CurrentValues[p]))
                    : null,
                Ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "IP não disponível",
                Navegador = httpContext.Request.Headers["User-Agent"].ToString(),
                Usuario = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Anonymous",
                Hash = string.Empty,
                TraceId = Guid.NewGuid()
            };
            trilhaDeAuditoria.Hash = HashMD5Helper.Encrypt(trilhaDeAuditoria);

            trilhaDeAuditoriaList.Add(trilhaDeAuditoria);

            _logger.LogInformation(
                "Trilha de auditoria criada: {Acao} em {Entidade} - Detalhes: {@ValoresNovos}",
                trilhaDeAuditoria.Operacao,
                trilhaDeAuditoria.Entidade,
                trilhaDeAuditoria.ValoresNovos
            );
        }

        if (trilhaDeAuditoriaList.Count > 0)
            context.Set<TrilhaDeAuditoria>().AddRange(trilhaDeAuditoriaList);

        _logger.LogInformation("Trilhas de auditoria adicionadas ao contexto: {Count}.", trilhaDeAuditoriaList.Count);
    }
}
