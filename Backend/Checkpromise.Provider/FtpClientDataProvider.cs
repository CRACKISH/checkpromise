using System.Text;
using System.Text.Json;
using CheckPromise.DTO;
using FluentFTP;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Checkpromise.Provider;

public class FtpClientDataProvider(
    IOptions<FtpClientDataProviderOptions> options,
    ILogger<FtpClientDataProvider> logger) : IClientDataProvider
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = false,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };

    private readonly FtpClientDataProviderOptions _options = options.Value;

    private readonly ILogger<FtpClientDataProvider> _logger = logger;

    public async Task PushAsync(ClientData clientData, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(_options.Host))
        {
            throw new InvalidOperationException("FTP host is not configured. Set Ftp:Host in configuration.");
        }

        var payload = JsonSerializer.SerializeToUtf8Bytes(clientData, SerializerOptions);

        using var client = BuildClient();
        await client.Connect(cancellationToken);

        try
        {
            _logger.LogInformation(
                "Uploading {Bytes} bytes to ftp://{Host}:{Port}{RemotePath}",
                payload.Length,
                _options.Host,
                _options.Port,
                _options.RemotePath);

            var status = await client.UploadBytes(
                payload,
                _options.RemotePath,
                FtpRemoteExists.Overwrite,
                createRemoteDir: true,
                token: cancellationToken);

            if (status != FtpStatus.Success)
            {
                throw new InvalidOperationException($"FTP upload finished with status {status}.");
            }
        }
        finally
        {
            await client.Disconnect(cancellationToken);
        }
    }

    private AsyncFtpClient BuildClient()
    {
        var client = new AsyncFtpClient(_options.Host, _options.Username, _options.Password, _options.Port);
        if (_options.UseExplicitFtps)
        {
            client.Config.EncryptionMode = FtpEncryptionMode.Explicit;
            client.Config.ValidateAnyCertificate = true;
        }
        return client;
    }
}
