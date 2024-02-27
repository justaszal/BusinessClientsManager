using BusinessClientsManager.Models;

namespace BusinessClientsManager.Services;

public interface IBusinessClientService
{
    public Task<bool> InsertBusinessClients(IEnumerable<BusinessClient> clients);
    public Task<int> UpdatePostCodes();
}
