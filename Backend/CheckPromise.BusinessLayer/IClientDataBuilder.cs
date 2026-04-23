using CheckPromise.DTO;

namespace CheckPromise.BusinessLayer;

public interface IClientDataBuilder
{
    Task<ClientData> BuildAsync(CancellationToken cancellationToken = default);
}
