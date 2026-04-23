using CheckPromise.DTO;

namespace Checkpromise.Provider;

public interface IClientDataProvider
{
    Task PushAsync(ClientData clientData, CancellationToken cancellationToken = default);
}
