namespace Contas.Infrastructure.Objects;

public class EmailSettings
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string FromAddress { get; set; } = string.Empty;
    public string FromName { get; set; } = string.Empty;
    public string? Username { get; set; }
    public string? Password { get; set; }
    public bool EnableSsl { get; set; }
}
