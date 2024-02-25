using BusinessClientsManager.Models;

namespace BusinessClientsManager.Data;

public class ClientRepo : IClientRepo
{
    Task<IEnumerable<BusinessClient>> IClientRepo.GetBusinessClients(int from, int count)
    {
        throw new NotImplementedException();
    }

    Task IClientRepo.UpdateClient(int id)
    {
        throw new NotImplementedException();
    }
}
