using BusinessClientsManager.Models;

namespace BusinessClientsManager.Data;

public interface IClientRepo
{
    public Task UpdateClient(int id);
    public Task<IEnumerable<BusinessClient>> GetBusinessClients(int from = 0, int count = 10);
}
