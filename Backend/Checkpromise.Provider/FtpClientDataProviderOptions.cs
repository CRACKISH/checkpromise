namespace Checkpromise.Provider;

public class FtpClientDataProviderOptions
{
    public const string SectionName = "Ftp";

    public string Host { get; set; } = string.Empty;

    public int Port { get; set; } = 21;

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string RemotePath { get; set; } = "/assets/data/data.json";

    public bool UseExplicitFtps { get; set; }
}
