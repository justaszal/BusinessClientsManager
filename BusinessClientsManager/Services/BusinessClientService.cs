using BusinessClientsManager.Data;
using BusinessClientsManager.Models;

namespace BusinessClientsManager.Services;

public class BusinessClientService : IBusinessClientService
{

    private readonly IClientRepo _clientRepo;

    public BusinessClientService(IClientRepo clientRepo)
    {
        _clientRepo = clientRepo;
    }

    public async Task<bool> InsertBusinessClients(IEnumerable<BusinessClient> clients)
    {
        try
        {
            var filteredClients = await _clientRepo.FilterDuplicateClients(clients);
            if (filteredClients.Count() > 0)
            {
                await _clientRepo.InsertClients(filteredClients);
                return await _clientRepo.SaveChanges();
            }

            return false;
        } catch (Exception ex)
        {
            throw new Exception("Operation failed. Business clients were not inserted");
        }
        
    }
}
