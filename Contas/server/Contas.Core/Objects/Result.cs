namespace Contas.Core.Objects;

/// <summary>
/// Representa o resultado de uma operação, incluindo dados, erros e mensagens.
/// </summary>
/// <typeparam name="T">Tipo do dado retornado.</typeparam>
public class Result<T>
{
    /// <summary>
    /// Indica se a operação foi bem-sucedida.
    /// </summary>
    public bool IsSuccessful => Errors == null || Errors.Count == 0;

    /// <summary>
    /// Dados retornados pela operação.
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Erros ocorridos durante a operação.
    /// </summary>
    public List<ValidationError>? Errors { get; set; } = null;

    /// <summary>
    /// Mensagem adicional sobre o resultado da operação.
    /// </summary>
    public string? Message { get; set; } = null;

    /// <summary>
    /// Cria um resultado bem-sucedido.
    /// </summary>
    /// <param name="data">Dados a serem associados ao resultado da operação.</param>
    /// <param name="message">Mensagem adicional sobre o resultado da operação.</param>
    /// <returns>O objeto do resultado estruturado com as informações da operação.</returns>
    public static Result<T> Successful(T? data = default, string? message = null) => new() { Data = data, Message = message };

    /// <summary>
    /// Cria um resultado de falha.
    /// </summary>
    /// <param name="errors">Lista de erros que ocorreram durante a operação.</param>
    /// <param name="message">Mensagem adicional sobre o resultado da operação.</param>
    /// <returns>O objeto do resultado estruturado com as informações da operação.</returns>
    public static Result<T> Failure(List<ValidationError> errors, string? message = null) => new() { Errors = errors, Message = message };
}


/// <summary>
/// Representa o resultado de uma operação, incluindo dados, erros e mensagens.
/// </summary>
/// <typeparam name="T">Tipo do dado retornado.</typeparam>
public class Result
{
    /// <summary>
    /// Indica se a operação foi bem-sucedida.
    /// </summary>
    public bool IsSuccessful => Errors == null || Errors.Count == 0;

    /// <summary>
    /// Dados retornados pela operação.
    /// </summary>
    public object? Data { get; set; }

    /// <summary>
    /// Erros ocorridos durante a operação.
    /// </summary>
    public List<ValidationError>? Errors { get; set; } = null;

    /// <summary>
    /// Mensagem adicional sobre o resultado da operação.
    /// </summary>
    public string? Message { get; set; } = null;

    /// <summary>
    /// Cria um resultado bem-sucedido.
    /// </summary>
    /// <param name="data">Dados a serem associados ao resultado da operação.</param>
    /// <param name="message">Mensagem adicional sobre o resultado da operação.</param>
    /// <returns>O objeto do resultado estruturado com as informações da operação.</returns>
    public static Result Successful<T>(T? data = default, string? message = null) => new() { Data = data, Message = message };

    /// <summary>
    /// Cria um resultado de falha.
    /// </summary>
    /// <param name="errors">Lista de erros que ocorreram durante a operação.</param>
    /// <param name="message">Mensagem adicional sobre o resultado da operação.</param>
    /// <returns>O objeto do resultado estruturado com as informações da operação.</returns>
    public static Result Failure(List<ValidationError> errors, string? message = null) => new() { Errors = errors, Message = message };
}