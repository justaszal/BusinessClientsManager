using BusinessClientsManager.Models;

namespace BusinessClientsManager.Data;

public interface IClientRepo
{
    public Task<IEnumerable<BusinessClient>> FilterDuplicateClients(IEnumerable<BusinessClient> clients);
    public Task InsertClients(IEnumerable<BusinessClient> clients);
    public Task<bool> UpdateClientPostcode(int id, string postCode, string city);
    public Task<IEnumerable<BusinessClient>> GetBusinessClientsWithoutPostCode();
    public Task<IEnumerable<BusinessClient>> GetBusinessClients(int from = 0, int count = 10);
    public Task<Postcode> AddPostcode(string name, string city);
    public Task<int> SaveChanges();
}
