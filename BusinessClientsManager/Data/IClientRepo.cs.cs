using BusinessClientsManager.Models;

namespace BusinessClientsManager.Data;

public interface IClientRepo
{
    public Task<IEnumerable<BusinessClient>> FilterDuplicateClients(IEnumerable<BusinessClient> clients);
    public Task InsertClients(IEnumerable<BusinessClient> clients);
    public Task UpdateClient(int id);
    public Task<IEnumerable<BusinessClient>> GetBusinessClients(int from = 0, int count = 10);
    public Task<bool> SaveChanges();
}
